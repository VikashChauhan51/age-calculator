
using System.IO;
using AgeCal.Services;
using Xamarin.Essentials;

namespace AgeCal.iOS.Services
{
    public class LocalDatabase : LocalDatabaseBaseService
    {
        public override string ConnectionString()
        {

            string documentsPath = FileSystem.AppDataDirectory;
            var path = base.GetPath(documentsPath);
            if (!File.Exists(path))
                File.Create(path).Dispose();

            return path;

        }
    }
}