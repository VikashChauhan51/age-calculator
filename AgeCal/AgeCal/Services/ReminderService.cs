using AgeCal.Interfaces;
using AgeCal.Models;
using AgeCal.Utilities;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgeCal.Services
{
    public class ReminderService : IReminderService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reminderRepository"></param>
        /// <param name="userRepository"></param>
        public ReminderService(IReminderRepository reminderRepository, IUserRepository userRepository)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;
            if (_reminderRepository == null)
                throw new ArgumentNullException(typeof(IReminderRepository).FullName);
            if (_userRepository == null)
                throw new ArgumentNullException(typeof(IUserRepository).FullName);
        }

        public IEnumerable<Reminder> Gets(int skip, int take)
        {
            return _reminderRepository.GetAll(skip, take);
        }
        public Reminder Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("ReminderService.id");

            return _reminderRepository.Get(id);
        }
        public void Add(IEnumerable<Reminder> reminders)
        {
            if (reminders == null)
                throw new ArgumentNullException(reminders.GetType().FullName);

            //set reminders before save into database
            foreach (var item in reminders)
                ReminderHelper.AddReminderNotification(item);

            //save reminder into database
            _reminderRepository.Add(reminders);

        }
        public void Add(Reminder reminder)
        {
            if (reminder == null)
                throw new ArgumentNullException(typeof(Reminder).FullName);

            if (string.IsNullOrEmpty(reminder.ReminderId))
                throw new ArgumentNullException("reminder.ReminderId");

            //set reminders before save into database
            ReminderHelper.AddReminderNotification(reminder);
            //save reminder into database
            _reminderRepository.Add(reminder);
        }

        public void DeleteReminders(string userId)
        {
            var reminders = _reminderRepository.GetReminders(userId);
            if (reminders != null && reminders.Any())
            {
                //removed schedule reminder if any
                foreach (var item in reminders)
                    ReminderHelper.DeleteReminderNotification(item);

                _reminderRepository.Delete(x=>x.UserId== userId);

            }

        }
        /// <summary>
        /// Delete reminder if time passed
        /// </summary>
        /// <param name="reminders"></param>
        public void DeletePassedReminders(IEnumerable<Reminder> reminders)
        {
            if (reminders == null)
                throw new ArgumentNullException(reminders.GetType().FullName);

            var today = DateTime.Now.AddDays(-1);
            var priorReminder = new List<Reminder>();
            //get passed reminders
            foreach (var item in reminders)
                if (item.When.Date < today.Date)
                    priorReminder.Add(item);

            //delete reminder from database
            if(priorReminder.Any())
            _reminderRepository.Delete(x=>x.When< today);

        }

        public void Delete(string id)
        {
            var reminder = _reminderRepository.Get(id);
            Delete(reminder);

        }
        public void Delete(Reminder reminder)
        {
            if (reminder != null)
            {
                ReminderHelper.DeleteReminderNotification(reminder);
                _reminderRepository.Delete(reminder);
            }

        }

        public int GetMaxId()
        {
            return _reminderRepository.GetRemindeMaxId();
        }

        public bool Any(Expression<Func<Reminder, bool>> predicate)
        {
            return _reminderRepository.Any(predicate);
        }
    }
}
