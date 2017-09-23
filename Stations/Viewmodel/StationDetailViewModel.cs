using System;
using System.Collections.Generic;
using System.ComponentModel;
using Stations.Model;
using Stations.Viewmodel;

namespace Stations
{
    public class StationDetailViewModel : IViewModel
    {
        Station Model { get; set; }

        public StationDetailViewModel(Station station)
        {
            this.Model = station;
            this.Title = "Details";
        }

        public String Title { get; }

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
                String lines = String.Empty;

                foreach(var line in Model.Lines)
                {
                    lines += line.Name + " ";
                }

                return lines;
            }   
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
