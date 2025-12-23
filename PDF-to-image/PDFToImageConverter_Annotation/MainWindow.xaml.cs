using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.PdfToImageConverter;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

namespace PDFToImageConverter_Annotation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Creates a new pdf document
            PdfDocument document = new PdfDocument();
            //Creates a new page
            PdfPage page = document.Pages.Add();

            //Creates PDF free text annotation
            PdfFreeTextAnnotation freeText = new PdfFreeTextAnnotation(new RectangleF(50, 100, 100, 50));
            //Sets properties to the annotation
            freeText.MarkupText = "Free Text with Callout";
            freeText.TextMarkupColor = new PdfColor(System.Drawing.Color.Black);
            freeText.Font = new PdfStandardFont(PdfFontFamily.Helvetica, 7f);
            freeText.Color = new PdfColor(System.Drawing.Color.Yellow);
            freeText.BorderColor = new PdfColor(System.Drawing.Color.Red);
            freeText.Border = new PdfAnnotationBorder(.5f);
            freeText.LineEndingStyle = PdfLineEndingStyle.OpenArrow;
            freeText.AnnotationFlags = PdfAnnotationFlags.Default;
            freeText.Text = "Free Text";
            freeText.Opacity = 0.5f;
            PointF[] points = { new PointF(100, 450), new PointF(100, 200), new PointF(100, 150) };
            freeText.CalloutLines = points;
            freeText.SetAppearance(true);
            //Adds the annotation to page
            page.Annotations.Add(freeText);

            //Save the document
            document.Save("../../Output.pdf");
            //Closes the document
            document.Close(true);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfToImageConverter imageConverter = new PdfToImageConverter();
            using (FileStream inputStream = new FileStream("../../Output.pdf", FileMode.Open, FileAccess.ReadWrite))
            {
                imageConverter.Load(inputStream);
                // Convert the first page (index 0)
                using (Stream outputStream = imageConverter.Convert(0, false, false))
                using (Bitmap Image = new Bitmap(outputStream))
                {
                    Image.Save("sample.png"); // Produces an image that includes the FreeText annotation
                }
            }
        }
    }
}
