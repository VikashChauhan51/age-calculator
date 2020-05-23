using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidApp = Android.App.Application;

namespace AgeCal.Droid.Services
{
    [BroadcastReceiver(Exported = true)]
    public class NotificationHandler : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            try
            {
                if (context == null || intent == null)
                    return;
   
                var manager = (NotificationManager)context.GetSystemService(AndroidApp.NotificationService);
                var notification = (Notification)intent.GetParcelableExtra("Notification");
                var id = (int)intent.GetParcelableExtra("NotificationId");
                var tag = (string)intent.GetParcelableExtra("NotificationTag");
                //get notification bundle
                Bundle notificationBundle = notification.Extras;
                var ExtraKeys = notificationBundle.KeySet().ToDictionary<string, string, object>(key => key, key => notificationBundle.Get(key));
                if (!string.IsNullOrEmpty(tag))
                {
                    manager.Notify(tag, id, notification);
                }
                else
                {
                    manager.Notify(1, notification);
                }

            }
            catch
            {


            }
        }
    }
}