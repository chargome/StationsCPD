using System;
using CoreLocation;
using Stations.Model;

namespace Stations.iOS.Helpers
{
    public class LocationDelegate : CLLocationManagerDelegate
    {
        public LocationDelegate()
        {
            // Testing purposes: VIC
            _deviceLocation = new Coordinate(48.231906, 16.415843);
        }

        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            foreach(var location in locations)
            {
                System.Diagnostics.Debug.WriteLine(location);
            }
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
            }
        }


    }
}
