using System;
using Stations.Model;

namespace Stations.Viewmodel
{
    
    public class StationViewModel : BaseViewModel
    {
        private Station model;
        public StationViewModel(Station station)
        {
            this.model = station;
        }

        public int Id 
        {
            get
            {
                return model.Id;
            }

            set
            {
                model.Id = value;
            }
        }

        public String Name 
        { 
            get
            {
                return model.Name;        
            }

            set
            {
                model.Name = value;
            }
        }

       

        public double latitude
        {
            get
            {
                return model.latitude; 
            }

            set
            {
                model.latitude = value;
            }
        }

        public double longitude
        {
            get
            {
                return model.longitude;
            }

            set
            {
                model.longitude = value;
            }
        }

        public String Lines 
        { 
            get
            {
                return model.Lines;        
            }

            set
            {
                model.Lines = value;
            }
        }

        double _distance;
        public double Distance
        {
            get
            {
                return _distance;    
            }

            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }

        public String ImageSource { get; set; }


    }
}