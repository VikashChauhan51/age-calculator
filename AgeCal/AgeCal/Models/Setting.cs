using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Models
{
    public class Setting
    {
        public string Title { get; set; }
        public Type ViewModel { get; set; }
    }

    public class SettingList : List<Setting>
    {
        public string Heading { get; set; }
        public List<Setting> Items => this;
    }
}
