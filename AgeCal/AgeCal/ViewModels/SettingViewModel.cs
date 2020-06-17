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
        public ObservableCollection<SettingList> SettingSource { get; set; }
        public Command LoadItemsCommand { get; set; }
        public SettingViewModel()
        {
            SettingSource = new ObservableCollection<SettingList>();
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
        }

        private void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                SettingSource.Clear();
                
                var support = new SettingList()
                {
                          new Setting { Title = "Contact Us", ViewModel = typeof(ContactUsViewModel) },
                          new Setting { Title = "App Version", ViewModel = typeof(AppVersionViewModel) },
                         // new Setting { Title = "About Us", ViewModel = typeof(AboutViewModel) },
                          //new Setting { Title = "User Manual", ViewModel = typeof(UserManualViewModel) },
                          new Setting { Title = "Reminder Settings", ViewModel = typeof(ReminderSettingViewModel) }
                };
                support.Heading = "SUPPORT";

                var documents = new SettingList()
                {
                          new Setting { Title = "Private Policy", ViewModel = typeof(PrivatePolicyViewModel) },
                          new Setting { Title = "Terms & Conditions", ViewModel = typeof(TermsViewModel) }
                };
                documents.Heading = "LEGAL DOCUMENTS";
                 
                SettingSource.Add(support);
                SettingSource.Add(documents);
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

    }
}
