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
            listPage.Icon = "tab_list.png";

            var mapPage = new NavigationPage(new StationMapPage(listViewModel));
            mapPage.Title = "Map";
            mapPage.Icon = "tab_map.png";

            Children.Add(listPage);
            Children.Add(mapPage);


        }
    }
}
