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

namespace Brushes1
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var tb1 = new TextBlock();
            tb1.Text = "Welcome to XAML!";
            tb1.Margin = new Thickness(8);
            tb1.FontFamily = new FontFamily("Georgia");
            tb1.FontSize = 36;
            //  tb1.Foreground = Brushes.LightSeaGreen;
            //  tb1.Foreground = new SolidColorBrush(Colors.LightSalmon);
            //  tb1.Foreground = new SolidColorBrush(Color.FromRgb(0x8E,0xCA,0xE6));
            //  Resources["brush1"] = Brushes.Gold;

            var brush1 = (LinearGradientBrush)FindResource("brush1");
            brush1.GradientStops[0].Color = Colors.Fuchsia;
            tb1.Foreground = brush1;
            Panel1.Children.Add(tb1);

        //    brush1.Color = Colors.LightPink;
        }
    }
}
