using System;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Xamarin.Forms;

namespace Stations.Droid.Helpers
{
    public class LocationHelper : Java.Lang.Object, ILocationListener
    {
        LocationManager locationManager;
        string provider = LocationManager.GpsProvider;

        public LocationHelper()
        {
            this.locationManager = (LocationManager) Forms.Context
                .GetSystemService(Java.Lang.Class.FromType(typeof(LocationManager)));
            
        }

        public IntPtr Handle => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void OnLocationChanged(Location location)
        {
            throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }

        public void StartUpdatingLocations()
        {
            locationManager.RequestLocationUpdates(provider, 2000, 1, this);

        }
    }
}
