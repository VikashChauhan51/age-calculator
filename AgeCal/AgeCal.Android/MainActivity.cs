using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using AgeCal.Ioc;
using AgeCal.Droid.Services;
using AgeCal.Interfaces;
using System.Collections.Generic;
using AgeCal.Models;
using AgeCal.Services;
using Plugin.LocalNotifications;
using Android.Content;

namespace AgeCal.Droid
{
    [Activity(Label = "Birthday Reminder", Icon = "@mipmap/icon", Theme = "@style/MainTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.reminder;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(typeof(Views.Home)));

            RegisterServices();

        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
             
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void RegisterComponentCallbacks(IComponentCallbacks callback)
        {
            base.RegisterComponentCallbacks(callback);  
        }
        public override void UnregisterReceiver(BroadcastReceiver receiver)
        {
            base.UnregisterReceiver(receiver);  
        }
        
        void RegisterServices()
        {
            IocRegistry.Register<ILocalDatabase>(() =>
            {
                var db = new SqliteDatabase();
                db.Initialize();
                db.InitializeTables(new List<Type>
                {
                    typeof(User),
                    typeof(Reminder),
                    typeof(ReminderSetting),
                    typeof(DashboardSetting)
                });
                return db;
            });
            IocRegistry.Register<INotificationManager>(() =>
            {
                var notification = new AndroidNotificationManager();
                notification.Initialize();
                return notification;
            });
            IocRegistry.Register<ILocalizer, Localizer>();
            IocRegistry.Register<IShare, Share>();

        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();   
        }
        
    }
}