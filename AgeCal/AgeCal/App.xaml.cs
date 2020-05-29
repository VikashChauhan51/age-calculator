﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgeCal.Views;
using GalaSoft.MvvmLight.Ioc;
using AgeCal.Ioc;
using AgeCal.Services;
using AgeCal.Models;
using GalaSoft.MvvmLight.Views;
using AgeCal.Core;
using AgeCal.ViewModels;
using AgeCal.Interfaces;
using AgeCal.Repository;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AgeCal
{
    public partial class App : Application
    {

        private static NavigationPage navPage;
        /// <summary>
        /// Thread Safe Singleton without using locks
        /// </summary>
        private static Lazy<App> lazy = new Lazy<App>(() => new App());

        /// <summary>
        /// Explicit static constructor to tell C# compiler not to mark type as before field init.
        /// </summary>
        static App()
        {
        }

        public App()
        {
            InitializeComponent();

            InitApp(null);
        }

        public App(Type page)
        {
            InitializeComponent();

            InitApp(page);
        }

        private void InitApp(Type page)
        {
            navPage = new NavigationPage(page == null ? new MainPage() : new MainPage());
            MainPage = navPage;
            RegisterServices();
        }
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        internal static void RegisterServices()
        {

            SimpleIoc container = SimpleIoc.Default;
            var serviceLocator = new SimpleIocLocatorProvider(container);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => serviceLocator);
            IocRegistry.Register<IAppMessagingCenter, AppMessagingCenter>();
            //register repositories
            IocRegistry.Register<IUserRepository, UserRepository>();
            IocRegistry.Register<IReminderRepository, ReminderRepository>();
            //register navigation
            IocRegistry.Register<IAgeNavigationService>(NavigationFactory);
        }
        internal static IAgeNavigationService NavigationFactory()
        {
            AgeNavigationService navService = new AgeNavigationService(navPage);
            navService.RegisterPage<MainViewModel, MainPage>();
            navService.RegisterPage<AddViewModel, AddPopup>();
            navService.RegisterPage<HomeViewModel, Home>();
            navService.RegisterPage<AboutViewModel, AboutPage>();
            navService.RegisterPage<ItemsViewModel, ItemsPage>();
            navService.RegisterPage<ItemDetailViewModel, ItemDetailPage>();
            navService.RegisterPage<SettingViewModel, SettingPage>();
            navService.RegisterPage<ContactUsViewModel, ContactUsPage>();
            navService.RegisterPage<AppVersionViewModel, AppVersionPage>();
            navService.RegisterPage<UserManualViewModel, UserManualPage>();
            navService.RegisterPage<RateUsViewModel, RateUsPage>();
            navService.RegisterPage<PrivatePolicyViewModel, PrivatePolicyPage>();
            navService.RegisterPage<TermsViewModel, TermsPage>();
            navService.RegisterPage<ReminderListViewModel, ReminderListPage>();
            navService.RegisterPage<AddReminderViewModel, AddReminderPopup>();
            return navService;

        }
    }
}
