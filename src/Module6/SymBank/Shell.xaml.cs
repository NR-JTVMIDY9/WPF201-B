using Microsoft.Win32;
using SymBank.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace SymBank
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Shell : Window
    {
        private static Shell _instance;

        private Style _sideRegionItemStyle;
        private Style _mainRegionItemStyle;

        public static Shell Instance { get { return _instance; } }

        public static void Success(string message)
        {
            MessageBox.Show(_instance, message, "Information",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void Failure(string message)
        {
            MessageBox.Show(_instance, message, "Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static bool Confirm(string message)
        {
            return MessageBox.Show(_instance, message, "Confirmation",
                MessageBoxButton.OKCancel, MessageBoxImage.Warning)
                == MessageBoxResult.OK;
        }

        public static string GetOpenFileName(string title, string filter, string filename)
        {
            var dialog = new OpenFileDialog(); dialog.Title = title;
            dialog.Filter = filter; dialog.FileName = filename;
            return dialog.ShowDialog() != true ? null : dialog.FileName;
        }
   
        public static string GetSaveFileName(string title, string filter, string filename)
        {
            var dialog = new SaveFileDialog(); dialog.Title = title;
            dialog.Filter = filter; dialog.FileName = filename;
            return dialog.ShowDialog() != true ? null : dialog.FileName;
        }

        public static object Status
        {
            get
            {
                return _instance.lblStatus.Content;
            }
            set
            {
                _instance.lblStatus.Content = value ?? "Ready.";
            }
        }

        public Shell()
        {
            _instance = this;
            InitializeComponent();
            _sideRegionViews = new Dictionary<BaseView, Expander>();
            _mainRegionViews = new Dictionary<BaseView, TabItem >();
            _sideRegionItemStyle = (Style)FindResource("SideRegionItemStyle");
            _mainRegionItemStyle = (Style)FindResource("MainRegionItemStyle");
        }

        public void Attach(BaseView view)
        {
            switch (view.Region)
            {
                case Region.SideRegion:
                    var host1 = new Expander();
                    host1.Style = _sideRegionItemStyle;
                    _sideRegionViews.Add(view, host1);
                    host1.SetBinding(Expander.HeaderProperty,
                        new Binding("Header") { Source = view });
                    host1.Content = view;
                    sideRegion.Items.Add(host1);
                    sideRegion.SelectedItem = host1;
                    if (!sideRegion.IsVisible)
                        sideRegion.Visibility = Visibility.Visible;
                    break;
                case Region.MainRegion:
                    var host2 = new TabItem();
                    host2.Style = _mainRegionItemStyle;
                    _mainRegionViews.Add(view, host2);
                    host2.SetBinding(TabItem.HeaderProperty,
                        new Binding("Header") { Source = view });
                    host2.Content = view;
                    mainRegion.Items.Add(host2);
                    mainRegion.SelectedItem = host2;
                    if (!mainRegion.IsVisible)
                        mainRegion.Visibility = Visibility.Visible;
                    break;
            }
        }

        public void Detach(BaseView view)
        {
            switch (view.Region)
            {
                case Region.SideRegion:
                    var host1 = _sideRegionViews[view];
                    sideRegion.Items.Remove(host1);
                    _sideRegionViews.Remove(view);
                    if (_sideRegionViews.Count == 0)
                        sideRegion.Visibility = Visibility.Collapsed;
                    break;
                case Region.MainRegion:
                    var host2 = _mainRegionViews[view];
                    mainRegion.Items.Remove(host2);
                    _mainRegionViews.Remove(view);
                    if (_mainRegionViews.Count == 0)
                        mainRegion.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblUserName.Content = MyApp.UserName;
            new CalendarView().Show();
            Debug.WriteLine("Window loaded and ready!");
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            Debug.WriteLine("Window activated! IsActive is " + IsActive);
        }

        private void Window_Deactivated(object sender, EventArgs e)
        {
            Debug.WriteLine("Window deactivated! IsActive is " + IsActive);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //    Debug.WriteLine("Window is closing!");
            if (!Confirm("Close application?"))
                e.Cancel = true;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Debug.WriteLine("Window closed!");
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"Location changed to {Left},{Top}!");
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Debug.WriteLine($"Size changed to {Width},{Height}!");
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            Debug.WriteLine($"Window state changed to {WindowState}!");
        }

        private void MnuAddNewAccount_Click(object sender, RoutedEventArgs e)
        {
            new AddAccountView().Show();
        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void mnuSearchForAccounts_Click(object sender, RoutedEventArgs e)
        {
        //    new SearchAccountsView().Show();
            var view = new SearchAccountsView();
            view.Show();
        }

        private void mnuAddTransaction_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuViewAccountTransactions_Click(object sender, RoutedEventArgs e)
        {

        }

        //private void mnuTextEditor_Click(object sender, RoutedEventArgs e)
        //{
        //    new TextEditorView().Show();
        //}

        private Dictionary<BaseView, Expander> _sideRegionViews;
        private Dictionary<BaseView, TabItem > _mainRegionViews;

    }
}
