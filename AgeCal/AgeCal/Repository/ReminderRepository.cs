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

        public void Update(Reminder entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                connect.Update(entity);
            }
        }
    }
}
