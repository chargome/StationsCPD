using System;
using System.Collections.Generic;
using Xamarin.Forms.Maps;

namespace Stations.Model
{
    public class CustomMap : Map
    {
        public List<CustomPin> CustomPins;

        public CustomMap()
        {
            CustomPins = new List<CustomPin>();

        }
    }

    public class CustomPin
    {
        public Pin Pin { get; set; }
        public string ImageSource { get; set; }
    }
}
