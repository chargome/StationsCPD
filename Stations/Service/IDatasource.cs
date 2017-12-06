using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Stations
{
    public interface IDatasource<T>
    {
        //Task<ObservableCollection<T>> GetStationsAsync();

        Task UpdateStationsFromApiAsync();
    }
}
