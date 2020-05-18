using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AgeCal.Interfaces;
using AgeCal.Services;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AgeCal.Droid.Services
{
    public class SqliteDatabase : LocalDatabaseBaseService
    {
        public override void Initialize()
        {

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = base.GetPath(documentsPath);
            if (!File.Exists(path))
                File.Create(path);

        }
    }
}