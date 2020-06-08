
using LiteDB;
using System;

namespace AgeCal.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DOB { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
