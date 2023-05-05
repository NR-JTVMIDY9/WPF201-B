using SymBank.ViewModels;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using FilePath = System.IO.Path;

namespace SymBank.Views
{
    /// <summary>
    /// Interaction logic for AddAccountView.xaml
    /// </summary>
    public partial class AddAccountView : BaseView
    {
        public AddAccountView()
        {
            InitializeComponent();
            var vm = (AddAccountViewModel)DataContext;
            Validation.AddErrorHandler(this,
                vm.OnAddAccountValidationError);
        }

        private void BaseView_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public override void ResetFocus()
        {
            txtCode.Focus();
        }

        private void MnuPicture_Opened(object sender, RoutedEventArgs e)
        {
        }

        private void MnuPicture_Closed(object sender, RoutedEventArgs e)
        {

        }

        private void BdrPicture_Drop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            var vm = (AddAccountViewModel)DataContext;
            try {
                var filename = files[0];
                var extension = FilePath.GetExtension(filename).ToLower();
                if (extension != ".jpg" && extension !=".jpeg" &&
                    extension != ".png" && extension != ".bmp")
                    throw new Exception("Must be JPG or PNG image file.");
                var file = new FileInfo(filename);
                if (file.Length > 20000) throw new Exception(
                    "Image size must be below 20K.");
                vm.LoadPicture(files[0]); }
            catch (Exception ex) {
                Shell.Failure("Cannot load picture.\n" + ex.Message);
            }
            
        }
    }
}
