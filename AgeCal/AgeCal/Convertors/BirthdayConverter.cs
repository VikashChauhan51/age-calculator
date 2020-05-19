using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Convertors
{
    public class BirthdayConverter : IValueConverter
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var birthday = (DateTime)value;
            DateTime today = DateTime.Today;
            DateTime next = new DateTime(today.Year, birthday.Month, birthday.Day);

            if (next < today)
                next = next.AddYears(1);

            int numDays = (next - today).Days;
            var ts = new TimeSpan(next.Ticks - today.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);
            if (numDays <= 0 || delta <= 24 * HOUR)
            {
                return "Today is your Birthday";

            }
            if (delta < 30 * DAY)
            {
                if (ts.Days == 1)
                    return "Tomorrow is your Birthday";

                return $"Your next Birthday after {ts.Days } days";
            }


            if (delta < 12 * MONTH)
            {
                int months = (int)(Math.Floor((double)ts.Days / 30));
                var reminder = 0;
                Math.DivRem(ts.Days, 30, out reminder);
                if (reminder > 0)
                    return months <= 1 ? $" Your next Birthday after one monthh and {reminder} days" : $" Your next Birthday after {months} months and {reminder} days";
                else
                    return months <= 1 ? "Your next Birthday after one month" : $" Your next Birthday after {months} months";

            }
            else
            {
                int years = (int)(Math.Floor((double)ts.Days / 365));
                var reminder = 0;
                Math.DivRem(ts.Days, 30, out reminder);
                return years == 1 ? "Your next Birthday after a year" : $"Your next Birthday after 11 months and {reminder} days";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
