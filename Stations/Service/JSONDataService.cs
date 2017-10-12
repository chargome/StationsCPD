using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Stations.Model;

namespace Stations.Service
{
    public class JSONDataService : IDatasource<Station>
    {
		//HttpClient client;
        JsonParser parser;
  

        public JSONDataService()
		{
          //  client = new HttpClient();
            parser = new JsonParser();
		}

        public Task<ObservableCollection<Station>> GetStationsAsync()
        {
			/*
			var request = new HttpRequestMessage();
			request.RequestUri = new Uri("https://data.wien.gv.at/daten/geo?service=WFS&request=GetFeature&version=1.1.0&typeName=ogdwien:OEFFHALTESTOGD&srsName=EPSG:4326&outputFormat=json");
			//request.RequestUri = new Uri("http://headers.jsontest.com/");
			request.Method = HttpMethod.Get;
			request.Headers.Add("Accept", "application/json");

            HttpResponseMessage response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                HttpContent content = response.Content;
                var json = await content.ReadAsStringAsync();
                return parser.DeserializeStations(json);
                //System.Diagnostics.Debug.WriteLine(json);
            }
            else return null;
            */


            // API call
			WebRequest request = WebRequest.Create("https://data.wien.gv.at/daten/geo?service=WFS&request=GetFeature&version=1.1.0&typeName=ogdwien:OEFFHALTESTOGD&srsName=EPSG:4326&outputFormat=json");
            WebResponse response = request.GetResponseAsync().Result;

            Stream stream = response.GetResponseStream();

			// get string for parsing
			string jsonResponse = new StreamReader(stream).ReadToEnd();

            // send to parser and return value
            return Task.FromResult(parser.DeserializeStations(jsonResponse));
			
        }

    }
}
