using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Interfaces
{
    public interface IUserRepository : IRepository<User, string>
    {

        User GetTodayBirthday();
        User GetMonthBirthday();
        User GetYearBirthday();
        bool TodayHasMoreBirthday();
        bool MonthHasMoreBirthday();
        bool YearsHasMoreBirthday();
    }
}
