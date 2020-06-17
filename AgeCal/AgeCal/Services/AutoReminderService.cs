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
    public class AutoReminderService : IPeriodicTask
    {
        public TimeSpan Interval { get; set; }
        public AutoReminderService(int hours)
        {
            Interval = TimeSpan.FromHours(hours);
        }

        public async Task<bool> StartJob()
        {
            try
            {
                var reminderSettingService = IocRegistry.Locate<IReminderSettingService>();
                var reminderService = IocRegistry.Locate<IReminderService>();
                var userService = IocRegistry.Locate<IUserService>();
                if (reminderSettingService != null && reminderService != null && userService != null)
                {
                    var setting = reminderSettingService.Gets(0, 1).FirstOrDefault();
                    if (setting != null && setting.AutoSetupReminder)
                    {
                        var upcomingBirthday = userService.GetUpcomingBirthdays(20);
                        if (upcomingBirthday != null && upcomingBirthday.Any())
                        {

                            foreach (var user in upcomingBirthday)
                            {
                                var nextBirthday = BirthdayHelper.GetNextBirthday(BirthdayHelper.GetDate(user.DOB, user.Time));
                                if (!reminderService.Any(x => x.UserId == user.Id && x.When.Date >= nextBirthday.Date))
                                {
                                    int maxId = reminderService.GetMaxId() + 1;
                                    var item = new Reminder
                                    {
                                        ReminderId = Guid.NewGuid().ToString(),
                                        Id = maxId,
                                        UserId = user.Id,
                                        Tag = user.Id,
                                        Title = $"{user.Text } Birthday",
                                        Message = $"Today,{user.Text} has birthday.",
                                        Active = true,
                                        When = AddTime(nextBirthday.Date, setting.Time)
                                    };
                                    reminderService.Add(item);
                                }
                            }
                        }

                    }

                }
            }
            catch
            {


            }
            return await Task.FromResult(true);
        }
        private static DateTimeOffset AddTime(DateTimeOffset date, TimeSpan time)
        {

            // creating object of  DateTimeOffset 
            DateTimeOffset offset = new DateTimeOffset(date.Year,
                    date.Month, date.Day, 0, 0, 0, date.Offset);

            return offset.Add(time);
        }
    }
}
