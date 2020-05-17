using AgeCal.Components;
using AgeCal.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Views
{
    public abstract class AgePopup<T> : AgePopup where T : BaseViewModel
    {
        public AgePopup() : base()
        {
            var vm = LocateViewModel();
            if (vm != null)
            {
                BindingContext = ViewModel = vm;
            }
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

    public class AgePopup : PopupPage
    {
        public AgePopup() : base()
        {
            HasSystemPadding = true;
        }
        public BaseViewModel ViewModel { get; set; }
        private PageLayout _pageLayout;
        protected PageLayout PageLayout
        {
            get
            {
                if (_pageLayout == null)
                    _pageLayout =this.FindElementByName<PageLayout>("PageLayout");
                return _pageLayout;
            }
        }

        protected void Toast(string message, int duration = 3000)
        {
            //TODO:
        }
        protected void ShowSpinner() => PageLayout?.ShowSpinner();
        protected void HideSpinner() => PageLayout?.HideSpinner();
        public void PageAppeared()
        {
            this.OnAppearingAnimationEnd();
        }
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }
        public void Disappeared()
        {
            this.OnDisappearingAnimationEnd();
        }
        

    }
}
