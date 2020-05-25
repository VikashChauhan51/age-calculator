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
    public partial class PrivatePolicyPage : AgeContentPage<PrivatePolicyViewModel>
    {
        public PrivatePolicyPage()
        {
            InitializeComponent();
            PageTitle = "Private Policy";
            ShowBottomNav = false;
            PageHasbackButton = true;
        }
    }
}