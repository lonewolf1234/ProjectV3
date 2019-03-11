using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace VHDLGenerator.ViewModels.Commands
{
    class DeleteCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action _execute;

        public DeleteCommand(Action execute)
        {
            _execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            //throw new NotImplementedException();
            return true;
        }

        public void Execute(object parameter)
        {
            //throw new NotImplementedException();
            _execute.Invoke();
        }
    }
}
