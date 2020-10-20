using System;
using System.ServiceProcess;
using Syncfusion.Windows.PdfViewer;
using System.Threading;
using System.IO;

namespace PDFPrinter
{
    public partial class Service1 : ServiceBase
    {
        PdfDocumentView pdfViewerControl = null;

        public Service1()
        {
            InitializeComponent();
            System.IO.Directory.SetCurrentDirectory(System.AppDomain.CurrentDomain.BaseDirectory);
        }
       
        protected override void OnStart(string[] args)
        {
            // Set the threading model to STA
            Thread thread = new Thread(PrintPDF);
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        // Prints PDF file
        void PrintPDF()
        {
            pdfViewerControl = new PdfDocumentView();
			// Load the PDF document to be printed
            pdfViewerControl.Load(@"../../Data/Barcode.pdf");
			// Print the PDF document silently using the printer name.
            pdfViewerControl.Print("Pass your printer name here...");
        }

        protected override void OnStop()
        {
        }
    }
}
