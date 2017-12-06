using System;
using System.IO;
using Stations.Droid.Service;
using Stations.Service;

using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace Stations.Droid.Service
{
    public class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filePath)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filePath);
        }
    }
}
