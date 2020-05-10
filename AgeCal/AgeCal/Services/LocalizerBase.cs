using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AgeCal.Services
{
    public abstract class LocalizerBase : ILocalizer
    {
        protected static CultureInfo _currentCulture;
        public abstract Task<CultureInfo> GetOSCurrentCultureInfo();
        public abstract Task<CultureInfo> GetCurrentCultureInfo(string localCode);
        public async Task<CultureInfo> GetCurrentCultureInfo()
        {
            if (_currentCulture != null)
                return _currentCulture;
            _currentCulture = await GetOSCurrentCultureInfo();
            return _currentCulture ?? CultureInfo.CurrentCulture;
        }

        public virtual async Task SetLocale(CultureInfo ci)
        {
            await Task.Delay(100);
            _currentCulture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            CultureInfo.CurrentCulture = ci;
            CultureInfo.CurrentUICulture = ci;
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
        }
        protected string NativeToDotNetLocale(string languageCode)
        {
            if (string.IsNullOrEmpty(languageCode))
                return languageCode;
            var netLanguage = languageCode;
            switch (languageCode)
            {
                case "ms-BN":
                case "ms-MY":
                case "ms-SG":
                    netLanguage = "ms";
                    break;
                case "in-ID":
                    netLanguage = "id-ID";
                    break;
                case "gsw-CH":
                    netLanguage = "de-CH";
                    break;
                case "de-US":
                    netLanguage = "de";
                    break;
                case "pt-US":
                    netLanguage = "pt-PT";
                    break;
                case "fr-US":
                    netLanguage = "fr";
                    break;
            }
            return netLanguage;
        }
        protected string ToDotnetFallbackLanguage(PlatfromCulture platformCulture)
        {
            var netLanguage = platformCulture.LanguageCode;
            switch (platformCulture.LanguageCode)
            {
                case "pt":
                    netLanguage = "pt-PT";
                    break;
                case "gsw":
                    netLanguage = "de-CH";
                    break;
            }
            return netLanguage;
        }

        protected class PlatfromCulture
        {

            public PlatfromCulture(string platfromLanguageString)
            {
                PlatformString = platfromLanguageString.Replace("_", "-");

                var dashIndex = PlatformString.IndexOf("-", StringComparison.Ordinal);
                if (dashIndex > 0)
                {
                    var parts = PlatformString.Split('-');
                    LanguageCode = parts[0];
                    LocaleCode = parts[1];
                }
                else
                {
                    LanguageCode = PlatformString;
                    LocaleCode = "";

                }
            }

            public string PlatformString { get; set; }
            public string LanguageCode { get; set; }
            public string LocaleCode { get; set; }
            public override string ToString()
            {
                return PlatformString;
            }
        }
    }
}
