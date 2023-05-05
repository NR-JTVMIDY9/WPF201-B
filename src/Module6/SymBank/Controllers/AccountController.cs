using SymBank.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SymBank.Controllers
{
    public class AccountController
    {
        public void Add(Account item)
        {
            var dc = new SymBankDataContext();
            //item.Created = DateTime.Now;
            //item.Creator = MyApp.UserName;
            //dc.Accounts.InsertOnSubmit(item);
            //dc.SubmitChanges();
            dc.AccountAdd(item.Code, (int)item.Type,
                item.Name, item.ZipCode, MyApp.UserName,
                DateTime.UtcNow, item.Balance,
                new Binary(item.Picture));
        }


        public Task AddAsync(Account item)
        {
            var task = new Task(() =>
            {
                var dc = new SymBankDataContext();
                dc.AccountAdd(item.Code, (int)item.Type,
                    item.Name, item.ZipCode, MyApp.UserName,
                    DateTime.UtcNow, item.Balance,
                    new Binary(item.Picture));
            });
            task.Start(); return task;
        }

        public Account GetAccount(int code)
        {
            var dc = new SymBankDataContext();
            //  return dc.Accounts.Single(a => a.Code == code);
            var account = dc.Accounts.SingleOrDefault(a => a.Code == code);
            if (account == null) throw new
                    Exception("Account does not exist.");
            return account;
        }

        public List<Account> GetAccountList()
        {
            var dc = new SymBankDataContext();
            return dc.Accounts.ToList();
        }

        public AccountList GetObservableAccountList()
        {
            var dc = new SymBankDataContext();
            return new AccountList(dc.Accounts);
        }

        public List<Account> GetAccountsForName(string name)
        {
            name = name.ToLower();
            var dc = new SymBankDataContext();
            var query = from account in dc.Accounts
                        where account.Name.ToLower().Contains(name)
                        select account;

            //var query = dc.Accounts
            //    .Where(a => a.Name.ToLower().Contains(name))
            //    .Select(a => a);

            return query.ToList();
        }

        public AccountList GetObservableAccountsForName(string name)
        {
            name = name.ToLower();
            var dc = new SymBankDataContext();
            var query = from account in dc.Accounts
                        where account.Name.ToLower().Contains(name)
                        select account;
            return new AccountList(query);
        }

        public Task<AccountList> GetObservableAccountsForNameAsync(string name)
        {
            var task = new Task<AccountList>(() =>
            {
                Thread.Sleep(6000);
                name = name.ToLower();
                var dc = new SymBankDataContext();
                var query = from account in dc.Accounts
                            where account.Name.ToLower().Contains(name)
                            select account;
                return new AccountList(query);
            });
            task.Start(); return task;
        }




        public int Debit(int source, decimal amount)
        {
            int? transactionCode = null;
            var dc = new SymBankDataContext();
            dc.AccountDebit(source, amount, MyApp.UserName,
                DateTime.UtcNow, ref transactionCode);
            return (int)transactionCode;
        }

        public int Credit(int source, decimal amount)
        {
            int? transactionCode = null;
            var dc = new SymBankDataContext();
            dc.AccountCredit(source, amount, MyApp.UserName,
                DateTime.UtcNow, ref transactionCode);
            return (int)transactionCode;
        }

        public int Transfer(int source, int target, decimal amount)
        {
            int? transactionCode = null;
            var dc = new SymBankDataContext();
            dc.AccountTransfer(source, target, amount, MyApp.UserName,
                DateTime.UtcNow, ref transactionCode);
            return (int)transactionCode;
        }
    }
}
