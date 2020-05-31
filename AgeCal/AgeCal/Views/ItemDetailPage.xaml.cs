using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;
using AgeCal.i18n;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemDetailPage : AgeContentPage<ItemDetailViewModel>
    {


        public ItemDetailPage() : base()
        {
            InitializeComponent();
            if (ViewModel != null)
            {
                ShowSpinner = ViewModel.IsReady;

            }
            PageHasbackButton = true;
            ShowBottomNav = false;

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
                            ShowSpinner = !ViewModel.IsReady;
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

        async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert(AppResource.Confirmation, AppResource.DeletePopupMessage, AppResource.Yes, AppResource.No);
            if (answer)
            {
                ViewModel.DeleteCommand.Execute(null);
            }

        }
    }
}