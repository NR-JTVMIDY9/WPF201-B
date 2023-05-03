using SymBank.Controllers;
using SymBank.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymBank.Views
{
    /// <summary>
    /// Interaction logic for SearchAccountsView.xaml
    /// </summary>
    public partial class SearchAccountsView : BaseView
    {
        private AccountController _controller;

        public SearchAccountsView()
        {
            InitializeComponent();
            _controller = new AccountController();
        }


        private void BaseView_Loaded(object sender, RoutedEventArgs e)
        {
        //    _controller = new AccountController();
        }

        public override void ResetFocus()
        {
            txtName.Focus();
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            //var dc = new SymBankDataContext();
            //var name = txtName.Text.ToLower();
            //var query = from account in dc.Accounts
            //            where account.Name.ToLower().Contains(name)
            //            select account;

            //lsbResults.ItemsSource = query.ToList();
            lsbResults.ItemsSource = _controller.GetObservableAccountsForName(txtName.Text);
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
