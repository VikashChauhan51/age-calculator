using System.Collections.Generic;
using AgeCal.Models;

namespace AgeCal.Services
{
    public interface IUserService
    {
        void Add(User user);
        void Delete(string id);
        void Delete(User user);
        User Get(string id);
        IEnumerable<User> Gets(int skip, int take);
        void Update(User user);
        User GetTodayBirthday();
        User GetMonthBirthday();
        User GetYearBirthday();
        bool TodayHasMoreBirthday();
        bool MonthHasMoreBirthday();
        bool YearsHasMoreBirthday();
    }
}