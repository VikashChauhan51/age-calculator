using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IReminderRepository : IRepository<Reminder, string>
    {
    }
}
