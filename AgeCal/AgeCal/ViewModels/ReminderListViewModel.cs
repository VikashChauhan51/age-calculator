using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using AgeCal.Models;
using AgeCal.Views;
using AgeCal.Interfaces;
using System.Linq;
using System.Collections.Generic;
using Plugin.LocalNotifications;

namespace AgeCal.ViewModels
{
    public class ReminderListViewModel : BaseViewModel
    {
        public ObservableCollection<Reminder> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command LoadMoreItemsCommand { get; set; }
        public Command AddCommand { get; set; }
        public Command DeleteCommand { get; set; }
        private readonly IReminderRepository _reminderRepository;
        public ReminderListViewModel(IReminderRepository reminderRepository)
        {
            Title = "Reminders";
            Items = new ObservableCollection<Reminder>();
            _reminderRepository = reminderRepository;
            LoadItemsCommand = new Command(ExecuteLoadItemsCommand);
            LoadMoreItemsCommand = new Command(LoadMore);
            AddCommand = new Command(NavigateOnAddPage);
            DeleteCommand = new Command(Delete);
            MessageService.Register<IEnumerable<Reminder>>(this, AddedReminder);
        }

        private void Delete(object obj)
        {
            var reminder = obj as Reminder;
            if (reminder != null)
            {
                DeletePriorReminder(reminder);
                _reminderRepository.Delete(reminder);
                Items.Remove(reminder);

            }
        }

        private void DeletePriorReminder(Reminder reminder)
        {
            try
            {
                var today = DateTime.Now;

                if (reminder.When.LocalDateTime > today)
                    CrossLocalNotifications.Current.Cancel(reminder.Id);
            }
            catch (Exception ex)
            {


            }
        }

        private void NavigateOnAddPage()
        {
            NavigationService.NavigateTo<AddReminderViewModel>();
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

        private void LoadMore(object obj)
        {
            if (IsBusy)
                return;

            IsBusy = true;
            try
            {


                var items = _reminderRepository.GetAll(Items.Count, 10);
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

        private void RenderData(IEnumerable<Reminder> items)
        {
            HasMore = items != null && items.Count() >= 10;
            if (items != null)
            {
                foreach (var item in items)
                    Items.Add(item);
            }

        }

        void ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = _reminderRepository.GetAll(0, 10);
                RenderData(items);
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
        void AddedReminder(IEnumerable<Reminder> reminders)
        {
            if (reminders != null)
                foreach (var reminder in reminders)
                    Items.Add(reminder);
        }
        public override void OnPageAppearing()
        {
            base.OnPageAppearing();
            Task.Run(() => ExecuteLoadItemsCommand());
        }

    }
}