using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
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

namespace Bookmark_Navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //Wire the DocumentLoaded event of PDF Viewer
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;

            //Loads the PDF document
#if NETFRAMEWORK
            pdfViewer.Load("../../../Data/PDF_Succinctly.pdf");
#else
            pdfViewer.Load("../../../../Data/PDF_Succinctly.pdf");
#endif


        }
        private void PdfViewer_DocumentLoaded(object sender, System.EventArgs args)
        {
            //navigate to the desired bookmark destination
             GoToBookmark();
            //navigate to the child bookmarks
           // GoToChildBookmark();
        }
        private void GoToBookmark()
        {
            //Get the loadedDocument object from PDF Viewer
            PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;
            //Get the complete bookmarks in the PDF.
            PdfBookmarkBase bookmarks = pdfLoadedDocument.Bookmarks;
            //In this example, we get the first bookmark in the PDF bookmarks collection at the index of 0.
            PdfBookmark firstBookmark = bookmarks[0];

            //Navigates to the first bookmark present in the PDF.
            pdfViewer.GoToBookmark(firstBookmark);
        }
        private void GoToChildBookmark()
        {
            //Get the loadedDocument object from PDF Viewer
            PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;
            //Get the complete bookmarks in the PDF.
            PdfBookmarkBase bookmarks = pdfLoadedDocument.Bookmarks;
            //Gets the fourth bookmark in the PDF bookmarks collection at the index of 3.
            PdfBookmark fourthBookmark = bookmarks[3];
            //Check whether it has child bookmarks.
            if (fourthBookmark.Count > 0)
            {
                //Navigates to the first child of the fourth bookmark in the PDF.
                pdfViewer.GoToBookmark(bookmarks[3][0]);
            }
        }
    }
}
