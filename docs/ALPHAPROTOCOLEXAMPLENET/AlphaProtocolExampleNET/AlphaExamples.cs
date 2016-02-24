using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/* We define two classes : AlphaExamples and CommunicationWrapper.
 * 'AlphaExamples' contains the protocol examples.
 * 'CommunicationWrapper' provides a wrapper to send the protocol.
*/

namespace AlphaProtocolExampleNET
{
    /// <summary>
    /// Alpha Protocol Samples
    /// </summary>
    public class AlphaExamples
    {
        public CommunicationWrapper CommMan = new CommunicationWrapper();
        public string SignTypecode;         // Typcode is a single ASCII character
        public string SignAddress;          // Address is two hexadecimal characters

        public bool Option_Poscag;          // Packet formating options
        public bool Option_Checksum;

        public const char cNULL = '\x00';   //standard control characters
        public const string cSOH = "\x01";
        public const string cSTX = "\x02";
        public const string cETX = "\x03";
        public const string cEOT = "\x04";

        public AlphaExamples() // Constructor
        {
            SignAddress = "00"; // All addresses
            SignTypecode = "Z"; // All sign types
            Option_Poscag = false;
            Option_Checksum = false;
        }

        public void Delay(float secs)
        {
            System.Threading.Thread.Sleep((int)(secs * 1000));
        }

        /// <summary>
        /// Applies packet formating option and passes packet to comm manager
        /// </summary>
        /// <param name="Data">ASCII Alpha Packet</param>
        private void SendData(string Data)
        {
            string temp = Data;
            if (Option_Checksum) temp = InsertChecksum(temp);
            if (Option_Poscag) temp = ConvPacketToPoscag(temp);
            CommMan.SendData(temp);
        }

        #region SendData Packet Formatter Support
        /*  Pocsage allows control codes to be represented by character combinations.
         *  When a sign receives a <SOH> that is pocsag, it will convert all pocsag combos in that packet.
         *  Inside of a pocsag-style packet, it is possible to mix and match ']' and '_' forms.
         *  In a non-pocsag packet, the characters ']' and '_' are treated as normal characters
         *      and don't require special handling. In a pocsag packet, these must have pocsag applied if part of message text.
         *  (Note while ']' + 0x7F combo will produce a '_' some systems don't consider 0x7F printable,
         *      so "_5F" may be needed.)
        */
        private static readonly int[] PocsagChars = { 0x5D, 0x5F }; // ']' and '_' are special
        private static string ConvPacketToPoscag(string AsciiPacket) { return ConvPacketToPoscag(AsciiPacket, true); }
        private static string ConvPacketToPoscag(string AsciiPacket, bool UseBracket)
        {
            StringBuilder Str = new StringBuilder();
            int code;
            for (int i = 0; i < AsciiPacket.Length; i++)
            {
                code = (int)AsciiPacket[i];
                if (code < 32 || PocsagChars.Contains(code))  // Replace any ascii under 32 decimal, OR pocsag controls
                {
                    if (UseBracket && code < 0x5F) // 0x7E is typically last printable ascii (is issue when replacing '_')
                    {
                        Str.Append("]");                //pocsag bracket
                        Str.Append((char)(code + 32));  //desired code + 32 decimal (or 20 hex)
                    }
                    else // underscore
                    {
                        Str.Append("_");    //pocsag underscore
                        Str.Append(code.ToString("X").PadLeft(2, '0')); //two hex digits
                    }
                }
                else Str.Append(AsciiPacket[i]);
            }
            return Str.ToString();
        }
        private static string InsertChecksum(string AsciiPacket)
        {
            /* This doesn't cover packets of multiple segments
            int iEOT = AsciiPacket.IndexOf("\x04");
            int iSTX = AsciiPacket.IndexOf("\x02");
            string temp = AsciiPacket.Substring(0, iEOT - 1) + "\x03"; //replace EOT w/ ETX
            // take [STX,ETX] portion of packet and generate checksum
            // place checksum between ETX and EOT of packet
            return temp + GenerateChecksumHex(temp.Substring(iSTX)) + "\x04";
             */
            // This should handle packets with multiple segments
            StringBuilder Packet = new StringBuilder();
            StringBuilder curSegment = new StringBuilder();
            bool InSegment = false;
            string temp = AsciiPacket.Replace("\x04", "\x03\x04"); // insert ETX in front of EOT
            for (int i = 0; i < temp.Length; i++)
            {
                Packet.Append(temp[i]);     // add current byte to packet
                if ((byte)temp[i] == 0x02)  // if STX, new packet segment
                {
                    InSegment = true;
                    curSegment.Length = 0; // flush curSegment
                }
                if (InSegment) curSegment.Append(temp[i]); // if in segment, add current to it
                if ((byte)temp[i] == 0x03)  // if ETX, segment ended so put in segment checksum
                {
                    InSegment = false;
                    Packet.Append(GenerateChecksumHex(curSegment.ToString()));
                }
            }
            return Packet.ToString();
        }
        private static string GenerateChecksumHex(string Msg)
        {
            int Total = 0;
            for (int i = 0; i < Msg.Length; i++)
            {
                Total += (int)Msg[i];
            }
            return Total.ToString("X").PadLeft(4, '0');
        }
        #endregion

        /// <summary>
        /// Sends command to clear Alpha sign's memory configuration 
        /// </summary>
        public void ClearMemory()
        {
            /*' SPECIAL FUNCTION command
            ' Page 21 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will clear the memory of the display
            ' Depending on the type of display you have, it may or may not
            ' cause the display to reset as if power cycled.
            ' You must allow the display to finish the reboot
            ' before resuming further communications.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("$");             //SPECIAL FUNCTIONS LABEL ($ = SET MEMORY CONFIGURATION)
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: ClearMemory()

        /// <summary>
        /// Sends a new memory configuration to Alpha sign
        /// </summary>
        public void ConfigureMemory()
        {
            /*' SPECIAL FUNCTION command
            ' Page 21 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will configure the memory of the display*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("$");             //SPECIAL FUNCTIONS LABEL ($ = SET MEMORY CONFIGURATION)

            // TEXT file 'A'
            Cmd.Append("A");            // What File Label do we want to configure
            Cmd.Append("A");            // What type of memory are we configuring this as (A=Text file; B=String File; D=DOTS Picture File)
            Cmd.Append("L");            // Is this file able to be editted using a remote control
            //L=Locked - Means that the file can not be accessed via an IR keyboard.
            //U=Unlocked - Means that the file can be accessed and changed via an IR keyboard.
            Cmd.Append("1000");         // SIZE - See manual page 21
            //Note: A string file can be a maximum of 125 bytes (007d)
            Cmd.Append("FF00");         // QQQQ - See manual page 21

            // TEXT file 'B'
            Cmd.Append("B");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("L");            // IR Lock
            Cmd.Append("1000");         // Size
            Cmd.Append("FF00");         // Flags

            // STRING file 'A'   Note: A string file can be a maximum of 125 bytes (007d)
            Cmd.Append("A");            // File Label
            Cmd.Append("B");            // File Type
            Cmd.Append("L");            // IR Lock
            Cmd.Append("007D");         // Size
            Cmd.Append("0000");         // Flags

            // STRING file 'B'   Note: A string file can be a maximum of 125 bytes (007d)
            Cmd.Append("B");            // File Label
            Cmd.Append("B");            // File Type
            Cmd.Append("L");            // IR Lock
            Cmd.Append("007D");         // Size
            Cmd.Append("0000");         // Flags

            // STRING file 'C'   Note: A string file can be a maximum of 125 bytes (007d)
            Cmd.Append("C");            // File Label
            Cmd.Append("B");            // File Type
            Cmd.Append("L");            // IR Lock
            Cmd.Append("007D");         // Size
            Cmd.Append("0000");         // Flags

            // DOT file 'A'
            Cmd.Append("A");            // File Label
            Cmd.Append("D");            // File Type
            Cmd.Append("L");            // IR Lock
            Cmd.Append("0823");         // Size
            Cmd.Append("2000");         // Flags
            //Valid Dot flag entries are "1000" = monochrome, "2000" = 3-color, "4000" = 8-color
            // If Dot pic does not work, try "4000". Some 215s seem to have trouble with 3-color mode
            // RGB Dot pics use "E8" command (page 26)

            Cmd.Append(cEOT);            //<EOT>

            SendData(Cmd.ToString());
        } // END: ConfigureMemory()

        /// <summary>
        /// Sends one line rotating "Hello World" message to TEXT file A
        /// </summary>
        public void SendTextFileA_SimpleText()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("a");             // MODE CODE (pg 89) (a = rotate)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in

            Cmd.Append("HELLO WORLD");   // Sample text to display
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFile_A()

        /// <summary>
        /// Sends multiline, multipage message to TEXT file A
        /// </summary>
        public void SendTextFileB_MultiMessage()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File B to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("B");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = HOLD)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in

            Cmd.Append("HELLO WORLD");   // Sample text to display
            Cmd.Append("\x0D");          // New Line command code (pg 81)

            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("2");             // Which color to display text in

            Cmd.Append("2nd LINE");      // Sample text to display on 2nd Line
            Cmd.Append("\x0C");          // New Page command code (pg 81)

            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("3");             // Which color to display text in

            Cmd.Append("A NEW PAGE");    // Sample text to display on 2nd page

            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFile_B()

        /// <summary>
        /// Sends current time, day of week, and date to the sign
        /// </summary>
        public void SendTimeOfDay()
        {
            /*' WRITE SPECIAL FUNCTION commands
            ' Page 21, 22 & 26 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will make 3 seperate transmissions to the display.
            ' The first will set the time to the current time on the PC.
            ' The second will set the current weekday to that of the PC.
            ' The final one will set the date to that of the PC.*/
            StringBuilder Cmd;
            DateTime TheNow = DateTime.Now;
            // THE TIME
            Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             // COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append(" ");             // Special Functions Label " " = Set Timeof Day (pg 21)
            Cmd.Append(TheNow.Hour.ToString("00"));     // HH - Hour
            Cmd.Append(TheNow.Minute.ToString("00"));   // MM - Minute
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());

            // DAY OF WEEK
            Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             // COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("\x26");          // Special Functions Label - SET DAY OF WEEK (pg 22)
            Cmd.Append(((int)TheNow.DayOfWeek + 1).ToString());     // Day of week as number (1-7)
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());

            // THE DATE
            Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             // COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("\x3B");             // Special Functions Label - SET DATE (pg 26)
            Cmd.Append(TheNow.Month.ToString("00"));        // MM - Month
            Cmd.Append(TheNow.Day.ToString("00"));          // DD - Day
            Cmd.Append((TheNow.Year % 100).ToString("00")); // YY - Year
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());

        } // END: SendTimeOfDay()

        /// <summary>
        /// Sends a message displaying time, day of week, date to TEXT file A
        /// </summary>
        public void SendTextFileA_DisplayTime()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running
            ' There is an embedded call to an external string as part of the text file.
            */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("a");             // MODE CODE (pg 89) (a = rotate)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in

            Cmd.Append("TIME: ");        // Text in text message

            Cmd.Append("\x13");          // Command Code to display time of day

            Cmd.Append(" ON ");          // White space and text in text message

            Cmd.Append("\x0B");          // CALL DATE command code (pg 81)
            Cmd.Append("9");             // Date Format for previous CALL DATE command 9 = Day of Week

            Cmd.Append(" ");             // White space and text in text message

            Cmd.Append("\x0B");          // CALL DATE command code (pg 81)
            Cmd.Append("8");             // Date Format for previous CALL DATE command 8 = MMM.DD,YYYY

            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFile_A_DisplayTime()

        /// <summary>
        /// Sends a simple graphic to DOT PICTURE A
        /// </summary>
        public void SendDotPicA()
        {
            /*' WRITE SMALL DOTS PICTURE command
            ' Page 39 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause DOT FILE A to be updated on the sign
            ' The picture will only be displayed if referenced by a CALL DOT file
            '   command from within a Text File.
            ' DEBUG> For some reason this will not work unless you already have
            ' a text file running on the sign (i.e., can't just clear mem, config mem, send dot file, call dot file.
            */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("I");             // Command Code for DOT Picture File Write
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("08");            // Height of picture file in pixels (Hex value)
            Cmd.Append("23");            // Width of picture file in pixels (Hex value)
            // Size must match that given in configure memory

            SendData(Cmd.ToString());
            CommMan.DelayAnnounced(100);    //Following the Width bytes, there should be at least a 100 millisecond delay (not to exceed the                                            
            Cmd = new StringBuilder();      //timeout period) before sending the Row Bit Pattern.
            // Picture Data
            Cmd.Append("00000000000000000200000000000000000" + "\x0D");
            Cmd.Append("00000000000000002020000000000000000" + "\x0D");
            Cmd.Append("00000000000000020002000000000000000" + "\x0D");
            Cmd.Append("00000000000000200000200000000000000" + "\x0D");
            Cmd.Append("00000000000002000000020000000000000" + "\x0D");
            Cmd.Append("00000000000020000000002000000000000" + "\x0D");
            Cmd.Append("00000000000200000000000200000000000" + "\x0D");
            Cmd.Append("00000000002222222222222220000000000" + "\x0D");

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendDotPic_A()

        /// <summary>
        /// Sends a message that calls up DOT PIC A to TEXT file A
        /// </summary>
        public void SendTextFileA_CallDotA()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = hold)
            // BEGIN MESSAGE
            Cmd.Append("\x14");          // Call SMALL DOTS PICTURE file
            Cmd.Append("A");             // File label of the SMALL DOTS PICTURE file to be displayed
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFile_A_CallDotA()

        /// <summary>
        /// Sends a message that calls up STRING A and B without waiting (No hold time)
        /// </summary>
        public void SendTextFileA_CallStringsProduction()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running
            ' There is an embedded call to an external string as part of the text file.
            '
            ' Of particular importance in this message is the NO HOLD speed command
            ' which allows us to update variables in near real time.


            ' ************************************
            ' ************************************
            ' **  REQUIRES A MULTILINE DISPLAY  **
            ' ************************************
            ' *************************************/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = hold)
            Cmd.Append("\x09");          // NO HOLD SPEED (Allows variables to update in near realtime)
            // BEGIN MESSAGE
            Cmd.Append("\x1E");          // Select Character Spacing (pg 82)
            Cmd.Append("1");             //       Fixed width left justified characters

            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("3");             // Which color to display text in

            Cmd.Append("GOOD    BAD");   // Sample text to display
            Cmd.Append("\x0D");          // New Line command code (pg 81)

            Cmd.Append("\x10");         // Command Code to call a string file
            Cmd.Append("A");            // File label of the string file we want to display

            Cmd.Append("  ");           // Sample text to display

            Cmd.Append("\x10");         // Command Code to call a string file
            Cmd.Append("B");            // File label of the string file we want to display
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFileA_CallStringsProduction()

        /// <summary>
        /// Sends a pair of variables to STRING files A and B
        /// </summary>
        /// <param name="GoodCount"></param>
        /// <param name="BadCount"></param>
        public void SendStringFiles_Production(int GoodCount, int BadCount)
        {
            /*' WRITE STRING file command
            ' Page 37 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause STRING File A to be updated on the sign
            ' The string file will only be displayed if referenced by a CALL STRING file
            '   command from within a Text File.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("B");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(BadCount.ToString().PadLeft(4, '0'));// VALUE we want displayed where ever the string file is referenced
            Cmd.Append(cETX);           // ETX Marker to append another

            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(GoodCount.ToString().PadLeft(4, '0'));// VALUE we want displayed where ever the string file is referenced

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendStringFiles_Production()

        /// <summary>
        /// Sends a rotating message that calls up STRING A and B
        /// </summary>
        public void SendTextFileA_CallStrings()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running
            ' There is an embedded call to an external string as part of the text file.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("a");             // MODE CODE (pg 89) (a = rotate)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in

            Cmd.Append("ONLY ");        // Sample text to display

            Cmd.Append("\x10");         // Command Code to call a string file
            Cmd.Append("A");            // File label of the string file we want to display

            Cmd.Append(" LEFT ");       // Sample text to display

            Cmd.Append("\x10");         // Command Code to call a string file
            Cmd.Append("B");            // File label of the string file we want to display
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFileA_CallStrings()

        /// <summary>
        /// Sends a string to STRING file A
        /// </summary>
        /// <param name="Msg"></param>
        public void SendStringFiles_StringA(string Msg)
        {
            /*' WRITE STRING file command
            ' Page 37 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause STRING File A to be updated on the sign
            ' The string file will only be displayed if referenced by a CALL STRING file
            '   command from within a Text File.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(Msg);            // VALUE we want displayed where ever the string file is referenced

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendStringFiles_StringA()

        /// <summary>
        ///  Sends a string to STRING file B
        /// </summary>
        /// <param name="Msg"></param>
        public void SendStringFiles_StringB(string Msg)
        {
            /*' WRITE STRING file command
            ' Page 37 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause STRING File A to be updated on the sign
            ' The string file will only be displayed if referenced by a CALL STRING file
            '   command from within a Text File.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("B");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(Msg);            // VALUE we want displayed where ever the string file is referenced

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendStringFiles_StringB()

        /// <summary>
        ///  Sends a pair of strings to STRING files A and B
        /// </summary>
        /// <param name="MsgA"></param>
        /// <param name="MsgB"></param>
        public void SendStringFiles_StringAB(string MsgA, string MsgB)
        {
            /*' WRITE STRING file command
            ' Page 37 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause STRING File A to be updated on the sign
            ' The string file will only be displayed if referenced by a CALL STRING file
            '   command from within a Text File.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("B");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(MsgB);           // VALUE we want displayed where ever the string file is referenced
            Cmd.Append(cETX);           // ETX Marker to append another

            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("G");             // COMMAND CODE (G = WRITE STRING FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append(MsgA);           // VALUE we want displayed where ever the string file is referenced

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendStringFiles_Production()

        /// <summary>
        /// Configures sign memory for use with Counters
        /// </summary>
        public void ConfigureMemoryCounter()
        {
            /*' SPECIAL FUNCTION command
            ' Page 21 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will configure the memory of the display
            ' It includes TEXT files "1" - "5" for counter triggered files. */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("$");             //SPECIAL FUNCTIONS LABEL ($ = SET MEMORY CONFIGURATION)

            // TEXT file 'A'
            Cmd.Append("A");            // What File Label do we want to configure
            Cmd.Append("A");            // What type of memory are we configuring this as (A=Text file; B=String File; D=DOTS Picture File)
            Cmd.Append("U");            // Is this file able to be editted using a remote control
            //L=Locked - Means that the file can not be accessed via an IR keyboard.
            //U=Unlocked - Means that the file can be accessed and changed via an IR keyboard.
            Cmd.Append("1000");         // SIZE - See manual page 21
            //Note: A string file can be a maximum of 125 bytes (007d)
            Cmd.Append("FF00");         // QQQQ - See manual page 21

            // Counter triggered TEXT files only need to be configured if used, below is provided as example.
            //  When setting counter, the Counter Target Byte would indicate which of these are activated by the counter.
            //  (See SetCounter_Counter1() for setting up Counter Target Byte)
            // TEXT file '1' as Counter Trigger 1
            Cmd.Append("1");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("U");            // IR Lock
            Cmd.Append("0064");         // Size (default size for trigger TEXT file)
            Cmd.Append("FE00");         // Flags (default start time is never, the trigger would toggle it to always)
            /*
            // TEXT file '2' as Counter Trigger 2
            Cmd.Append("2");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("U");            // IR Lock
            Cmd.Append("0064");         // Size
            Cmd.Append("FE00");         // Flags

            // TEXT file '3' as Counter Trigger 3
            Cmd.Append("3");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("U");            // IR Lock
            Cmd.Append("0064");         // Size
            Cmd.Append("FE00");         // Flags

            // TEXT file '4' as Counter Trigger 4
            Cmd.Append("4");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("U");            // IR Lock
            Cmd.Append("0064");         // Size
            Cmd.Append("FE00");         // Flags

            // TEXT file '5' as Counter Trigger 5
            Cmd.Append("5");            // File Label
            Cmd.Append("A");            // File Type
            Cmd.Append("U");            // IR Lock
            Cmd.Append("0064");         // Size
            Cmd.Append("FE00");         // Flags
            */
            Cmd.Append(cEOT);            //<EOT>

            SendData(Cmd.ToString());
        } // END: ConfigureMemoryCounter()

        /// <summary>
        /// Sends to TEXT file "A" a message that has counter in it
        /// </summary>
        public void SendTextFileA_CallCounter()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running
            ' There is an embedded call to a counter as part of the text file.*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = hold)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in

            Cmd.Append("Count ");        // Sample text to display

            Cmd.Append("\x08");         // Command Code combination
            Cmd.Append("z");            //   to insert Counter 1 (Extended Char set, pg 87)
            // (Values 0x7A "z" to 0x7E "~" = Counter 1 to 5)

            Cmd.Append("!");            // Some more text for the end
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());

            // Go ahead and write a counter trigger TEXT file in "1" as well
            Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("1");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = hold)
            // BEGIN MESSAGE
            Cmd.Append("Count is DONE");        // Sample text to display
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());

        } // END: SendTextFileA_CallCounter()

        /// <summary>
        /// Sends command to set the counters
        /// </summary>
        public void SetCounter_Counter1()
        {
            /*' WRITE SPECIAL - Set Counter command
            ' Page 25 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function used to set the five internal timers available on counter-equipped signs.
            ' Data for all five counters must be sent as one, large block.
            ' Even if you are only setting one counter, data must be sent to the other counters as well.
            ' The counter will only be displayed if referenced in a TEXT file
            ' (See Appendix C for more info on Counters)
            ' (For a list of signs supporting Counters, see page 93)  */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("5");             //SPECIAL FUNCTIONS LABEL (5 = SET COUNTER)

            Cmd.Append("1");            // Counter 1
            Cmd.Append("E0");           // Counter Control byte in hexadecimal characters (0xE0 = 1110 0000)
            /*  Counter Control Byte Bit Breakdown
                bit 7 — 1 = counter on, 0 = counter off (default = 0)
                bit 6 — 1 = increment, 0 = decrement (default = 1)
                bit 5 — 1 = count minutes, 0 = don’t count minutes (default = 1)
                bit 4 — 1 = count hours, 0 = don’t count hours (default = 0)
                bit 3 — 1 = count days, 0 = don’t count days (default = 0)
                bit 2 — 1 = weekends on, 0 = weekends off (default = 1)
                bit 1 — 1 = Auto Reload ON, Auto Reload OFF (default = 0)
                bit 0 — 0 (default = 0)*/
            Cmd.Append("FF");           // Counter Start Time (FF = ALWAYS) See App.B pg51
            //  Time codes “FD” and “FE” are not valid as Counter Start Times.
            Cmd.Append("00");           // Counter Stop Time (00 = Never) See App.B pg51
            //  Time codes “FD”, “FE”, and “FF” are not valid as Counter Stop Times.
            Cmd.Append("00000000");     // Counter Start BCD Value 8 characters '0'-'9' (Loaded when Reload is ON and target reached)
            Cmd.Append("00000001");     // Counter Step BCD Value 8 characters '0'-'9'
            Cmd.Append("00000000");     // Counter Current BCD Value 8 characters '0'-'9'
            Cmd.Append("00000005");     // Counter Target BCD Value 8 characters '0'-'9'
            Cmd.Append("10");           // Counter Target File Byte (which TEXT files should be switched ON? In this case file "1")
            /*  Counter Target File Byte Bit Breakdown
                bit 7 — 0 (default = 0)
                bit 6 — 0 (default = 0)
                bit 5 — 0 (default = 0)
                bit 4 — Target File 1: 1 = enabled, 0 = disabled (default = 0)
                bit 3 — Target File 2: 1 = enabled, 0 = disabled (default = 0)
                bit 2 — Target File 3: 1 = enabled, 0 = disabled (default = 0)
                bit 1 — Target File 4: 1 = enabled, 0 = disabled (default = 0)
                bit 0 — Target File 5: 1 = enabled, 0 = disabled (default = 0)*/
            Cmd.Append("00");           // Change Minutes Sync in hex (00 [:00] - 3B [:59])
            // This value is used when the Counter Control Byte is set to count hours or days. 
            // If minutes are being counted, this value is ignored. However, a value must still be supplied.
            Cmd.Append("00");           // Change Hours Sync in hex (00 [12am] - 17 [11 pm])
            // This value is used when the Counter Control Byte is set to count days. 
            // If minutes or hours are being counted, this value is ignored. However, a value must still be supplied.

            Cmd.Append("2");            // Counter 2
            Cmd.Append("00");           // Counter Control byte (just turn it off)
            Cmd.Append("FF");           // Counter Start Time
            Cmd.Append("00");           // Counter Stop Time
            Cmd.Append("00000000");     // Counter Start BCD Value
            Cmd.Append("00000001");     // Counter Step BCD Value
            Cmd.Append("00000000");     // Counter Current BCD Value
            Cmd.Append("00000000");     // Counter Target BCD Value
            Cmd.Append("00");           // Counter Target File Byte
            Cmd.Append("00");           // Change Minutes Sync
            Cmd.Append("00");           // Change Hours Sync

            Cmd.Append("3");            // Counter 3
            Cmd.Append("00");           // Counter Control byte
            Cmd.Append("FF");           // Counter Start Time
            Cmd.Append("00");           // Counter Stop Time
            Cmd.Append("00000000");     // Counter Start BCD Value
            Cmd.Append("00000001");     // Counter Step BCD Value
            Cmd.Append("00000000");     // Counter Current BCD Value
            Cmd.Append("00000000");     // Counter Target BCD Value
            Cmd.Append("00");           // Counter Target File Byte
            Cmd.Append("00");           // Change Minutes Sync
            Cmd.Append("00");           // Change Hours Sync

            Cmd.Append("4");            // Counter 4
            Cmd.Append("00");           // Counter Control byte
            Cmd.Append("FF");           // Counter Start Time
            Cmd.Append("00");           // Counter Stop Time
            Cmd.Append("00000000");     // Counter Start BCD Value
            Cmd.Append("00000001");     // Counter Step BCD Value
            Cmd.Append("00000000");     // Counter Current BCD Value
            Cmd.Append("00000000");     // Counter Target BCD Value
            Cmd.Append("00");           // Counter Target File Byte
            Cmd.Append("00");           // Change Minutes Sync
            Cmd.Append("00");           // Change Hours Sync

            Cmd.Append("5");            // Counter 5
            Cmd.Append("00");           // Counter Control byte
            Cmd.Append("FF");           // Counter Start Time
            Cmd.Append("00");           // Counter Stop Time
            Cmd.Append("00000000");     // Counter Start BCD Value
            Cmd.Append("00000001");     // Counter Step BCD Value
            Cmd.Append("00000000");     // Counter Current BCD Value
            Cmd.Append("00000000");     // Counter Target BCD Value
            Cmd.Append("00");           // Counter Target File Byte
            Cmd.Append("00");           // Change Minutes Sync
            Cmd.Append("00");           // Change Hours Sync

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SetCounter_Counter1()

        /// <summary>
        /// Sends command to program a smiley custom character
        /// to the ten high custom character set at position 0x21.
        /// </summary>
        public void SendCustomChar()
        {
            /*' WRITE SPECIAL - Program Custom Character Set
            ' Page 114 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("<");             //SPECIAL FUNCTIONS LABEL (< = Program Custom Character Set)

            Cmd.Append("Y");            // One ASCII char - Which of the four custom sets ('Y' = Ten high)
            /* “W” (57H) = Five high custom character set
             * “X” (58H) = Seven/Eight high custom character set
             * “Y” (59H) = Ten high custom character set
             * “Z” (5AH) = Fifteen/Sixteen high custom character set
             */
            Cmd.Append("21");           // Two ASCII char - Which character in the set (21-60 for five high, 21-C1 for all others)
            /* NOTE: It is recommended that you do not use space (0x20) as character position, sign may override with normal space */
            Cmd.Append("08");           // Two ASCII char - Character width
            /*      
                Maximum of 6 for Five high and Seven/Eight high sets
                Maximum of 8 for Ten high set
                Maximum of 11 for Fifteen/Sixteen high set
            */
            /* Bitmap of character Two bytes per row for EACH row (Use actual binary values not ASCII representations) */
            Cmd.Append("\x1E\x00");     // (00011110 00000000) bitmapped representation of character row 1 (top)
            Cmd.Append("\x21\x00");     // (00100001 00000000) bitmapped representation of character row 2
            Cmd.Append("\x52\x40");     // (01010010 01000000) bitmapped representation of character row 3
            Cmd.Append("\x52\x40");     // (01010010 01000000) bitmapped representation of character row 4
            Cmd.Append("\x40\x40");     // (01000000 01000000) bitmapped representation of character row 5
            Cmd.Append("\x40\x40");     // (01000000 01000000) bitmapped representation of character row 6
            Cmd.Append("\x52\x40");     // (01010010 01000000) bitmapped representation of character row 7
            Cmd.Append("\x4C\x40");     // (01001100 01000000) bitmapped representation of character row 8
            Cmd.Append("\x21\x00");     // (00100001 00000000) bitmapped representation of character row 9
            Cmd.Append("\x1E\x00");     // (00011110 00000000) hexadecimal bitmapped representation of character row 10 (bottom)

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendCustomChar()

        /// <summary>
        /// Sends a command to write to TEXT file 'A' a message using custom smiley character
        /// (Used in conjunction with SendCustomChar)
        /// </summary>
        public void SendTextFileA_CustomChar()
        {
            /*' WRITE TEXT file command
            ' Page 18 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            ' This function will cause Text File A to be sent to the sign
            ' If the memory configuration for the text file is configured to Run Always,
            ' the text file will also begin running*/
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("A");             // COMMAND CODE (A = WRITE TEXT FILE)
            Cmd.Append("A");             // FILE LABEL (Must have been configured in Memory command unless using "z" as Sign Type Code..)  See page 10
            Cmd.Append("\x1B");          // <ESC> - ALWAYS starts the Mode Field
            Cmd.Append("\x30");          // Display Position - 30 = FILL
            Cmd.Append("b");             // MODE CODE (pg 89) (b = Hold)
            // BEGIN MESSAGE
            Cmd.Append("\x1C");          // Select Character Color Command (pg 82)
            Cmd.Append("1");             // Which color to display text in (1=RED)

            Cmd.Append("\x1A");         // Select Character Set Command
            Cmd.Append("6");            // '6' = Ten high standard set (pg 82)

            Cmd.Append("HELLO ");   // Sample text to display

            Cmd.Append("\x1A");         // Select Character Set Command
            Cmd.Append("Y");            // 'Y' = Ten high custom set

            Cmd.Append("\x21");         // Character code for our custom smiley

            Cmd.Append("\x1A");         // Select Character Set Command
            Cmd.Append("6");            // '6' = Ten high standard set (pg 82)

            Cmd.Append(" !!!");   // Sample text to display
            // END MESSAGE
            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: SendTextFileA_CustomChar()

        /// <summary>
        /// Sends a command to clear the ten high custom character set
        /// </summary>
        public void ClearCustomChar()
        {
            /*' WRITE SPECIAL - Program Custom Character Set
            ' Page 114 of the Alpha Sign Communications Protocol Manual (9708-8061F)
            */
            StringBuilder Cmd = new StringBuilder();
            Cmd.Append(cNULL, 20);         // Leading NULLS for the sign to autobaud against
            Cmd.Append(cSOH);            //<SOH>
            Cmd.Append(SignTypecode);    //CONST for Sign Type Code (See top of code to change)
            Cmd.Append(SignAddress);     //CONST for Sign Network Address (See top of code to change)
            Cmd.Append(cSTX);            //<STX>
            Cmd.Append("E");             //COMMAND CODE (E = WRITE SPECIAL FUNCTION)
            Cmd.Append("<");             //SPECIAL FUNCTIONS LABEL (< = Program Custom Character Set)

            Cmd.Append("Y");            // One ASCII char - Which of the four custom sets ('Y' = Ten high)
            /* “W” (57H) = Five high custom character set
             * “X” (58H) = Seven/Eight high custom character set
             * “Y” (59H) = Ten high custom character set
             * “Z” (5AH) = Fifteen/Sixteen high custom character set
             */
            Cmd.Append("00");           // Two ASCII char - Which character in the set (21-60 for five high, 21-C1 for all others)
            /* WHEN ZERO IS USED, it will clear the character set */

            Cmd.Append(cEOT);            //<EOT>
            SendData(Cmd.ToString());
        } // END: ClearCustomChar()

    } // END CLASS: AlphaExamples

} // END NAMESPACE: AlphaProtocolExampleNET
