using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Stations.View
{
    public partial class StationListPage : ContentPage
    {
        public StationListPage()
        {
            InitializeComponent();
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new StationDetailPage());
            //throw new NotImplementedException();
        }
    }
}
