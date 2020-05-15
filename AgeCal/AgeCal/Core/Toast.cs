using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Core
{
    public class Toast
    {
        public string Message { get; set; }
        public int Duration { get; set; }
        public ToastType Type { get; set; }
    }
    public enum ToastType
    {
        Saved,
        Deleted
    }
}
