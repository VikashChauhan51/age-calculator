using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Convertors
{
    public class DatetimeToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;
            if (value is DateTimeOffset)
            {
                var date = (DateTimeOffset)value;
                return date.LocalDateTime.ToShortDateString();
            }
            else
            {
                var date = (DateTime)value;
                return date.ToShortDateString();
            }


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
