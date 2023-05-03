using SymBank.Commands;
using SymBank.Controllers;
using SymBank.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SymBank.ViewModels
{
    public class AddAccountViewModel : BaseViewModel
    {
        private Account _account;
        private List<AccountType> _accountTypes;
        private AccountController _accountController;

        private int _errors;

        public void OnAddAccountValidationError(object sender, 
            ValidationErrorEventArgs e)
        {
            switch (e.Action)
            {
                case ValidationErrorEventAction.Added: ++_errors; break;
                case ValidationErrorEventAction.Removed: --_errors; break;
            }
            AddCommand.NotifyCanExecuteChanged();
        }

        public string AccountName
        {
            get
            {
                return _account.Name;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new Exception("Account name cannot be blank.");
                _account.Name = value;
                NotifyPropertyChanged("AccountName");
            }
        }

        public decimal OpeningBalance
        {
            get
            {
                return _account.Balance;
            }
            set
            {
                if (value < 100) throw new Exception(
                    "Minimum opening balance must be 100.");
                _account.Balance = value;
                NotifyPropertyChanged("OpeningBalance");
            }
        }

        private Regex _zipcodePattern = new Regex(@"^\d{5}$");

        public string AccountZipCode
        {
            get { return _account.ZipCode; }
            set
            {
                if (!_zipcodePattern.IsMatch(value))
                    throw new Exception("Zip code must be exactly 5 digits.");
                _account.ZipCode = value;
            }
        }

        public Account Account
        {
            get { return _account; }
            set {
                _account = value;
                NotifyPropertyChanged("Account");
            }
        }

        public List<AccountType> AccountTypes
        {
            get { return _accountTypes; }
        }

        public void Add()
        {
            try
            {
                _accountController.Add(_account);
                Shell.Status = string.Format(
                    "Account {0} added.", _account.Code);
                Account = new Account(); Clear();
                //  CloseView();
            }
            catch (Exception ex)
            {
                Shell.Failure("Error adding account.\n" + ex.Message);
            }
        }

        public void Clear()
        {
            AccountName = "New account";
            _account.Type = AccountType.Savings;
            AccountZipCode = "11111";
            OpeningBalance = 100m;
        }

        public void LoadPicture(string path)
        {
            try
            {
                _account.Picture = File.ReadAllBytes(path);
                //    _account.Picture = new Binary(File.ReadAllBytes(path));
            }
            catch (Exception ex)
            {
                Shell.Failure("Error loading picture.\n" + ex.Message);
            }
        }

        public void LoadPicture()
        {
            var filename = Shell.GetOpenFileName("Load Picture",
                "Image Files|*.bmp;*.jpg;*.png", string.Empty);
            if (filename != null) LoadPicture(filename);
        }

        public RelayCommand AddCommand { get; private set; }
        public RelayCommand ClearCommand { get; private set; }
        public RelayCommand LoadPictureCommand { get; private set; }

        public AddAccountViewModel()
        {
            AddCommand = new RelayCommand(p => Add(), p => _errors == 0);
            ClearCommand = new RelayCommand(p => Clear());
            LoadPictureCommand = new RelayCommand(p => LoadPicture());

            _account = new Account(); Clear();
            _accountController = new AccountController();
            _accountTypes = new List<AccountType>
            {
                AccountType.Savings,
                AccountType.Checking,
                AccountType.Loan
            };
        }

    }
}
