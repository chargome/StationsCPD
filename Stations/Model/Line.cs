using System;
namespace Stations.Model
{
    public class Line
    {
        public Line(int id, String name)
        {
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }
        public String Name { get; set;}
    }
}
