using AgeCal.Interfaces;
using AgeCal.Models;
using AgeCal.Utilities;
using Plugin.LocalNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AgeCal.Services
{
    public class UserService : IUserService
    {
        private readonly IReminderRepository _reminderRepository;
        private readonly IUserRepository _userRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="reminderRepository"></param>
        /// <param name="userRepository"></param>
        public UserService(IReminderRepository reminderRepository, IUserRepository userRepository)
        {
            _reminderRepository = reminderRepository;
            _userRepository = userRepository;

            if (_reminderRepository == null)
                throw new ArgumentNullException(typeof(IReminderRepository).FullName);
            if (_userRepository == null)
                throw new ArgumentNullException(typeof(IUserRepository).FullName);
        }
        public IEnumerable<User> Gets(int skip, int take)
        {
            return _userRepository.GetAll(skip, take);
        }
        public User Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException("UserService.id");

            return _userRepository.Get(id);
        }
        public void Add(User user)
        {
            Validate(user);
            _userRepository.Add(user);

        }

        public void Update(User user)
        {
            Validate(user);
            _userRepository.Update(user);

        }
        public void Delete(string id)
        {
            Delete(Get(id));
        }
        public void Delete(User user)
        {
            Validate(user);
            DeleteReminders(user.Id);
            _userRepository.Delete(user);

        }

        private void DeleteReminders(string userId)
        {
            var reminders = _reminderRepository.GetReminders(userId);
            if (reminders != null && reminders.Any())
            {
                //removed schedule reminder if any
                foreach (var item in reminders)
                    ReminderHelper.DeleteReminderNotification(item);

                _reminderRepository.Delete(reminders);

            }

        }

  
        private static void Validate(User user)
        {
            if (user == null)
                throw new ArgumentNullException(typeof(User).FullName);

            if (string.IsNullOrEmpty(user.Id))
                throw new ArgumentNullException("user.Id");

            if (string.IsNullOrEmpty(user.Text))
                throw new ArgumentNullException("user.Name");
        }

        public User GetTodayBirthday()
        {
            return _userRepository.GetTodayBirthday();
        }

        public User GetMonthBirthday()
        {
            return _userRepository.GetMonthBirthday();
        }

        public User GetYearBirthday()
        {
            return _userRepository.GetYearBirthday();
        }

        public bool TodayHasMoreBirthday()
        {
           return _userRepository.TodayHasMoreBirthday();
        }

        public bool MonthHasMoreBirthday()
        {
            return _userRepository.MonthHasMoreBirthday();
        }

        public bool YearsHasMoreBirthday()
        {
            return _userRepository.YearsHasMoreBirthday();
        }
    }
}
