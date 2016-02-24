using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AlphaProtocolExampleNET
{
    public partial class frmMain : Form
    {
        private AlphaExamples Alpha = new AlphaExamples();

        public frmMain()
        {
            InitializeComponent();
            this.Text += " (" + Application.ProductVersion + ")";

            Alpha.CommMan.SendEvent += new SendEventHandler(CommMan_SendEvent);
            Alpha.CommMan.OwnerHandle = this.Handle;
        }

        void CommMan_SendEvent(object o, SendEventArgs e)
        {
            this.txOutput.Text += CommunicationWrapper.ReplaceNonPrint(e.SentBytes) + Environment.NewLine;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // force initial selections
            frmMain_Move(this, null);
            chkUsePocsag_CheckedChanged(this.chkUsePocsag, null);
            chkUseChecksums_CheckedChanged(this.chkUseChecksums, null);
            tabControl1_SelectedIndexChanged(this.tabControl1, null); 
        }

        private void frmMain_Move(object sender, EventArgs e)
        {
            Alpha.CommMan.Sim_X = this.Left;    //reset initial simulator position
            Alpha.CommMan.Sim_Y = this.Bottom;
        }

        private void ControlsEn(bool V)
        {
            this.grpProtocol.Enabled = V;
            this.grpCommunication.Enabled = V;
            this.grpStdSamples.Enabled = V;
            this.grpQSF.Enabled = V;
            this.grpSSF.Enabled = V;
            this.grpCounterSample.Enabled = V;
            this.grpCustChar.Enabled = V;
            if (!V) // controls disabling, something is going to happen
            {
                this.txOutput.Text = ""; // clear output box when disabling
                UpdateSettings();   // re-grab comm settings
            }
        }

        private void chkUsePocsag_CheckedChanged(object sender, EventArgs e)
        {
            Alpha.Option_Poscag = this.chkUsePocsag.Checked;
        }

        private void chkUseChecksums_CheckedChanged(object sender, EventArgs e)
        {
            Alpha.Option_Checksum = this.chkUseChecksums.Checked;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                CommMethodType CommSelected = (CommMethodType)tabControl1.SelectedIndex;
                Alpha.CommMan.CommMethod = CommSelected;
                //UpdateSettings();
            }
            catch (Exception ex)
            {
                ErrorBox(ex.Message, "Invalid Communication Method");
            }
        }

        private void UpdateSettings()
        {
            Alpha.CommMan.Ethernet_IP = this.txIpAddress.Text.Trim();
            Alpha.CommMan.Ethernet_Port = GetTextInt(this.txIpPort.Text);
            Alpha.CommMan.Serial_Settings = this.txCommSettings.Text.Trim();
            Alpha.CommMan.Serial_Port = GetTextInt(this.txCommPort.Text);
        }

        /// <summary>
        /// Provide single point error reporting
        /// </summary>
        /// <param name="Msg"></param>
        /// <param name="Title"></param>
        private void ErrorBox(string Msg, string Title)
        {
            MessageBox.Show(this, Msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void InfoBox(string Msg, string Title)
        {
            MessageBox.Show(this, Msg, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Extracts integer from string, or zero if it can't
        /// </summary>
        /// <param name="Str">String containing a number</param>
        /// <returns>Integer in the string, or zero</returns>
        private int GetTextInt(string Str)
        {
            int temp;
            if (!int.TryParse(Str.Trim(),out temp)) return 0;
            return temp;
        }

        /*
         * Alpha Protocol Example Button Handlers
         * NOTE: Clear and Configure Memory buttons use same event handlers!
        */

        private void cmdSS_ClearMem_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.ClearMemory(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_ConfigMem_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.ConfigureMemory(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_SendText1_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTextFileA_SimpleText(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_SendText2_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTextFileB_MultiMessage(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_SetTime_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTimeOfDay(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_DisplayTime_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTextFileA_DisplayTime(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_SendDot_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendDotPicA(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSS_DisplayDOT_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendTextFileA_CallDotA(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdQSF_SendText_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendTextFileA_CallStringsProduction(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdQSF_SendVariables_Click(object sender, EventArgs e)
        {
            int Good, Bad;
            ControlsEn(false);
            Good = (int)this.nudGood.Value; // get values for variables
            Bad = (int)this.nudBad.Value;
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendStringFiles_Production(Good,Bad); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSSF_SendText_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendTextFileA_CallStrings(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSSF_SendStringA_Click(object sender, EventArgs e)
        {
            string temp;
            ControlsEn(false);
            temp = this.txSSF_StringA.Text;
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendStringFiles_StringA(temp); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSSF_SendStringB_Click(object sender, EventArgs e)
        {
            string temp;
            ControlsEn(false);
            temp = this.txSSF_StringB.Text;
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendStringFiles_StringB(temp); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdSSF_SendStringBoth_Click(object sender, EventArgs e)
        {
            string MsgA, MsgB;
            ControlsEn(false);
            MsgA = this.txSSF_StringA.Text;
            MsgB = this.txSSF_StringB.Text;
            if (Alpha.CommMan.CommMethod == CommMethodType.Simulator)
            {
                InfoBox("The simulator does not support the use of multiple transmissions - so this demo will not operate correctly.",
                    "Simulator Warning!!");
            }
            try { Alpha.SendStringFiles_StringAB(MsgA,MsgB); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCounterConfig_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.ConfigureMemoryCounter(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCounterText_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTextFileA_CallCounter(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCounterSet_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SetCounter_Counter1(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCC_SendChar_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendCustomChar(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCC_SendText_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.SendTextFileA_CustomChar(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

        private void cmdCC_ClearChar_Click(object sender, EventArgs e)
        {
            ControlsEn(false);
            try { Alpha.ClearCustomChar(); }
            catch (Exception ex) { ErrorBox(ex.Message, "Error Sending Command"); }
            ControlsEn(true);
        }

    } // END CLASS: frmMain
} // END NAMESPACE: AlphaProtocolExampleNET
