using AgeCal.UITest.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;

namespace AgeCal.UITest.Tests
{
    public abstract class BaseTest
    {
        protected IApp app;
        protected readonly Platform platform;
        protected readonly string language;
        protected readonly IAppInitializer appInitializer;
        private static readonly Dictionary<string, string> _config;
        #region Properties

        public static string AssemblyDirectory
        {
            get
            {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }
        #endregion
        static BaseTest()
        {
            _config = ResourceLoader.ReadEmbededFile("App.config");
        }
        public BaseTest(Platform platform)
        {
            this.platform = platform;
            appInitializer = new AppInitializer();

        }
        protected void StartApp(AppDataMode dataMode)
        {
            app = appInitializer.StartApp(platform, dataMode);
        }
    }
}
