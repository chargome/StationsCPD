using System;
using System.Collections.Generic;
using Stations.Model;
using Stations.Settings;
using Stations.Viewmodel;
using Xamarin.Forms;

namespace Stations.View
{
    public partial class StationListPage : ContentPage
    {
        StationListViewModel viewModel;

        public Action<StationDetailPage> ItemSelected { get; set; } 
        public ListView StationsListView { get { return stationsListView; }}

        public StationListPage(StationListViewModel viewmodel)
        {
            InitializeComponent();
            viewModel = viewmodel;
            BindingContext = viewModel;
        }

		protected override void OnAppearing()
		{
			base.OnAppearing();

			if (viewModel.StationList.Count == 0)
            {
                viewModel.RefreshCommand.Execute(null); 
            }
		        
            //System.Diagnostics.Debug.WriteLine("Listpage appearing..");
			SettingsButton.IsVisible =
			  DependencyService.Get<ISettingsService>().
			  SettingsDisplayDialogAvailable;
		}

		void Handle_Clicked(object sender, System.EventArgs e)
		{
			DependencyService.Get<ISettingsService>().
				 DisplaySettings();
		}

		async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
		{
			var item = args.SelectedItem as Station;

			if (item == null)
				return;

			if (ItemSelected == null)
			{
				var DetailPage = new StationDetailPage(new StationDetailViewModel(item));
				await Navigation.PushAsync(DetailPage);
				StationsListView.SelectedItem = null;
			}
			else
			{
				ItemSelected.Invoke(new StationDetailPage(new StationDetailViewModel(item)));
			}
		}




    }
}
