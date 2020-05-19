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

        public IEnumerable<User> Find(Func<User, bool> predicate, int skip, int take)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<User>()
                           .Where(predicate)
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            }
        }

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

        public bool Any(Func<User, bool> predicate)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<User>()
                           .Where(predicate)
                           .Any();


            }
        }

        public User GetTodayBirthday()
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month == month && item.DOB.Day == day)
                            return item;

                    }

                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return null;
        }

        public User GetMonthBirthday()
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month == month && item.DOB.Day > day)
                            return item;

                    }
                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return null;
        }

        public User GetYearBirthday()
        {

            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month > month)
                            return item;



                    }
                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return null;
        }

        public bool TodayHasMoreBirthday()
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            int count = 0;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month == month && item.DOB.Day == day)
                            count++;

                        if (count > 1)
                            return true;


                    }
                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return false;
        }

        public bool MonthHasMoreBirthday()
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            int count = 0;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month == month && item.DOB.Day > day)
                            count++;

                        if (count > 1)
                            return true;



                    }
                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return false;
        }

        public bool YearsHasMoreBirthday()
        {
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            int count = 0;
            using (var connect = AgeDatabase.Database.Connection())
            {
                var items = connect.Table<User>().Skip(skip).Take(take).ToList();
                while (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        if (item.DOB.Month > month)
                            count++;

                        if (count > 1)
                            return true;



                    }
                    skip += take;
                    items = connect.Table<User>().Skip(skip).Take(take).ToList();
                }
            }
            return false;
        }
    }
}
