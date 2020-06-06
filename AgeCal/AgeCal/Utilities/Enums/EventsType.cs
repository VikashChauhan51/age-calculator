using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AgeCal.Utilities.Enums
{
    public enum EventsType : int
    {
        [Description("Birthday")]
        Birthday,
        [Description("Wedding Anniversary")]
        WeddingAnniversary,
        [Description("Job Anniversary")]
        JobAnniversary,
        [Description("Business Anniversary")]
        BusinessAnniversary
    }
}
