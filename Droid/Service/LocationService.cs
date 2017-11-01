using System;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Stations.Droid.Helpers;

using Stations.Droid.Service;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;


[assembly: Dependency(typeof(LocationService))]
namespace Stations.Droid.Service
{

    public class LocationService : Java.Lang.Object, ILocationService, ILocationListener
    {

        LocationManager locationManager;
        string provider;

        public LocationService()
        {
            locationManager = (LocationManager)Forms.Context
                                 .GetSystemService(Java.Lang.Class
                                                   .FromType(typeof(LocationManager)));

            provider = LocationManager.GpsProvider;

            // Location initialLoc = locationManager.GetLastKnownLocation(provider);
            // System.Diagnostics.Debug.WriteLine("InitialLocation: " + initialLoc.Latitude + initialLoc.Longitude);

            // Set initial location to random coordinate (VIC)
            DeviceLocation = new Coordinate(48.231906, 16.415843);
            
        }

        Coordinate _deviceLocation;
        public Coordinate DeviceLocation 
        {
            get
            {
                return _deviceLocation;
            }

            set
            {
                _deviceLocation = value;
                // Push new Location into Messaging center
                MessagingCenter.Send<ILocationService, Coordinate>(this, "deviceLocation", DeviceLocation);
                // System.Diagnostics.Debug.WriteLine("DeviceLocation sent!");
            }
        }

        public Coordinate GetMockLocation()
        {
            // VIC
            return new Coordinate(48.231906, 16.415843);
        }

        public void ActivateLocationManager()
        {
            locationManager.RequestLocationUpdates(LocationManager.GpsProvider,2000,1,this);
        }

        public void DeactivateLocationManager()
        {
            locationManager.RemoveUpdates(this);
        }

        public Coordinate GetDeviceLocation()
        {
            throw new NotImplementedException();
        }


        public void OnLocationChanged(Location location)
        {
            System.Diagnostics.Debug.WriteLine("Location changed to: " + location.Latitude + location.Longitude);
            DeviceLocation = new Coordinate(location.Latitude, location.Longitude);
        }

        public void OnProviderDisabled(string provider)
        {
            System.Diagnostics.Debug.WriteLine("Provider disabled " + provider);
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            System.Diagnostics.Debug.WriteLine(provider + " availability has changed to " + status.ToString());
        }

    }



}
