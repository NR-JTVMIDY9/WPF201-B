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

namespace Windows1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private Window1 window1;

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (window1 != null)
            {
                window1.Focus();
                return;
            }

            window1 = new Window1();
            window1.Closed += (ws, we) => window1 = null;
            window1.Owner = this;
            window1.Show();
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window2();
            window.Owner = this;
            window.Show();
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window3();
            window.Owner = this;
            window.Show();
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window4();
            window.Owner = this;
            window.Show();
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window5();
            window.Owner = this;
            window.Show();
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window6();
            window.Owner = this;
            window.Show();
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            //Application app = App.Current;
            //Window mainWindow = app.MainWindow;
            //WindowCollection windows = app.Windows;
            //foreach (Window window in windows)
            //    if (window != mainWindow) window.Close();
            //    foreach (Window window in App.Current.Windows)
            //        if (window != this) window.Close();
            foreach (Window window in OwnedWindows)
                window.Close();
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            var window = new Window7();
            window.Owner = this;
            bool? result = window.ShowDialog();
            if (result == true)
            {
                MessageBox.Show("Window6 return true");
            }
        }
    }
}
