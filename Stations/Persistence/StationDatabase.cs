using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;
using Stations.Model;
using Stations.Service;
using Xamarin.Forms;

namespace Stations.Persistence
{
    public class StationDatabase
    {
        readonly SQLiteAsyncConnection database;

        private static StationDatabase _instance;
        private static object _syncRoot = new object();

        public StationDatabase(String filePath)
        {
            database = new SQLiteAsyncConnection(filePath);
            database.CreateTableAsync<Station>().Wait();
        }


        // threadsafe singleton pattern
        public static StationDatabase GetInstance
        {
            get
            {
                if(_instance == null)
                {
                    lock (_syncRoot)
                    {
                        if(_instance == null)
                        {
                            _instance = new StationDatabase(DependencyService
                                                     .Get<IFilePathService>()
                                                     .GetLocalFilePath("StationDb.db3"));        
                        }
                    }
                }

                return _instance;
            }
        }


        public async Task<int> UpdateItemAsync(Station station)
        {
            int count;

            count = await database.UpdateAsync(station);

            if (count > 0)
            {
                //NotifyUpdate();
            }

            return count;
        }

        public async Task<int> SaveItemAsync(Station station) 
        {
            int count;

            count = await database.InsertAsync(station);

            if(count > 0)
            {
                //NotifyUpdate();
            }

            return count;
        }

        public async Task<List<Station>> GetItemsAsync()
        {
            return await database.Table<Station>().ToListAsync();
        }

        public async Task<int> DeleteItem(Station station)
        {
            return await database.DeleteAsync(station);
        }


    }
            
}
