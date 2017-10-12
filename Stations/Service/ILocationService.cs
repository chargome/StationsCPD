using System;
using Stations.Model;

namespace Stations.Service
{
    public interface ILocationService
    {
        Coordinate GetDeviceLocation();
        Coordinate GetMockLocation();
    }
}
