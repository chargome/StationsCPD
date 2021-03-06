﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Stations.Model;
using Stations.Viewmodel;
using Xamarin.Forms;

namespace Stations.Service
{
    public class JSONDataService : IDatasource<StationViewModel>
    {
		//HttpClient client;
        JsonParser parser;
  

        public JSONDataService()
		{
          //  client = new HttpClient();
            parser = new JsonParser();
		}

        /*
        public Task<ObservableCollection<StationViewModel>> GetStationsAsync()
        {
			// API call
			WebRequest request = WebRequest.Create("https://data.wien.gv.at/daten/geo?service=WFS&request=GetFeature&version=1.1.0&typeName=ogdwien:OEFFHALTESTOGD&srsName=EPSG:4326&outputFormat=json");
            WebResponse response = request.GetResponseAsync().Result;

            Stream stream = response.GetResponseStream();

			// get string for parsing
			string jsonResponse = new StreamReader(stream).ReadToEnd();

            // send to parser and return value
            return Task.FromResult(parser.DeserializeStations(jsonResponse));
			
        }
        */

        public async Task UpdateStationsFromApiAsync()
        {
            // API call
            WebRequest request = WebRequest.Create("https://data.wien.gv.at/daten/geo?service=WFS&request=GetFeature&version=1.1.0&typeName=ogdwien:OEFFHALTESTOGD&srsName=EPSG:4326&outputFormat=json");
            WebResponse response = request.GetResponseAsync().Result;

            Stream stream = response.GetResponseStream();

            // get string for parsing
            string jsonResponse = new StreamReader(stream).ReadToEnd();

            bool update = await parser.UpdateDatabaseAsync(jsonResponse);

            if(update)
            {
                MessagingCenter.Send<JSONDataService>
                        (this, "updated");
            }



        }
    }
}
