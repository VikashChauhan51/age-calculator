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
    public partial class ReminderListPage : AgeContentPage<ReminderListViewModel>
    {
        public ReminderListPage()
        {
            InitializeComponent();
            if (ViewModel != null)
            {
                ShowSpinner = !ViewModel.IsReady;

            }
            PageTitle = "Reminders";
            ShowBottomNav = true;
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as User;
            if (item == null)
                return;

            //if (ViewModel != null)
            //    IocRegistry.Locate<IAgeNavigationService>().NavigateTo<ItemDetailViewModel>(item);

            // Manually deselect item.
            ItemsListView.SelectedItem = null;
        }

        protected override void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    base.OnViewModelPropertyChanged(sender, e);
                    switch (e.PropertyName)
                    {
                        case (nameof(ViewModel.IsReady)):
                            ShowSpinner = !ViewModel.IsReady;
                            break;
                        default:
                            break;
                    }
                }
                catch
                {


                }
            });

        }

        private void ItemsListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (ViewModel == null || ViewModel.IsBusy || ViewModel.Items.Count == 0)
                return;

            if (ViewModel.HasMore)
            {
                var reminder = e.Item as Reminder;
                if (reminder != null && reminder == ViewModel.Items.LastOrDefault())
                    ViewModel.LoadMoreItemsCommand.Execute(null);
            }

        }
    }
}