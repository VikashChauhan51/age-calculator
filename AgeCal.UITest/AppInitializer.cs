using AgeCal.UITest.Utilities.Enums;
using NUnit.Framework;
using System;
using System.IO;
using System.Reflection;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Xamarin.UITest.Queries;

namespace AgeCal.UITest
{
    public interface IAppInitializer
    {
        /// <summary>
        /// Initializer App
        /// </summary>
        /// <param name="platform"></param>
        /// <param name="dataMode"></param>
        /// <returns></returns>
        IApp StartApp(Platform platform, AppDataMode dataMode);
    }
    public class AppInitializer : IAppInitializer
    {
        #region Fields
        public bool XAMARIN_TEST_CLOUD_ENABLED = false;
        readonly string APP_LOCATION = @"D:\Project\age-calculator\AgeCal\AgeCal.Android\bin\Debug\com.companyname.AgeCal-Signed.apk";
        readonly string DEVICE_ID = "HKE7YWCW";
        readonly string IOS_APP_NAME = "";
        IOSDeviceType IOSDevice = IOSDeviceType.Physical;
        #endregion
        
        /// <summary>
        /// Constructor
        /// </summary>
        public AppInitializer()
        {

            XAMARIN_TEST_CLOUD_ENABLED = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("XAMARIN_TEST_CLOUD"));
            if (string.IsNullOrEmpty(APP_LOCATION))
                APP_LOCATION = TestContext.Parameters["APP_LOCATION"];

            if (string.IsNullOrEmpty(DEVICE_ID))
                DEVICE_ID = TestContext.Parameters["DEVICE_ID"];

            if (string.IsNullOrEmpty(IOS_APP_NAME))
                IOS_APP_NAME = TestContext.Parameters["IOS_APP_NAME"];

        }


        public IApp StartApp(Platform platform, AppDataMode dataMode)
        {
            if (platform == Platform.Android)
            {
                return InitAndroidApp(dataMode);
            }
            else
            {
                return InitIOSApp(dataMode);
            }

        }

        private IApp InitIOSApp(AppDataMode dataMode)
        {
            if (XAMARIN_TEST_CLOUD_ENABLED)
            {
                return ConfigureApp.iOS.EnableLocalScreenshots().StartApp(dataMode);
            }
            else
            {
                switch (IOSDevice)
                {
                    case IOSDeviceType.Simulator:
                        return ConfigureApp.iOS.AppBundle(APP_LOCATION).DeviceIdentifier(DEVICE_ID).EnableLocalScreenshots().Debug().StartApp(dataMode);

                    case IOSDeviceType.Physical:
                    default:
                        return ConfigureApp.iOS.InstalledApp(IOS_APP_NAME).DeviceIdentifier(DEVICE_ID).EnableLocalScreenshots().StartApp(dataMode);
                }

            }

        }
        private IApp InitAndroidApp(AppDataMode dataMode)
        {
            if (XAMARIN_TEST_CLOUD_ENABLED)
            {
                return ConfigureApp.Android.EnableLocalScreenshots().StartApp(dataMode);
            }
            else
            {
                return ConfigureApp.Android.ApkFile(APP_LOCATION).DeviceSerial(DEVICE_ID).EnableLocalScreenshots().StartApp(dataMode);
            }

        }

 
    }
}