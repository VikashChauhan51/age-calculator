using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface INotificationManager
    {
        event EventHandler NotificationReceived;

        int ScheduleNotification(string title, string message);

        void ReceiveNotification(string title, string message);
    }
}
