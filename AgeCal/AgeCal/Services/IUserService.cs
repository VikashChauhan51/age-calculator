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
        IEnumerable<User> Gets(string text,int skip, int take);
        void Update(User user);
        IEnumerable<User> GetTodayBirthdays(int max = 10);
        IEnumerable<User> GetUpcomingBirthdays(int max = 10);
    }
}