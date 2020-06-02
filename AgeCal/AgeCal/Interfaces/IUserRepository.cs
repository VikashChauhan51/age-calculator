using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {

        IEnumerable<User> GetTodayBirthdays(int max = 10);
        IEnumerable<User> GetUpcomingBirthdays(int max = 10);
    }
}
