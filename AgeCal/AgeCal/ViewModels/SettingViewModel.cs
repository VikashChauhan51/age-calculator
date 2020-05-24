using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace AgeCal.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        public ObservableCollection<Setting> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public SettingViewModel()
        {
            Items = new ObservableCollection<Setting>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
        }

        private void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();

                foreach (var item in GetSettings())
                    Items.Add(item);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            ExecuteLoadItemsCommand();
        }
        private static IEnumerable<Setting> GetSettings()
        {
            yield return new Setting { Title = "Dashboard", ViewModel = typeof(DashboardSettingViewModel) };
            yield return new Setting { Title = "Reminder", ViewModel = typeof(ReminderSettingViewModel) };
        }
    }
}
