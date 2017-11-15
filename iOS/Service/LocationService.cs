using System;
using System.ComponentModel;
using CoreLocation;
using Stations.iOS.Helpers;
using Stations.iOS.Service;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace Stations.iOS.Service
{
    public class LocationService : CLLocationManagerDelegate, ILocationService
    {
        CLLocationManager manager;

        public LocationService()
        {
            manager = new CLLocationManager();
            manager.RequestWhenInUseAuthorization();
            manager.Delegate = this;
            //deviceLocation = CLLocationToCoordinate(manager.Location);
            //System.Diagnostics.Debug.WriteLine(manager.Location.Coordinate.ToString());
        }

        public Coordinate deviceLocation;
        public Coordinate DeviceLocation
        {
            get
            {
                return deviceLocation;
            }

            set
            {
                this.deviceLocation = value;
                MessagingCenter.Send<ILocationService, Coordinate>
                        (this, "deviceLocation", DeviceLocation);
            }
        }



        public override void LocationsUpdated(CLLocationManager manager, CLLocation[] locations)
        {
            foreach(var loc in locations)
            {
                System.Diagnostics.Debug.WriteLine("Location changed to: " + loc.Coordinate.ToString());
                DeviceLocation = CLLocationToCoordinate(loc);
            }
        }

        public Coordinate GetMockLocation()
        {
            // VIC kaiserm
            return new Coordinate(48.231906, 16.415843);
        }

        public void ActivateLocationManager()
        {
            manager.StartUpdatingLocation();
            updateLocationForSeconds(1);
        }

        private void updateLocationForSeconds(int seconds)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () => 
            {
                DeviceLocation = CLLocationToCoordinate(manager.Location);
                return true;
            });

        }

        public void DeactivateLocationManager()
        {
            manager.StopUpdatingLocation();
        }

        private Coordinate CLLocationToCoordinate(CLLocation location)
        {
            return new Coordinate(location.Coordinate.Latitude,
                                  location.Coordinate.Longitude);
        }


    }
}
