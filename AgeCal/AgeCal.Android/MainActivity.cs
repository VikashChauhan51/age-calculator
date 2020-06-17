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
using System.Threading.Tasks;
using Matcha.BackgroundService.Droid;

namespace AgeCal.Droid
{
    [Activity(Label = "Birthday Reminder", Icon = "@mipmap/icon", Theme = "@style/MainTheme", LaunchMode = LaunchMode.SingleTop, MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;
            AndroidEnvironment.UnhandledExceptionRaiser += AndroidEnvironment_UnhandledExceptionRaiser;
            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.reminder_icon;
            BackgroundAggregator.Init(this);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App(typeof(Views.Home)));

            RegisterServices();

        }

        private void AndroidEnvironment_UnhandledExceptionRaiser(object sender, RaiseThrowableEventArgs e)
        {  //TODO:Logging
            e.Handled = true;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            //TODO:Logging
            e.SetObserved();
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
            IocRegistry.Register<ILocalDatabase, LocalDatabase>();
            //IocRegistry.Register<INotificationManager>(() =>
            //{
            //    var notification = new AndroidNotificationManager();
            //    notification.Initialize();
            //    return notification;
            //});
            IocRegistry.Register<ILocalizer, Localizer>();
            IocRegistry.Register<IShare, Share>();

        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {

            }
        }
        public override void OnTrimMemory([GeneratedEnum] TrimMemory level)
        {

            base.OnTrimMemory(level);
            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
            base.OnTrimMemory(level);
        }

    }
}