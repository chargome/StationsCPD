// Settings/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Stations.Settings
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class SystemSettings
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        #endregion


        public static string GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsKey, value);
            }
        }

        // Visibility methods get called before filtering data from API call
        public static bool MetroVisible
        {
            get
            {
                return AppSettings.GetValueOrDefault("metro_visible", true);   
            }

            set
            {
                AppSettings.AddOrUpdateValue("metro_visible", value);
            }
        }

		public static bool STrainVisible
		{
			get
			{
				return AppSettings.GetValueOrDefault("strain_visible", true);
			}

			set
			{
				AppSettings.AddOrUpdateValue("strain_visible", value);
			}
		}
	 }
}