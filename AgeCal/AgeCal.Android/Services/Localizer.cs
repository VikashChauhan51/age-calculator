using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgeCal.Services;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace AgeCal.Droid.Services
{
    public class Localizer : LocalizerBase
    {
        public override async Task<CultureInfo> GetCurrentCultureInfo(string localCode)
        {
            await Task.Delay(100);
            CultureInfo ci = CultureInfo.CurrentCulture.Clone() as CultureInfo;
            try
            {
                if (ci?.Name == null || !ci.Name.Equals(localCode, StringComparison.OrdinalIgnoreCase))
                {
                    ci = new CultureInfo(localCode, true);
                }

            }
            catch (CultureNotFoundException ex)
            {

                try
                {

                    var falback = ToDotnetFallbackLanguage(new PlatfromCulture(localCode));
                    ci = new CultureInfo(falback, true);
                }
                catch (CultureNotFoundException e)
                {
                    ci = new CultureInfo("en", true);
                }
            }
            catch (Exception main)
            {

            }

            return ci;
        }

        public override async Task<CultureInfo> GetOSCurrentCultureInfo()
        {
            var androidLocale = Java.Util.Locale.Default;
            string netLanguage = AndroidToDotNetLnguage(androidLocale.ToString());
            return await GetCurrentCultureInfo(netLanguage);
        }

        private string AndroidToDotNetLnguage(string androidLanguage) => base.NativeToDotNetLocale(androidLanguage?.Replace("_", "-"));

        private static Java.Util.Locale ToAndroidLocale(CultureInfo culture)
        {
            var platfromCulture = new PlatfromCulture(culture.Name);
            return new Java.Util.Locale(platfromCulture.LanguageCode, platfromCulture.LocaleCode);
        }
        public override async Task SetLocale(CultureInfo ci)
        {
            await base.SetLocale(ci);
            var currentAndroidCulture = await GetOSCurrentCultureInfo();
            var currentAndroidLang = currentAndroidCulture.Name;
            if (currentAndroidLang != ci.Name)
            {
                Java.Util.Locale locale = ToAndroidLocale(ci);
                Java.Util.Locale.Default = locale;
                if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.N)
                {
                    Java.Util.Locale.SetDefault(Java.Util.Locale.Category.Display, locale);
                    Java.Util.Locale.SetDefault(Java.Util.Locale.Category.Format, locale);
                }

            }
        }
    }
}