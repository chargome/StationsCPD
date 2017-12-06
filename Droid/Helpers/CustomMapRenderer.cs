using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.Views;
using Newtonsoft.Json;
using Stations.Droid.Helpers;
using Stations.Model;
using Stations.Model.Route;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Stations.Droid.Helpers
{
    public class CustomMapRenderer : MapRenderer
    {

        List<CustomPin> customPins;
        Polyline polyline;
        Android.Locations.Location currentLocation;

        public CustomMapRenderer()
        {
        }


        protected override void OnElementChanged(Xamarin.Forms.Platform.Android.ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);


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

            NativeMap.MapClick += OnMapClick;
            NativeMap.MarkerClick += OnMarkerClick;
            NativeMap.MyLocationChange += OnMyLocationChange;
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


        private void OnMyLocationChange(object sender, GoogleMap.MyLocationChangeEventArgs e)
        {
            currentLocation = e.Location;
        }

        private void OnMarkerClick(object sender, GoogleMap.MarkerClickEventArgs e)
        {
            if (currentLocation == null)
            {
                Console.Write("Location could not be determined");
                return;
            }

            LatLng startLocation = new LatLng(currentLocation.Latitude, 
                                              currentLocation.Longitude);

            Marker tarketMarker = e.Marker;
            LatLng targetLocation = tarketMarker.Position;

            String url = "https://maps.googleapis.com/maps/api/directions/json"
                                    + "?origin=" + startLocation.Latitude + "," + startLocation.Longitude
                                    + "&destination=" + targetLocation.Latitude + "," + targetLocation.Longitude
                                    + "&sensor=false&units=metric&mode=walking"
                                    + "&key=" + Resources.GetString(Resource.String.maps_api_key);

            // Get json data from the internet
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponseAsync().Result;
            Stream stream = response.GetResponseStream();

            // Read json string from stream
            string JsonResponse = new StreamReader(stream).ReadToEnd();

            PolyRoute PolyRoute = JsonConvert
                .DeserializeObject<PolyRoute>(JsonResponse);
            Step[] Steps = PolyRoute.routes[0].legs[0].steps;


            var RouteOptions = new PolylineOptions();

            RouteOptions.InvokeColor(Android.Graphics.Color.Blue);

            RouteOptions.Add(new LatLng(Steps[0].start_location.lat, Steps[0].start_location.lng));
            foreach (Step Step in Steps)
            {
                RouteOptions.Add(new LatLng(Step.end_location.lat, Step.end_location.lng));
            }
            if (polyline != null)
            {
                polyline.Remove();
            }
            polyline = NativeMap.AddPolyline(RouteOptions);
             
        }

        private void OnMapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
            if(polyline != null)
            {
                polyline.Remove();
            }
        }
    }
}
