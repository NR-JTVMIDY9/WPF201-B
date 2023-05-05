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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SymBank.Reports
{
    /// <summary>
    /// Interaction logic for AccountListReport.xaml
    /// </summary>
    public partial class AccountListReport : UserControl
    {
        public AccountListReport()
        {
            InitializeComponent();

        }

        public IEnumerable<AccountItem> Accounts
        {
            set
            {
                lsbAccounts.ItemsSource = value;
            }
        }

        public int CurPage
        {
            set
            {
                txtCurPage.Text = value.ToString();
            }
        }

        public int MaxPage
        {
            set
            {
                txtMaxPage.Text = value.ToString();
            }
        }

    }

}
