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

namespace Symbion
{
    /// <summary>
    /// Interaction logic for PhotoImage.xaml
    /// </summary>
    public partial class PhotoImage : UserControl
    {
        public PhotoImage()
        {
            InitializeComponent();
        }

        public ImageSource PhotoSource
        {
            get { return photoImage.Source; }
            set { photoImage.Source = value; }
        }

        public string PhotoTitle
        {
            get { return photoText.Text; }
            set { photoText.Text = value; }
        }

        public Brush PhotoBorderBrush
        {
            get { return photoBorder.BorderBrush; }
            set { photoBorder.BorderBrush = value; }
        }
    }
}
