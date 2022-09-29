using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;

namespace WpfPDFViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Loads the document in PDF Viewer
            pdfViewer.Load("../../F Sharp Succinctly.pdf");
        }

        private void Add_Ink_Click(object sender, RoutedEventArgs e)
        {
            //Gets the loadedDocument from PDF Viewer
            PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;

            //Specifies the ink points
            List<float> linePoints = new List<float> { 40, 300, 60, 100, 40, 50, 40, 300 };

            //Specifies the bounds of an annotation
            RectangleF rectangle = new RectangleF(0, 0, 300, 400);

            //Creates a new ink annotation
            PdfInkAnnotation inkAnnotation = new PdfInkAnnotation(rectangle, linePoints);

            inkAnnotation.Color = new PdfColor(System.Drawing.Color.Red);

            //Adds annotation to the page
            loadedDocument.Pages[0].Annotations.Add(inkAnnotation);
        }
    }
}
