using SymBank.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SymBank.Commands
{
    public class OpenTextEditorCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
        //  return DateTime.Now.Hour < 12;
            return true;
        }

        public void Execute(object parameter)
        {
            new TextEditorView().Show();
        }
    }
}
