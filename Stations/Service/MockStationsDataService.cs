using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stations.Model;
using Stations.Viewmodel;

namespace Stations.Service
{
    public class MockStationsDataService: IDatasource<StationViewModel>
    {
        public MockStationsDataService(){}

        public Task<ObservableCollection<StationViewModel>> GetStationsAsync()
        {
     /*       var u1 = new Line(1, "U1");
            var u2 = new Line(1, "U2");
            var u3 = new Line(1, "U3");
            var u4 = new Line(1, "U4");
            var u5 = new Line(1, "U5");
            var u6 = new Line(1, "U6");
     */
     
            //Create some random stations
            var stations = new ObservableCollection<StationViewModel>
            {
                new StationViewModel(new Station(1, "Station A", 48.239, 16.3775, "U1, 13A")),
                new StationViewModel(new Station(2, "Station B", 48.240, 16.3785, "U1, 13A")),
                new StationViewModel(new Station(3, "Station C", 48.241, 16.3779, "U1, 13A")),
                new StationViewModel(new Station(4, "Station D", 48.242, 16.3781, "U1, 13A")),
                new StationViewModel(new Station(5, "Station E", 48.243, 16.3785, "U1, 13A"))
			};

            return Task.FromResult(stations);
        }

        public Task UpdateStationsFromApiAsync()
        {
            throw new NotImplementedException();
        }
    }
}
