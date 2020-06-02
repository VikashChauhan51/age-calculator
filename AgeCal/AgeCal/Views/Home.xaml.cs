using AgeCal.Core;
using AgeCal.Ioc;
using AgeCal.Models;
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

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            if (item == null)
                return;

            if (ViewModel != null)
                IocRegistry.Locate<IAgeNavigationService>().NavigateTo<ItemDetailViewModel>(item);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }
    }
}