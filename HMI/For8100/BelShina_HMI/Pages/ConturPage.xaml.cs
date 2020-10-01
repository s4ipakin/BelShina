using LiveCharts;
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

namespace BelShina_HMI.Pages
{
    /// <summary>
    /// Interaction logic for ConturPage.xaml
    /// </summary>
    public partial class ConturPage : Page
    {
        public ConturPage()
        {
            InitializeComponent();
            Chart_P.ZoomingSpeed = 0.7;
            Chart_P.DisableAnimations = true;
            Chart_P.Zoom = ZoomingOptions.Y;
            Chart_P.AxisX[0].Separator.Step = 1;
            Chart_P.PreviewMouseRightButtonDown += Chart_P_PreviewMouseRightButtonDown;
            Chart_P.PreviewMouseRightButtonUp += Chart_P_PreviewMouseRightButtonUp;
        }

        private void Chart_P_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Chart_P.Zoom = ZoomingOptions.Y;
        }

        private void Chart_P_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Chart_P.Zoom = ZoomingOptions.X;
        }
    }
}
