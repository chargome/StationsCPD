using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stations.Helper;
using Stations.Model;
using Stations.Persistence;
using Stations.Service;
using Stations.Settings;
using Stations.Viewmodel;
using Xamarin.Forms;

namespace Stations
{
    public class JsonParser
    {
        public JsonParser()
        {
        }


        // Takes JSON string as argument and converts it to .NET object
        public ObservableCollection<StationViewModel> DeserializeStations(String strJSON)
        {
            ObservableCollection<StationViewModel> allStations = new ObservableCollection<StationViewModel>();
            ObservableCollection<StationViewModel> finalStations = new ObservableCollection<StationViewModel>();

            var testObject = JsonConvert.DeserializeObject<dynamic>(strJSON);

            // first loop filters for redundant stations
            foreach (var f in testObject.features)
            {
                bool isRedundant = false;

                // cache JSON data
                int id = f.properties.OBJECTID;
                string name = f.properties.HTXT.ToString();
                string lines = f.properties.HLINIEN.ToString();
                double lat = f.geometry.coordinates[1];
                double lon = f.geometry.coordinates[0];

                foreach (var station in allStations)
                {   
                    if(name.Equals(station.Name))
					{
						isRedundant = true;

                        if(!station.Lines.Contains(lines))
                        {
                            station.Lines += ", " + lines;    
                        }
					}
				}

                //create new station if does not exist yet
                if(!isRedundant)
                {
					Station model = new Station
                        (id, name,lat, lon, lines);

                    StationViewModel viewModel = new StationViewModel(model);
					allStations.Add(viewModel);        
                }
            }


            char[] criteria = new char[2];
                

            // Filter from settings
            if(SystemSettings.MetroVisible)
            {
                criteria[0] = 'U';
            }

            if(SystemSettings.STrainVisible)
            {
                criteria[1] = 'S';    
            }

            //second loop filters merged stations for U and S lines
            foreach (var station in allStations)
            {
                bool final = false;

                // check which icon is used
                if(station.Lines.IndexOf(criteria[0]) >= 0 &&
                   station.Lines.IndexOf(criteria[1]) >= 0)
                {
                    final = true;
                    station.ImageSource = "merged.png";
                }
                else if(station.Lines.IndexOf(criteria[0]) >= 0)
                {
                    final = true;
                    station.ImageSource = "metro.png";
                }
                else if (station.Lines.IndexOf(criteria[1]) >= 0)
                {
                    final = true;
                    station.ImageSource = "strain.png";
                }

                if (final)
                {
                    finalStations.Add(station);
                }
            }

			return finalStations;

        }

        public async Task<bool> UpdateDatabaseAsync(string jsonResponse)
        {
            var items = await StationDatabase.GetInstance.GetItemsAsync();
            ObservableCollection<Station> stationsfromDb = new ObservableCollection<Station>(items);
            ObservableCollection<Station> allStations = new ObservableCollection<Station>();
            var stationsFromApi = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
            bool dbGotUpdate = false;


            // parse all stations from api call and check for redundancy
            foreach (var station in stationsFromApi.features)
            {
                bool isRedundant = false;

                // cache JSON data
                int id = station.properties.OBJECTID;
                string name = station.properties.HTXT.ToString();
                string lines = station.properties.HLINIEN.ToString();
                double lat = station.geometry.coordinates[1];
                double lon = station.geometry.coordinates[0];

                foreach (var allStation in allStations)
                {
                    if (name.Equals(allStation.Name))
                    {
                        isRedundant = true;

                        if (!allStation.Lines.Contains(lines))
                        {
                            allStation.Lines += ", " + lines;
                        }
                    }
                }

                //create new station if does not exist yet
                if (!isRedundant)
                {

                    //var old = lines.Split(',').
                    Station model = new Station
                        (id, name, lat, lon, lines);
                    
                    allStations.Add(model);
                }
            }


            // check for every station if needs to be inserted or updated 
            foreach(Station newStation in allStations)
            {
                bool isInDb = false;

                foreach (Station oldStation in stationsfromDb)
                {
                    if (oldStation.Id == newStation.Id)
                    {
                        isInDb = true;

                        if (oldStation != newStation)
                        {
                            await StationDatabase.GetInstance.UpdateItemAsync(newStation);
                            dbGotUpdate = true;
                        }
                    }
                }

                if (!isInDb)
                {
                    await StationDatabase.GetInstance.SaveItemAsync(newStation);
                    dbGotUpdate = true;
                }
            }


            // check for items that need to be deleted
            foreach (Station station in stationsfromDb)
            {
                if (!allStations.Contains(station))
                {
                    await StationDatabase.GetInstance.DeleteItem(station);
                    dbGotUpdate = true;
                }
            }

            return dbGotUpdate;
        }
    }
}
