using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Services;
using AgeCal.Utilities;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.ViewModels
{
    public class AddReminderViewModel : BaseViewModel
    {

        public ExclusiveRelayCommand SaveCommand { get; set; }
        public ExclusiveRelayCommand CloseCommand { get; set; }
        private readonly IReminderService _reminderService;
        private readonly IUserService _userService;
        public AddReminderViewModel(IReminderService reminderService, IUserService userService)
        {
            _reminderService = reminderService;
            _userService = userService;
            SaveCommand = new ExclusiveRelayCommand(SaveInfo);
            CloseCommand = new ExclusiveRelayCommand(ClosePopup);
            Reset();
        }

        private void Reset()
        {
            selectedUser = null;
            ValidationMessage = string.Empty;
            CustomMessage = string.Empty;
            Time = new TimeSpan();
            HasError = false;
            HasMore = false;
            NotifySameDay = false;
            NotifyDayBefore = false;
            NotifyWeekBefore = false;
            NotifyMonthBefore = false;
        }

        public IEnumerable<User> ExecuteLoadUsers(string name)
        {
            if (IsBusy)
                return null;

            IsBusy = true;

            try
            {
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
                    return _userService.Gets(name, 0, 10);
                else
                    return _userService.Gets(0, 10);


            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
            return null;
        }

        private void ClosePopup()
        {
            Reset();
            NavigationService.GoBackModel();

        }

        private void SaveInfo()
        {
            HasError = SelectedUser == null;
            try
            {
                if (HasError)
                    return;

                if (!IsBusy)
                {
                    IsBusy = true;
                    var today = DateTime.Now.Date;
                    var nextBirthday = BirthdayHelper.GetNextBirthday(BirthdayHelper.GetDate(SelectedUser.DOB, SelectedUser.Time));
                    var maxId = _reminderService.GetMaxId();
                    var reminders = new List<Reminder>();
                    if (NotifySameDay)
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = SelectedUser.Id,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = !string.IsNullOrEmpty(CustomMessage?.Trim()) ? CustomMessage.Trim() : $"Today,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = AddTime(nextBirthday.Date)
                        };
                        reminders.Add(item);
                    }
                    if (notifyDayBefore && today <= nextBirthday.AddDays(-1).Date)
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = SelectedUser.Id,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = !string.IsNullOrEmpty(CustomMessage?.Trim()) ? CustomMessage.Trim() : $"Tomorrow,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = AddTime(nextBirthday.AddDays(-1).Date)
                        };
                        reminders.Add(item);
                    }
                    if (NotifyWeekBefore && today <= nextBirthday.AddDays(-7).Date)
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = SelectedUser.Id,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = !string.IsNullOrEmpty(CustomMessage?.Trim()) ? CustomMessage.Trim() : $"{SelectedUser.Text} has birthday on {nextBirthday.AddDays(-7).ToLocalTime().ToString()}.",
                            Active = true,
                            When = AddTime(nextBirthday.AddDays(-7).Date)
                        };
                        reminders.Add(item);
                    }
                    if (NotifyMonthBefore && today <= nextBirthday.AddMonths(-1).Date)
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = SelectedUser.Id,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = !string.IsNullOrEmpty(CustomMessage?.Trim()) ? CustomMessage.Trim() : $"{SelectedUser.Text} has birthday on {nextBirthday.AddMonths(-1).ToLocalTime().ToString()}.",
                            Active = true,
                            When = AddTime(nextBirthday.AddMonths(-1).Date)
                        };
                        reminders.Add(item);
                    }
                    if (!reminders.Any())
                    {
                        ValidationMessage = "Invald information";
                        return;
                    }
                    ValidationMessage = string.Empty;
                    //save reminder into database
                    _reminderService.Add(reminders);
                    //notify reminder list about new reminder
                    MessageService.Send<Reminder>(reminders.FirstOrDefault());
                    Reset();
                    NavigationService.GoBackModel(new Core.Toast { Message = AppResource.DataSaveMessage });


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

        User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                if (value != null && HasError)
                    HasError = false;
                RaisePropertyChanged(nameof(SelectedUser));
            }
        }
        string customMessage = string.Empty;
        public string CustomMessage
        {
            get { return customMessage; }
            set
            {

                customMessage = value;
                RaisePropertyChanged(nameof(CustomMessage));
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

        bool hasMore;
        public bool HasMore
        {
            get { return hasMore; }
            set
            {

                hasMore = value;
                RaisePropertyChanged(nameof(HasMore));
            }
        }
        bool hasError;
        public bool HasError
        {
            get { return hasError; }
            set
            {

                hasError = value;
                RaisePropertyChanged(nameof(HasError));
            }
        }


        string validationMessage;
        public string ValidationMessage
        {
            get { return validationMessage; }
            set
            {

                validationMessage = value;
                RaisePropertyChanged(nameof(ValidationMessage));
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

        private DateTimeOffset AddTime(DateTimeOffset date)
        {

            // creating object of  DateTimeOffset 
            DateTimeOffset offset = new DateTimeOffset(date.Year,
                    date.Month, date.Day, 0, 0, 0, date.Offset);

            return offset.Add(Time);
        }
    }
}
