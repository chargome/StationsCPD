using System;
using System.Collections.Generic;

namespace Stations.Model
{
    public class Station
    {
        public Station(String name, Coordinate cdn, List<Line> lines)
        {
            this.name = name;
            this.coordinate = coordinate;
            this.lines = lines;
        }

        private String name { get; set; }
        private Coordinate coordinate { get; set; }
        private List<Line> lines { get; set; }

    }
}
