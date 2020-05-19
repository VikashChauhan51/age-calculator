using AgeCal.Interfaces;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Database
{
    public sealed class AgeDatabase
    {
        private static Lazy<ILocalDatabase> lazy = new Lazy<ILocalDatabase>(() => Ioc.IocRegistry.Locate<ILocalDatabase>());
       
        static AgeDatabase()
        {

        }
        private AgeDatabase()
        {
             
        }

        public static ILocalDatabase Database => lazy.Value;
        
    }
}

