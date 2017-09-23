using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Stations.Model;

namespace Stations.Service
{
    public class MockStationsDataService: IDatasource
    {
        public List<Station> GetStations()
        {
            var u1 = new Line(1, "U1");
            var u2 = new Line(1, "U2");
            var u3 = new Line(1, "U3");
            var u4 = new Line(1, "U4");
            var u5 = new Line(1, "U5");
            var u6 = new Line(1, "U6");

            var stations = new List<Station>
            {
                new Station(1, "Station A", new Coordinate(123,123), new List<Line>{u1, u3}),
                new Station(2, "Station B", new Coordinate(312,223), new List<Line>{u2, u3}),
                new Station(3, "Station C", new Coordinate(11223,133), new List<Line>{u4, u3, u5}),
                new Station(4, "Station D", new Coordinate(1232,133), new List<Line>{u4, u2, u6}),
                new Station(5, "Station E", new Coordinate(123234,12133), new List<Line>{u1, u2, u4})
			};

            return stations;
        }
    }
}
