using System;
using System.Collections.Generic;
using System.ComponentModel;
using Stations.Model;
using Stations.Service;

namespace Stations.Viewmodel
{
    public class StationListViewModel: IViewModel
    {
        public List<Station> StationList { get; set; }
        public String Title { get; set; }

        public StationListViewModel()
        {
            var stationService = new MockStationsDataService();
            StationList = stationService.GetStations();
            Title = "All Stations";
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
