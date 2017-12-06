using System;
using System.Collections.Generic;
using Stations.Model;
using Stations.Service;
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

            MoveMapToCurrentPosition();

            Map.HasZoomEnabled = true;
            Map.HasScrollEnabled = true;
            Map.IsShowingUser = true;


            /*ToolbarItems.Add(new ToolbarItem("Refresh", "refresh.png", () =>
            {
                Map.Pins.Clear();
                AddPinsFromList();
                MoveMapToCurrentPosition();
            }));

            /*ToolbarItems.Add(new ToolbarItem("Cancel", "cancel.png", () =>
            {
                // :(
            }));*/

            MessagingCenter.Subscribe<ILocationService, Coordinate>(this, "deviceLocation", (sender, coordinate) =>
            {
                //System.Diagnostics.Debug.WriteLine("Received location: " 
                // + coordinate.Latitude + coordinate.Longitude);
                MoveMapToCurrentPosition();
            });
        }

        private void MoveMapToCurrentPosition()
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
            AddPinsFromList();
            MoveMapToCurrentPosition();
        }

        private String GetImageString(string lines)
        {
            if (lines.Contains("U") && lines.Contains("S"))
            {
                return "mergedpin.png";
            }
            else if (lines.Contains("U"))
            {
                return "metropin.png";
            }
            else return "strainpin.png";
        }


        private void AddPinsFromList()
        {
            foreach(StationViewModel station in viewModel.StationList)
            {
                var position = new Position(station.latitude, 
                                            station.longitude);
                

                var customPin = new CustomPin()
                {
                    Pin = new Pin
                    {
                        Type = PinType.Place,
                        Label = station.Name,
                        Position = position
                    },
                    ImageSource = GetImageString(station.Lines)
                };

                /*
                var pin = new Pin
                {
                    Type = PinType.Place,
                    Label = station.Name,
                    Position = position
                };
                Map.Pins.Add(pin);
                */

                Map.CustomPins.Add(customPin);
                Map.Pins.Add(customPin.Pin);


            }
        }


    }
}
