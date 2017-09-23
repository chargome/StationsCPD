using System;

namespace Stations.Model
{
    public class Coordinate
    {
        public Coordinate(long lat, long lon)
        {
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public long Latitude { get; set;}
        public long Longitude { get; set;}

    }
}
