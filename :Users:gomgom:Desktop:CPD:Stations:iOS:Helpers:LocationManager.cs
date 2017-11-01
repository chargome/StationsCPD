using System;
using CoreLocation;
using Stations.iOS.Service;

namespace Stations.iOS.Helpers
{
    public class LocationManager
    {
        public CLLocationManager manager;

        public LocationManager()
        {
            manager = new CLLocationManager();
            manager.RequestWhenInUseAuthorization();
            manager.Delegate = new LocationDelegate();
        }

        internal void activate()
        {
            manager.StartUpdatingLocation();
        }

        internal void deactivate()
        {
            manager.StopUpdatingLocation();
        }
    }
}
