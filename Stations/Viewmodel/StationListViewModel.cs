using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Stations.Helper;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.Viewmodel
{
    public class StationListViewModel : BaseViewModel
    {
        Coordinate _location;
        public Coordinate Location
        {
            get
            {
                return _location;
            }

            set
            {
                _location = value;
                CalculateDistancesInModel();
            }
        }

        ObservableCollection<Station> _stationList;
		public ObservableCollection<Station> StationList
		{
			get
			{
				return _stationList;
			}
			set
			{
				_stationList = value;
                OnPropertyChanged(nameof(StationList));
			}
		}


		Command _refreshCommand;
		public Command RefreshCommand
		{
			get
			{
				return _refreshCommand;
			}
			set
			{
				_refreshCommand = value;
			}
		}


        // constructor
        public StationListViewModel()
        {
            Title = "Stations";

            _stationList = new ObservableCollection<Station>();
            _refreshCommand = new Command(async () => await Task.Run(() => ExecuteRefreshCommand()));

            // receive location upates
            MessagingCenter.Subscribe<ILocationService, Coordinate>(this, "deviceLocation", (sender, coordinate) => 
            {
                //System.Diagnostics.Debug.WriteLine("Received location: " 
                // + coordinate.Latitude + coordinate.Longitude);
                Location = coordinate;
            });
        }



        // other methods
        async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

			// execute
			StationList = await Datasource.GetStationsAsync();

			IsBusy = false;
		}

        private void CalculateDistancesInModel()
        {
            //System.Diagnostics.Debug.WriteLine("Calculating distances");

            foreach (Station station in StationList)
            {
                station.Distance = DistanceCalculator.DistanceBetweenPoints(
                    Location.Longitude, Location.Latitude,
                    station.Coordinate.Longitude,
                    station.Coordinate.Latitude);

            }

            OnPropertyChanged(nameof(StationList));

            // Sort by distance
            StationList = new ObservableCollection<Station>(StationList.OrderBy(o => o.Distance).ToList());
        }




    }
}
