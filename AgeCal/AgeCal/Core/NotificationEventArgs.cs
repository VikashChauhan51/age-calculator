using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Core
{

    public enum NotificationEventAction { Create, Cancel }
    public enum NotificationRepeatInterval { None, Monthly, Yearly }
    public class NotificationEventArgs : EventArgs
    {
        public NotificationEventArgs()
        {
            Id = 1;
            When = DateTime.Now;
            Action = NotificationEventAction.Create;
            RepeatInterval = NotificationRepeatInterval.None;
        }
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset When { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public NotificationEventAction Action { get; set; }
        public NotificationRepeatInterval RepeatInterval { get; set; }
        public Type NavigationTarget { get; set; }
    }
}
