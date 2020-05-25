using AgeCal.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RateUsPage : AgeContentPage<RateUsViewModel>
    {
        public RateUsPage()
        {
            InitializeComponent();
            PageTitle = "Rate Us";
            ShowBottomNav = false;
            PageHasbackButton = true;
        }
    }
}