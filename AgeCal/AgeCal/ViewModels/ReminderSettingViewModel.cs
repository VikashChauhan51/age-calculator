using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Services;
using System;
using System.Linq;
using System.Windows.Input;

using Xamarin.Forms;

namespace AgeCal.ViewModels
{
    public class ReminderSettingViewModel : BaseViewModel
    {
        public ExclusiveRelayCommand SaveCommand { get; set; }
        private ReminderSetting item;
        private readonly IReminderSettingService _reminderSettingService;
        public ReminderSettingViewModel(IReminderSettingService reminderSettingService)
        {
            _reminderSettingService = reminderSettingService;
            item = null;
            SaveCommand = new ExclusiveRelayCommand(SaveInfo);
        }

        TimeSpan time;
        public TimeSpan Time
        {
            get { return time; }
            set
            {
                time = value;
                RaisePropertyChanged(nameof(Time));
            }
        }

        bool autoDeletePriorReminder;
        public bool AutoDeletePriorReminder
        {
            get { return autoDeletePriorReminder; }
            set
            {

                autoDeletePriorReminder = value;
                RaisePropertyChanged(nameof(AutoDeletePriorReminder));
            }
        }
        bool autoSetupReminder;
        public bool AutoSetupReminder
        {
            get { return autoSetupReminder; }
            set
            {

                autoSetupReminder = value;
                RaisePropertyChanged(nameof(AutoSetupReminder));
            }
        }

        public override void OnNavigationParameter(object parm)
        {

            LoadSetting();

        }
        private void SaveInfo()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                if (item == null)
                {
                    item = new ReminderSetting
                    {
                        Id = Guid.NewGuid().ToString(),
                        AutoSetupReminder = AutoSetupReminder,
                        AutoDeletePriorReminder = AutoDeletePriorReminder,
                        Time = Time,
                        UserId = string.Empty
                    };

                    _reminderSettingService.Add(item);
                }
                else
                {
                    item.AutoSetupReminder = AutoSetupReminder;
                    item.AutoDeletePriorReminder = AutoDeletePriorReminder;
                    item.Time = Time;
                    _reminderSettingService.Update(item);
                }

            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private void LoadSetting()
        {
            try
            {
                if (IsBusy)
                    return;
                IsBusy = true;

                item = null;
                item = _reminderSettingService.Gets(0, 1).FirstOrDefault();
                if (item != null)
                {
                    Time = item.Time;
                    AutoDeletePriorReminder = item.AutoDeletePriorReminder;
                    AutoSetupReminder = item.AutoSetupReminder;
                }

            }
            catch
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}