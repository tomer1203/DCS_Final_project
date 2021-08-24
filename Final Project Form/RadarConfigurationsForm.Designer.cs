namespace FinalProject
{
    partial class RadarConfigurationsForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.radarConfigurationsPanel = new System.Windows.Forms.Panel();
            this.cmLabel = new System.Windows.Forms.Label();
            this.maskedDistanceTextBox = new System.Windows.Forms.TextBox();
            this.maskDistanceLabel = new System.Windows.Forms.Label();
            this.beamRangeLabel = new System.Windows.Forms.Label();
            this.beamRangeTextBox = new System.Windows.Forms.TextBox();
            this.beamRangeDegLabel = new System.Windows.Forms.Label();
            this.radarConfigurationsSaveutton = new System.Windows.Forms.Button();
            this.saveConfErrorLabel = new System.Windows.Forms.Label();
            this.radarConfigurationsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.label1.Location = new System.Drawing.Point(52, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(225, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Set Rdar Configurations:";
            // 
            // radarConfigurationsPanel
            // 
            this.radarConfigurationsPanel.Controls.Add(this.beamRangeDegLabel);
            this.radarConfigurationsPanel.Controls.Add(this.beamRangeTextBox);
            this.radarConfigurationsPanel.Controls.Add(this.beamRangeLabel);
            this.radarConfigurationsPanel.Controls.Add(this.cmLabel);
            this.radarConfigurationsPanel.Controls.Add(this.maskedDistanceTextBox);
            this.radarConfigurationsPanel.Controls.Add(this.maskDistanceLabel);
            this.radarConfigurationsPanel.Location = new System.Drawing.Point(24, 113);
            this.radarConfigurationsPanel.Name = "radarConfigurationsPanel";
            this.radarConfigurationsPanel.Size = new System.Drawing.Size(301, 126);
            this.radarConfigurationsPanel.TabIndex = 13;
            // 
            // cmLabel
            // 
            this.cmLabel.AutoSize = true;
            this.cmLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.cmLabel.Location = new System.Drawing.Point(245, 26);
            this.cmLabel.Name = "cmLabel";
            this.cmLabel.Size = new System.Drawing.Size(30, 20);
            this.cmLabel.TabIndex = 2;
            this.cmLabel.Text = "cm";
            // 
            // maskedDistanceTextBox
            // 
            this.maskedDistanceTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.maskedDistanceTextBox.Location = new System.Drawing.Point(149, 16);
            this.maskedDistanceTextBox.Name = "maskedDistanceTextBox";
            this.maskedDistanceTextBox.Size = new System.Drawing.Size(90, 30);
            this.maskedDistanceTextBox.TabIndex = 1;
            // 
            // maskDistanceLabel
            // 
            this.maskDistanceLabel.AutoSize = true;
            this.maskDistanceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.maskDistanceLabel.Location = new System.Drawing.Point(7, 19);
            this.maskDistanceLabel.Name = "maskDistanceLabel";
            this.maskDistanceLabel.Size = new System.Drawing.Size(136, 20);
            this.maskDistanceLabel.TabIndex = 0;
            this.maskDistanceLabel.Text = "Masked Distance:";
            // 
            // beamRangeLabel
            // 
            this.beamRangeLabel.AutoSize = true;
            this.beamRangeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.beamRangeLabel.Location = new System.Drawing.Point(29, 75);
            this.beamRangeLabel.Name = "beamRangeLabel";
            this.beamRangeLabel.Size = new System.Drawing.Size(107, 20);
            this.beamRangeLabel.TabIndex = 3;
            this.beamRangeLabel.Text = "Beam Range:";
            // 
            // beamRangeTextBox
            // 
            this.beamRangeTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.beamRangeTextBox.Location = new System.Drawing.Point(149, 68);
            this.beamRangeTextBox.Name = "beamRangeTextBox";
            this.beamRangeTextBox.Size = new System.Drawing.Size(90, 30);
            this.beamRangeTextBox.TabIndex = 4;
            // 
            // beamRangeDegLabel
            // 
            this.beamRangeDegLabel.AutoSize = true;
            this.beamRangeDegLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.beamRangeDegLabel.Location = new System.Drawing.Point(245, 68);
            this.beamRangeDegLabel.Name = "beamRangeDegLabel";
            this.beamRangeDegLabel.Size = new System.Drawing.Size(18, 20);
            this.beamRangeDegLabel.TabIndex = 5;
            this.beamRangeDegLabel.Text = "o";
            // 
            // radarConfigurationsSaveutton
            // 
            this.radarConfigurationsSaveutton.ForeColor = System.Drawing.Color.Teal;
            this.radarConfigurationsSaveutton.Location = new System.Drawing.Point(123, 274);
            this.radarConfigurationsSaveutton.Name = "radarConfigurationsSaveutton";
            this.radarConfigurationsSaveutton.Size = new System.Drawing.Size(75, 37);
            this.radarConfigurationsSaveutton.TabIndex = 14;
            this.radarConfigurationsSaveutton.Text = "Save";
            this.radarConfigurationsSaveutton.UseVisualStyleBackColor = true;
            this.radarConfigurationsSaveutton.Click += new System.EventHandler(this.radarConfigurationsSaveButton_Click);
            // 
            // saveConfErrorLabel
            // 
            this.saveConfErrorLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.saveConfErrorLabel.ForeColor = System.Drawing.Color.Tomato;
            this.saveConfErrorLabel.Location = new System.Drawing.Point(0, 325);
            this.saveConfErrorLabel.Name = "saveConfErrorLabel";
            this.saveConfErrorLabel.Size = new System.Drawing.Size(346, 20);
            this.saveConfErrorLabel.TabIndex = 15;
            this.saveConfErrorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RadarConfigurationsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 345);
            this.Controls.Add(this.saveConfErrorLabel);
            this.Controls.Add(this.radarConfigurationsSaveutton);
            this.Controls.Add(this.radarConfigurationsPanel);
            this.Controls.Add(this.label1);
            this.Name = "RadarConfigurationsForm";
            this.Text = "Final Project";
            this.radarConfigurationsPanel.ResumeLayout(false);
            this.radarConfigurationsPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.KeyPreview = true;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.saveConfButton_KeyDown);
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel radarConfigurationsPanel;
        private System.Windows.Forms.Label cmLabel;
        private System.Windows.Forms.TextBox maskedDistanceTextBox;
        private System.Windows.Forms.Label maskDistanceLabel;
        private System.Windows.Forms.Label beamRangeDegLabel;
        private System.Windows.Forms.TextBox beamRangeTextBox;
        private System.Windows.Forms.Label beamRangeLabel;
        private System.Windows.Forms.Button radarConfigurationsSaveutton;
        private System.Windows.Forms.Label saveConfErrorLabel;
    }
}