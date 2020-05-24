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
	public partial class DashboardSettingPage : AgeContentPage<DashboardSettingViewModel>
    {
		public DashboardSettingPage()
		{
			InitializeComponent();
            PageTitle = "Dashboard Settings";
            PageHasbackButton = true;
            ShowBottomNav = false;
        }
        
    }
}