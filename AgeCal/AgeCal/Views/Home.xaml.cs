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
	public partial class Home :AgeContentPage<HomeViewModel>
    {
		public Home ():base()
		{
			InitializeComponent();
            PageTitle = "Dashboard";
            if (ViewModel!=null)
            {
                ShowSpinner = !ViewModel.IsReady || ViewModel.IsBusy;
                ViewModel.SetBirthday();
            }
           
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
    }
}