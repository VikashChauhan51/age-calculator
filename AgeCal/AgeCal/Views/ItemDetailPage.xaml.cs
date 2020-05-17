using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.ViewModels;
using System.ComponentModel;

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

    }
}