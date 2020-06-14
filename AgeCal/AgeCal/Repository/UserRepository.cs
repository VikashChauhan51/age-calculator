
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
        private readonly ILocalDatabase _localDatabase;
        public UserRepository(ILocalDatabase localDatabase)
        {
            _localDatabase = localDatabase;
        }
        public void Add(User entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                connect.Users.Insert(entity);
            }
        }


        public void Delete(User entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.Users.Delete(entity.Id);
            }
        }

        public User Get(string id)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Users.FindById(id);

            }
        }

        public IEnumerable<User> GetAll(int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Users.Query()
                           .OrderBy(x => x.Text)
                           .Skip(skip)
                           .Limit(take)
                           .ToList();

            }
        }

        public void Update(User entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                connect.Users.Update(entity);
            }
        }

        public IEnumerable<User> GetTodayBirthdays(int max = 10)
        {
            var day = DateTime.Now.Day;
            var month = DateTime.Now.Month;
            int skip = 0;
            int take = 100;
            List<User> list = new List<User>();
            using (var connect = new DBContext(_localDatabase))
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
            using (var connect = new DBContext(_localDatabase))
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

        private static List<User> GetItems(int skip, int take, DBContext connect)
        {
            return connect.Users.Query().OrderBy(x => x.Text).Skip(skip).Limit(take).ToList();
        }

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate, int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Users.Find(predicate).OrderBy(x=>x.Text).Skip(skip).Take(take).ToList();
            }

        }

        public bool Any(Expression<Func<User, bool>> predicate)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Users.Exists(predicate);
            }
        }
    }
}
