using SymBank.Commands;
using SymBank.Controllers;
using SymBank.Models;
using SymBank.Reports;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Documents;

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

        private AccountList _tempResults;

        private void SearchExecute(object p)
        {
            Thread.Sleep(6000);
            _tempResults = _accountController.GetObservableAccountsForName(_name);
        }

        private void SearchCompleted(Exception ex)
        {
            if (ex != null)
            {
                Shell.Failure(
                    "Error searching for accounts.\n" + ex.Message);
                return;
            }

            Results = _tempResults;
            Shell.Status = $"Search completed. {_results.Count} items found.";
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

        public void Print()
        {
            if (_results == null || _results.Count == 0)
            {
                Shell.Failure("Nothing to print.");
                return;
            }

            var itemsPerPage = 1;
            var totalItems = (uint)_results.Count;
            var totalPages = totalItems / itemsPerPage;
            if (totalItems % itemsPerPage > 0) ++totalPages;

            var pd = new PrintDialog();
            pd.MinPage = 1;
            pd.MaxPage = (uint)totalPages;
            if (pd.ShowDialog() != true) return;
            var minPage = (int)pd.MinPage;
            var maxPage = (int)pd.MaxPage;

            var doc = new FlowDocument();
            doc.PageHeight = pd.PrintableAreaHeight;
            doc.PageWidth = pd.PrintableAreaWidth;
            doc.ColumnWidth = pd.PrintableAreaWidth;
            doc.PagePadding = new System.Windows.Thickness(0);
            doc.ColumnGap = 0;

            try
            {
                for (var page = minPage; page <= maxPage; page++)
                {
                    var items = _results
                        .Skip((page - 1) * itemsPerPage)
                        .Take(itemsPerPage).ToList();
                    var p = new Paragraph();
                    p.BreakPageBefore = true;
                    var template = new AccountListReport();
                    template.Height = pd.PrintableAreaHeight;
                    template.Width = pd.PrintableAreaWidth;
                    template.Accounts = items;
                    template.CurPage = page;
                    template.MaxPage = maxPage;
                    p.Inlines.Add(template);
                    doc.Blocks.Add(p);
                }
                IDocumentPaginatorSource dps = doc;
                pd.PrintDocument(dps.DocumentPaginator, "Account List");
                Shell.Success("Account List printed.");
            }
            catch (Exception ex)
            {
                Shell.Failure("Error printing list.\n" + ex.Message);
            }
        }

        public RelayCommand PrintCommand { get; private set; }
        public RelayCommand SearchCommand { get; private set; }
        public AsyncRelayCommand SearchAsyncCommand { get; private set; }

        public SearchAccountsViewModel()
        {
            PrintCommand = new RelayCommand(p => Print());
            SearchCommand = new RelayCommand(p => Search());
            SearchAsyncCommand = new AsyncRelayCommand(
                SearchExecute, null, SearchCompleted);
            _accountController = new AccountController();
            _name = string.Empty;
        }
    }
}
