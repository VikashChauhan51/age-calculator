using AgeCal.Interfaces;
using AgeCal.Models;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Repository
{
    public sealed class DBContext : IDisposable
    {
        private ILocalDatabase _localDatabase;
        private ILiteDatabase _liteDatabase;
        public DBContext(ILocalDatabase localDatabase)
        {
            _localDatabase = localDatabase;
            _liteDatabase = new LiteDatabase(_localDatabase.ConnectionString());
            EnsureIndexing();
        }
        public ILiteCollection<User> Users => _liteDatabase.GetCollection<User>("Users");
        public ILiteCollection<Reminder> Reminders => _liteDatabase.GetCollection<Reminder>("Reminders");
        public ILiteCollection<ReminderSetting> ReminderSettings => _liteDatabase.GetCollection<ReminderSetting>("ReminderSettings");


        private void EnsureIndexing()
        {
            Users.EnsureIndex(x => x.Text);
            Reminders.EnsureIndex(x => x.When);
            Reminders.EnsureIndex(x => x.Id);
        }
        public void Dispose()
        {
            _liteDatabase?.Dispose();
            if (_liteDatabase != null)
                _liteDatabase = null;
            if (_localDatabase != null)
                _localDatabase = null;
        }
    }
}
