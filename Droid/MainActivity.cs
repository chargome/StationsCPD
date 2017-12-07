using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase.JobDispatcher;
using Stations.Droid.Service;

namespace Stations.Droid
{
    [Activity(Label = "Stations.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var driver = new GooglePlayDriver(this);
            var dispatcher = new FirebaseJobDispatcher(driver);

            dispatcher.Cancel("my-job-service-tag");

            var myJob = dispatcher.NewJobBuilder()
                                  .SetService<BackgroundJobService>("my-job-service-tag")
                                  .SetTrigger(Trigger.ExecutionWindow(2, 5))
                                  .SetLifetime(Lifetime.Forever)
                                  .SetRecurring(true)
                                  .Build();
            dispatcher.MustSchedule(myJob);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}
