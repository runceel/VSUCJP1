using Demo3.ClientLib;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
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

namespace Demo3.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private AlertNotifier _alertNotifier;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _alertNotifier = new AlertNotifier();
            _alertNotifier.Alert += async (_, args) =>
            {
                await Dispatcher.InvokeAsync(() =>
                {
                    textBlockAlertMessage.Text = $"{DateTime.Now}: {args.Data.Value}";
                });
            };
            await _alertNotifier.InitializeAsync();
        }
    }


}
