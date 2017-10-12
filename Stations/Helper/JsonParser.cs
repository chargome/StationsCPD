using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Stations.Helper;
using Stations.Model;
using Stations.Service;
using Stations.Settings;
using Xamarin.Forms;

namespace Stations
{
    public class JsonParser
    {
        public JsonParser()
        {
        }


        // Takes JSON string as argument and converts it to .NET object
        public ObservableCollection<Station> DeserializeStations(String strJSON)
        {
			ObservableCollection<Station> allStations = new ObservableCollection<Station>();
            ObservableCollection<Station> finalStations = new ObservableCollection<Station>();
            
			
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
                {   if(name.Equals(station.Name))
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
                        (id, name, new Coordinate(lat, lon), lines);
                    
					allStations.Add(model);        
                }
            }


            char[] criteria = new char[2];

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
                    //calculate distance to device distance
                    Coordinate myPosition = DependencyService
                        .Get<ILocationService>()
                        .GetMockLocation();

                    station.Distance = Math.Round(DistanceCalculator
                                   .DistanceBetweenPoints(
                                                    station.Coordinate.Longitude,
                                                    station.Coordinate.Latitude,
                                                    myPosition.Longitude,
                                                    myPosition.Latitude), 1);
                    
                    finalStations.Add(station);
                }
            }

			return finalStations;

        }
    }
}
