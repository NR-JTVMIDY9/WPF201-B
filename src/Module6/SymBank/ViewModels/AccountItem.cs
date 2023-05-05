using SymBank.Commands;
using SymBank.Controllers;
using SymBank.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace SymBank.ViewModels
{
    public class AccountItem : INotifyPropertyChanged
    {
        private Account _account;
        private decimal _amount;

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public Account Account
        {
            get { return _account; }
        }

        public decimal Amount
        {
            get { return _amount; }
            set {
                _amount = value;
                DebitCommand.NotifyCanExecuteChanged();
                NotifyPropertyChanged("Amount");
            }
        }

        private AccountController _controller;

        public void Debit()
        {
            try
            {
                var amount = Amount;
                _controller.Debit(_account.Code, amount);
                _account.Balance += amount;
                Amount = 0;
                Shell.Success($"Account {_account.Code} debited with {amount:C2}.");
            }
            catch (Exception ex)
            {
                Shell.Failure(
                    "Cannot debit account.\n" + ex.Message);
            }
        }

        public RelayCommand DebitCommand { get; private set; }

        private BitmapImage _image;

        public BitmapImage AccountPicture
        {
            get
            {
                if (_image != null) return _image;
                if (_account.Picture == null) return null;
                var stream = new System.IO.MemoryStream(_account.Picture);
                _image = new BitmapImage();
                _image.BeginInit();
                _image.CacheOption = BitmapCacheOption.OnLoad;
                _image.StreamSource = stream;
                _image.EndInit();
                stream.Dispose();
                return _image;
            }
        }
   
        public AccountItem(Account account)
        {
            DebitCommand = new RelayCommand(
                p => Debit(), p => _amount > 0);
            _controller = new AccountController();
            _account = account;
            _amount = 0;
        }


    }
}
