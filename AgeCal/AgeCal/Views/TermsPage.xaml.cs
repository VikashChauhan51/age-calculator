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
    public partial class TermsPage : AgeContentPage<TermsViewModel>
    {
        public TermsPage()
        {
            InitializeComponent();
            PageTitle = "Terms & Conditions";
            ShowBottomNav = false;
            PageHasbackButton = true;
        }
    }
}