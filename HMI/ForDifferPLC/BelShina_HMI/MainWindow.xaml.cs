using BelShina_HMI.Pages;
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

namespace BelShina_HMI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainPage mainPage;
        Settings settingsPage;
        public MainWindow()
        {
            InitializeComponent();
            mainPage = new MainPage();
            settingsPage = new Settings();
            Main.Content = mainPage;
        }

        private void MainBtcClck(object sender, RoutedEventArgs e)
        {
            //new Thread(() => this.Dispatcher.Invoke(() => Main.Content = mainPage)).Start();
            Main.Content = mainPage;
        }

        private void SettingsBtnClck(object sender, RoutedEventArgs e)
        {
            //new Thread(() => this.Dispatcher.Invoke(() => Main.Content = mainPage)).Start();
            Main.Content = settingsPage;
        }
    }
}
