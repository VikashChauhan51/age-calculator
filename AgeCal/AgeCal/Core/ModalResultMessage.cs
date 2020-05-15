using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Core
{
    public enum ModalResultStatus { Positive, Negative, Netural }
    public class ModalResultMessage
    {
        public ModalResultStatus Result { get; }
        public object Data { get; set; }
        public ModalResultMessage(ModalResultStatus result)
        {
            Result = result;

        }
        public ModalResultMessage(ModalResultStatus result, object data) : this(result)
        {
            Data = data;

        }
    }
}
