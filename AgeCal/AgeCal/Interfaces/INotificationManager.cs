using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;

        int ScheduleNotification(Reminder reminder);
        void UnscheduleNotification(int id);

        void ReceiveNotification(string title, string message);
    }
}
