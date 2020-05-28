using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AgeCal.Core;
using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Utilities;
using Plugin.LocalNotifications;
using Xamarin.Essentials;

namespace AgeCal.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ExclusiveRelayCommand DeleteCommand { get; set; }
        public ExclusiveRelayCommand ShareCommand { get; set; }
        private readonly IUserRepository _userRepository;
        private readonly IReminderRepository _reminderRepository;
        public ItemDetailViewModel(IUserRepository userRepository, IReminderRepository reminderRepository)
        {
            _userRepository = userRepository;
            _reminderRepository = reminderRepository;
            DeleteCommand = new ExclusiveRelayCommand(Delete);
            ShareCommand = new ExclusiveRelayCommand(ShareData);
        }

        private async void ShareData()
        {
            try
            {
                var date = new DateTime(DOB.Year, DOB.Month, DOB.Day, Time.Hours, Time.Minutes, Time.Seconds);
                var nextBirthday = BirthdayHelper.GetDateToMessage(date);
                var age = BirthdayHelper.GetCurrentAge(date);
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = string.Format("{0}:{1}\n{2}", name, age, nextBirthday),
                    Title = AppResource.AppName
                });
            }
            catch (Exception ex)
            {

            }
        }

        private void Delete()
        {
            try
            {
                if (!IsBusy)
                {
                    IsBusy = true;
                    var item = _userRepository.Get(Id);
                    if (item != null)
                    {
                        DeleteUserReminders();
                        _userRepository.Delete(item);
                    }


                    NavigationService.GoBack();
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

        private void DeleteUserReminders()
        {
            try
            {
                var today = DateTime.Now;
                var reminders = _reminderRepository.GetReminders(id);
                if (reminders != null && reminders.Any())
                {
                    //removed schedule reminder if any
                    foreach (var item in reminders)
                        if (item.When.LocalDateTime > today)
                            CrossLocalNotifications.Current.Cancel(item.Id);

                    _reminderRepository.Delete(reminders);

                }

            }
            catch (Exception ex)
            {


            }
        }

        string id = string.Empty;
        public string Id
        {
            get { return id; }
            set
            {
                id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }
        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }
        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                RaisePropertyChanged(nameof(Description));
            }
        }

        DateTime dob = DateTime.Now;
        public DateTime DOB
        {
            get { return dob; }
            set
            {
                dob = value;
                RaisePropertyChanged(nameof(DOB));
            }
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

        public override void OnNavigationParameter(object parm)
        {
            if (parm is User item)
            {
                Id = item.Id;
                Name = item.Text;
                Description = item.Description;
                DOB = item.DOB;
                Time = item.Time;
            }

        }

        public override void OnPageAppearing()
        {
            base.OnPageAppearing();


        }

        void ExecuteLoadCommand(string id)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var item = _userRepository.Get(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Text;
                    Description = item.Description;
                    DOB = item.DOB;
                    Time = item.Time;
                }


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;

            }
        }


    }
}
