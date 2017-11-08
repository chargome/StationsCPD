using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Stations.View
{
    public partial class StationDetailPage : ContentPage
    {
        StationDetailViewModel viewModel;

        public StationDetailPage(StationDetailViewModel model)
        {
            InitializeComponent();
            BindingContext = viewModel = model;

            // Set map frame to the location of the selected station
            var position = new Position(Convert.ToDouble(viewModel.Latitude), 
                                        Convert.ToDouble(viewModel.Longitude));

            DetailMap.IsShowingUser = true;
            DetailMap.MoveToRegion(
                MapSpan
                .FromCenterAndRadius(position, Distance.FromMeters(500)));
            

            var pin = new Pin
            {
                Label = viewModel.Name,
                Position = position,
                Type = PinType.Place
            };

            DetailMap.Pins.Add(pin);

        }

        public StationDetailPage()
        {
            InitializeComponent();
        }
    }
}
