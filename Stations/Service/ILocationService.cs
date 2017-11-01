using System;
using System.ComponentModel;
using Stations.Model;

namespace Stations.Service
{
    public interface ILocationService
    {
        Coordinate DeviceLocation
        {
            get;
            set;
        }

        Coordinate GetMockLocation();
        void ActivateLocationManager();
        void DeactivateLocationManager();

    }
}
