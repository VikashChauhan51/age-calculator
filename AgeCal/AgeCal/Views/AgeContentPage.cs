using AgeCal.Components;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public abstract class AgeContentPage<T> : AgeContentPage where T : BaseViewModel
    {
        public AgeContentPage() : base(LocateViewModel(),true)
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
                //TODO:

            }
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
        protected PageLayout MyProperty
        {
            get
            {
                if (_pageLayout == null)
                    _pageLayout = this.FindByName<PageLayout>("PageLayout");
                return _pageLayout;
            }
        }
        private BottomNavigationView _bottomNavigationView;
        protected BottomNavigationView BottomNavigationView
        {
            get
            {
                if (_bottomNavigationView == null)
                    _bottomNavigationView = this.FindByName<BottomNavigationView>("BottomNavigationView");
                return _bottomNavigationView;
            }
        }
        private TopNavigationView _topNavigationView;
        protected TopNavigationView TopNavigationView
        {
            get
            {
                if (_topNavigationView == null)
                    _topNavigationView = this.FindByName<TopNavigationView>("TopNavigationView");
                return _topNavigationView;
            }
        }
    }
}
