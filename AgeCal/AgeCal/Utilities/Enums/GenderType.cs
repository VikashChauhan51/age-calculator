using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgeCal.Utilities.Enums
{
    public enum GenderType : int
    {
        [Description("Male")]
        Male = 1,
        [Description("Female")]
        Female = 2,
        [Description("Other")]
        Other = 3

    }
}
