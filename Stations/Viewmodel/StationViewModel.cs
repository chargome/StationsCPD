using System;
using Stations.Model;

namespace Stations.Viewmodel
{
    // todo
    public class StationViewModel : BaseViewModel
    {
        public StationViewModel(Station station)
        {

        }

        double _distance;
        public double Distance
        {
            get
            {
                return _distance;    
            }

            set
            {
                _distance = value;
                OnPropertyChanged(nameof(Distance));
            }
        }
    }
}