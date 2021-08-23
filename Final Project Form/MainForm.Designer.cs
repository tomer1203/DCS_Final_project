namespace TerminalProject
{
    partial class MainForm
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
            this.telemetriaButton = new System.Windows.Forms.Button();
            this.telemetriaDataTextBox = new System.Windows.Forms.TextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.statusLabel = new System.Windows.Forms.Label();
            this.radarPanel = new System.Windows.Forms.Panel();
            this.maskedDistancePanel = new System.Windows.Forms.Panel();
            this.cmLabel = new System.Windows.Forms.Label();
            this.maskedDistanceTextBox = new System.Windows.Forms.TextBox();
            this.maskDistanceLabel = new System.Windows.Forms.Label();
            this.deg150Label = new System.Windows.Forms.Label();
            this.deg120Label = new System.Windows.Forms.Label();
            this.deg90Label = new System.Windows.Forms.Label();
            this.deg60Label = new System.Windows.Forms.Label();
            this.deg30Label = new System.Windows.Forms.Label();
            this.telemetriaPanel = new System.Windows.Forms.Panel();
            this.telemetriaAngleLabel = new System.Windows.Forms.Label();
            this.telemetriaDistanceLabel = new System.Windows.Forms.Label();
            this.telemetriaLabel = new System.Windows.Forms.Label();
            this.scanButton = new System.Windows.Forms.Button();
            this.radarTextPanel = new System.Windows.Forms.Panel();
            this.angleLabel = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.radarPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label2 = new System.Windows.Forms.Label();
            this.saveScriptButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.scriptNameTextBox = new System.Windows.Forms.TextBox();
            this.scriptDataRichTextBox = new System.Windows.Forms.RichTextBox();
            this.fileStatusLabel = new System.Windows.Forms.Label();
            this.sendFileButton = new System.Windows.Forms.Button();
            this.filesListView = new System.Windows.Forms.ListView();
            this.fileNamePanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectingLabel = new System.Windows.Forms.Label();
            this.connectingPictureBox = new System.Windows.Forms.PictureBox();
            this.configurationsLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.cmRadarPanelLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.radarPanel.SuspendLayout();
            this.maskedDistancePanel.SuspendLayout();
            this.telemetriaPanel.SuspendLayout();
            this.radarTextPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarPictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.fileNamePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectingPictureBox)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // telemetriaButton
            // 
            this.telemetriaButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.telemetriaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaButton.ForeColor = System.Drawing.Color.Teal;
            this.telemetriaButton.Location = new System.Drawing.Point(798, 108);
            this.telemetriaButton.Name = "telemetriaButton";
            this.telemetriaButton.Size = new System.Drawing.Size(61, 36);
            this.telemetriaButton.TabIndex = 0;
            this.telemetriaButton.Text = "Send";
            this.telemetriaButton.UseVisualStyleBackColor = true;
            this.telemetriaButton.Click += new System.EventHandler(this.telemetriaButton_Click);
            // 
            // telemetriaDataTextBox
            // 
            this.telemetriaDataTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.telemetriaDataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaDataTextBox.Location = new System.Drawing.Point(202, 109);
            this.telemetriaDataTextBox.Name = "telemetriaDataTextBox";
            this.telemetriaDataTextBox.Size = new System.Drawing.Size(571, 35);
            this.telemetriaDataTextBox.TabIndex = 0;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.tabControl1.ItemSize = new System.Drawing.Size(100, 35);
            this.tabControl1.Location = new System.Drawing.Point(0, 51);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(196, 2);
            this.tabControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(985, 593);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.statusLabel);
            this.tabPage1.Controls.Add(this.radarPanel);
            this.tabPage1.Location = new System.Drawing.Point(4, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(977, 550);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Radar Detector";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // statusLabel
            // 
            this.statusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.statusLabel.Location = new System.Drawing.Point(3, 527);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(971, 20);
            this.statusLabel.TabIndex = 4;
            this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // radarPanel
            // 
            this.radarPanel.BackColor = System.Drawing.Color.Transparent;
            this.radarPanel.Controls.Add(this.maskedDistancePanel);
            this.radarPanel.Controls.Add(this.deg150Label);
            this.radarPanel.Controls.Add(this.deg120Label);
            this.radarPanel.Controls.Add(this.deg90Label);
            this.radarPanel.Controls.Add(this.deg60Label);
            this.radarPanel.Controls.Add(this.deg30Label);
            this.radarPanel.Controls.Add(this.telemetriaPanel);
            this.radarPanel.Controls.Add(this.telemetriaLabel);
            this.radarPanel.Controls.Add(this.scanButton);
            this.radarPanel.Controls.Add(this.radarTextPanel);
            this.radarPanel.Controls.Add(this.telemetriaButton);
            this.radarPanel.Controls.Add(this.telemetriaDataTextBox);
            this.radarPanel.Controls.Add(this.radarPictureBox);
            this.radarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radarPanel.Location = new System.Drawing.Point(3, 3);
            this.radarPanel.Name = "radarPanel";
            this.radarPanel.Size = new System.Drawing.Size(971, 544);
            this.radarPanel.TabIndex = 2;
            // 
            // maskedDistancePanel
            // 
            this.maskedDistancePanel.Controls.Add(this.cmLabel);
            this.maskedDistancePanel.Controls.Add(this.maskedDistanceTextBox);
            this.maskedDistancePanel.Controls.Add(this.maskDistanceLabel);
            this.maskedDistancePanel.Location = new System.Drawing.Point(41, 466);
            this.maskedDistancePanel.Name = "maskedDistancePanel";
            this.maskedDistancePanel.Size = new System.Drawing.Size(315, 55);
            this.maskedDistancePanel.TabIndex = 12;
            // 
            // cmLabel
            // 
            this.cmLabel.AutoSize = true;
            this.cmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmLabel.Location = new System.Drawing.Point(277, 26);
            this.cmLabel.Name = "cmLabel";
            this.cmLabel.Size = new System.Drawing.Size(30, 20);
            this.cmLabel.TabIndex = 2;
            this.cmLabel.Text = "cm";
            // 
            // maskedDistanceTextBox
            // 
            this.maskedDistanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.maskedDistanceTextBox.Location = new System.Drawing.Point(181, 16);
            this.maskedDistanceTextBox.Name = "maskedDistanceTextBox";
            this.maskedDistanceTextBox.Size = new System.Drawing.Size(90, 30);
            this.maskedDistanceTextBox.TabIndex = 1;
            // 
            // maskDistanceLabel
            // 
            this.maskDistanceLabel.AutoSize = true;
            this.maskDistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.maskDistanceLabel.Location = new System.Drawing.Point(7, 19);
            this.maskDistanceLabel.Name = "maskDistanceLabel";
            this.maskDistanceLabel.Size = new System.Drawing.Size(169, 25);
            this.maskDistanceLabel.TabIndex = 0;
            this.maskDistanceLabel.Text = "Masked Distance:";
            // 
            // deg150Label
            // 
            this.deg150Label.AutoSize = true;
            this.deg150Label.ForeColor = System.Drawing.Color.Green;
            this.deg150Label.Location = new System.Drawing.Point(890, 255);
            this.deg150Label.Name = "deg150Label";
            this.deg150Label.Size = new System.Drawing.Size(40, 20);
            this.deg150Label.TabIndex = 11;
            this.deg150Label.Text = "150 ";
            this.deg150Label.Visible = false;
            // 
            // deg120Label
            // 
            this.deg120Label.AutoSize = true;
            this.deg120Label.ForeColor = System.Drawing.Color.Green;
            this.deg120Label.Location = new System.Drawing.Point(715, 71);
            this.deg120Label.Name = "deg120Label";
            this.deg120Label.Size = new System.Drawing.Size(40, 20);
            this.deg120Label.TabIndex = 10;
            this.deg120Label.Text = "120 ";
            this.deg120Label.Visible = false;
            // 
            // deg90Label
            // 
            this.deg90Label.AutoSize = true;
            this.deg90Label.ForeColor = System.Drawing.Color.Green;
            this.deg90Label.Location = new System.Drawing.Point(468, 14);
            this.deg90Label.Name = "deg90Label";
            this.deg90Label.Size = new System.Drawing.Size(31, 20);
            this.deg90Label.TabIndex = 9;
            this.deg90Label.Text = "90 ";
            this.deg90Label.Visible = false;
            // 
            // deg60Label
            // 
            this.deg60Label.AutoSize = true;
            this.deg60Label.ForeColor = System.Drawing.Color.Green;
            this.deg60Label.Location = new System.Drawing.Point(219, 71);
            this.deg60Label.Name = "deg60Label";
            this.deg60Label.Size = new System.Drawing.Size(31, 20);
            this.deg60Label.TabIndex = 8;
            this.deg60Label.Text = "60 ";
            this.deg60Label.Visible = false;
            // 
            // deg30Label
            // 
            this.deg30Label.AutoSize = true;
            this.deg30Label.ForeColor = System.Drawing.Color.Green;
            this.deg30Label.Location = new System.Drawing.Point(37, 255);
            this.deg30Label.Name = "deg30Label";
            this.deg30Label.Size = new System.Drawing.Size(31, 20);
            this.deg30Label.TabIndex = 7;
            this.deg30Label.Text = "30 ";
            this.deg30Label.Visible = false;
            // 
            // telemetriaPanel
            // 
            this.telemetriaPanel.Controls.Add(this.telemetriaAngleLabel);
            this.telemetriaPanel.Controls.Add(this.telemetriaDistanceLabel);
            this.telemetriaPanel.Location = new System.Drawing.Point(121, 226);
            this.telemetriaPanel.Name = "telemetriaPanel";
            this.telemetriaPanel.Size = new System.Drawing.Size(705, 67);
            this.telemetriaPanel.TabIndex = 6;
            // 
            // telemetriaAngleLabel
            // 
            this.telemetriaAngleLabel.AutoSize = true;
            this.telemetriaAngleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaAngleLabel.Location = new System.Drawing.Point(431, 20);
            this.telemetriaAngleLabel.Name = "telemetriaAngleLabel";
            this.telemetriaAngleLabel.Size = new System.Drawing.Size(87, 29);
            this.telemetriaAngleLabel.TabIndex = 5;
            this.telemetriaAngleLabel.Text = "Angle:";
            // 
            // telemetriaDistanceLabel
            // 
            this.telemetriaDistanceLabel.AutoSize = true;
            this.telemetriaDistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaDistanceLabel.Location = new System.Drawing.Point(86, 20);
            this.telemetriaDistanceLabel.Name = "telemetriaDistanceLabel";
            this.telemetriaDistanceLabel.Size = new System.Drawing.Size(121, 29);
            this.telemetriaDistanceLabel.TabIndex = 4;
            this.telemetriaDistanceLabel.Text = "Distance:";
            // 
            // telemetriaLabel
            // 
            this.telemetriaLabel.AutoSize = true;
            this.telemetriaLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaLabel.Location = new System.Drawing.Point(429, 25);
            this.telemetriaLabel.Name = "telemetriaLabel";
            this.telemetriaLabel.Size = new System.Drawing.Size(147, 29);
            this.telemetriaLabel.TabIndex = 3;
            this.telemetriaLabel.Text = "Telemetria ";
            // 
            // scanButton
            // 
            this.scanButton.BackColor = System.Drawing.Color.DimGray;
            this.scanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanButton.ForeColor = System.Drawing.Color.Black;
            this.scanButton.Location = new System.Drawing.Point(19, 17);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(110, 36);
            this.scanButton.TabIndex = 2;
            this.scanButton.Text = "Start Scan";
            this.scanButton.UseVisualStyleBackColor = false;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
            // 
            // radarTextPanel
            // 
            this.radarTextPanel.BackColor = System.Drawing.Color.Transparent;
            this.radarTextPanel.Controls.Add(this.cmRadarPanelLabel);
            this.radarTextPanel.Controls.Add(this.angleLabel);
            this.radarTextPanel.Controls.Add(this.distanceLabel);
            this.radarTextPanel.Location = new System.Drawing.Point(752, 14);
            this.radarTextPanel.Name = "radarTextPanel";
            this.radarTextPanel.Size = new System.Drawing.Size(214, 77);
            this.radarTextPanel.TabIndex = 1;
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.angleLabel.ForeColor = System.Drawing.Color.Green;
            this.angleLabel.Location = new System.Drawing.Point(3, 46);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(75, 25);
            this.angleLabel.TabIndex = 1;
            this.angleLabel.Text = "Angle:";
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.distanceLabel.ForeColor = System.Drawing.Color.Green;
            this.distanceLabel.Location = new System.Drawing.Point(3, 11);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(103, 25);
            this.distanceLabel.TabIndex = 0;
            this.distanceLabel.Text = "Distance:";
            // 
            // radarPictureBox
            // 
            this.radarPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radarPictureBox.Location = new System.Drawing.Point(0, 0);
            this.radarPictureBox.Name = "radarPictureBox";
            this.radarPictureBox.Size = new System.Drawing.Size(971, 544);
            this.radarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.radarPictureBox.TabIndex = 0;
            this.radarPictureBox.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.saveScriptButton);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.scriptNameTextBox);
            this.tabPage2.Controls.Add(this.scriptDataRichTextBox);
            this.tabPage2.Controls.Add(this.fileStatusLabel);
            this.tabPage2.Controls.Add(this.sendFileButton);
            this.tabPage2.Controls.Add(this.filesListView);
            this.tabPage2.Controls.Add(this.fileNamePanel);
            this.tabPage2.Location = new System.Drawing.Point(4, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(977, 550);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "File Transfer";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label2.Location = new System.Drawing.Point(30, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 25);
            this.label2.TabIndex = 10;
            this.label2.Text = "Name:";
            // 
            // saveScriptButton
            // 
            this.saveScriptButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.saveScriptButton.ForeColor = System.Drawing.Color.Teal;
            this.saveScriptButton.Location = new System.Drawing.Point(197, 450);
            this.saveScriptButton.Name = "saveScriptButton";
            this.saveScriptButton.Size = new System.Drawing.Size(98, 43);
            this.saveScriptButton.TabIndex = 9;
            this.saveScriptButton.Text = "Save";
            this.saveScriptButton.UseVisualStyleBackColor = true;
            this.saveScriptButton.Click += new System.EventHandler(this.saveScriptButton_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.ForeColor = System.Drawing.Color.Teal;
            this.button1.Location = new System.Drawing.Point(519, 450);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 27);
            this.button1.TabIndex = 8;
            this.button1.Text = "Delete";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.deleteScriptButton_Click);
            // 
            // scriptNameTextBox
            // 
            this.scriptNameTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.scriptNameTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.scriptNameTextBox.Location = new System.Drawing.Point(120, 102);
            this.scriptNameTextBox.Name = "scriptNameTextBox";
            this.scriptNameTextBox.Size = new System.Drawing.Size(348, 35);
            this.scriptNameTextBox.TabIndex = 7;
            // 
            // scriptDataRichTextBox
            // 
            this.scriptDataRichTextBox.BackColor = System.Drawing.SystemColors.MenuBar;
            this.scriptDataRichTextBox.Location = new System.Drawing.Point(35, 149);
            this.scriptDataRichTextBox.Name = "scriptDataRichTextBox";
            this.scriptDataRichTextBox.Size = new System.Drawing.Size(433, 277);
            this.scriptDataRichTextBox.TabIndex = 6;
            this.scriptDataRichTextBox.Text = "";
            // 
            // fileStatusLabel
            // 
            this.fileStatusLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.fileStatusLabel.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.fileStatusLabel.Location = new System.Drawing.Point(3, 527);
            this.fileStatusLabel.Name = "fileStatusLabel";
            this.fileStatusLabel.Size = new System.Drawing.Size(971, 20);
            this.fileStatusLabel.TabIndex = 5;
            this.fileStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sendFileButton
            // 
            this.sendFileButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.sendFileButton.ForeColor = System.Drawing.Color.Teal;
            this.sendFileButton.Location = new System.Drawing.Point(692, 450);
            this.sendFileButton.Name = "sendFileButton";
            this.sendFileButton.Size = new System.Drawing.Size(98, 43);
            this.sendFileButton.TabIndex = 2;
            this.sendFileButton.Text = "Send";
            this.sendFileButton.UseVisualStyleBackColor = true;
            this.sendFileButton.Click += new System.EventHandler(this.sendFileButton_Click);
            // 
            // filesListView
            // 
            this.filesListView.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.filesListView.BackColor = System.Drawing.SystemColors.MenuBar;
            this.filesListView.FullRowSelect = true;
            this.filesListView.GridLines = true;
            this.filesListView.HideSelection = false;
            this.filesListView.Location = new System.Drawing.Point(519, 102);
            this.filesListView.MultiSelect = false;
            this.filesListView.Name = "filesListView";
            this.filesListView.Scrollable = false;
            this.filesListView.Size = new System.Drawing.Size(434, 324);
            this.filesListView.TabIndex = 1;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.Details;
            this.filesListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.filesListView_ColumnWidthChanging);
            this.filesListView.SelectedIndexChanged += new System.EventHandler(this.filesListView_SelectedIndexChanged);
            // 
            // fileNamePanel
            // 
            this.fileNamePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fileNamePanel.Controls.Add(this.label1);
            this.fileNamePanel.Controls.Add(this.fileNameLabel);
            this.fileNamePanel.Location = new System.Drawing.Point(35, 16);
            this.fileNamePanel.Name = "fileNamePanel";
            this.fileNamePanel.Size = new System.Drawing.Size(918, 64);
            this.fileNamePanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(119, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 64);
            this.label1.TabIndex = 2;
            this.label1.Text = "New Script";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fileNameLabel.Location = new System.Drawing.Point(608, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(209, 64);
            this.fileNameLabel.TabIndex = 1;
            this.fileNameLabel.Text = "Choose file to send ";
            this.fileNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.connectingLabel);
            this.panel1.Controls.Add(this.connectingPictureBox);
            this.panel1.Location = new System.Drawing.Point(59, 11);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(826, 37);
            this.panel1.TabIndex = 2;
            // 
            // connectingLabel
            // 
            this.connectingLabel.AutoSize = true;
            this.connectingLabel.ForeColor = System.Drawing.Color.Navy;
            this.connectingLabel.Location = new System.Drawing.Point(392, 8);
            this.connectingLabel.Name = "connectingLabel";
            this.connectingLabel.Size = new System.Drawing.Size(102, 20);
            this.connectingLabel.TabIndex = 2;
            this.connectingLabel.Text = "Connecting...";
            this.connectingLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // connectingPictureBox
            // 
            this.connectingPictureBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.connectingPictureBox.Location = new System.Drawing.Point(371, 13);
            this.connectingPictureBox.Name = "connectingPictureBox";
            this.connectingPictureBox.Size = new System.Drawing.Size(15, 15);
            this.connectingPictureBox.TabIndex = 4;
            this.connectingPictureBox.TabStop = false;
            // 
            // configurationsLabel
            // 
            this.configurationsLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.configurationsLabel.BackColor = System.Drawing.Color.Transparent;
            this.configurationsLabel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.configurationsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.configurationsLabel.Location = new System.Drawing.Point(11, 8);
            this.configurationsLabel.Name = "configurationsLabel";
            this.configurationsLabel.Size = new System.Drawing.Size(65, 29);
            this.configurationsLabel.TabIndex = 3;
            this.configurationsLabel.Text = "Serial";
            this.configurationsLabel.Click += new System.EventHandler(this.configurationButton_click);
            this.configurationsLabel.MouseLeave += new System.EventHandler(this.settingsPanel_MouseLeave);
            this.configurationsLabel.MouseHover += new System.EventHandler(this.settingsPanel_MouseHover);
            // 
            // settingsPanel
            // 
            this.settingsPanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.settingsPanel.BackColor = System.Drawing.Color.Transparent;
            this.settingsPanel.Controls.Add(this.configurationsLabel);
            this.settingsPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.settingsPanel.Location = new System.Drawing.Point(11, 8);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(82, 37);
            this.settingsPanel.TabIndex = 2;
            this.settingsPanel.MouseLeave += new System.EventHandler(this.settingsPanel_MouseLeave);
            this.settingsPanel.MouseHover += new System.EventHandler(this.settingsPanel_MouseHover);
            // 
            // cmRadarPanelLabel
            // 
            this.cmRadarPanelLabel.AutoSize = true;
            this.cmRadarPanelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmRadarPanelLabel.ForeColor = System.Drawing.Color.Green;
            this.cmRadarPanelLabel.Location = new System.Drawing.Point(179, 15);
            this.cmRadarPanelLabel.Name = "cmRadarPanelLabel";
            this.cmRadarPanelLabel.Size = new System.Drawing.Size(32, 20);
            this.cmRadarPanelLabel.TabIndex = 2;
            this.cmRadarPanelLabel.Text = "cm";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumTurquoise;
            this.ClientSize = new System.Drawing.Size(985, 644);
            this.Controls.Add(this.settingsPanel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tabControl1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(515, 400);
            this.Name = "MainForm";
            this.Text = "Final Project";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainForm_KeyDown);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.radarPanel.ResumeLayout(false);
            this.radarPanel.PerformLayout();
            this.maskedDistancePanel.ResumeLayout(false);
            this.maskedDistancePanel.PerformLayout();
            this.telemetriaPanel.ResumeLayout(false);
            this.telemetriaPanel.PerformLayout();
            this.radarTextPanel.ResumeLayout(false);
            this.radarTextPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarPictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.fileNamePanel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectingPictureBox)).EndInit();
            this.settingsPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button telemetriaButton;

        // Text Box
        private System.Windows.Forms.TextBox telemetriaDataTextBox;
        private System.Windows.Forms.Label connectingLabel;

        // Tabs
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label configurationsLabel;
        private System.Windows.Forms.PictureBox connectingPictureBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel settingsPanel;
        private System.Windows.Forms.Panel fileNamePanel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.ListView filesListView;
        private System.Windows.Forms.Button sendFileButton;
        private System.Windows.Forms.Label fileStatusLabel;
        private System.Windows.Forms.Panel radarPanel;
        private System.Windows.Forms.PictureBox radarPictureBox;
        private System.Windows.Forms.Panel radarTextPanel;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Button scanButton;
        private System.Windows.Forms.RichTextBox scriptDataRichTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox scriptNameTextBox;
        private System.Windows.Forms.Button saveScriptButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label telemetriaLabel;
        private System.Windows.Forms.Panel telemetriaPanel;
        private System.Windows.Forms.Label telemetriaAngleLabel;
        private System.Windows.Forms.Label telemetriaDistanceLabel;
        private System.Windows.Forms.Label deg90Label;
        private System.Windows.Forms.Label deg60Label;
        private System.Windows.Forms.Label deg30Label;
        private System.Windows.Forms.Label deg150Label;
        private System.Windows.Forms.Label deg120Label;
        private System.Windows.Forms.Panel maskedDistancePanel;
        private System.Windows.Forms.Label cmLabel;
        private System.Windows.Forms.TextBox maskedDistanceTextBox;
        private System.Windows.Forms.Label maskDistanceLabel;
        private System.Windows.Forms.Label cmRadarPanelLabel;
    }
}

