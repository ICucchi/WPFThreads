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

namespace WPFThreads
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _counter = 0;
        private int _counter1 = 0;
        private int _counter2 = 0;
        private int _totalCounter = 0;
        private bool contatore = false;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            contatore = true;
            StartButton.IsEnabled = false;

            Thread counterThread = new Thread(
            () =>
            {
                while (_counter < 5000 && contatore)
                {
                    _counter++;
                    _totalCounter++;
                    Dispatcher.Invoke(
                    () =>
                        CounterTextBlock.Text = _counter.ToString()
                    );
                    Dispatcher.Invoke(
                    () => 
                        TotalCounterTextBlock.Text = _totalCounter.ToString()
                    );
                    Thread.Sleep(1);
                }
            }
            );
            counterThread.Start();

            Thread counterThread1 = new Thread(
            () =>
            {
                while (_counter1 < 500 && contatore)
                {
                    _counter1++;
                    _totalCounter++;
                    Dispatcher.Invoke(
                    () => 
                        Counter1TextBlock.Text = _counter1.ToString()
                    );
                    Dispatcher.Invoke(
                    () => 
                        TotalCounterTextBlock.Text = _totalCounter.ToString()
                    );
                    Thread.Sleep(10);
                }
            }
            );
            counterThread1.Start();

            Thread counterThread2 = new Thread(
            () =>
            {
                while (_counter2 < 50 && contatore)
                {
                    _counter2++;
                    _totalCounter++;
                    Dispatcher.Invoke(
                    () =>
                        Counter2TextBlock.Text = _counter2.ToString()
                    ); ;
                    Dispatcher.Invoke(
                    () => 
                        TotalCounterTextBlock.Text = _totalCounter.ToString()
                    );
                    Thread.Sleep(100);
                }
            }
            );
            counterThread2.Start();

            
            Thread reenableButtonThread = new Thread(
            () =>
            {
                counterThread.Join();
                counterThread1.Join();
                counterThread2.Join();
                Dispatcher.Invoke(
                () => 
                    StartButton.IsEnabled = true,

                );
            }
            );
            reenableButtonThread.Start();
        }
    }
}
