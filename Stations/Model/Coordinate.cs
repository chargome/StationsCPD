using System;

namespace Stations.Model
{
    public class Coordinate
    {
        public Coordinate(long lat, long lon)
        {
            this.latitude = lat;
            this.longitude = lon;
        }

        private long latitude { get; set;}
        private long longitude { get; set;}

    }
}
