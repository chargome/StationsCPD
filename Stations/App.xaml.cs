using Stations.Service;
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

            // Initialize and active location service
            locationService = DependencyService.Get<ILocationService>();
            locationService.ActivateLocationManager();

            MainPage = new NavigationPage(new View.StationListPage());
        }

        // todo xaml compilation here

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
			DependencyService.Register<JSONDataService>();
			MainPage = new NavigationPage(new View.StationListPage());

            // enable location
            locationService.ActivateLocationManager();
        }
    }
}
