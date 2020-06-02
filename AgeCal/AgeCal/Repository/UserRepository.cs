using AgeCal.Database;
using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgeCal.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {

        }
        public void Add(User entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Insert(entity);
            }
        }


        public void Delete(User entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Delete(entity);
            }
        }

        //public IEnumerable<User> Find(Func<User, bool> predicate, int skip, int take)
        //{
        //    using (var connect = AgeDatabase.Database.Connection())
        //    {
        //        return connect.Table<User>()
        //                   .Where(predicate)
        //                   .Skip(skip)
        //                   .Take(take)
        //                   .ToList();

        //    }
        //}

        public User Get(string id)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<User>()
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

            }
        }

        public IEnumerable<User> GetAll(int skip, int take)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<User>()
                           .OrderBy(x => x.Text)
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            }
        }

        public void Update(User entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                connect.Update(entity);
            }
        }
 
        public IEnumerable<User> GetTodayBirthdays(int max = 10)
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            List<User> list = new List<User>();
            using (var connect = AgeDatabase.Database.Connection())
            {
                List<User> items = GetItems(skip, take, connect);
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (list.Count == max)
                            break;
                        if (item.DOB.Month == month && item.DOB.Day == day)
                            list.Add(item);

                    }
                    if (list.Count == max)
                        break;
                    skip += take;
                    items = GetItems(skip, take, connect);
                }
            }
            return list;
        }

 
        public IEnumerable<User> GetUpcomingBirthdays(int max = 10)
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            List<User> list = new List<User>();
            using (var connect = AgeDatabase.Database.Connection())
            {
                List<User> items = GetItems(skip, take, connect);
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (list.Count == max)
                            break;

                        //current month and day must be grater than today or month must be grater than current month.
                        if (item.DOB.Month == month && item.DOB.Day > day || item.DOB.Month > month)
                            list.Add(item);

                    }
                    if (list.Count == max)
                        break;
                    skip += take;
                    items = GetItems(skip, take, connect);
                }
            }
            return list;
        }

        private static List<User> GetItems(int skip, int take, SQLite.SQLiteConnection connect)
        {
            return connect.Table<User>().OrderBy(x => x.Text).Skip(skip).Take(take).ToList();
        }
    }
}
