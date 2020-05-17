using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        public MainViewModel()
        {
            Title = "Age Calculator";

          
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            NavigationService.NavigateTo<HomeViewModel>();
        }

    }
}
