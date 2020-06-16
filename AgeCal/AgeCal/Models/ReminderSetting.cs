using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public class ReminderSetting
    {
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public TimeSpan Time { get; set; }
        public bool AutoDeletePriorReminder { get; set; }
        public bool AutoSetupReminder { get; set; }
    }
}
