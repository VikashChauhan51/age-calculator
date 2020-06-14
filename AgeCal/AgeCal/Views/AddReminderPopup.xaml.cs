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
    public partial class AddReminderPopup : FullPagePopup<AddReminderViewModel>
    {
        public AddReminderPopup()
        {
            InitializeComponent();

        }


        private void SearchUser_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (ViewModel != null)
                {
                    if (ViewModel.IsBusy)
                        return;

                    var text = e.NewTextValue.ToLower();
                    var length = text.Trim().Length;
                    if (length == 0)
                        ViewModel.SelectedUser = null;

                    if (length != 0 && length < 3)
                        return;

                    UserListView.IsVisible = true;
                    SecondryContainer.IsVisible = false;
                    UserListView.BeginRefresh();

                    var dataEmpty = ViewModel.ExecuteLoadUsers(text);

                    if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    {
                        UserListView.IsVisible = false;
                        SecondryContainer.IsVisible = true;
                        
                    }
                       
                    else if (dataEmpty == null || !dataEmpty.Any())
                    {
                       
                        UserListView.IsVisible = false;
                        SecondryContainer.IsVisible = true;
                    }
                      
                    else
                        UserListView.ItemsSource = dataEmpty;
                }
            }
            catch
            {
                UserListView.IsVisible = false;
                SecondryContainer.IsVisible = true;
            }
            UserListView.EndRefresh();
        }

        private void UserListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (ViewModel != null)
            {
                if (ViewModel.IsBusy)
                    return;

                if (!(e.Item is User item))
                    return;

                SearchUser.Text = item.Text;
                ViewModel.SelectedUser = item;
                UserListView.IsVisible = false;
                SecondryContainer.IsVisible = true;
                // Manually deselect item.
                UserListView.SelectedItem = null;
            }
        }
    }
}