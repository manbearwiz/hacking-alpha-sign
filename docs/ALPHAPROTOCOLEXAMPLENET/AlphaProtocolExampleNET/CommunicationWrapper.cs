using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Net.Sockets;
using System.Diagnostics;
using System.Runtime.InteropServices;

/*
 * 'CommunicationWrapper' class provides abstraction for multiple
 *  communication options.
*/

namespace AlphaProtocolExampleNET
{
    public enum CommMethodType : int
    {
        Serial = 0,
        Ethernet = 1,
        USB = 2,
        Simulator = 3,
    }

    // EVENTS for CommunicationWrapper
    public delegate void SendEventHandler(object o, SendEventArgs e);
    public class SendEventArgs : EventArgs
    {
        public readonly string SentBytes;
        public SendEventArgs(string SendingBytes) { SentBytes = SendingBytes; }
    }

    /// <summary>
    /// Class that wraps the various communication methods
    /// </summary>
    public class CommunicationWrapper
    {
        public CommMethodType CommMethod;           // current communications method
        public event SendEventHandler SendEvent;    // send data event

        public int Serial_Port;                     // serial settings
        public string Serial_Settings;

        public string Ethernet_IP;                  // ethernet settings
        public int Ethernet_Port;

        // Ascii encoder enforces 0-127, to allow 0-255 use this encoding for byte conversion
        public static Encoding DEFAULT_ENCODE { get { return Encoding.GetEncoding(1252); } }

        #region BetaBrite Prism USB DLLs
        [DllImport("betabriteusb.dll")]             // USB DLL
        internal static extern int USBBULK_Open();
        [DllImport("betabriteusb.dll")]
        internal static extern int USBBULK_Close();
        [DllImport("betabriteusb.dll")]
        internal static extern int USBBULK_Write(string Msg, int Size);
        [DllImport("betabriteusb.dll")]
        internal static extern int USBBULK_Read(string Msg, int Size);
        [DllImport("betabriteusb.dll")]
        internal static extern int USBBULK_SetTimeout(int timeval);
        #endregion

        #region Simulator variables and Win32 declares
        private Process SimProc;              // win32 handles for simulator
        public IntPtr OwnerHandle;
        public int Sim_X;   // Allow for initial positioning of simulator
        public int Sim_Y;

        [DllImport("user32", CharSet = CharSet.Auto)]   // simulator uses SendMessage for IPC
        private extern static int SendMessage(
            IntPtr hwnd,
            int wMsg,
            int wParam,
            ref COPYDATASTRUCT lParam
            );
        private const int WM_COPYDATA = 0x4A;
        private const int WM_DESTROY = 0x2;
        [StructLayout(LayoutKind.Sequential)]
        private struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            public IntPtr lpData;
        }

        [DllImport("user32.dll")]  // Win32 API to make simulator TOPMOST
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        // From winuser.h for Flags
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;  /* The frame changed: send WM_NCCALCSIZE */
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;  /* Don't do owner Z ordering */
        const UInt32 SWP_NOSENDCHANGING = 0x0400;  /* Don't send WM_WINDOWPOSCHANGING */
        #endregion

        public CommunicationWrapper()
        {
            SimProc = null;
            OwnerHandle = IntPtr.Zero;
        }

        private void OnSendEvent(string Sent) { if (SendEvent != null) SendEvent(this, new SendEventArgs(Sent)); }

        public void SendData(string Data)
        {
            OnSendEvent(Data);
            switch (CommMethod)
            {
                case CommMethodType.Serial:
                    serial_SendData(Data);
                    break;
                case CommMethodType.Ethernet:
                    ether_SendData(Data);
                    break;
                case CommMethodType.USB:
                    usb_SendData(Data);
                    break;
                case CommMethodType.Simulator:
                    sim_SendData(Data);
                    break;
            }
            System.Threading.Thread.Sleep(10); // a little command delay
        }

        public void DelayAnnounced(int Milliseconds)
        {
            OnSendEvent("{Delay " + Milliseconds.ToString() + " ms}");
            System.Threading.Thread.Sleep(Milliseconds);
        }

        private void serial_SendData(string Data)
        {
            TimeOutManager OpTimer = new TimeOutManager();
            SerialPort RS232 = new SerialPort();
            RS232.Encoding = DEFAULT_ENCODE;    // IMPORTANT FOR CHARACTERS >127
            RS232.PortName = "COM" + Serial_Port.ToString();
            SetSerial(Serial_Settings, ref RS232);

            RS232.Open();

            RS232.Write(Data);

            OpTimer.Start();
            while (RS232.BytesToWrite > 0 && !OpTimer.IsTimedOut) ;

            RS232.Close();
        }
        private void ether_SendData(string Data)
        {
            TcpClient Sock = new TcpClient();
            Sock.SendTimeout = 15000;
            //Sock.LingerState.Enabled = true;
            //Sock.LingerState.LingerTime = 5000;
            Sock.Connect(Ethernet_IP, Ethernet_Port);
            System.Threading.Thread.Sleep(150); // some lantronix devices need things slow

            NetworkStream Stm = Sock.GetStream();
            byte[] BinData = DEFAULT_ENCODE.GetBytes(Data);
            Stm.Write(BinData, 0, BinData.Length);

            Sock.Close();
        }

        private void usb_SendData(string Data)
        {
            int suc = USBBULK_Open();
            if (suc == 0)
            {
                USBBULK_Close();
                throw new Exception("Failed to Connect to USB Device");
            }
            suc = USBBULK_Write(Data, Data.Length);
            USBBULK_Close();
        }

        private void sim_SendData(string Data)
        {
            byte[] Buffer = DEFAULT_ENCODE.GetBytes(Data + "\x00");
            COPYDATASTRUCT cds;

            if (SimProc == null || SimProc.HasExited) // do we need to launch a new simulator?
            {
                TimeOutManager OpTimer = new TimeOutManager();
                string path = System.Reflection.Assembly.GetExecutingAssembly().Location; //Get App Path
                path = System.IO.Path.GetDirectoryName(path); //remove App exe from App Path
                SimProc = new Process();
                SimProc.StartInfo.FileName = System.IO.Path.Combine(path, "alphasim.exe");
                SimProc.StartInfo.WorkingDirectory = path;
                SimProc.StartInfo.Arguments = " -class simwin";
                SimProc.Start();

                OpTimer.Start();
                while (SimProc.MainWindowHandle == IntPtr.Zero && !OpTimer.IsTimedOut)
                {
                    System.Threading.Thread.Sleep(100); // don't pester windows
                    SimProc.Refresh(); // need this to refresh MainWindowHandle property
                }

                if (SimProc.MainWindowHandle == IntPtr.Zero) throw new Exception("Cannot launch simulator:" + SimProc.StartInfo.FileName);
                SetWindowPos(SimProc.MainWindowHandle, HWND_TOPMOST, Sim_X, Sim_Y, 0, 0, SWP_NOSIZE);
            }

            IntPtr ptrData = Marshal.AllocCoTaskMem(Buffer.Length);
            Marshal.Copy(Buffer, 0, ptrData, Buffer.Length);
            cds.dwData = (IntPtr)0x24A;
            cds.cbData = Buffer.Length;
            cds.lpData = ptrData;
            int Ret = SendMessage(SimProc.MainWindowHandle, WM_COPYDATA, (int)OwnerHandle, ref cds);
            if (Ret == 0) throw new Exception("Win32 SendMessage Failed");
            //Marshal.FreeCoTaskMem(ptrData); // receiver must deallocate
        }

        /// <summary>
        /// Emulates setting the serial with VB6 MSComm32.Settings property
        /// </summary>
        /// <param name="MscommString">"Baud,Parity,DataBits,StopBits"</param>
        /// <param name="Comm">SerialPort object to change</param>
        /// <returns>"OK" or error message</returns>
        public static string SetSerial(string MscommString, ref SerialPort Comm)
        {
            try
            {
                int Baud, Data;
                Parity Par;
                StopBits Stop;
                string[] Fields = MscommString.Split(',');
                Baud = Convert.ToInt32(Fields[0]);
                switch (Fields[1].ToUpper())
                {
                    case "N":
                        Par = Parity.None;
                        break;
                    case "E":
                        Par = Parity.Even;
                        break;
                    case "O":
                        Par = Parity.Odd;
                        break;
                    case "M":
                        Par = Parity.Mark;
                        break;
                    case "S":
                        Par = Parity.Space;
                        break;
                    default:
                        return "Invalid Parity";
                }
                Data = Convert.ToInt32(Fields[2]);
                //Stop = Convert.ToInt32(Fields[3]);
                switch (Fields[3])
                {
                    case "1":
                        Stop = StopBits.One;
                        break;
                    case "2":
                        Stop = StopBits.Two;
                        break;
                    case "1.5":
                        Stop = StopBits.OnePointFive;
                        break;
                    case "0":
                        Stop = StopBits.None;
                        break;
                    default:
                        return "Invalid Stop Bits";
                }
                //if (Fields.Length > 4) Port = Convert.ToInt32(Fields[4]);
                Comm.BaudRate = Baud;
                Comm.Parity = Par;
                Comm.DataBits = Data;
                Comm.StopBits = Stop;
                return "OK";
            }
            catch (Exception e) { return e.Message; }
        } //END SetSerial
        /// <summary>
        /// Emulates getting the serial settings like reading VB6 MSComm32.Settings property
        /// </summary>
        /// <returns>"Baud,Parity,DataBits,StopBits"</returns>
        public static string GetSerial(SerialPort Comm)
        {
            string temp;
            temp = Comm.BaudRate.ToString() + ",";
            switch (Comm.Parity)
            {
                case Parity.Even:
                    temp += "E";
                    break;
                case Parity.Odd:
                    temp += "O";
                    break;
                case Parity.None:
                    temp += "N";
                    break;
                case Parity.Mark:
                    temp += "M";
                    break;
                case Parity.Space:
                    temp += "S";
                    break;
            }
            temp += "," + Comm.DataBits.ToString() + ",";
            switch (Comm.StopBits)
            {
                case StopBits.None:
                    temp += "0";
                    break;
                case StopBits.One:
                    temp += "1";
                    break;
                case StopBits.OnePointFive:
                    temp += "1.5";
                    break;
                case StopBits.Two:
                    temp += "2";
                    break;
            }
            return temp;
        } // END GetSerial

        public static string ReplaceNonPrint(string Str)
        {
            StringBuilder StrB = new StringBuilder(Str.Length);
            char temp;
            for (int i = 0; i < Str.Length; i++)
            {
                temp = Str[i];
                if (IsPrint(temp)) StrB.Append(temp);
                else
                {
                    StrB.Append("{");
                    StrB.Append(GetHex((int)temp, 2));
                    StrB.Append("}");
                }
            }
            return StrB.ToString();
        }
        public static bool IsPrint(char Char)
        {
            int code = (int)Char;
            if (code < 32 || code > 126) return false;
            return true;
        }
        private static string GetHex(int Value, int Len)
        {
            return Value.ToString("X").PadLeft(Len, '0');
        }
    } // END CLASS: CommunicationWrapper

    /// <summary>
    /// Quick and dirty time out checker
    /// </summary>
    public class TimeOutManager
    {
        [DllImport("winmm.dll")]
        internal static extern UInt32 timeGetTime();
        public UInt32 TimeOutMS;
        private UInt32 StartTime;

        public TimeOutManager() { TimeOutMS = 5000; }

        public void Start()
        {
            StartTime = timeGetTime();
        }
        public void Start(UInt32 TimeOut)
        {
            TimeOutMS = TimeOut;
            Start();
        }

        public bool IsTimedOut
        {
            get
            {
                return (timeGetTime() - StartTime > TimeOutMS);
            }
        }
    } // END CLASS: TimeOutManager
} // END NAMESPACE
