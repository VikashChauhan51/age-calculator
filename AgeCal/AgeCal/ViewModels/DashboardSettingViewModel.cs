using AgeCal.Core;
using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AgeCal.ViewModels
{
    public class DashboardSettingViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand SaveCommand { get; set; }
        private readonly IUserRepository _userRepository;

        public DashboardSettingViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            SaveCommand = new ExclusiveRelayCommand(SaveInfo);
            Items = new ObservableCollection<Item>();

        }


        ObservableCollection<Item> items;
        public ObservableCollection<Item> Items
        {
            get { return items; }
            set
            {
                items = value;
                RaisePropertyChanged(nameof(Items));
            }
        }
        Item selectedType;
        public Item SelectedType
        {
            get { return selectedType; }
            set
            {
                selectedType = value;
                RaisePropertyChanged(nameof(SelectedType));
            }
        }
        int count = 1;
        public int Count
        {
            get { return count; }
            set
            {
                if (value < 1)
                    count = 1;
                else
                    count = value;
                RaisePropertyChanged(nameof(Count));
            }
        }
        private void SaveInfo()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    var setting = new DashboardSetting
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = string.Empty,
                        DisplayType = selectedType.Key,
                        Count = Count
                    };

                }
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
            BindTypeList();
            SelectedType = new Item { Key = 0, Text = "Today" };

        }

        private void BindTypeList()
        {
            Items.Clear();
            Items.Add(new Item { Key = 0, Text = "Today" });
            Items.Add(new Item { Key = 1, Text = "Weekly" });
            Items.Add(new Item { Key = 2, Text = "Monthly" });

        }
    }
}
