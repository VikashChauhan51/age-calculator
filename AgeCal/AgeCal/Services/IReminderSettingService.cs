using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Services
{
    public interface IReminderSettingService
    {
        void Add(ReminderSetting reminderSetting);
        void Delete(string id);
        void Update(ReminderSetting reminderSetting);
        ReminderSetting Get(string id);
        IEnumerable<ReminderSetting> Gets(int skip, int take);

    }
}
