using System;
using System.Collections.Generic;
using Stations.Model;
using Stations.Viewmodel;
using Xamarin.Forms;

namespace Stations.View
{
    public partial class StationListPage : ContentPage
    {
        StationListViewModel viewModel;

        public Action<StationDetailPage> ItemSelected { get; set; }
        public ListView StationsListView { get { return stationsListView; }}

        public StationListPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new StationListViewModel();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
			var item = args.SelectedItem as Station;

			if (item == null)
				return;

			if (ItemSelected == null)
			{
                var DetailPage = new NavigationPage(new StationDetailPage(new StationDetailViewModel(item)));
				await Navigation.PushAsync(DetailPage);
                StationsListView.SelectedItem = null;
			}
			else
			{
                // todo wos is des
                ItemSelected.Invoke(new StationDetailPage(new StationDetailViewModel(item)));
			}
        }
    }
}
