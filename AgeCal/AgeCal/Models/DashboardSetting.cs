using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public enum DashboardInfo : int { Today, Weekly, Monthly }
    public class DashboardSetting
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public int DisplayType { get; set; }
        public int Count { get; set; }
    }
}
