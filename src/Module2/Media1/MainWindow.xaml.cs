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

namespace Media1
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

        private void BtnPlay_Click(object sender, RoutedEventArgs e)
        {
            media1.Play();
        }

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            if (media1.CanPause)
                media1.Pause();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            media1.Stop();
        }

        private void BtnMute_Click(object sender, RoutedEventArgs e)
        {
            media1.IsMuted = !media1.IsMuted;
        }

        private void Media1_MediaOpened(object sender, RoutedEventArgs e)
        {

        }

        private void Media1_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }

        private void Media1_MediaEnded(object sender, RoutedEventArgs e)
        {
            
        }

        //private void HslVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    try
        //    {
        //        media1.Volume = hslVolume.Value;
        //    }   catch {  }
        //}
    }
}
