using System;
namespace Stations.Model.Route
{
    public class Leg
    {
        public string end_address { get; set; }
        public Step[] steps { get; set; }
    }
}
