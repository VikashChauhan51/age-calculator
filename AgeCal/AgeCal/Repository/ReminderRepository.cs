
using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace AgeCal.Repository
{
    public class ReminderRepository : IReminderRepository
    {
        private readonly ILocalDatabase _localDatabase;
        public ReminderRepository(ILocalDatabase localDatabase)
        {
            _localDatabase = localDatabase;
        }
        public void Add(IEnumerable<Reminder> entities)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.Reminders.InsertBulk(entities);
            }
        }
        public void Add(Reminder entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.Reminders.Insert(entity);
            }
        }

        public bool Any(Expression<Func<Reminder, bool>> predicate)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                return connect.Reminders.Exists(predicate);
            }
        }

        public void Delete(Reminder entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.Reminders.Delete(entity.Id);
            }
        }

        public void Delete(Expression<Func<Reminder, bool>> predicate)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                connect.Reminders.DeleteMany(predicate);

            }
        }

        public IEnumerable<Reminder> Find(Expression<Func<Reminder, bool>> predicate, int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                return connect.Reminders.Find(predicate).OrderBy(x => x.When).Skip(skip).Take(take);
            }
        }

        public Reminder Get(string id)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Reminders.FindById(id);

            }
        }

        public IEnumerable<Reminder> GetAll(int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Reminders.Query()
                           .OrderBy(x => x.When)
                           .Skip(skip)
                           .Limit(take)
                           .ToList();

            }
        }

        public int GetRemindeMaxId()
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Reminders.Count();

            }
        }

        public IEnumerable<Reminder> GetReminders(string userId)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.Reminders.Query()
                    .Where(x => x.UserId == userId)
                           .ToList();

            }
        }

        public void Update(Reminder entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                connect.Reminders.Update(entity);
            }
        }
    }
}
