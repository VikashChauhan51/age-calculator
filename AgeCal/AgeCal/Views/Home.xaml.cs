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
    public partial class Home : AgeContentPage<HomeViewModel>
    {
        public Home() : base()
        {
            InitializeComponent();
            ShowBottomNav = true;
        }

    }
}