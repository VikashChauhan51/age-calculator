using AgeCal.Database;
using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Repository
{
    public class ReminderRepository : IReminderRepository
    {

        public void Add(IEnumerable<Reminder> entities)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.InsertAll(entities, false);
            }
        }
        public void Add(Reminder entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Insert(entity);
            }
        }

        public void Delete(Reminder entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Delete(entity);
            }
        }

        public void Delete(IEnumerable<Reminder> entities)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                foreach (var item in entities)
                    connect.Delete(item);

            }
        }

        public Reminder Get(string id)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<Reminder>()
                           .Where(x => x.ReminderId == id)
                           .FirstOrDefault();

            }
        }

        public IEnumerable<Reminder> GetAll(int skip, int take)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<Reminder>()
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            }
        }

        public int GetRemindeMaxId()
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<Reminder>().Count();

            }
        }

        public IEnumerable<Reminder> GetReminders(string userId)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<Reminder>()
                    .Where(x => x.UserId == userId)
                           .ToList();

            }
        }

        public void Update(Reminder entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                connect.Update(entity);
            }
        }
    }
}
