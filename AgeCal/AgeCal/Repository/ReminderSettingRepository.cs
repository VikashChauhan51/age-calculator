using AgeCal.Database;
using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Repository
{
    public class ReminderSettingRepository : IReminderSettingRepository
    {
        public void Add(ReminderSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Insert(entity);
            }
        }

        public void Delete(ReminderSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Delete(entity);
            }
        }

        public ReminderSetting Get(string id)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<ReminderSetting>()
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

            }
        }

        public IEnumerable<ReminderSetting> GetAll(int skip, int take)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<ReminderSetting>()
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            }
        }

        public void Update(ReminderSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                connect.Update(entity);
            }
        }
    }
}
