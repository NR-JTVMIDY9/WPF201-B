using System;
using System.Windows.Input;

namespace SymBank.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _execute;
        private Predicate<object> _canExecute;

        public RelayCommand(
            Action<object> execute = null,
            Predicate<object> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            //    if (_execute != null) _execute(parameter);
            _execute?.Invoke(parameter);
        }

        public void NotifyCanExecuteChanged()
        {
            //    if (CanExecuteChanged != null)
            //        CanExecuteChanged(this, EventArgs.Empty);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
