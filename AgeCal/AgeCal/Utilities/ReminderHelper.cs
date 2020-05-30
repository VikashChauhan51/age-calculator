using AgeCal.Models;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Utilities
{
    public static class ReminderHelper
    {
        public static void DeleteReminderNotification(Reminder reminder)
        {
            var today = DateTime.Now;
            //removed schedule reminder if any
            if (reminder.When.LocalDateTime > today)
                CrossLocalNotifications.Current.Cancel(reminder.Id);
        }

        public static void AddReminderNotification(Reminder reminder)
        {
            CrossLocalNotifications.Current.Show(reminder.Title, reminder.Message, reminder.Id, reminder.When.LocalDateTime);
        }
    }
}
