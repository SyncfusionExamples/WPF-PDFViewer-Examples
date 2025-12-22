using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bitmap = System.Drawing.Bitmap;
using Path = System.Windows.Shapes.Path;

namespace WPF_Sample_FW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "../../../Data/";
        public MainWindow()
        {
            InitializeComponent();

            PDFViewer.Load(filePath + "Input.pdf");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Load a PDF document
            PdfLoadedDocument ldoc = new PdfLoadedDocument(filePath + "Input.pdf");

            ldoc.FlattenAnnotations();

            if (ldoc.Form != null && ldoc.Form.Fields.Count > 0)
                ldoc.Form.FlattenFields();

            //Create a new instance of PdfDocument class
            PdfDocument document = new PdfDocument();

            //Set page margin value as 0
            document.PageSettings.Margins.All = 0;

            //Set page size as A3
            document.PageSettings.Size = PdfPageSize.A3;

            for (int i = 0; i < ldoc.Pages.Count; i++)
            {
                //Get loaded page as template
                PdfTemplate template = ldoc.Pages[i].CreateTemplate();

                //Create new page
                PdfPage page = document.Pages.Add();

                //Create Pdf graphics for the page 
                PdfGraphics g = page.Graphics;

                //Draw template with the size as page size
                g.DrawPdfTemplate(template, new PointF(0, 0), new SizeF(page.GetClientSize().Width, page.GetClientSize().Height));

            }
            document.Save(filePath + "Output.pdf");
            PDFViewer.Load(filePath + "Output.pdf");
        }
    }
}
