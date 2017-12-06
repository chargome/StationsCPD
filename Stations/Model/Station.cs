using System;
using System.Collections.Generic;
using System.ComponentModel;
using SQLite;
using Xamarin.Forms;

namespace Stations.Model
{
    [Table("Station")]
    public class Station
    {
        public Station(int id, String name, double lat, double lon, String lines)
        {
            this.Id = id;
            this.Name = name;
            this.latitude = lat;
            this.longitude = lon;
            this.Lines = lines;
        }

        public Station(){}

        [PrimaryKey, AutoIncrement, Column("_id")]
        public int Id { get; set; }
        [Column("name")]
        public String Name { get; set; }
        [Column("latitude")]
        public double latitude { get; set; }
        [Column("longitude")]
        public double longitude { get; set; }

        [Column("lines")]
        public String Lines { get; set; }


        override 
        public String ToString()
        {
            return "id: " + Id +
                ", \nName: " + Name + 
                ", \nCoordinates: " + latitude + " " + longitude +
                ", \nLines: " + Lines;    
        }

    }
}
