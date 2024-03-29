﻿using AgeCal.Components;
using AgeCal.Core;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public abstract class AgeContentPage<T> : AgeContentPage where T : BaseViewModel
    {
        public AgeContentPage() : base(LocateViewModel(), true)
        {

        }
        private static T LocateViewModel()
        {
            var vm = Ioc.IocRegistry.Locate<T>();
            if (vm == null)
            {
                Ioc.IocRegistry.Register<T>();
                vm = Ioc.IocRegistry.Locate<T>();
            }
            return vm;
        }

        public new T ViewModel
        {
            get
            {
                return (T)base.ViewModel;
            }
            set { base.ViewModel = value; }
        }
    }

    public abstract class AgeContentPage : ContentPage
    {
        protected BaseTopNavigationView NavigationBarView = null;

        protected View ContainerView;
        protected readonly bool ShowNavBar;
        public AgeContentPage() : this(null, true)
        {

        }

        public BaseViewModel ViewModel { get; set; }

        public AgeContentPage(BaseViewModel vm, bool showTopNavBar)
        {
            ControlTemplate = new ContentPageTemplate();
            ShowNavBar = showTopNavBar;
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, ShowNavBar);
            RenderTopNavBar(showTopNavBar);
            BindingContext = ViewModel = vm;
        }

        protected void RenderTopNavBar(bool visible)
        {
            if (ShowNavBar)
            {
                NavigationBarView = new TopNavigationView();

                NavigationPage.SetTitleView(this, NavigationBarView);
            }
            else
            {
                NavigationBarView = null;

            }
            AdjustNavigationBar();
        }


        private void AdjustNavigationBar()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if (!NavigationPage.GetHasNavigationBar(this) && ShowNavBar)
                {
                    NavigationPage.SetHasNavigationBar(this, ShowNavBar);

                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AdjustNavigationBar();
            if (ViewModel != null)
            {
                ViewModel.DisplayToast += HandleToast;
                ViewModel.PropertyChanged += OnViewModelPropertyChanged;
                ViewModel.DisplaySpinner += OnShowSpinner;
                if (NavigationBarView != null)
                    NavigationBarView.ButtonPressed = ViewModel.TopNavigationCommand;
                if (BottomNavigationView != null)
                {
                    BottomNavigationView.ButtonPressed = ViewModel.BottomNavigationCommand;
                    RenderBottomNavbar(BottomNavigationView.IsVisible);
                }

                if (!string.IsNullOrEmpty(ViewModel.Title))
                    PageTitle = ViewModel.Title;

                ShowSpinner = !ViewModel.IsReady || ViewModel.IsBusy;

                ViewModel.OnPageAppearing();

            }
        }

        public void HandleToast(object sender, Toast e)
        {
            PageLayout?.Toast(e.Message, e.Duration);

        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            if (ViewModel != null)
            {
                ViewModel.DisplayToast -= HandleToast;
                ViewModel.PropertyChanged -= OnViewModelPropertyChanged;
                ViewModel.DisplaySpinner -= OnShowSpinner;
                ViewModel.OnPageDisappearing();
            }
        }

        private void OnShowSpinner(object sender, bool e)
        {
            //TODO:
        }

        public string PageTitle
        {
            get { return NavigationBarView?.Title; }
            set
            {
                if (NavigationBarView != null)
                    NavigationBarView.Title = value;
            }
        }
        public string PageSubTitle
        {
            get { return NavigationBarView?.SubTitle; }
            set
            {
                if (NavigationBarView != null)
                    NavigationBarView.SubTitle = value;
            }
        }
        public string PageTitleIcon
        {
            get { return NavigationBarView?.TitleIcon; }
            set
            {
                if (NavigationBarView != null)
                    NavigationBarView.TitleIcon = value;
            }
        }
        public bool PageHasbackButton
        {
            get { return NavigationBarView?.HasBackButton ?? false; }
            set
            {
                if (NavigationBarView != null)
                    NavigationBarView.HasBackButton = value;
            }
        }

        private PageLayout _pageLayout;
        protected PageLayout PageLayout
        {
            get
            {
                if (_pageLayout == null)
                    _pageLayout = this.FindElementByName<PageLayout>("PageLayout");
                return _pageLayout;
            }
        }
        private BottomNavigationView _bottomNavigationView;
        protected BottomNavigationView BottomNavigationView
        {
            get
            {
                if (_bottomNavigationView == null)
                    _bottomNavigationView = this.FindElementByName<BottomNavigationView>("BottomNavigationView");
                return _bottomNavigationView;
            }
        }
        private TopNavigationView _topNavigationView;
        protected TopNavigationView TopNavigationView
        {
            get
            {
                if (_topNavigationView == null)
                    _topNavigationView = this.FindElementByName<TopNavigationView>("TopNavigationView");
                return _topNavigationView;
            }
        }

        public static readonly BindableProperty ShowBottomNavProperty = BindableProperty.Create(
            nameof(ShowBottomNav),
            typeof(bool),
            typeof(AgeContentPage),
            true,
            propertyChanged: (bindable, oldV, newV) => ((AgeContentPage)bindable).UpdateShowBottomNav((bool)oldV, (bool)newV));
        public bool ShowBottomNav { get { return (bool)GetValue(ShowBottomNavProperty); } set { SetValue(ShowBottomNavProperty, value); } }

        protected virtual void UpdateShowBottomNav(bool oldV, bool newV)
        {
            ShowBottomNav = newV;
            RenderBottomNavbar(ShowBottomNav);
        }
        void RenderBottomNavbar(bool visible)
        {
            if (BottomNavigationView != null)
            {
                BottomNavigationView.IsVisible = visible;
                BottomNavigationView.HasVisibility = visible;
                if (visible && ViewModel != null)
                    BottomNavigationView.SetActiveIcon(ViewModel.GetType());
            }

        }
        public static readonly BindableProperty ShowSpinnerProperty = BindableProperty.Create(
            nameof(ShowSpinner),
            typeof(bool),
            typeof(AgeContentPage),
            true,
            propertyChanged: (bindable, oldV, newV) => ((AgeContentPage)bindable).UpdateShowSpinner((bool)oldV, (bool)newV));
        public bool ShowSpinner { get { return (bool)GetValue(ShowSpinnerProperty); } set { SetValue(ShowSpinnerProperty, value); } }

        protected virtual void UpdateShowSpinner(bool oldV, bool newV)
        {
            if (oldV != newV)
            {
                ShowSpinner = newV;
                IsBusy = ShowSpinner;
                if (newV)
                    PageLayout?.ShowSpinner();
                else
                    PageLayout?.HideSpinner();
            }

        }


        protected virtual void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    switch (e.PropertyName)
                    {
                        case (nameof(ViewModel.IsReady)):
                        case (nameof(ViewModel.IsBusy)):
                            ShowSpinner = !ViewModel.IsReady || ViewModel.IsBusy;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {


                }
            });
        }

    }
}
