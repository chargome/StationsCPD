using System;
using System.Collections.Generic;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using Stations.Droid.Helpers;
using Stations.Model;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Stations.Droid.Helpers
{
    public class CustomMapRenderer : MapRenderer, GoogleMap.IInfoWindowAdapter
    {

        List<CustomPin> customPins;

        public CustomMapRenderer()
        {
        }


        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                //NativeMap.InfoWindowClick -= OnInfoWindowClick;
            }

            if (e.NewElement != null)
            {
                var formsMap = (CustomMap)e.NewElement;
                customPins = formsMap.CustomPins;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnMapReady(GoogleMap map)
        {
            base.OnMapReady(map);

            //NativeMap.InfoWindowClick += OnInfoWindowClick;
            //NativeMap.SetInfoWindowAdapter(this);
        }


        protected override MarkerOptions CreateMarker(Pin pin)
        {
            var customPin = GetCustomPin(pin);

            int source;

            switch(customPin.ImageSource)
            {
                case "mergedpin.png":
                    source = Resource.Drawable.mergedpin;
                    break;
                case "metropin.png":
                    source = Resource.Drawable.metropin;
                    break;
                case "strainpin.png":
                    source = Resource.Drawable.strainpin;
                    break;
                default:
                    source = 0;
                    break;
            }

            var marker = new MarkerOptions();

            if(source == 0)
            {
                marker.SetIcon(BitmapDescriptorFactory.DefaultMarker());
            }
            else
            {
                marker.SetIcon(BitmapDescriptorFactory.FromResource(source));
            }

            marker.SetPosition(new LatLng(pin.Position.Latitude, pin.Position.Longitude));
            marker.SetTitle(pin.Label);
            marker.SetSnippet(pin.Address);


            return marker;
        }


        CustomPin GetCustomPin(Pin annotation)
        {
            var position = new Position(annotation.Position.Latitude, annotation.Position.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }





        public Android.Views.View GetInfoContents(Marker marker)
        {
            throw new NotImplementedException();
        }

        public Android.Views.View GetInfoWindow(Marker marker)
        {
            throw new NotImplementedException();
        }
    }
}
