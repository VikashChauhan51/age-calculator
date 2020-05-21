using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public class Reminder
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string Message { get; set; }
        public string Title { get; set; }
        public string Tag { get; set; }
        public Reminder()
        {
        }
    }
}
