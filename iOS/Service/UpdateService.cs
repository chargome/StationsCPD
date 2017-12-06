using System;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.iOS.Service
{
    public class UpdateService: IUpdateService
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
