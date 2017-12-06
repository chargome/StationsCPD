using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stations.Model;
using Stations.Persistence;
using Stations.Settings;
using Stations.Viewmodel;

namespace Stations.Service
{
    public class StationService
    {
        public StationService()
        {
        }

        public async Task<ObservableCollection<StationViewModel>> GetAllStationsAsync()
        {
            var items = await StationDatabase.GetInstance.GetItemsAsync();
            ObservableCollection<Station> allStations = new ObservableCollection<Station>(items);
            ObservableCollection<StationViewModel> stationList = new ObservableCollection<StationViewModel>();

            char[] criteria = new char[2];

            // Filter from settings
            if (SystemSettings.MetroVisible)
            {
                criteria[0] = 'U';
            }

            if (SystemSettings.STrainVisible)
            {
                criteria[1] = 'S';
            }

            // loop filters merged stations for U and S lines
            foreach (Station station in allStations)
            {
                bool final = false;
                String imageSource = String.Empty;

                // check which icon is used
                if (station.Lines.IndexOf(criteria[0]) >= 0 &&
                   station.Lines.IndexOf(criteria[1]) >= 0)
                {
                    final = true;
                    imageSource = "merged.png";
                }
                else if (station.Lines.IndexOf(criteria[0]) >= 0)
                {
                    final = true;
                    imageSource = "metro.png";
                }
                else if (station.Lines.IndexOf(criteria[1]) >= 0)
                {
                    final = true;
                    imageSource = "strain.png";
                }

                if (final)
                {
                    StationViewModel viewModel = new StationViewModel(station);
                    viewModel.ImageSource = imageSource;
                    stationList.Add(viewModel);
                }
            }

            return stationList;


        }
    }
}
