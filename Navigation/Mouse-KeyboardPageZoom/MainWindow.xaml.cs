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

namespace Mouse_KeyboardPageZoom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            PdfDocumentView1.Load(@"../../Data/HTTP Succinctly.pdf");
            this.SizeChanged += MainWindow_SizeChanged;
            PdfDocumentView1.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
            //Wire PdfDocumentView PreviewMouseWheel event
            PdfDocumentView1.PreviewMouseWheel += PdfDocumentView1_PreviewMouseWheel;
            //Wire PdfDocumentView KeyDown event
            PdfDocumentView1.KeyDown += PdfDocumentView1_KeyDown;
        }
        /// <summary>
        /// Zooming the Pdf page based on Mouse Wheel
        /// </summary>
        private void PdfDocumentView1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.Control)
            {
                if (e.Delta > 0)
                    PdfDocumentView1.ZoomTo(PdfDocumentView1.ZoomPercentage + 10);
                else
                    PdfDocumentView1.ZoomTo(PdfDocumentView1.ZoomPercentage - 10);
            }
        }
        /// <summary>
        /// Set the Zoom mode as FitPage/FitWidth for Pdf page based on the key
        /// </summary>
        private void PdfDocumentView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D0 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                PdfDocumentView1.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitPage;
            }
            if (e.Key == Key.D2 && Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                PdfDocumentView1.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
            }
        }
        /// <summary>
        /// Set the PdfDocumentView height and width based on Window size for User Interface
        /// </summary>
        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            PdfDocumentView1.Height = e.NewSize.Height - 40;
            PdfDocumentView1.Width = e.NewSize.Width - 40;
            PdfDocumentView1.ZoomMode = PdfDocumentView1.ZoomMode;
        }
    }
}
