using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgeCal.Ioc
{
    public class ExclusiveRelayCommand : RelayCommand
    {
        public bool Lock { get; set; } = false;
        public ExclusiveRelayCommand(Action action) : base(action)
        {

        }
        public ExclusiveRelayCommand(Action action, Func<bool> canExecute) : base(action, canExecute)
        {

        }
        public override void Execute(object parameter)
        {
            if (!Lock)
            {
                Lock = true;
                base.Execute(parameter);
                Lock = false;
            }

        }
    }
    public class ExclusiveRelayCommand<T> : RelayCommand<T>
    {
        public bool Lock { get; set; } = false;
        public ExclusiveRelayCommand(Action<T> action) : base(action)
        {

        }
        public ExclusiveRelayCommand(Action<T> action, Func<T, bool> canExecute) : base(action, canExecute)
        {

        }
        public override void Execute(object parameter)
        {
            if (!Lock)
            {
                Lock = true;
                base.Execute(parameter);
                Lock = false;
            }

        }
    }
}
