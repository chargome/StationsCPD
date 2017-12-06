using System;
using System.IO;
using Stations.iOS.Service;
using Stations.Service;
using Xamarin.Forms;

[assembly: Dependency(typeof(FilePathService))]
namespace Stations.iOS.Service
{
    public class FilePathService : IFilePathService
    {
        public string GetLocalFilePath(string filePath)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filePath);
        }
    }
}
