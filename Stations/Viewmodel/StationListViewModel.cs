using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Stations.Helper;
using Stations.Model;
using Stations.Persistence;
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

        public Command LoadListCommand { get; set; }

        ObservableCollection<StationViewModel> _stationList;
		public ObservableCollection<StationViewModel> StationList
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


        Command _updateCommand;
        public Command UpdateCommand
        {
            get
            {
                return _updateCommand;
            }
            set
            {
                _updateCommand = value;
            }
        }


        // constructor
        public StationListViewModel()
        {
            Title = "Stations";

            _stationList = new ObservableCollection<StationViewModel>();
            _refreshCommand = new Command(async () => await Task.Run(() => ExecuteRefreshCommand()));
            LoadListCommand = new Command(async () => await LoadDataAsync());
            UpdateCommand = new Command(async () => await ExecuteUpdateCommand());

            // receive location upates
            MessagingCenter.Subscribe<ILocationService, Coordinate>(this, "deviceLocation", (sender, coordinate) => 
            {
                //System.Diagnostics.Debug.WriteLine("Received location: " 
                // + coordinate.Latitude + coordinate.Longitude);
                Location = coordinate;
            });

            // receive update trigger
            MessagingCenter.Subscribe<IUpdateService>(this, "update", async (sender) =>
            {
                await ExecuteUpdateCommand();
            });

            // receive DB upates
            MessagingCenter.Subscribe<JSONDataService>(this, "updated", async (sender) =>
            {
                await ExecuteRefreshCommand();
            });
        }

        private async Task LoadDataAsync()
        {
            var stations = await StationDatabase.GetInstance.GetItemsAsync();
            //StationList = new ObservableCollection<StationViewModel>(stations);
        }

        async Task ExecuteUpdateCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            // execute 
            await Datasource.UpdateStationsFromApiAsync();

            IsBusy = false;
        }


        async Task ExecuteRefreshCommand()
		{
			if (IsBusy)
				return;

			IsBusy = true;

            // execute 
            StationList = await stationService.GetAllStationsAsync();

			IsBusy = false;
		}

        private void CalculateDistancesInModel()
        {
            //System.Diagnostics.Debug.WriteLine("Calculating distances");

            foreach (StationViewModel station in StationList)
            {
                station.Distance = DistanceCalculator.DistanceBetweenPoints(
                    Location.Longitude, Location.Latitude,
                    station.longitude,
                    station.latitude);

            }

            OnPropertyChanged(nameof(StationList));

            // Sort by distance
            StationList = new ObservableCollection<StationViewModel>(StationList.OrderBy(o => o.Distance).ToList());
        }




    }
}
