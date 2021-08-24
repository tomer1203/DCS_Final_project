using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FinalProject.Source_files;

namespace FinalProject
{
    public partial class RadarConfigurationsForm : Form
    {

        public RadarConfigurationsForm(float maskedDistance, int range)
        {
            InitializeComponent();

            this.maskedDistanceTextBox.Text = maskedDistance.ToString();
            this.beamRangeTextBox.Text = range.ToString();
        }

        /*
         * Save Configurations Button Click
         */ 
        private void radarConfigurationsSaveButton_Click(object sender, EventArgs e)
        {
            string maskedDistanceString = maskedDistanceTextBox.Text;
            string beamRangeString = beamRangeTextBox.Text;
            float maskedDistance;
            int beamRange;

            if (maskedDistanceString.Equals(""))
            {
                saveConfErrorLabel.Text = "masked distance cannot be empty";
                return;
            }

            if (!float.TryParse(maskedDistanceString, out maskedDistance))
            {
                saveConfErrorLabel.Text = "masked distance should be of type float";
                return;
            }

            if (beamRangeString.Equals(""))
            {
                saveConfErrorLabel.Text = "beam range cannot be empty";
                return;
            }

            if (!int.TryParse(beamRangeString, out beamRange) || beamRange < 1 || beamRange > 180)
            {
                saveConfErrorLabel.Text = "beam range should be integer in range [1 , 180]";
                return;
            }

            // Send Data to main form 
            EventHub.OnSaveRadarConfigurations(this , maskedDistance, beamRange);
            this.Close();
        }


        /*
        * Listen to EnterKey to send data
        */
        private void saveConfButton_KeyDown(Object sender, KeyEventArgs keyEvent)
        {
            if (keyEvent.KeyCode == Keys.Enter)
            {
                radarConfigurationsSaveButton_Click(sender, new EventArgs());
            }
        }
    }
}
