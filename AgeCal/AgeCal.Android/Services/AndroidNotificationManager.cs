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
using AgeCal.Core;
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

        public int ScheduleNotification(NotificationEventArgs reminder)
        {
            if (!channelInitialized)
            {
                CreateNotificationChannel();
            }
            Notify(AndroidApp.Context, reminder);

            return reminder.Id;
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

        public void UnscheduleNotification(string tag,int id)=> CancelNotofication(AndroidApp.Context, tag, id);

        public void Reminder(int seconds, string title, string message)
        {

            Intent alarmIntent = new Intent(AndroidApp.Context, typeof(AlarmReceiver));
            alarmIntent.PutExtra("message", message);
            alarmIntent.PutExtra("title", title);

            PendingIntent pendingIntent = PendingIntent.GetBroadcast(AndroidApp.Context, 0, alarmIntent, PendingIntentFlags.UpdateCurrent);
            AlarmManager alarmManager = (AlarmManager)AndroidApp.Context.GetSystemService(Context.AlarmService);

            //TODO: For demo set after 5 seconds.
            alarmManager.Set(AlarmType.ElapsedRealtime, SystemClock.ElapsedRealtime() + 5 * 1000, pendingIntent);

        }
        private long NotifyTimeInMilliseconds(DateTime notifyTime)
        {
            var utcTime = TimeZoneInfo.ConvertTimeToUtc(notifyTime);
            var epochDifference = (new DateTime(1970, 1, 1) - DateTime.MinValue).TotalSeconds;

            var utcAlarmTimeInMillis = utcTime.AddSeconds(-epochDifference).Ticks / 10000;
            return utcAlarmTimeInMillis;
        }

        private Notification Notify(Context context, NotificationEventArgs e)
        {
            Notification notificationResult = null;
            if (context == null || e == null)
                return notificationResult;


            try
            {
                NotificationCompat.Builder notificationBuilder;
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    notificationBuilder = new NotificationCompat.Builder(context, "Default");
                else
                    notificationBuilder = new NotificationCompat.Builder(context);

                notificationBuilder.SetContentTitle(e.Title);
                notificationBuilder.SetContentText(e.Message);
                if (e.NavigationTarget != null)
                {
                    Intent intent = GetViewIntent(context, e);
                    if (intent != null)
                    {
                        PendingIntent pendingIntent = PendingIntent.GetActivity(context, pendingIntentId, intent, PendingIntentFlags.UpdateCurrent);
                        notificationBuilder.SetContentIntent(pendingIntent);
                        notificationBuilder.SetExtras(intent.Extras);
                    }
                }
                long newNotificationTarget;
                if (e.RepeatInterval != NotificationRepeatInterval.None)
                    newNotificationTarget = GetNotoficationTime(e.When);
                else
                    newNotificationTarget = SystemClock.ElapsedRealtime() + (e.When.Ticks - DateTimeOffset.Now.Ticks) / 10000;

                notificationBuilder.SetWhen(newNotificationTarget);

                var dismisIntent = GetDismissIntent(context, e);
                var dismisPendingIntent = PendingIntent.GetBroadcast(context.ApplicationContext, e.Id, dismisIntent, 0);
                notificationBuilder.SetDeleteIntent(dismisPendingIntent);
                notificationBuilder.SetContentTitle(e.Title);
                notificationBuilder.SetContentText(e.Message);
                notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(AndroidApp.Context.Resources, Resource.Drawable.data));
                notificationBuilder.SetSmallIcon(Resource.Drawable.data);
                notificationBuilder.SetDefaults((int)NotificationDefaults.Sound | (int)NotificationDefaults.Vibrate);
                var pendingAlarmIntent = GetAlarmPendingIntent(context, e, ref notificationBuilder);
                var alarmManager = context.GetSystemService(Context.AlarmService) as AlarmManager;
                if (e.When.DateTime > DateTime.Now)
                {
                    switch (e.Action)
                    {
                        case NotificationEventAction.Create:
                            if (e.RepeatInterval != NotificationRepeatInterval.None)
                            {
                                long repeatInterval = GetRepeatInterval(e.RepeatInterval);
                                alarmManager.SetRepeating(AlarmType.RtcWakeup, newNotificationTarget, repeatInterval, pendingAlarmIntent);
                            }
                            else
                            {
                                alarmManager.Set(AlarmType.ElapsedRealtimeWakeup, newNotificationTarget, pendingAlarmIntent);
                            }
                            break;
                        case NotificationEventAction.Cancel:
                            alarmManager.Cancel(pendingAlarmIntent);
                            break;
                        default:
                            long targetTime = Java.Lang.JavaSystem.CurrentTimeMillis() + (e.When.Ticks - DateTimeOffset.Now.Ticks) / 10000;
                            alarmManager.Set(AlarmType.RtcWakeup, targetTime, pendingAlarmIntent);
                            break;

                    }
                }
                else
                {
                    switch (e.Action)
                    {

                        case NotificationEventAction.Cancel:
                            CancelNotofication(context, e.Tag, e.Id);
                            pendingAlarmIntent.Cancel();
                            alarmManager.Cancel(pendingAlarmIntent);
                            break;
                        case NotificationEventAction.Create:
                        default:
                            notificationResult = notificationBuilder.Build();
                            ShowNotification(context, e.Tag, e.Id, notificationResult);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {


            }

            return notificationResult;
        }


        public void NotifyClearAll() => manager.CancelAll();

        private void ShowNotification(Context context, string tag, int id, Notification notification)
        {
            if (string.IsNullOrEmpty(tag))
                manager.Notify(id, notification);
            else
                manager.Notify(tag, id, notification);
        }
        private void CancelNotofication(Context context, string tag, int id)
        {
            if (string.IsNullOrEmpty(tag))
                manager.Cancel(id);
            else
                manager.Cancel(tag, id);
        }
        private long GetRepeatInterval(NotificationRepeatInterval repeat)
        {
            long interval = 0;
            switch (repeat)
            {
                case NotificationRepeatInterval.Monthly:
                    interval = AlarmManager.IntervalDay * 30;// not accurate
                    break;
                case NotificationRepeatInterval.Yearly:
                    interval = AlarmManager.IntervalDay * 365;
                    break;
                default:
                    break;
            }
            return interval;
        }

        private PendingIntent GetAlarmPendingIntent(Context context, NotificationEventArgs e, ref NotificationCompat.Builder notificationBuilder)
        {
            var intent = new Intent(context, typeof(NotificationHandler));
            intent.SetAction(e.Id.ToString());
            intent.PutExtra("NotificationId", e.Id);
            intent.PutExtra("NotificationTag", e.Tag);
            intent.PutExtra("Notification", notificationBuilder.Build());

            return PendingIntent.GetBroadcast(context, e.Id, intent, PendingIntentFlags.UpdateCurrent);
        }
        private long GetNotoficationTime(DateTimeOffset date)
        {
            Java.Util.Calendar calendar = Java.Util.Calendar.Instance;
            calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
            calendar.Set(date.Year, date.Month - 1, date.Day, date.Hour, date.Minute, 0);
            return calendar.TimeInMillis;
        }

        public Intent GetDismissIntent(Context context, NotificationEventArgs e)
        {
            var intent = new Intent(context, typeof(NotificationHandler));
            intent.PutExtra("NotificationArgs", "");
            //todo:add more info
            return intent;
        }

        public Intent GetViewIntent(Context context, NotificationEventArgs e)
        {
            var intent = new Intent(context, typeof(MainActivity));
            intent.PutExtra("NotificationArgs", "");
            return intent;
        }
    }
}