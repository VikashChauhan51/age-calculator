using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public class User
    {
        [PrimaryKey]
        public string Id { get; set; }
        public string Text { get; set; }
        public string Description { get; set; }
        public DateTime DOB { get; set; }
        public TimeSpan Time { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
