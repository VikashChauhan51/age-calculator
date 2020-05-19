using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Convertors
{
    public class AgeConverter : IValueConverter
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        const int YEAR = 12 * MONTH;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var currentDate = DateTime.Now;
            var birthdayDate = (DateTime)value;

            var ts = new TimeSpan(currentDate.Ticks - birthdayDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (delta < 1 * MINUTE)
            {
                if (ts.Seconds < 0)
                {
                    return "sometime old";
                }
                return ts.Seconds == 1 ? "One second old" : ts.Seconds + " seconds old";
            }

            if (delta < 2 * MINUTE)
                return "A minute old";

            if (delta < 45 * MINUTE)
            {
                if (ts.Seconds < 0)
                {
                    return "sometime old";
                }
                return ts.Minutes + " minutes old";
            }

            if (delta <= 90 * MINUTE)
                return "An hour old";

            if (delta < 24 * HOUR)
            {
                if (ts.Hours < 0)
                {
                    return "sometime old";
                }

                if (ts.Hours == 1)
                    return "1 hour old";

                return ts.Hours + " hours old";
            }

            if (delta < 48 * HOUR)
                return $"Yesterday at {birthdayDate.ToString("t")}";

            if (delta < 30 * DAY)
            {
                if (ts.Days == 1)
                    return "1 day old";

                return ts.Days + " days old";
            }


            if (delta < 12 * MONTH)
            {
                int months = (int)(Math.Floor((double)ts.Days / 30));
                var reminder = 0;
                Math.DivRem(ts.Days, 30, out reminder);
                if (reminder > 0)
                    return months <= 1 ? $"one monthh and {reminder} days old" : $"{months} months and {reminder} days old";
                else
                    return months <= 1 ? "one month old" : months + " months old";

            }
            else
            {
                int years = (int)(Math.Floor((double)ts.Days / 365));
                var reminder = 0;
                Math.DivRem(ts.Days, 365, out reminder);
                if (reminder > 0)
                    return years <= 1 ? $"one year and {reminder} days old" : $"{years} years and {reminder} days old";
                else
                    return years <= 1 ? "one year old" : $"{years} years old";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
