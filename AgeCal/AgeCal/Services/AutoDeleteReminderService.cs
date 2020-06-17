using AgeCal.Ioc;
using AgeCal.Models;
using AgeCal.Utilities;
using Matcha.BackgroundService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgeCal.Services
{
    public class AutoDeleteReminderService : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }
        public AutoDeleteReminderService(int hours)
        {
            Interval = TimeSpan.FromHours(hours);
        }


        public async Task<bool> StartJob()
        {
            try
            {
                var reminderSettingService = IocRegistry.Locate<IReminderSettingService>();
                var reminderService = IocRegistry.Locate<IReminderService>();
                if (reminderSettingService != null && reminderService != null)
                {
                    var setting = reminderSettingService.Gets(0, 1).FirstOrDefault();
                    if (setting != null && setting.AutoDeletePriorReminder)
                    {                       
                        var reminders = reminderService.Gets(0, 10);
                        if (reminders != null && reminders.Any())
                            reminderService.DeletePassedReminders(reminders);

                    }

                }
            }
            catch
            {


            }
            return await Task.FromResult(true);
        }
    }
}
