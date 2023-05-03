using SymBank.Views;
using System.Windows;

namespace SymBank.Commands
{
    public static class MyCommands
    {
        public static readonly RelayCommand OpenTextEditor =
            new RelayCommand(parameter => new TextEditorView().Show());

        //public static readonly RelayCommand OpenTextEditor =
        //    new RelayCommand(
        //        delegate (object parameter)
        //        {
        //            new TextEditorView().Show();
        //        }
        //    );

        //private static void OpenTextEditorExecute(object parameter)
        //{
        //    new TextEditorView().Show();
        //}


        public static readonly RelayCommand ExitApp =
            new RelayCommand(
                parameter => Application.Current.Shutdown(int.Parse(parameter.ToString())),
                parameter =>
                    {
                        var exitCode = 0;
                        return parameter != null &&
                            int.TryParse(parameter.ToString(), out exitCode);
                    }
                );
    }
}
