using Microsoft.Win32;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            //Creates a new ink annotation

            RectangleF rectangle = new RectangleF(0, 0, 300, 400);

            PdfInkAnnotation inkAnnotation = new PdfInkAnnotation(rectangle, linePoints);

            inkAnnotation.Color = new PdfColor(System.Drawing.Color.Red);

            ////Adds annotation to the page
            loadedDocument.Pages[0].Annotations.Add(inkAnnotation);

            //Creates new memory stream
            MemoryStream stream = new MemoryStream();

            //Save the loadedDocument as stream
            loadedDocument.Save(stream);

            //Load the stream in PDF Viewer
            pdfViewer.Load(stream);
        }
    }
}
