using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Parts2B
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

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            var thumb = (Thumb)sender;
            var transform = thumb.RenderTransform as TranslateTransform;
            if (transform == null)
            {
                transform = new TranslateTransform();
                thumb.RenderTransform = transform;
            }
            transform.X += e.HorizontalChange;
            transform.Y += e.VerticalChange;
        }
    }
}
