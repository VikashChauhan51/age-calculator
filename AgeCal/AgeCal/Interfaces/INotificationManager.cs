using AgeCal.Core;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;

        int ScheduleNotification(NotificationEventArgs reminder);
        void UnscheduleNotification(string tag, int id);
        void Reminder(int  seconds, string title, string message);
    }
}
