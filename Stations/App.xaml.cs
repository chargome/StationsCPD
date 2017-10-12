using Stations.Service;
using Xamarin.Forms;

namespace Stations
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<JSONDataService>();

            MainPage = new NavigationPage(new View.StationListPage());
        }

        // todo xaml compilation here
        // todo images: only lowercase letters + underscores

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
			DependencyService.Register<JSONDataService>();

			MainPage = new NavigationPage(new View.StationListPage());
        }
    }
}
