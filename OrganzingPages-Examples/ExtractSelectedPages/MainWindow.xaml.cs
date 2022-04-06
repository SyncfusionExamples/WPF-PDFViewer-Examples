using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System.Diagnostics;
using System.Windows;

namespace PdfViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        int[] selectedPages = null;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //Wire the PageSelected event
            pdfViewer.PageSelected += PdfViewer_PageSelected;
            pdfViewer.Load(@"../../Data/PDF_Succinctly.pdf");
        }

        //Handle the PageSelected Event
        private void PdfViewer_PageSelected(object sender, PageSelectedEventArgs e)
        {
            //Get the selected page indices
            selectedPages = e.SelectedPages;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (selectedPages != null)
            {
                //Get the instance of loadedDocument
                PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;
                //Create a new document.
                PdfDocument doc = new PdfDocument();

                for (int i = 0; i < selectedPages.Length; i++)
                {
                    // Importing pages from source document.
                    doc.ImportPage(loadedDocument, selectedPages[i]);
                }
                doc.Save("ImportPages.pdf");
                //Close both the instance
                doc.Close(true);
                //Load the document containing the imported pages
                pdfViewer.Load("ImportPages.pdf");
            }
        }
    }
}
