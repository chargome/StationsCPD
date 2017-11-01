using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;

namespace Stations.Model
{
    public class Station : INotifyPropertyChanged
    {
        public Station(int id, String name, Coordinate cdn, String lines)
        {
            this.Id = id;
            this.Name = name;
            this.Coordinate = cdn;
            this.Lines = lines;
        }

        public Station(){}

        public int Id { get; set; }
        public String Name { get; set; }
        public Coordinate Coordinate { get; set; }
        public String Lines { get; set; }
        //public List<Line> Lines { get; set; }

        // todo: create viewmodel for station
        double _distance;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public double Distance { 
        
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


        override 
        public String ToString()
        {
            return "id: " + Id +
                ", \nName: " + Name + 
                ", \nCoordinates: " + Coordinate.Latitude + " " + Coordinate.Longitude +
                ", \nLines: " + Lines;    
        }

    }
}
