using AgeCal.Interfaces;
using System.IO;

namespace AgeCal.Services
{
    public abstract class LocalDatabaseBaseService : ILocalDatabase
    {
        private const string DATABASE = "Tidingsdb.db";
        public abstract string ConnectionString();
        protected string GetPath(string nativePath) => Path.Combine(nativePath, DATABASE);
    }
}
