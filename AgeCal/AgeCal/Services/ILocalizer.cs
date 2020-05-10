using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Services
{
    public interface ILocalizer
    {
        Task<CultureInfo> GetCurrentCultureInfo();
        Task<CultureInfo> GetCurrentCultureInfo(string localCode);
        Task SetLocale(CultureInfo ci);
    }
}
