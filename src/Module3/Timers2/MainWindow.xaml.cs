using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace Timers2
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

        private Timer timer;
        private int counter;
        private Action updateAction;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            int startTime = 0;      // start immediately
            int timerTick = 250;    // 0.25 seconds

            timer = new Timer(Timer_Tick, null, startTime, timerTick);
            updateAction = UpdateCounter;
        }

        void UpdateCounter()
        {
            txtCounter.Text = counter.ToString();
        }

        void Timer_Tick(object state)
        {
            if (++counter == 100) timer.Dispose();
            Dispatcher.Invoke(updateAction);
        }
    }
}
