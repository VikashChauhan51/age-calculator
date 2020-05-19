using AgeCal.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Services
{
    public abstract class LocalDatabaseBaseService : ILocalDatabase
    {

        private const string DATABASE = "AgeCalculator.db";

        public abstract SQLiteConnection Connection();

        public abstract SQLiteAsyncConnection ConnectionAsync();

        public abstract void Initialize();

        protected string GetPath(string nativePath) => Path.Combine(nativePath, DATABASE);
        public abstract Task InitializeTablesAsync(IEnumerable<Type> tables);
        public abstract void InitializeTables(IEnumerable<Type> tables);
    }
}
