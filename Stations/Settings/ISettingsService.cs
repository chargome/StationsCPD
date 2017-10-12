using System;
using System.ComponentModel;

namespace Stations.Settings
{
    public interface ISettingsService :INotifyPropertyChanged
    {
        bool SettingsDisplayDialogAvailable { get; }
        void DisplaySettings();
    }
}
