using System;

namespace Stations.Model
{
    public class Coordinate
    {
        public Coordinate(double lat, double lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public double Latitude { get; set;}
        public double Longitude { get; set;}

    }
}
