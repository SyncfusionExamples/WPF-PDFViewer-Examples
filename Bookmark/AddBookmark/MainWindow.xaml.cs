using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf;
using System.IO;
using System.Windows;

namespace AddBookmark
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double hOffset, vOffset;
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../../PDF/HTTP Succinctly.pdf");
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //Retrive the HorizontalOffset and VerticalOffset properties value.
            hOffset = pdfViewer.HorizontalOffset;
            vOffset = pdfViewer.VerticalOffset;
            //Calculate the zoom factor using the ZoomPercentage property, convert the offset values based on this factor.
            float zoomFactor = (float)pdfViewer.ZoomPercentage / 100;
            vOffset /= zoomFactor;
            hOffset /= zoomFactor;
            //Convert offset values from pixels to points
            PdfUnitConvertor converter = new PdfUnitConvertor();
            float vertical = converter.ConvertFromPixels((float)vOffset, PdfGraphicsUnit.Point);
            float horizontal = converter.ConvertFromPixels((float)hOffset, PdfGraphicsUnit.Point);
            var document = pdfViewer.LoadedDocument;
            int index = pdfViewer.CurrentPageIndex - 1;
            var currentPage = document.Pages[index];
            // Calculate the offset for the current page alone excluding the previous page offset values.
            foreach (var page in document.Pages)
            {
                float height = (page as PdfPageBase).Size.Height;
                if (vertical > height)
                    vertical = vertical - height;
            }
            //Insert a new bookmark in the existing bookmark collection.
            PdfBookmark bookmark = document.Bookmarks.Insert(document.Bookmarks.Count, "New Bookmark");
            //Sets the destination page and location.
            bookmark.Destination = new PdfDestination(currentPage);
            bookmark.Destination.Location = new System.Drawing.PointF((float)horizontal, (float)vertical);
            MemoryStream stream = new MemoryStream();
            document.Save(stream);
            pdfViewer.Load(stream);
        }
    }
}
