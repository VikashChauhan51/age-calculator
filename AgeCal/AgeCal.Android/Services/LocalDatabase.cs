
using System.IO;
using AgeCal.Services;
using Xamarin.Essentials;

namespace AgeCal.Droid.Services
{
    public class LocalDatabase : LocalDatabaseBaseService
    {

        public override string ConnectionString()
        {

            string documentsPath = FileSystem.AppDataDirectory;
            var path = base.GetPath(documentsPath);
            return path;
        }
  
    }
}