using System;
using System.Collections.Generic;

namespace Stations.Model
{
    public class Station
    {
        public Station(int id, String name, Coordinate cdn, List<Line> lines)
        {
            this.Id = id;
            this.Name = name;
            this.Coordinate = cdn;
            this.Lines = lines;
        }

        public int Id { get; set; }
        public String Name { get; set; }
        public Coordinate Coordinate { get; set; }
        public List<Line> Lines { get; set; }

    }
}
