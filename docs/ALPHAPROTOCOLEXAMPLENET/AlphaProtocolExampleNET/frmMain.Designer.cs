namespace AlphaProtocolExampleNET
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpProtocol = new System.Windows.Forms.GroupBox();
            this.chkUseChecksums = new System.Windows.Forms.CheckBox();
            this.chkUsePocsag = new System.Windows.Forms.CheckBox();
            this.grpCommunication = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabComSerial = new System.Windows.Forms.TabPage();
            this.txCommSettings = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txCommPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tabComTCP = new System.Windows.Forms.TabPage();
            this.txIpPort = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txIpAddress = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabComUSB = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.tabComSim = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.grpStdSamples = new System.Windows.Forms.GroupBox();
            this.cmdSS_DisplayDOT = new System.Windows.Forms.Button();
            this.cmdSS_SendDot = new System.Windows.Forms.Button();
            this.cmdSS_DisplayTime = new System.Windows.Forms.Button();
            this.cmdSS_SetTime = new System.Windows.Forms.Button();
            this.cmdSS_SendText2 = new System.Windows.Forms.Button();
            this.cmdSS_SendText1 = new System.Windows.Forms.Button();
            this.cmdSS_ConfigMem = new System.Windows.Forms.Button();
            this.cmdSS_ClearMem = new System.Windows.Forms.Button();
            this.grpOutput = new System.Windows.Forms.GroupBox();
            this.txOutput = new System.Windows.Forms.TextBox();
            this.grpQSF = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.nudBad = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudGood = new System.Windows.Forms.NumericUpDown();
            this.cmdQSF_SendVariables = new System.Windows.Forms.Button();
            this.cmdQSF_SendText = new System.Windows.Forms.Button();
            this.cmdQSF_ConfigMem = new System.Windows.Forms.Button();
            this.cmdQSF_ClearMemory = new System.Windows.Forms.Button();
            this.grpSSF = new System.Windows.Forms.GroupBox();
            this.txSSF_StringB = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txSSF_StringA = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cmdSSF_SendStringBoth = new System.Windows.Forms.Button();
            this.cmdSSF_SendStringB = new System.Windows.Forms.Button();
            this.cmdSSF_SendStringA = new System.Windows.Forms.Button();
            this.cmdSSF_SendText = new System.Windows.Forms.Button();
            this.cmdSSF_ConfigMem = new System.Windows.Forms.Button();
            this.cmdSSF_ClearMem = new System.Windows.Forms.Button();
            this.grpCounterSample = new System.Windows.Forms.GroupBox();
            this.cmdCounterClear = new System.Windows.Forms.Button();
            this.cmdCounterSet = new System.Windows.Forms.Button();
            this.cmdCounterText = new System.Windows.Forms.Button();
            this.cmdCounterConfig = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.grpCustChar = new System.Windows.Forms.GroupBox();
            this.cmdCC_ClearChar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cmdCC_SendText = new System.Windows.Forms.Button();
            this.cmdCC_SendChar = new System.Windows.Forms.Button();
            this.cmdCC_ConfigMem = new System.Windows.Forms.Button();
            this.cmdCC_ClearMem = new System.Windows.Forms.Button();
            this.grpProtocol.SuspendLayout();
            this.grpCommunication.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabComSerial.SuspendLayout();
            this.tabComTCP.SuspendLayout();
            this.tabComUSB.SuspendLayout();
            this.tabComSim.SuspendLayout();
            this.grpStdSamples.SuspendLayout();
            this.grpOutput.SuspendLayout();
            this.grpQSF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGood)).BeginInit();
            this.grpSSF.SuspendLayout();
            this.grpCounterSample.SuspendLayout();
            this.grpCustChar.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpProtocol
            // 
            this.grpProtocol.Controls.Add(this.chkUseChecksums);
            this.grpProtocol.Controls.Add(this.chkUsePocsag);
            this.grpProtocol.Location = new System.Drawing.Point(9, 7);
            this.grpProtocol.Name = "grpProtocol";
            this.grpProtocol.Size = new System.Drawing.Size(179, 95);
            this.grpProtocol.TabIndex = 0;
            this.grpProtocol.TabStop = false;
            this.grpProtocol.Text = "Protocol Format";
            // 
            // chkUseChecksums
            // 
            this.chkUseChecksums.AutoSize = true;
            this.chkUseChecksums.Location = new System.Drawing.Point(13, 69);
            this.chkUseChecksums.Name = "chkUseChecksums";
            this.chkUseChecksums.Size = new System.Drawing.Size(103, 17);
            this.chkUseChecksums.TabIndex = 1;
            this.chkUseChecksums.Text = "Use Checksums";
            this.chkUseChecksums.UseVisualStyleBackColor = true;
            this.chkUseChecksums.CheckedChanged += new System.EventHandler(this.chkUseChecksums_CheckedChanged);
            // 
            // chkUsePocsag
            // 
            this.chkUsePocsag.AutoSize = true;
            this.chkUsePocsag.Location = new System.Drawing.Point(14, 19);
            this.chkUsePocsag.Name = "chkUsePocsag";
            this.chkUsePocsag.Size = new System.Drawing.Size(106, 30);
            this.chkUsePocsag.TabIndex = 0;
            this.chkUsePocsag.Text = "Use POCSAG\r\n (ASCII Printable)";
            this.toolTip1.SetToolTip(this.chkUsePocsag, "Pocsag represents control characters as two character combination of ] and the de" +
        "sired code + 32 decimal");
            this.chkUsePocsag.UseVisualStyleBackColor = true;
            this.chkUsePocsag.CheckedChanged += new System.EventHandler(this.chkUsePocsag_CheckedChanged);
            // 
            // grpCommunication
            // 
            this.grpCommunication.Controls.Add(this.tabControl1);
            this.grpCommunication.Location = new System.Drawing.Point(202, 7);
            this.grpCommunication.Name = "grpCommunication";
            this.grpCommunication.Size = new System.Drawing.Size(474, 95);
            this.grpCommunication.TabIndex = 1;
            this.grpCommunication.TabStop = false;
            this.grpCommunication.Text = "Communication Method (Currently selected tab is the method that will be used)";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabComSerial);
            this.tabControl1.Controls.Add(this.tabComTCP);
            this.tabControl1.Controls.Add(this.tabComUSB);
            this.tabControl1.Controls.Add(this.tabComSim);
            this.tabControl1.Location = new System.Drawing.Point(11, 19);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(453, 67);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabComSerial
            // 
            this.tabComSerial.Controls.Add(this.txCommSettings);
            this.tabComSerial.Controls.Add(this.label2);
            this.tabComSerial.Controls.Add(this.txCommPort);
            this.tabComSerial.Controls.Add(this.label1);
            this.tabComSerial.Location = new System.Drawing.Point(4, 22);
            this.tabComSerial.Name = "tabComSerial";
            this.tabComSerial.Padding = new System.Windows.Forms.Padding(3);
            this.tabComSerial.Size = new System.Drawing.Size(445, 41);
            this.tabComSerial.TabIndex = 0;
            this.tabComSerial.Text = "Serial";
            this.tabComSerial.UseVisualStyleBackColor = true;
            // 
            // txCommSettings
            // 
            this.txCommSettings.Location = new System.Drawing.Point(286, 11);
            this.txCommSettings.Name = "txCommSettings";
            this.txCommSettings.Size = new System.Drawing.Size(103, 20);
            this.txCommSettings.TabIndex = 3;
            this.txCommSettings.Text = "9600,n,8,1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(198, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port Settings:";
            // 
            // txCommPort
            // 
            this.txCommPort.Location = new System.Drawing.Point(93, 11);
            this.txCommPort.Name = "txCommPort";
            this.txCommPort.Size = new System.Drawing.Size(52, 20);
            this.txCommPort.TabIndex = 1;
            this.txCommPort.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "COM Port:";
            // 
            // tabComTCP
            // 
            this.tabComTCP.Controls.Add(this.txIpPort);
            this.tabComTCP.Controls.Add(this.label4);
            this.tabComTCP.Controls.Add(this.txIpAddress);
            this.tabComTCP.Controls.Add(this.label3);
            this.tabComTCP.Location = new System.Drawing.Point(4, 22);
            this.tabComTCP.Name = "tabComTCP";
            this.tabComTCP.Padding = new System.Windows.Forms.Padding(3);
            this.tabComTCP.Size = new System.Drawing.Size(445, 41);
            this.tabComTCP.TabIndex = 1;
            this.tabComTCP.Text = "TCP/IP Ethernet";
            this.tabComTCP.UseVisualStyleBackColor = true;
            // 
            // txIpPort
            // 
            this.txIpPort.Location = new System.Drawing.Point(325, 10);
            this.txIpPort.Name = "txIpPort";
            this.txIpPort.Size = new System.Drawing.Size(103, 20);
            this.txIpPort.TabIndex = 7;
            this.txIpPort.Text = "10001";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(237, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "IP Port:";
            // 
            // txIpAddress
            // 
            this.txIpAddress.Location = new System.Drawing.Point(94, 10);
            this.txIpAddress.Name = "txIpAddress";
            this.txIpAddress.Size = new System.Drawing.Size(103, 20);
            this.txIpAddress.TabIndex = 5;
            this.txIpAddress.Text = "10.11.11.254";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "IP Address:";
            // 
            // tabComUSB
            // 
            this.tabComUSB.Controls.Add(this.label5);
            this.tabComUSB.Location = new System.Drawing.Point(4, 22);
            this.tabComUSB.Name = "tabComUSB";
            this.tabComUSB.Size = new System.Drawing.Size(445, 41);
            this.tabComUSB.TabIndex = 2;
            this.tabComUSB.Text = "USB Bulk Device";
            this.tabComUSB.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(344, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Use USB Bulk driver for BetaBrite Prism (No additional settings required)";
            // 
            // tabComSim
            // 
            this.tabComSim.Controls.Add(this.label6);
            this.tabComSim.Location = new System.Drawing.Point(4, 22);
            this.tabComSim.Name = "tabComSim";
            this.tabComSim.Size = new System.Drawing.Size(445, 41);
            this.tabComSim.TabIndex = 3;
            this.tabComSim.Text = "Simulator";
            this.tabComSim.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(394, 32);
            this.label6.TabIndex = 0;
            this.label6.Text = "Application should launch and Use Alpha Simulator Application to emulate the sign" +
    ". (Not all functions supported by simulator)";
            // 
            // grpStdSamples
            // 
            this.grpStdSamples.Controls.Add(this.cmdSS_DisplayDOT);
            this.grpStdSamples.Controls.Add(this.cmdSS_SendDot);
            this.grpStdSamples.Controls.Add(this.cmdSS_DisplayTime);
            this.grpStdSamples.Controls.Add(this.cmdSS_SetTime);
            this.grpStdSamples.Controls.Add(this.cmdSS_SendText2);
            this.grpStdSamples.Controls.Add(this.cmdSS_SendText1);
            this.grpStdSamples.Controls.Add(this.cmdSS_ConfigMem);
            this.grpStdSamples.Controls.Add(this.cmdSS_ClearMem);
            this.grpStdSamples.Location = new System.Drawing.Point(9, 108);
            this.grpStdSamples.Name = "grpStdSamples";
            this.grpStdSamples.Size = new System.Drawing.Size(161, 311);
            this.grpStdSamples.TabIndex = 2;
            this.grpStdSamples.TabStop = false;
            this.grpStdSamples.Text = "Standard Samples";
            // 
            // cmdSS_DisplayDOT
            // 
            this.cmdSS_DisplayDOT.Location = new System.Drawing.Point(14, 271);
            this.cmdSS_DisplayDOT.Name = "cmdSS_DisplayDOT";
            this.cmdSS_DisplayDOT.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_DisplayDOT.TabIndex = 7;
            this.cmdSS_DisplayDOT.Text = "Display DOT Picture File";
            this.cmdSS_DisplayDOT.UseVisualStyleBackColor = true;
            this.cmdSS_DisplayDOT.Click += new System.EventHandler(this.cmdSS_DisplayDOT_Click);
            // 
            // cmdSS_SendDot
            // 
            this.cmdSS_SendDot.Location = new System.Drawing.Point(12, 235);
            this.cmdSS_SendDot.Name = "cmdSS_SendDot";
            this.cmdSS_SendDot.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_SendDot.TabIndex = 6;
            this.cmdSS_SendDot.Text = "Send DOT Picture File";
            this.cmdSS_SendDot.UseVisualStyleBackColor = true;
            this.cmdSS_SendDot.Click += new System.EventHandler(this.cmdSS_SendDot_Click);
            // 
            // cmdSS_DisplayTime
            // 
            this.cmdSS_DisplayTime.Location = new System.Drawing.Point(13, 199);
            this.cmdSS_DisplayTime.Name = "cmdSS_DisplayTime";
            this.cmdSS_DisplayTime.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_DisplayTime.TabIndex = 5;
            this.cmdSS_DisplayTime.Text = "Display System Time";
            this.cmdSS_DisplayTime.UseVisualStyleBackColor = true;
            this.cmdSS_DisplayTime.Click += new System.EventHandler(this.cmdSS_DisplayTime_Click);
            // 
            // cmdSS_SetTime
            // 
            this.cmdSS_SetTime.Location = new System.Drawing.Point(13, 163);
            this.cmdSS_SetTime.Name = "cmdSS_SetTime";
            this.cmdSS_SetTime.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_SetTime.TabIndex = 4;
            this.cmdSS_SetTime.Text = "Set Time and Date";
            this.cmdSS_SetTime.UseVisualStyleBackColor = true;
            this.cmdSS_SetTime.Click += new System.EventHandler(this.cmdSS_SetTime_Click);
            // 
            // cmdSS_SendText2
            // 
            this.cmdSS_SendText2.Location = new System.Drawing.Point(13, 127);
            this.cmdSS_SendText2.Name = "cmdSS_SendText2";
            this.cmdSS_SendText2.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_SendText2.TabIndex = 3;
            this.cmdSS_SendText2.Text = "Send Second Text File";
            this.cmdSS_SendText2.UseVisualStyleBackColor = true;
            this.cmdSS_SendText2.Click += new System.EventHandler(this.cmdSS_SendText2_Click);
            // 
            // cmdSS_SendText1
            // 
            this.cmdSS_SendText1.Location = new System.Drawing.Point(13, 91);
            this.cmdSS_SendText1.Name = "cmdSS_SendText1";
            this.cmdSS_SendText1.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_SendText1.TabIndex = 2;
            this.cmdSS_SendText1.Text = "Send Simple Text File";
            this.cmdSS_SendText1.UseVisualStyleBackColor = true;
            this.cmdSS_SendText1.Click += new System.EventHandler(this.cmdSS_SendText1_Click);
            // 
            // cmdSS_ConfigMem
            // 
            this.cmdSS_ConfigMem.Location = new System.Drawing.Point(14, 55);
            this.cmdSS_ConfigMem.Name = "cmdSS_ConfigMem";
            this.cmdSS_ConfigMem.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_ConfigMem.TabIndex = 1;
            this.cmdSS_ConfigMem.Text = "Configure Memory";
            this.cmdSS_ConfigMem.UseVisualStyleBackColor = true;
            this.cmdSS_ConfigMem.Click += new System.EventHandler(this.cmdSS_ConfigMem_Click);
            // 
            // cmdSS_ClearMem
            // 
            this.cmdSS_ClearMem.Location = new System.Drawing.Point(14, 19);
            this.cmdSS_ClearMem.Name = "cmdSS_ClearMem";
            this.cmdSS_ClearMem.Size = new System.Drawing.Size(133, 30);
            this.cmdSS_ClearMem.TabIndex = 0;
            this.cmdSS_ClearMem.Text = "Clear Memory";
            this.cmdSS_ClearMem.UseVisualStyleBackColor = true;
            this.cmdSS_ClearMem.Click += new System.EventHandler(this.cmdSS_ClearMem_Click);
            // 
            // grpOutput
            // 
            this.grpOutput.Controls.Add(this.txOutput);
            this.grpOutput.Location = new System.Drawing.Point(12, 425);
            this.grpOutput.Name = "grpOutput";
            this.grpOutput.Size = new System.Drawing.Size(664, 101);
            this.grpOutput.TabIndex = 3;
            this.grpOutput.TabStop = false;
            this.grpOutput.Text = "Output (Non-printable characters shown in hexadecimal form with brackets {XX}. Ne" +
    "w lines added between sends for clarity. )";
            // 
            // txOutput
            // 
            this.txOutput.Location = new System.Drawing.Point(15, 19);
            this.txOutput.Multiline = true;
            this.txOutput.Name = "txOutput";
            this.txOutput.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txOutput.Size = new System.Drawing.Size(635, 73);
            this.txOutput.TabIndex = 0;
            this.txOutput.WordWrap = false;
            // 
            // grpQSF
            // 
            this.grpQSF.Controls.Add(this.label8);
            this.grpQSF.Controls.Add(this.nudBad);
            this.grpQSF.Controls.Add(this.label7);
            this.grpQSF.Controls.Add(this.nudGood);
            this.grpQSF.Controls.Add(this.cmdQSF_SendVariables);
            this.grpQSF.Controls.Add(this.cmdQSF_SendText);
            this.grpQSF.Controls.Add(this.cmdQSF_ConfigMem);
            this.grpQSF.Controls.Add(this.cmdQSF_ClearMemory);
            this.grpQSF.Location = new System.Drawing.Point(177, 108);
            this.grpQSF.Name = "grpQSF";
            this.grpQSF.Size = new System.Drawing.Size(210, 216);
            this.grpQSF.TabIndex = 4;
            this.grpQSF.TabStop = false;
            this.grpQSF.Text = "Quick String File Samples";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(33, 189);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(29, 13);
            this.label8.TabIndex = 8;
            this.label8.Text = "Bad:";
            // 
            // nudBad
            // 
            this.nudBad.Location = new System.Drawing.Point(75, 189);
            this.nudBad.Name = "nudBad";
            this.nudBad.Size = new System.Drawing.Size(45, 20);
            this.nudBad.TabIndex = 7;
            this.nudBad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(33, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Good:";
            // 
            // nudGood
            // 
            this.nudGood.Location = new System.Drawing.Point(75, 163);
            this.nudGood.Name = "nudGood";
            this.nudGood.Size = new System.Drawing.Size(45, 20);
            this.nudGood.TabIndex = 5;
            this.nudGood.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // cmdQSF_SendVariables
            // 
            this.cmdQSF_SendVariables.Location = new System.Drawing.Point(15, 127);
            this.cmdQSF_SendVariables.Name = "cmdQSF_SendVariables";
            this.cmdQSF_SendVariables.Size = new System.Drawing.Size(164, 30);
            this.cmdQSF_SendVariables.TabIndex = 4;
            this.cmdQSF_SendVariables.Text = "Send Variable Update";
            this.cmdQSF_SendVariables.UseVisualStyleBackColor = true;
            this.cmdQSF_SendVariables.Click += new System.EventHandler(this.cmdQSF_SendVariables_Click);
            // 
            // cmdQSF_SendText
            // 
            this.cmdQSF_SendText.Location = new System.Drawing.Point(15, 91);
            this.cmdQSF_SendText.Name = "cmdQSF_SendText";
            this.cmdQSF_SendText.Size = new System.Drawing.Size(164, 30);
            this.cmdQSF_SendText.TabIndex = 3;
            this.cmdQSF_SendText.Text = "Send Text w/ String Calls";
            this.cmdQSF_SendText.UseVisualStyleBackColor = true;
            this.cmdQSF_SendText.Click += new System.EventHandler(this.cmdQSF_SendText_Click);
            // 
            // cmdQSF_ConfigMem
            // 
            this.cmdQSF_ConfigMem.Location = new System.Drawing.Point(15, 55);
            this.cmdQSF_ConfigMem.Name = "cmdQSF_ConfigMem";
            this.cmdQSF_ConfigMem.Size = new System.Drawing.Size(164, 30);
            this.cmdQSF_ConfigMem.TabIndex = 2;
            this.cmdQSF_ConfigMem.Text = "Configure Memory";
            this.cmdQSF_ConfigMem.UseVisualStyleBackColor = true;
            this.cmdQSF_ConfigMem.Click += new System.EventHandler(this.cmdSS_ConfigMem_Click);
            // 
            // cmdQSF_ClearMemory
            // 
            this.cmdQSF_ClearMemory.Location = new System.Drawing.Point(15, 19);
            this.cmdQSF_ClearMemory.Name = "cmdQSF_ClearMemory";
            this.cmdQSF_ClearMemory.Size = new System.Drawing.Size(164, 30);
            this.cmdQSF_ClearMemory.TabIndex = 1;
            this.cmdQSF_ClearMemory.Text = "Clear Memory";
            this.cmdQSF_ClearMemory.UseVisualStyleBackColor = true;
            this.cmdQSF_ClearMemory.Click += new System.EventHandler(this.cmdSS_ClearMem_Click);
            // 
            // grpSSF
            // 
            this.grpSSF.Controls.Add(this.txSSF_StringB);
            this.grpSSF.Controls.Add(this.label10);
            this.grpSSF.Controls.Add(this.txSSF_StringA);
            this.grpSSF.Controls.Add(this.label9);
            this.grpSSF.Controls.Add(this.cmdSSF_SendStringBoth);
            this.grpSSF.Controls.Add(this.cmdSSF_SendStringB);
            this.grpSSF.Controls.Add(this.cmdSSF_SendStringA);
            this.grpSSF.Controls.Add(this.cmdSSF_SendText);
            this.grpSSF.Controls.Add(this.cmdSSF_ConfigMem);
            this.grpSSF.Controls.Add(this.cmdSSF_ClearMem);
            this.grpSSF.Location = new System.Drawing.Point(393, 108);
            this.grpSSF.Name = "grpSSF";
            this.grpSSF.Size = new System.Drawing.Size(186, 311);
            this.grpSSF.TabIndex = 5;
            this.grpSSF.TabStop = false;
            this.grpSSF.Text = "Standard String File Samples";
            // 
            // txSSF_StringB
            // 
            this.txSSF_StringB.Location = new System.Drawing.Point(80, 163);
            this.txSSF_StringB.Name = "txSSF_StringB";
            this.txSSF_StringB.Size = new System.Drawing.Size(96, 20);
            this.txSSF_StringB.TabIndex = 11;
            this.txSSF_StringB.Text = "Hurry In!";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(12, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(44, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "String B";
            // 
            // txSSF_StringA
            // 
            this.txSSF_StringA.Location = new System.Drawing.Point(80, 128);
            this.txSSF_StringA.Name = "txSSF_StringA";
            this.txSSF_StringA.Size = new System.Drawing.Size(96, 20);
            this.txSSF_StringA.TabIndex = 9;
            this.txSSF_StringA.Text = "183";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 131);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "String A";
            // 
            // cmdSSF_SendStringBoth
            // 
            this.cmdSSF_SendStringBoth.Location = new System.Drawing.Point(13, 271);
            this.cmdSSF_SendStringBoth.Name = "cmdSSF_SendStringBoth";
            this.cmdSSF_SendStringBoth.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_SendStringBoth.TabIndex = 7;
            this.cmdSSF_SendStringBoth.Text = "Send String File A+B";
            this.cmdSSF_SendStringBoth.UseVisualStyleBackColor = true;
            this.cmdSSF_SendStringBoth.Click += new System.EventHandler(this.cmdSSF_SendStringBoth_Click);
            // 
            // cmdSSF_SendStringB
            // 
            this.cmdSSF_SendStringB.Location = new System.Drawing.Point(13, 235);
            this.cmdSSF_SendStringB.Name = "cmdSSF_SendStringB";
            this.cmdSSF_SendStringB.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_SendStringB.TabIndex = 6;
            this.cmdSSF_SendStringB.Text = "Send String File B";
            this.cmdSSF_SendStringB.UseVisualStyleBackColor = true;
            this.cmdSSF_SendStringB.Click += new System.EventHandler(this.cmdSSF_SendStringB_Click);
            // 
            // cmdSSF_SendStringA
            // 
            this.cmdSSF_SendStringA.Location = new System.Drawing.Point(13, 199);
            this.cmdSSF_SendStringA.Name = "cmdSSF_SendStringA";
            this.cmdSSF_SendStringA.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_SendStringA.TabIndex = 5;
            this.cmdSSF_SendStringA.Text = "Send String File A";
            this.cmdSSF_SendStringA.UseVisualStyleBackColor = true;
            this.cmdSSF_SendStringA.Click += new System.EventHandler(this.cmdSSF_SendStringA_Click);
            // 
            // cmdSSF_SendText
            // 
            this.cmdSSF_SendText.Location = new System.Drawing.Point(13, 91);
            this.cmdSSF_SendText.Name = "cmdSSF_SendText";
            this.cmdSSF_SendText.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_SendText.TabIndex = 4;
            this.cmdSSF_SendText.Text = "Send Text File w/ String Calls";
            this.cmdSSF_SendText.UseVisualStyleBackColor = true;
            this.cmdSSF_SendText.Click += new System.EventHandler(this.cmdSSF_SendText_Click);
            // 
            // cmdSSF_ConfigMem
            // 
            this.cmdSSF_ConfigMem.Location = new System.Drawing.Point(13, 55);
            this.cmdSSF_ConfigMem.Name = "cmdSSF_ConfigMem";
            this.cmdSSF_ConfigMem.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_ConfigMem.TabIndex = 3;
            this.cmdSSF_ConfigMem.Text = "Configure Memory";
            this.cmdSSF_ConfigMem.UseVisualStyleBackColor = true;
            this.cmdSSF_ConfigMem.Click += new System.EventHandler(this.cmdSS_ConfigMem_Click);
            // 
            // cmdSSF_ClearMem
            // 
            this.cmdSSF_ClearMem.Location = new System.Drawing.Point(13, 19);
            this.cmdSSF_ClearMem.Name = "cmdSSF_ClearMem";
            this.cmdSSF_ClearMem.Size = new System.Drawing.Size(164, 30);
            this.cmdSSF_ClearMem.TabIndex = 2;
            this.cmdSSF_ClearMem.Text = "Clear Memory";
            this.cmdSSF_ClearMem.UseVisualStyleBackColor = true;
            this.cmdSSF_ClearMem.Click += new System.EventHandler(this.cmdSS_ClearMem_Click);
            // 
            // grpCounterSample
            // 
            this.grpCounterSample.Controls.Add(this.cmdCounterClear);
            this.grpCounterSample.Controls.Add(this.cmdCounterSet);
            this.grpCounterSample.Controls.Add(this.cmdCounterText);
            this.grpCounterSample.Controls.Add(this.cmdCounterConfig);
            this.grpCounterSample.Location = new System.Drawing.Point(177, 333);
            this.grpCounterSample.Name = "grpCounterSample";
            this.grpCounterSample.Size = new System.Drawing.Size(210, 86);
            this.grpCounterSample.TabIndex = 6;
            this.grpCounterSample.TabStop = false;
            this.grpCounterSample.Text = "Counter Sample (Only certain signs)";
            // 
            // cmdCounterClear
            // 
            this.cmdCounterClear.Location = new System.Drawing.Point(21, 19);
            this.cmdCounterClear.Name = "cmdCounterClear";
            this.cmdCounterClear.Size = new System.Drawing.Size(80, 24);
            this.cmdCounterClear.TabIndex = 3;
            this.cmdCounterClear.Text = "Clear Mem";
            this.cmdCounterClear.UseVisualStyleBackColor = true;
            this.cmdCounterClear.Click += new System.EventHandler(this.cmdSS_ClearMem_Click);
            // 
            // cmdCounterSet
            // 
            this.cmdCounterSet.Location = new System.Drawing.Point(107, 49);
            this.cmdCounterSet.Name = "cmdCounterSet";
            this.cmdCounterSet.Size = new System.Drawing.Size(80, 24);
            this.cmdCounterSet.TabIndex = 2;
            this.cmdCounterSet.Text = "Set Counter";
            this.cmdCounterSet.UseVisualStyleBackColor = true;
            this.cmdCounterSet.Click += new System.EventHandler(this.cmdCounterSet_Click);
            // 
            // cmdCounterText
            // 
            this.cmdCounterText.Location = new System.Drawing.Point(21, 49);
            this.cmdCounterText.Name = "cmdCounterText";
            this.cmdCounterText.Size = new System.Drawing.Size(80, 24);
            this.cmdCounterText.TabIndex = 1;
            this.cmdCounterText.Text = "Send Text";
            this.cmdCounterText.UseVisualStyleBackColor = true;
            this.cmdCounterText.Click += new System.EventHandler(this.cmdCounterText_Click);
            // 
            // cmdCounterConfig
            // 
            this.cmdCounterConfig.Location = new System.Drawing.Point(107, 19);
            this.cmdCounterConfig.Name = "cmdCounterConfig";
            this.cmdCounterConfig.Size = new System.Drawing.Size(80, 24);
            this.cmdCounterConfig.TabIndex = 0;
            this.cmdCounterConfig.Text = "Config Mem";
            this.cmdCounterConfig.UseVisualStyleBackColor = true;
            this.cmdCounterConfig.Click += new System.EventHandler(this.cmdCounterConfig_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 7000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.ReshowDelay = 100;
            // 
            // grpCustChar
            // 
            this.grpCustChar.Controls.Add(this.cmdCC_ClearChar);
            this.grpCustChar.Controls.Add(this.label11);
            this.grpCustChar.Controls.Add(this.cmdCC_SendText);
            this.grpCustChar.Controls.Add(this.cmdCC_SendChar);
            this.grpCustChar.Controls.Add(this.cmdCC_ConfigMem);
            this.grpCustChar.Controls.Add(this.cmdCC_ClearMem);
            this.grpCustChar.Location = new System.Drawing.Point(585, 108);
            this.grpCustChar.Name = "grpCustChar";
            this.grpCustChar.Size = new System.Drawing.Size(147, 265);
            this.grpCustChar.TabIndex = 7;
            this.grpCustChar.TabStop = false;
            this.grpCustChar.Text = "Custom Character Sample";
            // 
            // cmdCC_ClearChar
            // 
            this.cmdCC_ClearChar.Location = new System.Drawing.Point(6, 163);
            this.cmdCC_ClearChar.Name = "cmdCC_ClearChar";
            this.cmdCC_ClearChar.Size = new System.Drawing.Size(133, 30);
            this.cmdCC_ClearChar.TabIndex = 7;
            this.cmdCC_ClearChar.Text = "Clear Smile Char";
            this.cmdCC_ClearChar.UseVisualStyleBackColor = true;
            this.cmdCC_ClearChar.Click += new System.EventHandler(this.cmdCC_ClearChar_Click);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(9, 208);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(129, 44);
            this.label11.TabIndex = 6;
            this.label11.Text = "This feature varies by sign. Check documentation!";
            // 
            // cmdCC_SendText
            // 
            this.cmdCC_SendText.Location = new System.Drawing.Point(6, 127);
            this.cmdCC_SendText.Name = "cmdCC_SendText";
            this.cmdCC_SendText.Size = new System.Drawing.Size(133, 30);
            this.cmdCC_SendText.TabIndex = 5;
            this.cmdCC_SendText.Text = "Send Text File";
            this.cmdCC_SendText.UseVisualStyleBackColor = true;
            this.cmdCC_SendText.Click += new System.EventHandler(this.cmdCC_SendText_Click);
            // 
            // cmdCC_SendChar
            // 
            this.cmdCC_SendChar.Location = new System.Drawing.Point(6, 91);
            this.cmdCC_SendChar.Name = "cmdCC_SendChar";
            this.cmdCC_SendChar.Size = new System.Drawing.Size(133, 30);
            this.cmdCC_SendChar.TabIndex = 4;
            this.cmdCC_SendChar.Text = "Send Smile Char";
            this.cmdCC_SendChar.UseVisualStyleBackColor = true;
            this.cmdCC_SendChar.Click += new System.EventHandler(this.cmdCC_SendChar_Click);
            // 
            // cmdCC_ConfigMem
            // 
            this.cmdCC_ConfigMem.Location = new System.Drawing.Point(6, 55);
            this.cmdCC_ConfigMem.Name = "cmdCC_ConfigMem";
            this.cmdCC_ConfigMem.Size = new System.Drawing.Size(133, 30);
            this.cmdCC_ConfigMem.TabIndex = 3;
            this.cmdCC_ConfigMem.Text = "Configure Memory";
            this.cmdCC_ConfigMem.UseVisualStyleBackColor = true;
            this.cmdCC_ConfigMem.Click += new System.EventHandler(this.cmdSS_ConfigMem_Click);
            // 
            // cmdCC_ClearMem
            // 
            this.cmdCC_ClearMem.Location = new System.Drawing.Point(6, 19);
            this.cmdCC_ClearMem.Name = "cmdCC_ClearMem";
            this.cmdCC_ClearMem.Size = new System.Drawing.Size(133, 30);
            this.cmdCC_ClearMem.TabIndex = 2;
            this.cmdCC_ClearMem.Text = "Clear Memory";
            this.cmdCC_ClearMem.UseVisualStyleBackColor = true;
            this.cmdCC_ClearMem.Click += new System.EventHandler(this.cmdSS_ClearMem_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 526);
            this.Controls.Add(this.grpCustChar);
            this.Controls.Add(this.grpCounterSample);
            this.Controls.Add(this.grpSSF);
            this.Controls.Add(this.grpQSF);
            this.Controls.Add(this.grpOutput);
            this.Controls.Add(this.grpStdSamples);
            this.Controls.Add(this.grpCommunication);
            this.Controls.Add(this.grpProtocol);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "[99990021 NET] Alpha Protocol Example NET";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.Move += new System.EventHandler(this.frmMain_Move);
            this.grpProtocol.ResumeLayout(false);
            this.grpProtocol.PerformLayout();
            this.grpCommunication.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabComSerial.ResumeLayout(false);
            this.tabComSerial.PerformLayout();
            this.tabComTCP.ResumeLayout(false);
            this.tabComTCP.PerformLayout();
            this.tabComUSB.ResumeLayout(false);
            this.tabComUSB.PerformLayout();
            this.tabComSim.ResumeLayout(false);
            this.grpStdSamples.ResumeLayout(false);
            this.grpOutput.ResumeLayout(false);
            this.grpOutput.PerformLayout();
            this.grpQSF.ResumeLayout(false);
            this.grpQSF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudGood)).EndInit();
            this.grpSSF.ResumeLayout(false);
            this.grpSSF.PerformLayout();
            this.grpCounterSample.ResumeLayout(false);
            this.grpCustChar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpProtocol;
        private System.Windows.Forms.GroupBox grpCommunication;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabComSerial;
        private System.Windows.Forms.TabPage tabComTCP;
        private System.Windows.Forms.TabPage tabComUSB;
        private System.Windows.Forms.TabPage tabComSim;
        private System.Windows.Forms.TextBox txCommSettings;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txCommPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txIpPort;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txIpAddress;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox grpStdSamples;
        private System.Windows.Forms.Button cmdSS_ClearMem;
        private System.Windows.Forms.GroupBox grpOutput;
        private System.Windows.Forms.TextBox txOutput;
        private System.Windows.Forms.Button cmdSS_ConfigMem;
        private System.Windows.Forms.CheckBox chkUseChecksums;
        private System.Windows.Forms.CheckBox chkUsePocsag;
        private System.Windows.Forms.Button cmdSS_SendText1;
        private System.Windows.Forms.Button cmdSS_SendText2;
        private System.Windows.Forms.Button cmdSS_SetTime;
        private System.Windows.Forms.Button cmdSS_DisplayTime;
        private System.Windows.Forms.Button cmdSS_SendDot;
        private System.Windows.Forms.Button cmdSS_DisplayDOT;
        private System.Windows.Forms.GroupBox grpQSF;
        private System.Windows.Forms.Button cmdQSF_ClearMemory;
        private System.Windows.Forms.Button cmdQSF_ConfigMem;
        private System.Windows.Forms.Button cmdQSF_SendText;
        private System.Windows.Forms.Button cmdQSF_SendVariables;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudBad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudGood;
        private System.Windows.Forms.GroupBox grpSSF;
        private System.Windows.Forms.Button cmdSSF_ConfigMem;
        private System.Windows.Forms.Button cmdSSF_ClearMem;
        private System.Windows.Forms.Button cmdSSF_SendText;
        private System.Windows.Forms.TextBox txSSF_StringB;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txSSF_StringA;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button cmdSSF_SendStringBoth;
        private System.Windows.Forms.Button cmdSSF_SendStringB;
        private System.Windows.Forms.Button cmdSSF_SendStringA;
        private System.Windows.Forms.GroupBox grpCounterSample;
        private System.Windows.Forms.Button cmdCounterSet;
        private System.Windows.Forms.Button cmdCounterText;
        private System.Windows.Forms.Button cmdCounterConfig;
        private System.Windows.Forms.Button cmdCounterClear;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox grpCustChar;
        private System.Windows.Forms.Button cmdCC_SendText;
        private System.Windows.Forms.Button cmdCC_SendChar;
        private System.Windows.Forms.Button cmdCC_ConfigMem;
        private System.Windows.Forms.Button cmdCC_ClearMem;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cmdCC_ClearChar;
    }
}

