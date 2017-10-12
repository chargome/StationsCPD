using System;
using System.ComponentModel;
using Android.Content;
using Android.Preferences;
using Stations.Droid;
using Stations.Settings;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsService))]
namespace Stations.Droid
{
    public class SettingsService : Java.Lang.Object, ISettingsService, ISharedPreferencesOnSharedPreferenceChangeListener
    {
        public SettingsService()
        {
			ISharedPreferences sharedPreferences =
				PreferenceManager.GetDefaultSharedPreferences(Forms.Context);
			sharedPreferences.RegisterOnSharedPreferenceChangeListener(this);
        }

		~SettingsService()
		{
			ISharedPreferences sharedPreferences =
				PreferenceManager.GetDefaultSharedPreferences(Forms.Context);
			sharedPreferences.UnregisterOnSharedPreferenceChangeListener(this);
		}

        public bool SettingsDisplayDialogAvailable
        {
            get
            {
                return true;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void DisplaySettings()
        {
            Forms.Context.StartActivity(typeof(SettingsActivity));
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(key));
        }
    }
}
