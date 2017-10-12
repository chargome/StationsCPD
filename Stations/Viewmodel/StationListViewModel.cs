using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.Viewmodel
{
    public class StationListViewModel : BaseViewModel
    {
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




    }
}
