using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace WorldCreator.Commands
{
    public class RelayCommandWithParameter : ICommand
    {
        public event EventHandler CanExecuteChanged;

        Func<object, bool> canExecute;
        Action<object> executeAction;
     
        public RelayCommandWithParameter(Action<object> executeAction, Func<object, bool> canExecute = null)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }
     
        public bool CanExecute(object parameter)
        {
            if (this.canExecute == null)
            {
                return true;
            }
     
            return this.canExecute(parameter);
        }
     
        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
