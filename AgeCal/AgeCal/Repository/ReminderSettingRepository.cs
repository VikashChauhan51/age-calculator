using AgeCal.Interfaces;
using AgeCal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AgeCal.Repository
{
    public class ReminderSettingRepository : IReminderSettingRepository
    {
        private readonly ILocalDatabase _localDatabase;
        public ReminderSettingRepository(ILocalDatabase localDatabase)
        {
            _localDatabase = localDatabase;
        }

        public void Add(ReminderSetting entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.ReminderSettings.Insert(entity);
            }
        }

        public bool Any(Expression<Func<ReminderSetting, bool>> predicate)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                return connect.ReminderSettings.Exists(predicate);
            }
        }

        public void Delete(ReminderSetting entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.ReminderSettings.Delete(entity.Id);
            }
        }
        public void Delete(string id)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                connect.ReminderSettings.Delete(id);
            }
        }

        public IEnumerable<ReminderSetting> Find(Expression<Func<ReminderSetting, bool>> predicate, int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {

                return connect.ReminderSettings.Find(predicate).OrderBy(x => x.Time).Skip(skip).Take(take).ToList();
            }
        }

        public ReminderSetting Get(string id)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.ReminderSettings.FindById(id);

            }
        }

        public IEnumerable<ReminderSetting> GetAll(int skip, int take)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                return connect.ReminderSettings.Query()
                           .OrderBy(x => x.Time)
                           .Skip(skip)
                           .Limit(take)
                           .ToList();

            }
        }

        public void Update(ReminderSetting entity)
        {
            using (var connect = new DBContext(_localDatabase))
            {
                connect.ReminderSettings.Update(entity);
            }
        }
    }
}
