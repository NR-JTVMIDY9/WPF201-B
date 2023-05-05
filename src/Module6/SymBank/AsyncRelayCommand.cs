using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SymBank
{
    public class AsyncRelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> _execute;
        private Predicate<object> _canExecute;
        private Action<Exception> _completed;
        private bool _running;

        public AsyncRelayCommand(
            Action<object> execute = null,
            Predicate<object> canExecute = null,
            Action<Exception> completed = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _completed = completed;
        }

        public bool Running { get { return _running; } }

        public bool CanExecute(object parameter)
        {
            if (_running) return false;
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        private Task ExecuteTask(object parameter)
        {
            var task = new Task(() => _execute(parameter));
            task.Start(); return task;
        }

        public async void Execute(object parameter)
        {
            if (_running) return;
            _running = true;
            NotifyCanExecuteChanged();
            try
            {
                if (_execute != null) await ExecuteTask(parameter);
                _completed?.Invoke(null);
            }
            catch (Exception ex)
            {
                _completed?.Invoke(ex);
            }
            _running = false;
            NotifyCanExecuteChanged();
        }

        public void NotifyCanExecuteChanged()
        {
            //    if (CanExecuteChanged != null)
            //        CanExecuteChanged(this, EventArgs.Empty);
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
