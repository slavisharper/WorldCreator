using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WorldCreator.Commands
{
    public class DelegateCommand : ICommand
    {
        Func<object, bool> canExecute;
        Action<object> executeAction;
        bool canExecuteCache;
     
        public DelegateCommand(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }
     
        public bool CanExecute(object parameter)
        {
            bool temp = canExecute(parameter);
     
            if (canExecuteCache != temp)
            {
                canExecuteCache = temp;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }
     
            return canExecuteCache;
        }
     
        public event EventHandler CanExecuteChanged;
     
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
