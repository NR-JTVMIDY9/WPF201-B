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

namespace Animation1
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

        // Storyboard sb1;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //sb1 = (Storyboard)FindResource("sb1");
            //sb1.Begin();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
        //  var sb1 = (Storyboard)FindResource("sb1");
            //sb1.Stop();
        }
    }
}
