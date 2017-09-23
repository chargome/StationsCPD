using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Stations.View
{
    public partial class StationDetailPage : ContentPage
    {
        StationDetailViewModel viewModel;

        public StationDetailPage(StationDetailViewModel model)
        {
            InitializeComponent();
            BindingContext = viewModel = model;
        }

        public StationDetailPage()
        {
            InitializeComponent();
        }
    }
}
