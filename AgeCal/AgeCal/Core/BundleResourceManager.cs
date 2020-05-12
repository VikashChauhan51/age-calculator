using AgeCal.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Core
{
    public class BundleResourceManager
    {
        private static CultureInfo CI = null;
        private static ResourceManager _baseResourceManager = null;
        private static readonly string resourceId = "AgeCal.i18n.AppResource";
        internal static ResourceManager BaseResourceManager
        {
            get
            {
                if (_baseResourceManager == null)
                {
                    _baseResourceManager = new ResourceManager(resourceId, typeof(BundleResourceManager).Assembly);
                }
                if (CI == null)
                {
                    ILocalizer locale = Ioc.IocRegistry.Locate<ILocalizer>();
                    if (locale != null)
                    {
                        CI = Task.Run(async () => { return await locale.GetCurrentCultureInfo(); }).Result;
                    }
                }
                return _baseResourceManager;
            }
        }

        public static string Translate(string key, params object[] vars)
        {
            string text = BaseResourceManager.GetString(key, CI);
            if (vars?.Length > 0)
            {
                text = string.Format(text, vars);
            }
            return text;
        }
    }
}
