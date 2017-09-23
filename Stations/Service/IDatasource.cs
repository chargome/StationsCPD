using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stations.Model;

namespace Stations
{
    public interface IDatasource
    {
       List<Station> GetStations();
    }
}
