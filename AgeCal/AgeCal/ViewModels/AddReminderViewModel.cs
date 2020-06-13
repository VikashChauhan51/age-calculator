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
        public ExclusiveRelayCommand LoadMoreCommand { get; set; }
        public ExclusiveRelayCommand CloseCommand { get; set; }
        private readonly IReminderService _reminderService;
        private readonly IUserService _userService;
        public AddReminderViewModel(IReminderService reminderService, IUserService userService)
        {
            _reminderService = reminderService;
            _userService = userService;
            Items = new ObservableCollection<User>();
            SaveCommand = new ExclusiveRelayCommand(SaveInfo);
            CloseCommand = new ExclusiveRelayCommand(ClosePopup);
            LoadMoreCommand = new ExclusiveRelayCommand(LoadMore);
            Reset();
        }

        private void Reset()
        {
            selectedUser = null;
            Items?.Clear();
            ValidationMessage = string.Empty;
            HasError = false;
            HasMore = false;
            NotifySameDay = false;
            NotifyDayBefore = false;
            NotifyWeekBefore = false;
            NotifyMonthBefore = false;
        }

        void ExecuteLoadUsers()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = _userService.Gets(0, 100);
                HasMore = items != null && items.Count() == 100;
                RenderData(items);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }
        private void LoadMore()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {


                var items = _userService.Gets(Items.Count, 10);
                HasMore = items != null && items.Count() == 10;
                RenderData(items);
            }
            catch (Exception ex)
            {


            }
            finally
            {
                IsBusy = false;
            }
        }

        private void ClosePopup()
        {
            Reset();
            NavigationService.GoBackModel();

        }
        private void RenderData(IEnumerable<User> items)
        {
            HasMore = items != null && items.Count() >= 10;
            if (items != null)
            {
                foreach (var item in items)
                    Items.Add(item);
            }

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
                    //Delete prior reminder of user if any.
                    DeletePriorReminder(SelectedUser.Id);
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
                            Message = $"Today,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = nextBirthday
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
                            Message = $"Tomorrow,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = nextBirthday.AddDays(-1)
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
                            Message = $"{SelectedUser.Text} has birthday on {nextBirthday.AddDays(-7).ToLocalTime().ToString()}.",
                            Active = true,
                            When = nextBirthday.AddDays(-7)
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
                            Message = $"{SelectedUser.Text} has birthday on {nextBirthday.AddMonths(-1).ToLocalTime().ToString()}.",
                            Active = true,
                            When = nextBirthday.AddMonths(-1)
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

        private ObservableCollection<User> items;
        public ObservableCollection<User> Items
        {
            get { return items; }
            set
            {
                items = value;
                if (items == null || items.Count == 0)
                    ExecuteLoadUsers();

                RaisePropertyChanged(nameof(Items));
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
                if (HasMore)
                    LoadMore();
                RaisePropertyChanged(nameof(SelectedUser));
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


        private void DeletePriorReminder(string userId)
        {
            try
            {
                _reminderService.DeleteReminders(userId);
            }
            catch (Exception ex)
            {


            }
        }

    }
}
