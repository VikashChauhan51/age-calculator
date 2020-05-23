using System;
using System.Diagnostics;
using System.Threading.Tasks;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Utilities;
using Xamarin.Essentials;

namespace AgeCal.ViewModels
{
    public class ItemDetailViewModel : BaseViewModel
    {
        public ExclusiveRelayCommand DeleteCommand { get; set; }
        public ExclusiveRelayCommand ShareCommand { get; set; }
        private readonly IUserRepository _userRepository;
        public ItemDetailViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
                //  NotificationManager.ScheduleNotification(new Reminder { Date = DateTime.Now, Time = new TimeSpan(0, 2, 0), Id = 2, Title = "Birthday", Message = "Today is your birthday" });
                await Share.RequestAsync(new ShareTextRequest
                {
                    Text = string.Format("{0}:{1}\n{2}", name, age, nextBirthday),
                    Title = "Age Calculation"
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
                        _userRepository.Delete(item);

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
