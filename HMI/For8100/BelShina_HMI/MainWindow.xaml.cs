using BelShina_HMI.Chart;
using BelShina_HMI.Pages;
using BelShina_HMI.ViewModels;
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
        //GrafViewModel grafViewModel_1;
        CycleForceGrafViewModel grafViewModel_1;
        LineForceGrafViewModel_1 lineForceGrafViewModel_1;
        LineForceGrafViewModel_2 lineForceGrafViewModel_2;
        ConturViewModel_1 conturViewModel_1;
        ConturViewModel_2 conturViewModel_2;
        ForceGrafSet forceGrafSet;
        ForceGrafSetLine_1 forceGrafSetLine_1;
        ForceGrafSetLine_2 forceGrafSetLine_2;
        ConturGrafSet conturGrafSet;
        GrafPage grafPage_1;
        GrafPage grafPage_2;
        GrafPage grafPage_3;
        GrafPage ConturPage_1;
        GrafPage ConturPage_2;
        
        public MainWindow()
        {
            InitializeComponent();
            
            mainPage = new MainPage();
            settingsPage = new Settings();
            forceGrafSet = new ForceGrafSet();
            forceGrafSetLine_1 = new ForceGrafSetLine_1();
            forceGrafSetLine_2 = new ForceGrafSetLine_2();
            conturGrafSet = new ConturGrafSet();
            //grafViewModel_1 = new GrafViewModel(forceGrafSet);
            grafViewModel_1 = new CycleForceGrafViewModel(forceGrafSet, "Угловое перемещение");
            lineForceGrafViewModel_1 = new LineForceGrafViewModel_1(forceGrafSetLine_1, "Продольное перемещение");
            lineForceGrafViewModel_2 = new LineForceGrafViewModel_2(forceGrafSetLine_2, "Поперечное перемещение");
            conturViewModel_1 = new ConturViewModel_1(conturGrafSet, "Продольный контур");
            conturViewModel_2 = new ConturViewModel_2(conturGrafSet, "Поперечный контур");
            grafPage_1 = new GrafPage(grafViewModel_1);
            grafPage_2 = new GrafPage(lineForceGrafViewModel_1);
            grafPage_3 = new GrafPage(lineForceGrafViewModel_2);
            ConturPage_1 = new GrafPage(conturViewModel_1);
            ConturPage_2 = new GrafPage(conturViewModel_2);
            Main.Content = mainPage;
        }

        private void MainBtcClck(object sender, RoutedEventArgs e)
        {
            //new Thread(() => this.Dispatcher.Invoke(() => Main.Content = mainPage)).Start();
            Main.Content = mainPage;
        }

        private void SettingsBtnClck(object sender, RoutedEventArgs e)
        {
            Main.Content = settingsPage;
        }

        private void btnGragClk_1(object sender, RoutedEventArgs e)
        {
            Main.Content = grafPage_1;
        }
        private void btnGragClk_2(object sender, RoutedEventArgs e)
        {
            Main.Content = grafPage_2;
        }
        private void btnGragClk_3(object sender, RoutedEventArgs e)
        {
            Main.Content = grafPage_3;
        }
        private void btnContClk_1(object sender, RoutedEventArgs e)
        {
            Main.Content = ConturPage_1;
        }
        private void btnContClk_2(object sender, RoutedEventArgs e)
        {
            Main.Content = ConturPage_2;
        }
        private void btnAlmClk(object sender, RoutedEventArgs e)
        {
            Main.Content = grafPage_1;
        }
    }
}
