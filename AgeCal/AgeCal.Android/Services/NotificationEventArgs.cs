using System;

namespace AgeCal.Droid.Services
{
    internal class NotificationEventArgs: EventArgs
    {
        public NotificationEventArgs()
        {
        }

        public string Title { get; set; }
        public string Message { get; set; }
    }
}