using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeCal.ViewModels
{
    public class ReminderSettingViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand SaveCommand { get; set; }
        private readonly IReminderSettingRepository _reminderSettingRepository;

        private ReminderSetting item;
        public ReminderSettingViewModel(IReminderSettingRepository reminderSettingRepository)
        {
            item = null;
            _reminderSettingRepository = reminderSettingRepository;
            SaveCommand = new ExclusiveRelayCommand(SaveInfo);


        }

        private void SaveInfo()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    if (item == null)
                    {
                        item = new ReminderSetting
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserId = string.Empty,
                            Enable = true,
                            NotifySameDay = NotifySameDay,
                            NotifyDayBefore = NotifyDayBefore,
                            NotifyWeekBefore = NotifyWeekBefore,
                            NotifyMonthBefore = NotifyMonthBefore,
                            NumberOfNotification = 5
                        };
                        _reminderSettingRepository.Add(item);
                    }
                    else
                    {
                        item.NotifySameDay = NotifySameDay;
                        item.NotifyDayBefore = NotifyDayBefore;
                        item.NotifyWeekBefore = NotifyWeekBefore;
                        item.NotifyMonthBefore = NotifyMonthBefore;
                        _reminderSettingRepository.Update(item);

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
 
        bool notifySameDay;
        public bool NotifySameDay
        {
            get { return notifySameDay; }
            set
            {

                notifySameDay = value;
                RaisePropertyChanged(nameof(NotifySameDay));
            }
        }

        bool notifyDayBefore;
        public bool NotifyDayBefore
        {
            get { return notifyDayBefore; }
            set
            {

                notifyDayBefore = value;
                RaisePropertyChanged(nameof(NotifyDayBefore));
            }
        }

        bool notifyWeekBefore;
        public bool NotifyWeekBefore
        {
            get { return notifyWeekBefore; }
            set
            {

                notifyWeekBefore = value;
                RaisePropertyChanged(nameof(NotifyWeekBefore));
            }
        }

        bool notifyMonthBefore;
        public bool NotifyMonthBefore
        {
            get { return notifyMonthBefore; }
            set
            {

                notifyMonthBefore = value;
                RaisePropertyChanged(nameof(NotifyMonthBefore));
            }
        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            LoadData();

        }
        private void LoadData()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    item = _reminderSettingRepository.GetAll(0, 1).FirstOrDefault();
                    if (item != null)
                    {
                        NotifySameDay = item.NotifySameDay;
                        NotifyDayBefore = item.NotifyDayBefore;
                        NotifyWeekBefore = item.NotifyWeekBefore;
                        NotifyMonthBefore = item.NotifyMonthBefore;

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
