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

namespace AgeCal.Droid
{
    [Activity(Label = "Age Calculator", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(App.Instance);

            RegisterServices();

            //Init Database
            IocRegistry.Locate<ILocalDatabase>();
        }

        void RegisterServices()
        {
            IocRegistry.Register<ILocalDatabase>(() =>
            {
                var db = new SqliteDatabase();
                db.Initialize();
                return db;
            });
        }
    }
}