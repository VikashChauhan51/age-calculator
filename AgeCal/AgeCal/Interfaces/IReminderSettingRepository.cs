using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IReminderSettingRepository : IRepository<ReminderSetting,string>
    {
        void Delete(string id);
    }
}
