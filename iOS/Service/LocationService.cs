using System;
using Stations.iOS.Service;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocationService))]
namespace Stations.iOS.Service
{
    public class LocationService : ILocationService
    {
        public LocationService()
        {
            
        }

        public Coordinate GetDeviceLocation()
        {
            throw new NotImplementedException();
        }

        public Coordinate GetMockLocation()
        {
            // VIC
            return new Coordinate(48.231906, 16.415843);
        }
    }
}
