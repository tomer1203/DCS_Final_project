using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Source_files
{
    public static class EventHub
    {

        //public class ConfigurationschangeEventArgs : EventArgs
        //{
        //    private readonly float _maskedDistance;
        //    private readonly int _beamRange;

        //    public 
        //}

        /////////////////////////////////
        // Save Serial Configurations
        ////////////////////////////////
        public static event EventHandler<EventArgs> SaveSerialConfigurationsHandler;

        public static void OnSaveSerialConfigurations(object sender, EventArgs e)
        {
            SaveSerialConfigurationsHandler?.Invoke(sender, e);
        }

        /////////////////////////////////
        // Save File Configurations
        ////////////////////////////////
        public static event EventHandler<EventArgs> SaveFileConfigurationsHandler;

        public static void OnSaveFileConfigurations(object sender, EventArgs e)
        {
            SaveFileConfigurationsHandler?.Invoke(sender, e);
        }

        /////////////////////////////////
        // Save Radar Configurations
        ////////////////////////////////

        public static event EventHandler<(float maskedDistance, int beamRange)> SaveRadarConfigurationsHandler;

        public static void OnSaveRadarConfigurations(object sender, float maskedDistance, int beamRange)
        {
            SaveRadarConfigurationsHandler?.Invoke(sender, (maskedDistance, beamRange));
        }

        ////////////////////////////////////
        // File Sending Progress Handler
        ///////////////////////////////////
        public static event EventHandler<EventArgs> FileSendingProgressHandler;

        public static void OnFileSendingProgress(object sender, EventArgs e)
        {
            FileSendingProgressHandler?.Invoke(sender, e);
        }

    }
}
