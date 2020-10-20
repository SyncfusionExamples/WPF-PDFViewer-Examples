using Syncfusion.Windows.PdfViewer;
using System;

namespace PDFPrinter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            PdfDocumentView pdfViewer = new PdfDocumentView();
            pdfViewer.Load(@"../../Data/Barcode.pdf");
            pdfViewer.Print();
        }
    }
}