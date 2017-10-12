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

        public StationDetailViewModel(Station station)
        {
            this.Model = station;
        }

        Station Model { get; set; }

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
            get { return Model.Coordinate.Latitude.ToString(); }
        }

        public String Longitude
        {
            get { return Model.Coordinate.Longitude.ToString(); }
        }

        public String Lines
        {
            get
            {
                return Model.Lines;
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
