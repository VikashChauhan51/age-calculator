using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AgeCal.Services;
using Foundation;
using UIKit;

namespace AgeCal.iOS.Services
{
    public class SqliteDatabase : LocalDatabaseBaseService
    {
        public override void Initialize()
        {

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = base.GetPath(libraryPath);
            if (!File.Exists(path))
                File.Create(path);

        }
    }
}