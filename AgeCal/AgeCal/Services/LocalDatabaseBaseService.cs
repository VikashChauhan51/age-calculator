using AgeCal.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AgeCal.Services
{
    public abstract class LocalDatabaseBaseService : ILocalDatabase
    {

        private const string DATABASE = "AgeCalculator.db";
        public abstract void Initialize();

        protected string GetPath(string nativePath) => Path.Combine(nativePath, DATABASE);
    }
}
