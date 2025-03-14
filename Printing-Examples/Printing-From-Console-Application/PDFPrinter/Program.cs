using Syncfusion.Windows.PdfViewer;
using System;
using System.Runtime.InteropServices;

namespace PDFPrinter
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            PdfDocumentView pdfViewer = new PdfDocumentView();
#if NETFRAMEWORK
            pdfViewer.Load(@"../../Data/Barcode.pdf");
#else
            pdfViewer.Load(@"../../../Data/Barcode.pdf");
#endif
            
            pdfViewer.Print();
        }
    }
}