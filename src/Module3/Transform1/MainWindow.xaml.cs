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

namespace Transform1
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
            var matrix = Matrix.Identity;
            matrix.Skew(10, 10);
            matrix.Scale(2, 2);
            matrix.Translate(-40, 40);
            matrix.Rotate(30);

            //  var tr = new MatrixTransform(matrix);
            //  button2.RenderTransform = tr;
            m1.Matrix = matrix;
        }
    }
}
