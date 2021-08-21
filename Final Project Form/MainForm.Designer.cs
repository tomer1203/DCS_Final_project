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
            this.panel2 = new System.Windows.Forms.Panel();
            this.angleLabel = new System.Windows.Forms.Label();
            this.distanceLabel = new System.Windows.Forms.Label();
            this.radarPictureBox = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.fileStatusLabel = new System.Windows.Forms.Label();
            this.sendFileButton = new System.Windows.Forms.Button();
            this.filesListView = new System.Windows.Forms.ListView();
            this.fileNamePanel = new System.Windows.Forms.Panel();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.connectingLabel = new System.Windows.Forms.Label();
            this.connectingPictureBox = new System.Windows.Forms.PictureBox();
            this.configurationsLabel = new System.Windows.Forms.Label();
            this.settingsPanel = new System.Windows.Forms.Panel();
            this.scanButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.radarPanel.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarPictureBox)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.fileNamePanel.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.connectingPictureBox)).BeginInit();
            this.settingsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendButton
            // 
            this.telemetriaButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.telemetriaButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaButton.ForeColor = System.Drawing.Color.Teal;
            this.telemetriaButton.Location = new System.Drawing.Point(807, 17);
            this.telemetriaButton.Name = "sendButton";
            this.telemetriaButton.Size = new System.Drawing.Size(61, 36);
            this.telemetriaButton.TabIndex = 0;
            this.telemetriaButton.Text = "Send";
            this.telemetriaButton.UseVisualStyleBackColor = true;
            this.telemetriaButton.Click += new System.EventHandler(this.telemetriaButton_Click);
            // 
            // dataToSendTextBox
            // 
            this.telemetriaDataTextBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.telemetriaDataTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.telemetriaDataTextBox.Location = new System.Drawing.Point(98, 17);
            this.telemetriaDataTextBox.Name = "dataToSendTextBox";
            this.telemetriaDataTextBox.Size = new System.Drawing.Size(703, 30);
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
            this.tabPage1.Controls.Add(this.telemetriaButton);
            this.tabPage1.Controls.Add(this.telemetriaDataTextBox);
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
            this.radarPanel.BackColor = System.Drawing.Color.Black;
            this.radarPanel.Controls.Add(this.scanButton);
            this.radarPanel.Controls.Add(this.panel2);
            this.radarPanel.Controls.Add(this.radarPictureBox);
            this.radarPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radarPanel.Location = new System.Drawing.Point(3, 3);
            this.radarPanel.Name = "radarPanel";
            this.radarPanel.Size = new System.Drawing.Size(971, 544);
            this.radarPanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.angleLabel);
            this.panel2.Controls.Add(this.distanceLabel);
            this.panel2.Location = new System.Drawing.Point(791, 14);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(163, 77);
            this.panel2.TabIndex = 1;
            // 
            // angleLabel
            // 
            this.angleLabel.AutoSize = true;
            this.angleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.angleLabel.ForeColor = System.Drawing.Color.Green;
            this.angleLabel.Location = new System.Drawing.Point(3, 46);
            this.angleLabel.Name = "angleLabel";
            this.angleLabel.Size = new System.Drawing.Size(60, 20);
            this.angleLabel.TabIndex = 1;
            this.angleLabel.Text = "Angle:";
            // 
            // distanceLabel
            // 
            this.distanceLabel.AutoSize = true;
            this.distanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.distanceLabel.ForeColor = System.Drawing.Color.Green;
            this.distanceLabel.Location = new System.Drawing.Point(3, 11);
            this.distanceLabel.Name = "distanceLabel";
            this.distanceLabel.Size = new System.Drawing.Size(85, 20);
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
            this.sendFileButton.Location = new System.Drawing.Point(385, 366);
            this.sendFileButton.Name = "sendFileButton";
            this.sendFileButton.Size = new System.Drawing.Size(75, 27);
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
            this.filesListView.Location = new System.Drawing.Point(27, 86);
            this.filesListView.MultiSelect = false;
            this.filesListView.Name = "filesListView";
            this.filesListView.Scrollable = false;
            this.filesListView.Size = new System.Drawing.Size(770, 218);
            this.filesListView.TabIndex = 1;
            this.filesListView.UseCompatibleStateImageBehavior = false;
            this.filesListView.View = System.Windows.Forms.View.Details;
            this.filesListView.ColumnWidthChanging += new System.Windows.Forms.ColumnWidthChangingEventHandler(this.filesListView_ColumnWidthChanging);
            this.filesListView.SelectedIndexChanged += new System.EventHandler(this.filesListView_SelectedIndexChanged);
            // 
            // fileNamePanel
            // 
            this.fileNamePanel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.fileNamePanel.Controls.Add(this.fileNameLabel);
            this.fileNamePanel.Location = new System.Drawing.Point(22, 16);
            this.fileNamePanel.Name = "fileNamePanel";
            this.fileNamePanel.Size = new System.Drawing.Size(782, 64);
            this.fileNamePanel.TabIndex = 0;
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.fileNameLabel.Location = new System.Drawing.Point(0, 0);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(782, 64);
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
            this.configurationsLabel.Location = new System.Drawing.Point(10, 7);
            this.configurationsLabel.Name = "configurationsLabel";
            this.configurationsLabel.Size = new System.Drawing.Size(63, 29);
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
            this.settingsPanel.Location = new System.Drawing.Point(12, 12);
            this.settingsPanel.Name = "settingsPanel";
            this.settingsPanel.Size = new System.Drawing.Size(76, 37);
            this.settingsPanel.TabIndex = 2;
            this.settingsPanel.MouseLeave += new System.EventHandler(this.settingsPanel_MouseLeave);
            this.settingsPanel.MouseHover += new System.EventHandler(this.settingsPanel_MouseHover);
            // 
            // scanButton
            // 
            this.scanButton.BackColor = System.Drawing.Color.DimGray;
            this.scanButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.scanButton.ForeColor = System.Drawing.Color.Black;
            this.scanButton.Location = new System.Drawing.Point(19, 17);
            this.scanButton.Name = "scanButton";
            this.scanButton.Size = new System.Drawing.Size(81, 36);
            this.scanButton.TabIndex = 2;
            this.scanButton.Text = "Scan";
            this.scanButton.UseVisualStyleBackColor = false;
            this.scanButton.Click += new System.EventHandler(this.scanButton_Click);
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
            this.tabPage1.PerformLayout();
            this.radarPanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radarPictureBox)).EndInit();
            this.tabPage2.ResumeLayout(false);
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
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label angleLabel;
        private System.Windows.Forms.Label distanceLabel;
        private System.Windows.Forms.Button scanButton;
    }
}

