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
