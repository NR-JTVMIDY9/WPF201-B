using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymBank.Views
{
    /// <summary>
    /// Interaction logic for TextEditorView.xaml
    /// </summary>
    public partial class TextEditorView : BaseView
    {
        public TextEditorView()
        {
            InitializeComponent();
        }

        public override void ResetFocus()
        {
            txtDocument.Focus();
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CmdOpen_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CmdOpen_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var filename = Shell.GetOpenFileName(
                "Open Document", "Documents|*.xaml", (string)Header);
            try
            {
                var stream = File.OpenRead(filename);
                txtDocument.Document = (FlowDocument)XamlReader.Load(stream);
                stream.Close();
                Header = System.IO.Path.GetFileName(filename);
                Shell.Status = "Document opened.";
            }
            catch (Exception ex)
            {
                Shell.Status = "Error opening document.";
                Shell.Failure("Cannot open document.\n" + ex.Message);
            }
        }

        private void CmdSave_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CmdSave_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var filename = Shell.GetSaveFileName(
                "Save Document", "Documents|*.xaml", (string)Header);
            if (filename == null) return;

            try
            {
                var stream = File.Create(filename);
                XamlWriter.Save(txtDocument.Document, stream);
                stream.Close();
                Header = System.IO.Path.GetFileName(filename);
                Shell.Status = "Document saved.";
            }
            catch (Exception ex)
            {
                Shell.Status = "Error saving document.";
                Shell.Failure("Cannot save document.\n" + ex.Message);
            }
        }
    }
}
