using AgeCal.i18n;
using AgeCal.Interfaces;
using AgeCal.Ioc;
using AgeCal.Models;
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
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        public AddReminderViewModel(IReminderRepository reminderRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _reminderRepository = reminderRepository;
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
                var items = _userRepository.GetAll(0, 10);
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
        private void LoadMore()
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {


                var items = _userRepository.GetAll(Items.Count, 10);
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
                    var today = DateTime.Now;
                    var nextBirthday = BirthdayHelper.GetNextBirthday(BirthdayHelper.GetDate(SelectedUser.DOB, SelectedUser.Time));
                    var maxId = _reminderRepository.GetRemindeMaxId();
                    var reminders = new List<Reminder>();
                    if (NotifySameDay)
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = string.Empty,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = $"Today,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = nextBirthday
                        };
                        reminders.Add(item);
                    }
                    if (notifyDayBefore && today <= nextBirthday.AddDays(-1))
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = string.Empty,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = $"Tomorrow,{SelectedUser.Text} has birthday.",
                            Active = true,
                            When = nextBirthday.AddDays(-1)
                        };
                        reminders.Add(item);
                    }
                    if (NotifyWeekBefore && today <= nextBirthday.AddDays(-7))
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = string.Empty,
                            Tag = SelectedUser.Id,
                            Title = $"{SelectedUser.Text } Birthday",
                            Message = $"{SelectedUser.Text} has birthday on {nextBirthday.AddDays(-7).ToLocalTime().ToString()}.",
                            Active = true,
                            When = nextBirthday.AddDays(-7)
                        };
                        reminders.Add(item);
                    }
                    if (NotifyMonthBefore && today <= nextBirthday.AddMonths(-1))
                    {
                        var item = new Reminder
                        {
                            ReminderId = Guid.NewGuid().ToString(),
                            Id = maxId + reminders.Count + 1,
                            UserId = string.Empty,
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
                    //set reminders before save into database
                    foreach (var item in reminders)
                        CrossLocalNotifications.Current.Show(item.Title, item.Message, item.Id, item.When.LocalDateTime);
                    //save reminder into database
                    _reminderRepository.Add(reminders);
                    //notify reminder list about new reminders
                    MessageService.Send<IEnumerable<Reminder>>(reminders);
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
                var today = DateTime.Now;
                var reminders = _reminderRepository.GetReminders(userId);
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

    }
}
