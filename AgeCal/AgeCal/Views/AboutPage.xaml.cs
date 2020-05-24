using AgeCal.ViewModels;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AboutPage : AgeContentPage<AboutViewModel>
    {
        public AboutPage():base()
        {
            InitializeComponent();
            PageTitle = "About";
            ShowBottomNav = true;
        }
    }
}