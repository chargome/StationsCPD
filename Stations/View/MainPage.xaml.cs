using System;
using System.Collections.Generic;
using Stations.Viewmodel;
using Xamarin.Forms;

namespace Stations.View
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            StationListViewModel listViewModel = new StationListViewModel();

            var listPage = new NavigationPage(new StationListPage(listViewModel));
            listPage.Title = "List";


            var mapPage = new NavigationPage(new StationMapPage(listViewModel));
            mapPage.Title = "Map";


            // check if app runs on iOS and set icons accordingly
            switch(Device.RuntimePlatform)
            {
                case Device.iOS:
                    listPage.Icon = "tab_list.png";
                    mapPage.Icon = "tab_map.png";
                    break;
            }


            Children.Add(listPage);
            Children.Add(mapPage);


        }
    }
}
