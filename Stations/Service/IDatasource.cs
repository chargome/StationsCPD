using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Stations.Model;

namespace Stations
{
    public interface IDatasource<T>
    {
        Task<ObservableCollection<T>> GetStationsAsync();
    }
}
