using System;
using System.Collections.Generic;
using Stations.Model;
using Stations.Viewmodel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Stations.View
{
    public partial class StationMapPage : ContentPage
    {
        StationListViewModel viewModel;

        public StationMapPage(StationListViewModel stationViewmodel)
        {
            InitializeComponent();
            viewModel = stationViewmodel;
            BindingContext = viewModel;

            moveMapToCurrentPosition();

            Map.HasZoomEnabled = true;
            Map.HasScrollEnabled = true;
            Map.IsShowingUser = true;


            ToolbarItems.Add(new ToolbarItem("Refresh", "refresh.png", () =>
            {
                Map.Pins.Clear();
                addPinsFromList();
                moveMapToCurrentPosition();
            }));
        }

        private void moveMapToCurrentPosition()
        {
            var position = new Position();

            if(viewModel.Location == null)
            {
                position = new Position(48.239, 16.3775);
            }
            else
            {
                position = new Position(viewModel.Location.Latitude,
                                        viewModel.Location.Longitude);
            }

            Map.MoveToRegion(MapSpan.FromCenterAndRadius
                             (position, Distance.FromMeters(1000)));

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //System.Diagnostics.Debug.WriteLine("Mappage appearing..");
            Map.Pins.Clear();

            if(viewModel.StationList.Count == 0)
            {
                viewModel.RefreshCommand.Execute(null);    
            }
            addPinsFromList();
            moveMapToCurrentPosition();
        }




        private void addPinsFromList()
        {
            foreach(Station station in viewModel.StationList)
            {
                var position = new Position(station.Coordinate.Latitude, 
                                            station.Coordinate.Longitude);

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Label = station.Name,
                    Position = position
                };


                Map.Pins.Add(pin);
            }
        }
    }
}
