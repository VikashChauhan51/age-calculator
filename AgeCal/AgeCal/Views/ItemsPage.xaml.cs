using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgeCal.Models;
using AgeCal.Views;
using AgeCal.ViewModels;
using AgeCal.Ioc;
using AgeCal.Core;

namespace AgeCal.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ItemsPage : AgeContentPage<ItemsViewModel>
    {


        public ItemsPage() : base()
        {
            InitializeComponent();

            PageTitle = "Data";

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Item;
            if (item == null)
                return;

            if (ViewModel != null)
                IocRegistry.Locate<IAgeNavigationService>().NavigateTo<ItemDetailViewModel>(item.Id);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }




    }
}