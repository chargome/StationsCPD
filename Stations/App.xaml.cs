using Stations.Service;
using Stations.View;
using Xamarin.Forms;

namespace Stations
{
    public partial class App : Application
    {
        ILocationService locationService;

        public App()
        {
            InitializeComponent();
            DependencyService.Register<JSONDataService>();

            // Initialize and activate location service
            locationService = DependencyService.Get<ILocationService>();
            MainPage = new MainPage();
            locationService.ActivateLocationManager();
            //MainPage = new NavigationPage(new View.StationListPage());
        }



        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            //disable location updates
            locationService.DeactivateLocationManager();

        }

        protected override void OnResume()
        {
            //DependencyService.Register<JSONDataService>();
            MainPage = new MainPage();

            // enable location
            locationService.ActivateLocationManager();
        }
    }
}
