using System;
using System.Windows;
using System.Windows.Input;

namespace SymBank.Commands
{
    public class ExitAppCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var exitCode = 0;
            return parameter != null &&
                int.TryParse(parameter.ToString(), out exitCode);
        }

        public void Execute(object parameter)
        {
            Application.Current.Shutdown(
                int.Parse(parameter.ToString()));
        }
    }
}
