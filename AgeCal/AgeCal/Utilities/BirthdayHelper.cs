using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Utilities
{

    public static class BirthdayHelper
    {
        const int SECOND = 1;
        const int MINUTE = 60 * SECOND;
        const int HOUR = 60 * MINUTE;
        const int DAY = 24 * HOUR;
        const int MONTH = 30 * DAY;
        const int YEAR = 12 * MONTH;


        public static string GetDateToMessage(DateTime birthday)
        {
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
        public static string GetCurrentAge(DateTime birthdayDate)
        {
            var currentDate = DateTime.Now;


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

        public static DateTimeOffset GetNextBirthday(DateTime birthday)
        {
            DateTime today = DateTime.Today;
            DateTime next = new DateTime(today.Year, birthday.Month, birthday.Day);

            if (next < today)
                next = next.AddYears(1);

            return new DateTimeOffset(next);
        }
        public static DateTime GetDate(DateTime date, TimeSpan time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hours, time.Minutes, time.Seconds);
        }
    }
}
