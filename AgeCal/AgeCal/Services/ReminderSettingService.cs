using System;
using System.Collections.Generic;
using System.Text;
using AgeCal.Interfaces;
using AgeCal.Models;

namespace AgeCal.Services
{
    public class ReminderSettingService : IReminderSettingService
    {
        private readonly IReminderSettingRepository _reminderSettingRepository;
        public ReminderSettingService(IReminderSettingRepository reminderSettingRepository)
        {
            _reminderSettingRepository = reminderSettingRepository;
        }

        public void Add(ReminderSetting reminderSetting)
        {
            if (reminderSetting == null)
                throw new ArgumentNullException(nameof(ReminderSetting));

            _reminderSettingRepository.Add(reminderSetting);
        }

        public void Delete(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));

            _reminderSettingRepository.Delete(id);
        }

        public ReminderSetting Get(string id)
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            return _reminderSettingRepository.Get(id);
        }

        public IEnumerable<ReminderSetting> Gets(int skip, int take)
        {
            return _reminderSettingRepository.GetAll(skip, take);
        }

        public void Update(ReminderSetting reminderSetting)
        {
            if (reminderSetting == null)
                throw new ArgumentNullException(nameof(ReminderSetting));

            _reminderSettingRepository.Update(reminderSetting);

        }
    }
}
