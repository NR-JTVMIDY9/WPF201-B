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
using System.Windows.Media.Animation;
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
            //  var vm = (SearchAccountsViewModel)DataContext;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (!e.PropertyName.Equals("Results")) return;
            var d = new Duration(TimeSpan.FromSeconds(0.1));
            var a = new DoubleAnimation(1.0, 0.5, d, FillBehavior.Stop);
            // a.RepeatBehavior = RepeatBehavior.Forever;
            a.RepeatBehavior = new RepeatBehavior(3);
            a.AutoReverse = true;
            lsbResults.BeginAnimation(ListBox.OpacityProperty, a);
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

        private async void BtnAsyncSearch_Click(object sender, RoutedEventArgs e)
        {
            //  IsEnabled = false;  // disable entire view
            //  toolbar1.IsEnabled = false;     // disable entire toolbar
            btnAsyncSearch.IsEnabled = false;
            try
            {
                var results = await _controller.GetObservableAccountsForNameAsync(txtName.Text);
                lsbResults.ItemsSource = results;
            }
            catch (Exception ex)
            {
                Shell.Failure(
                    "Error searching for accounts.\n" + ex.Message);
            }
            btnAsyncSearch.IsEnabled = true;
            //  IsEnabled = true;  // re-enable entire view
            //  toolbar1.IsEnabled = true; // re-enable entire toolbar
        }
    }
}
