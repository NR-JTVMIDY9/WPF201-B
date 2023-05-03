using SymBank.Commands;
using SymBank.Controllers;
using SymBank.Models;
using System;

namespace SymBank.ViewModels
{
    public class SearchAccountsViewModel : BaseViewModel
    {
        private AccountController _accountController;
        private AccountList _results;
        private string _name;

        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        public AccountList Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
                NotifyPropertyChanged("Results");
            }
        }

        public void Search()
        {
            try
            {
                Results = _accountController.GetObservableAccountsForName(_name);
                //Shell.Status = string.Format(
                //    "Search completed. {0} items found.",
                //    _results.Count);
                Shell.Status = $"Search completed. {_results.Count} items found.";
            }
            catch (Exception ex)
            {
                Shell.Failure(
                    "Error searching for accounts.\n" + ex.Message);
            }
        }

        public RelayCommand SearchCommand { get; private set; }

        public SearchAccountsViewModel()
        {
            SearchCommand = new RelayCommand(p => Search());
            _accountController = new AccountController();
            _name = string.Empty;
        }
    }
}
