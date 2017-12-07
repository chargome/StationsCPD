using System;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.Droid.Service
{
    public class UpdateService : IUpdateService
    {
        public UpdateService()
        {
        }

        public void TriggerUpdate()
        {
            MessagingCenter.Send<UpdateService>(this, "update");
        }
    }
}
