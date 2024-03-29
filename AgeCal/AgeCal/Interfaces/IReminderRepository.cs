﻿using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IReminderRepository : IRepository<Reminder, string>
    {
        int GetRemindeMaxId();
        void Add(IEnumerable<Reminder> entities);
        void Delete(Expression<Func<Reminder, bool>> predicate);
        IEnumerable<Reminder> GetReminders(string userId);
    }
}
