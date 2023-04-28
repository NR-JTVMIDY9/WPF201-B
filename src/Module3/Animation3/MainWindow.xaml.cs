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

namespace Animation3
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

        private void sb1_Completed(object sender, EventArgs e)
        {
            sb1.Stop(); sb2.Begin();
        }

        private void sb2_Completed(object sender, EventArgs e)
        {
            sb2.Stop(); sb1.Begin();
        }

        Storyboard sb1;
        Storyboard sb2;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        //    sb1 = (Storyboard)FindResource("sb1");
        //    sb2 = (Storyboard)FindResource("sb2");
        //    sb1.Begin();
        }
    }
}
