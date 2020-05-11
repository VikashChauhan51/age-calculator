using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.Views
{
    public abstract class AgeContentPage<T> : AgeContentPage where T : BaseViewModel
    {
        public AgeContentPage() : base(LocateViewModel())
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

        public AgeContentPage() : this(null)
        {

        }

        public BaseViewModel ViewModel { get; set; }

        public AgeContentPage(BaseViewModel vm)
        {
            BindingContext = ViewModel = vm;
        }
    }
}
