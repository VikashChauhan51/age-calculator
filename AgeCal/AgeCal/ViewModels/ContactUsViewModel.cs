using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace AgeCal.ViewModels
{
    public class ContactUsViewModel : BaseViewModel
    {
        public ContactUsViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://xamarin.com/platform")));
        }

        public ICommand OpenWebCommand { get; }
    }
}