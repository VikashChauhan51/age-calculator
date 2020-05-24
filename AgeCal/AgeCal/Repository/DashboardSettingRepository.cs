using AgeCal.Database;
using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Repository
{
    public class DashboardSettingRepository : IDashboardSettingRepository
    {
        public void Add(DashboardSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Insert(entity);
            }
        }

        public void Delete(DashboardSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {

                connect.Delete(entity);
            }
        }

        public DashboardSetting Get(string id)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<DashboardSetting>()
                           .Where(x => x.Id == id)
                           .FirstOrDefault();

            }
        }

        public IEnumerable<DashboardSetting> GetAll(int skip, int take)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                return connect.Table<DashboardSetting>()
                           .Skip(skip)
                           .Take(take)
                           .ToList();

            }
        }

        public void Update(DashboardSetting entity)
        {
            using (var connect = AgeDatabase.Database.Connection())
            {
                connect.Update(entity);
            }
        }
    }
}
