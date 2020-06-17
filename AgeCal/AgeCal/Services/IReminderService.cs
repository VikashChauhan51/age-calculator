using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AgeCal.Models;

namespace AgeCal.Services
{
    public interface IReminderService
    {
        void Add(IEnumerable<Reminder> reminders);
        void Add(Reminder reminder);
        void Delete(Reminder reminder);
        void Delete(string id);
        void DeleteReminders(string userId);
        Reminder Get(string id);
        IEnumerable<Reminder> Gets(int skip, int take);
        int GetMaxId();
        void DeletePassedReminders(IEnumerable<Reminder> reminders);
        bool Any(Expression<Func<Reminder, bool>> predicate);
    }
}