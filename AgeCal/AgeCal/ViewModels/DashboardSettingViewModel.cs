using AgeCal.Core;
using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace AgeCal.ViewModels
{
    public class DashboardSettingViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand SaveCommand { get; set; }
        private readonly IDashboardSettingRepository _dashboardSettingRepository;
        private DashboardSetting setting;
        public DashboardSettingViewModel(IDashboardSettingRepository dashboardSettingRepository)
        {
            setting = null;
            _dashboardSettingRepository = dashboardSettingRepository;
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
                    if (setting == null)
                    {
                        setting = new DashboardSetting
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = string.Empty,
                            DisplayType = selectedType.Key,
                            Count = Count
                        };
                        _dashboardSettingRepository.Add(setting);
                    }
                    else
                    {
                        setting.DisplayType = selectedType.Key;
                        setting.Count = Count;
                        _dashboardSettingRepository.Update(setting);

                    }

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
            LoadData();

        }

        private void BindTypeList()
        {
            Items.Clear();
            foreach (var item in GetItems())
                Items.Add(item);

        }
        private IEnumerable<Item> GetItems()
        {
            yield return new Item { Key = 0, Text = "Today" };
            yield return new Item { Key = 1, Text = "Weekly" };
            yield return new Item { Key = 2, Text = "Monthly" };
        }
        private void LoadData()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    setting = _dashboardSettingRepository.GetAll(0, 1).FirstOrDefault();
                    if (setting != null)
                    {
                        SelectedType = GetItems().FirstOrDefault(x => x.Key == setting.DisplayType);
                        Count = setting.Count;
                    }

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
    }
}
