using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stations.Model;

namespace Stations.Service
{
    public class MockStationsDataService: IDatasource<Station>
    {
        public MockStationsDataService(){}

        public Task<ObservableCollection<Station>> GetStationsAsync()
        {
     /*       var u1 = new Line(1, "U1");
            var u2 = new Line(1, "U2");
            var u3 = new Line(1, "U3");
            var u4 = new Line(1, "U4");
            var u5 = new Line(1, "U5");
            var u6 = new Line(1, "U6");
     */
     
            //Create some random stations
            var stations = new ObservableCollection<Station>
            {
                new Station(1, "Station A", new Coordinate(123,123), "U1, 13A"),
                new Station(2, "Station B", new Coordinate(312,223), "U1, 13A"),
                new Station(3, "Station C", new Coordinate(11223,133), "U1, 13A"),
                new Station(4, "Station D", new Coordinate(1232,133), "U1, 13A"),
                new Station(5, "Station E", new Coordinate(123234,12133), "U1, 13A")
			};

            return Task.FromResult(stations);
        }

    }
}
