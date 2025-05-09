using Syncfusion.Pdf.Graphics;
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

namespace PdfCoordinateDetection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string file;
        public MainWindow()
        {
            InitializeComponent();

#if NETCOREAPP
            file = "../../../Data/F#.pdf";
#else
            file = "../../Data/F#.pdf";
#endif
            //Load the document
            pdfViewer.Load(file);

            //Hooking the Pdf Viewer page clicked event
            pdfViewer.PageClicked += PdfViewer_PageClicked; ;
        }

        private void PdfViewer_PageClicked(object sender, Syncfusion.Windows.PdfViewer.PageClickedEventArgs args)
        {
            //Get the current point where the mouse is clicked. The value is in pixels.
            Point currentPointInPixels = args.Position;

            //Convert the point from pixels to points.
            PdfUnitConvertor convertor = new PdfUnitConvertor();
            System.Drawing.PointF currentPoint = convertor.ConvertFromPixels(new System.Drawing.PointF((float)currentPointInPixels.X, (float)currentPointInPixels.Y), PdfGraphicsUnit.Point);

            //Convert the point based on the zoom factor.
            float zoomFactor = (float)pdfViewer.ZoomPercentage / 100;
            System.Drawing.PointF finalPoint = new System.Drawing.PointF(currentPoint.X / zoomFactor, currentPoint.Y / zoomFactor);

            //Display the point through message box.
            MessageBox.Show("The point clicked is: " + finalPoint.ToString());
        }
    }
}
