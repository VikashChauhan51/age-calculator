using AgeCal.Core;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : AgeContentPage<SettingViewModel>
    {
        public SettingPage()
        {
            InitializeComponent();
            PageTitle = "Settings";
            if (ViewModel != null)
            {
                ShowSpinner = !ViewModel.IsReady || ViewModel.IsBusy;

            }
            ShowBottomNav = true;
        }

        protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    base.OnViewModelPropertyChanged(sender, e);
                    switch (e.PropertyName)
                    {
                        case (nameof(ViewModel.IsReady)):
                            ShowSpinner = !ViewModel.IsReady || ViewModel.IsBusy; ;
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

        private void SettingListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Setting;
            if (item == null)
                return;

            if (ViewModel != null)
            {
                var navigation = IocRegistry.Locate<IAgeNavigationService>();
                var key = navigation.GetKey(item.ViewModel);
                navigation.NavigateTo(key, true);
            }


            // Manually deselect item.
            SettingListView.SelectedItem = null;
        }
    }
}