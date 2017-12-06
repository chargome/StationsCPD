using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using Foundation;
using UIKit;
using Stations.iOS.Service;

namespace Stations.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UpdateService updateService = new UpdateService();

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();

            UIApplication.SharedApplication.SetMinimumBackgroundFetchInterval(UIApplication.BackgroundFetchIntervalMinimum);

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }


        public override void PerformFetch(UIApplication application, Action<UIBackgroundFetchResult> completionHandler)
        {
            Debug.WriteLine("Fetch enabled");
            updateService.TriggerUpdate();

            completionHandler(UIBackgroundFetchResult.NewData);
        }
    }
}
