using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AgeCal.Interfaces;
using AgeCal.Models;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using AndroidApp = Android.App.Application;
namespace AgeCal.Droid.Services
{
    public class AndroidNotificationManager : INotificationManager
    {
        const string channelId = "AgeCalculator";
        const string channelName = "AgeCalculator";
        const string channelDescription = "The Age Calculator channel for notifications.";
        const int pendingIntentId = 0;

        public const string TitleKey = "title";
        public const string MessageKey = "message";

        bool channelInitialized = false;
        NotificationManager manager;

        public event EventHandler NotificationReceived;

        public void Initialize()
        {
            CreateNotificationChannel();
        }

        public int ScheduleNotification(Reminder reminder)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }

            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            intent.PutExtra(TitleKey, reminder.Title);
            intent.PutExtra(MessageKey, reminder.Message);

            PendingIntent pendingIntent = PendingIntent.GetActivity(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.OneShot);

            NotificationCompat.Builder builder = new NotificationCompat.Builder(AndroidApp.Context, channelId)
                .SetContentIntent(pendingIntent)
                .SetContentTitle(reminder.Title)
                .SetContentText(reminder.Message)
                .SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.data))
                .SetSmallIcon(Resource.Drawable.data)
                .SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);


            //var pendingIntent = PendingIntent.GetBroadcast(Application.Context, 0, intent, PendingIntentFlags.CancelCurrent);
            //var triggerTime = NotifyTimeInMilliseconds(localNotification.NotifyTime);
            //var alarmManager = GetAlarmManager();

            //alarmManager.Set(AlarmType.RtcWakeup, triggerTime, pendingIntent);
            var notification = builder.Build();
            manager.Notify(reminder.Id, notification);

            return reminder.Id;
        }

        public void ReceiveNotification(string title, string message)
        {
            var args = new NotificationEventArgs()
            {
                Title = title,
                Message = message,
            };
            NotificationReceived?.Invoke(null, args);
        }

        void CreateNotificationChannel()
        {
            manager = (NotificationManager)AndroidApp.Context.GetSystemService(AndroidApp.NotificationService);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                var channelNameJava = new Java.Lang.String(channelName);
                var channel = new NotificationChannel(channelId, channelNameJava, NotificationImportance.Default)
                {
                    Description = channelDescription
                };
                manager.CreateNotificationChannel(channel);
            }

            channelInitialized = true;
        }

        public void UnscheduleNotification(int id)
        {
            Intent intent = new Intent(AndroidApp.Context, typeof(MainActivity));
            var pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, pendingIntentId, intent, PendingIntentFlags.CancelCurrent);;
            var alarmManager = GetAlarmManager();
            alarmManager.Cancel(pendingIntent);
            manager.Cancel(id);
        }
        private AlarmManager GetAlarmManager()
        {
            var alarmManager = AndroidApp.Context.GetSystemService(Context.AlarmService) as AlarmManager;
            return alarmManager;
        }
        private long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }
    }
}