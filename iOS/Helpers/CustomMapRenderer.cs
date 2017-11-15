using System;
using System.Collections.Generic;

using Stations.Model;
using Stations.iOS;

using MapKit;
using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.iOS;
using Stations.iOS.Helpers;

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
            }

            if (e.NewElement != null)
            {
                var formsMap = e.NewElement as CustomMap;
                var nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation = GetViewForAnnotation;
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
    }
}
