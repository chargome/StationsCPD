using System;
using System.Collections.Generic;
using System.ComponentModel;
using Stations.Model;
using Stations.Service;
using Stations.Viewmodel;
using Xamarin.Forms;



namespace Stations
{
    public class StationDetailViewModel : BaseViewModel
    {
        public ILocationService locationService = DependencyService.Get<ILocationService>();

        public StationDetailViewModel(StationViewModel station)
        {
            this.Model = station;
        }

        StationViewModel Model { get; set; }

        public String Name
        {
            get { return Model.Name; }
        }

        public int Id
        {
            get { return Model.Id; }
        }

        public String Latitude
        {
            get 
            { 
                return Math.Round(Model.latitude, 2).ToString(); 
            }
        }

        public String Longitude
        {
            get 
            {
                return Math.Round(Model.longitude, 2).ToString(); 
            }
        }

        public String Lines
        {
            get
            {
                return Model.Lines.Replace(", ", "\n");
            }   
        }

        public String Distance
        {
            get
            {
                return Model.Distance.ToString() + " km";   
            }
        }

        public String ImageSource
        {
            get
            {
                return Model.ImageSource;    
            }
        }



		

    }
}
