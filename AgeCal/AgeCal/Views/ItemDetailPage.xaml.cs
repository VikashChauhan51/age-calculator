using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.ViewModels;
using System.ComponentModel;
using System.Threading.Tasks;

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
            PageTitle = "Details";
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
            bool answer = await DisplayAlert("Confirmation", "Are you sure you want to delete?", "Yes", "No");
            if (answer)
            {
                ViewModel.DeleteCommand.Execute(null);
            }

        }
    }
}