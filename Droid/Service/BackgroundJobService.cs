using System;
using Android.App;
using Firebase.JobDispatcher;

namespace Stations.Droid.Service
{

    [Service(Name = "Stations.Backgroundservice")]
    [IntentFilter(new[] { FirebaseJobServiceIntent.Action })]
    public class BackgroundJobService : JobService
    {
        public BackgroundJobService()
        {
        }

        UpdateService updateService = new UpdateService();

        public override bool OnStartJob(IJobParameters jobParameters)
        {
            updateService.TriggerUpdate();

            return false;
        }

        public override bool OnStopJob(IJobParameters jobParameters)
        {
            return false;
        }
    }
}
