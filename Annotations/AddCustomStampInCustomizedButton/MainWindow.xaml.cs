using Syncfusion.Pdf.Graphics;
using Syncfusion.Windows.PdfViewer;
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

namespace AddCustomStampInCustomizedButton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool checkAddAnnotation = false;
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../Data/F#.pdf");
            pdfViewer.PageClicked += PdfViewer_PageClicked;
        }

        private void PdfViewer_PageClicked(object sender, Syncfusion.Windows.PdfViewer.PageClickedEventArgs args)
        {
            if(checkAddAnnotation)
            {
                PdfUnitConvertor convertor = new PdfUnitConvertor();
                Mouse.OverrideCursor = Cursors.Arrow;
                Point clientPoint = args.Position;
                //Retrieve the page number that corresponds to the client point
                int pageNumber = pdfViewer.CurrentPageIndex;

                //Retrieve the page point
                Point pagePoint = pdfViewer.ConvertClientPointToPagePoint(clientPoint, pageNumber);
                double x = pagePoint.X;
                double y = pagePoint.Y;
                x = convertor.ConvertToPixels((float)args.Position.X, PdfGraphicsUnit.Pixel);
                y = convertor.ConvertToPixels((float)args.Position.Y, PdfGraphicsUnit.Pixel);

                Point position = new Point(x, y);
                pdfViewer.AnnotationMode = PdfDocumentView.PdfViewerAnnotationMode.None;
                var bitmapImage = new BitmapImage(new Uri("../../Data/ThankYou.png", UriKind.RelativeOrAbsolute));
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                var image = new System.Windows.Controls.Image() { Source = bitmapImage };
                var pdfStamp = new PdfStampAnnotation(image);
                if (pdfViewer.ZoomPercentage != 100)
                {
                    float zoomFactor = (float)pdfViewer.ZoomPercentage / 100.0f;
                    var x1 = position.X / zoomFactor;
                    var y1 = position.Y / zoomFactor;
                    position = new Point(x1, y1);
                }
                //Enter the required size of the stamp.
                System.Drawing.Size stampSize = new System.Drawing.Size(200, 100);
                pdfViewer.AddStamp(pdfStamp, pageNumber, position, stampSize);
                checkAddAnnotation = false;
            }          
        }

        private void AddStamp_Click(object sender, RoutedEventArgs e)
        {
            checkAddAnnotation = !checkAddAnnotation;            
        }
    }
}
