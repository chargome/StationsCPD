using System;
using System.Collections.Generic;
using System.Drawing;

using Stations.Model;
using Stations.iOS;

using MapKit;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Stations.iOS.Helpers;
using Foundation;
using CoreLocation;
using ObjCRuntime;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace Stations.iOS.Helpers
{
    public class CustomMapRenderer : MapRenderer
    {
        List<CustomPin> customPins { get; set; }

        public CustomMapRenderer()
        {
        }

        protected override void OnElementChanged(Xamarin.Forms.Platform.iOS.ElementChangedEventArgs<Xamarin.Forms.View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = null;
                nativeMap.DidSelectAnnotationView -= OnDidSelectAnnotationViewAsync;
                nativeMap.OverlayRenderer -= GetOverlayRenderer;
            }

            if (e.NewElement != null)
            {
                var formsMap = e.NewElement as CustomMap;
                var nativeMap = Control as MKMapView;
                var TapRecognizer = new UITapGestureRecognizer(OnMapTapped);

                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
                nativeMap.DidSelectAnnotationView += OnDidSelectAnnotationViewAsync;
                nativeMap.OverlayRenderer += GetOverlayRenderer;
                nativeMap.AddGestureRecognizer(TapRecognizer);

                customPins = formsMap.CustomPins;
            }
        }



        private MKAnnotationView GetViewForAnnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;

            if (annotation is MKUserLocation)
                return null;

            var customPin = GetCustomPin(annotation);
            if (customPin == null)
                return null;

            annotationView = mapView.DequeueReusableAnnotation(customPin.Pin.Label);
            if (annotationView == null)
            {
                annotationView = new MKAnnotationView(annotation, customPin.Pin.Label);
                annotationView.Image = UIImage.FromBundle(customPin.ImageSource);
            }

            return annotationView;
        }

        private CustomPin GetCustomPin(IMKAnnotation annotation)
        {
            var position = new Position(annotation.Coordinate.Latitude, annotation.Coordinate.Longitude);
            foreach (var pin in customPins)
            {
                if (pin.Pin.Position == position)
                {
                    return pin;
                }
            }
            return null;
        }

        private async void OnDidSelectAnnotationViewAsync(object sender, MKAnnotationViewEventArgs e)
        {
            var nativeMap = Control as MKMapView;

            // Get user's location
            var userLocation = nativeMap.UserLocation.Location.Coordinate;

            //System.Diagnostics.Debug.WriteLine("Pin clicked");
            var start = new Position(userLocation.Latitude, userLocation.Longitude);
            var end = e.View.Annotation.Coordinate;

            var emptyDict = new NSDictionary();
            var request = new MKDirectionsRequest()
            {
                Source = new MKMapItem(new MKPlacemark(
                    new CLLocationCoordinate2D(start.Latitude, start.Longitude), emptyDict)),
                Destination = new MKMapItem(new MKPlacemark(
                    new CLLocationCoordinate2D(end.Latitude, end.Longitude), emptyDict)),
                TransportType = MKDirectionsTransportType.Walking
            };

            var directions = new MKDirections(request);
            var response = await directions.CalculateDirectionsAsync();

            var r = response.Routes[0];
            var coords = r.Polyline.GetCoordinates(0, (int)r.Polyline.PointCount);
            foreach (var point in coords)
            {
                System.Diagnostics.Debug.WriteLine("" + point.Latitude + "/" + point.Longitude);
            }


            nativeMap.DeselectAnnotation(e.View.Annotation, false);

            if (nativeMap.Overlays != null)
                nativeMap.RemoveOverlays(nativeMap.Overlays);

            MKPolyline polyline = response.Routes[0].Polyline;
            nativeMap.AddOverlay(polyline);
        }

        private MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
            var polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
            {
                FillColor = UIColor.Blue,
                StrokeColor = UIColor.Blue,
                LineWidth = 5,
                Alpha = 1f
            };
            return polylineRenderer;
        }

        private void OnMapTapped()
        {
            var nativeMap = Control as MKMapView;
            // Remove all routes displayed
            if (nativeMap.Overlays != null)
            {
                nativeMap.RemoveOverlays(nativeMap.Overlays);
            }
        }


    }
}
