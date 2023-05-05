using SymBank.Controllers;
using SymBank.Models;
using SymBank.ViewModels;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SymBank.Dialogs
{
    /// <summary>
    /// Interaction logic for AccountDialog.xaml
    /// </summary>
    public partial class AccountDialog : Window
    {
        public AccountDialog()
        {
            InitializeComponent();
            _controller = new AccountController();
        }

        private AccountItem _item;
        private AccountController _controller;

        public AccountItem Account
        {
            get { return _item; }
            set { DataContext = _item = value; }
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            if (_item.DebitCommand.CanExecute(null))
            {
                DialogResult = true;
                Close();
                _item.Debit();
            }
            else
            {
                Shell.Failure("Amount is not valid.");
            }
        }
    }
}
