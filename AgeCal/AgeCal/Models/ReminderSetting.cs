using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public class ReminderSetting
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string UserId { get; set; }
        public bool Enable { get; set; }
        public bool NotifySameDay { get; set; }
        public bool NotifyDayBefore { get; set; }
        public bool NotifyWeekBefore { get; set; }
        public bool NotifyMonthBefore { get; set; }
        public int NumberOfNotification { get; set; }
    }
}
