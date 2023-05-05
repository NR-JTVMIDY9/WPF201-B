using SymBank.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SymBank.Models
{
    public class AccountList : ObservableCollection<AccountItem>
    {
        public AccountList() { }
        public AccountList(IEnumerable<Account> items)
        {
            foreach (var item in items) Add(new AccountItem(item));
        }
    }
}
