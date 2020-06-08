
using LiteDB;
using System;

namespace AgeCal.Models
{
    public class Reminder
    {

        [BsonId]
        public string ReminderId { get; set; }
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTimeOffset When { get; set; }
        public bool Active { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public string Type { get; set; }
        public int RepeatInterval { get; set; }
        
    }
}
