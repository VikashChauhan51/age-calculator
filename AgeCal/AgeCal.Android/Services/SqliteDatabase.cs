using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeCal.Interfaces;
using AgeCal.Services;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace AgeCal.Droid.Services
{
    public class SqliteDatabase : LocalDatabaseBaseService
    {
        private string path;
        static bool initialized = false;
        public const SQLiteOpenFlags Flags =
       // open the database in read/write mode
       SQLiteOpenFlags.ReadWrite |
       // create the database if it doesn't exist
       SQLiteOpenFlags.Create |
       // enable multi-threaded database access
       SQLiteOpenFlags.SharedCache;
        public override void Initialize()
        {

            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            path = base.GetPath(documentsPath);
            if (!File.Exists(path))
                File.Create(path);



        }
        public override SQLiteAsyncConnection ConnectionAsync() => new SQLiteAsyncConnection(path, Flags);
        public override SQLiteConnection Connection() => new SQLiteConnection(path, Flags);
        public override async Task InitializeTablesAsync(IEnumerable<Type> tables)
        {
            if (!initialized)

                if (tables != null && tables.Any())
                {
                    var connection = ConnectionAsync();
                    foreach (Type item in tables)
                        if (!connection.TableMappings.Any(m => m.MappedType.Name == item.Name))
                            await connection.CreateTablesAsync(CreateFlags.None, item).ConfigureAwait(false);
                }
            initialized = true;

        }
       public override void InitializeTables(IEnumerable<Type> tables)
        {
            if (!initialized)

                if (tables != null && tables.Any())
                {
                    var connection = Connection();
                    foreach (Type item in tables)
                        if (!connection.TableMappings.Any(m => m.MappedType.Name == item.Name))
                            connection.CreateTables(CreateFlags.None, item);
                }
            initialized = true;

        }
    }
}