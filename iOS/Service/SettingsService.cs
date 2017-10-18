using System;

using Xamarin.Forms;
using Stations.iOS;
using System.ComponentModel;
using Foundation;
using Stations.Settings;

[assembly: Dependency(typeof(SettingsService))]
namespace Stations.iOS
{
	public class SettingsService : ISettingsService
	{
		NSObject observer;

		public SettingsService()
		{
			observer = NSNotificationCenter.DefaultCenter.
										   AddObserver(NSUserDefaults.DidChangeNotification, UserDefaultsChanged, null);
		}

		~SettingsService()
		{
			NSNotificationCenter.DefaultCenter.RemoveObserver(observer);
		}

		public bool SettingsDisplayDialogAvailable
		{ get { return false; } }

		public event PropertyChangedEventHandler PropertyChanged;

		public void DisplaySettings()
		{
			throw new NotImplementedException();
		}

		private void UserDefaultsChanged(NSNotification n)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));
		}
	}
}
