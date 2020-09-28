using BelShina_HMI.Chart;
using BelShina_HMI.ViewModels;
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
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;
//using System.Drawing.Imaging;

namespace BelShina_HMI.Pages
{
    /// <summary>
    /// Interaction logic for GrafPage.xaml
    /// </summary>
    public partial class GrafPage : Page
    {
        private readonly GrafViewModel grafViewModel;
        public GrafPage(GrafViewModel grafViewModel)
        {
            InitializeComponent();
            //ForceGrafSet forceGrafSet = new ForceGrafSet();
            //grafViewModel = new GrafViewModel(forceGrafSet);
            this.grafViewModel = grafViewModel;
            DataContext = this.grafViewModel;
            Chart_P.ZoomingSpeed = 0.7;
            Chart_P.DisableAnimations = true;
            Chart_P.Zoom = ZoomingOptions.Y;
            Chart_P.AxisX[0].Separator.Step = 1;
            //this.grid.PreviewMouseRightButtonDown += Grid_PreviewMouseRightButtonDown;
            //this.grid.PreviewMouseRightButtonUp += Grid_PreviewMouseRightButtonUp;
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

        private void Grid_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            Chart_P.Zoom = ZoomingOptions.Y;
        }

        private void Grid_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Chart_P.Zoom = ZoomingOptions.X;
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            int width = (int)Chart_P.ActualWidth;
            int height = (int)Chart_P.ActualHeight;
            System.Windows.Point position = Chart_P.PointToScreen(new System.Windows.Point(0d, 0d));

            System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(/*(int)position.X*/0, /*(int)position.Y*/0, width, height + 86);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new System.Drawing.Point((int)position.X, (int)position.Y - 36), System.Drawing.Point.Empty, bounds.Size);
                }
                System.Windows.Forms.SaveFileDialog saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
                //saveFileDialog1.Filter = "JPeg Image|*.jpg";
                //saveFileDialog1.Title = "Сохранить график как изображение JPeg";
                //saveFileDialog1.ShowDialog();
                string imagePath = "hern.jpeg";//saveFileDialog1.FileName;
                bitmap.Save(imagePath, ImageFormat.Jpeg);
                Process.Start(imagePath);
            }
        }
    }
}
