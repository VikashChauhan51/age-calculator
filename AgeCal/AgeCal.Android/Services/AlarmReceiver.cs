﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using AndroidApp = Android.App.Application;

namespace AgeCal.Droid.Services
{
    [BroadcastReceiver(Exported = true)]
    public class AlarmReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var message = intent.GetStringExtra("message");
            var title = intent.GetStringExtra("title");

            var notIntent = new Intent(context, typeof(MainActivity));
            var contentIntent = PendingIntent.GetActivity(context, 0, notIntent, PendingIntentFlags.UpdateCurrent);
           var manager = (NotificationManager)context.GetSystemService(AndroidApp.NotificationService);

            var style = new NotificationCompat.BigTextStyle();
            style.BigText(message);

         

            var wearableExtender = new NotificationCompat.WearableExtender()
    .SetBackground(BitmapFactory.DecodeResource(context.Resources, Resource.Drawable.reminder_icon))
                ;

            //Generate a notification with just short text and small icon
            var builder = new NotificationCompat.Builder(context)
                            .SetContentIntent(contentIntent)
                            .SetSmallIcon(Resource.Drawable.reminder_icon)
                            .SetContentTitle(title)
                            .SetContentText(message)
                            .SetStyle(style)
                            .SetWhen(Java.Lang.JavaSystem.CurrentTimeMillis())
                            .SetAutoCancel(true)
                            .Extend(wearableExtender);

            var notification = builder.Build();
            manager.Notify(0, notification);
        }
    }
}