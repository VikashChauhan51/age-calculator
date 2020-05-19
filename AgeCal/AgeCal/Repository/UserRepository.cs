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

        public IEnumerable<User> Find(Expression<Func<User, bool>> predicate, int skip, int take)
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
    }
}
