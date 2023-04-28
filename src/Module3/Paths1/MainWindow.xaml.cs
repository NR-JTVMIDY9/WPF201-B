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

namespace Paths1
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
            var stream = new StreamGeometry();
            var context = stream.Open();
            context.BeginFigure(new Point(10, 0), true, true);
            context.QuadraticBezierTo(
                new Point(0, 12.5),
                new Point(10, 25),
                true, true);
            context.LineTo(new Point(80, 25), true, true);
            context.LineTo(new Point(90, 37.5), true, true);
            context.LineTo(new Point(90, 25), true, true);
            context.QuadraticBezierTo(
                new Point(100, 12.5),
                new Point(90, 0),
                true, true);
            context.Close();
            stream.Freeze();
            path2.Data = stream;
        }
    }
}
