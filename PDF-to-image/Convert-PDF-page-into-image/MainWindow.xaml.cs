using Syncfusion.PdfToImageConverter;
using System.Drawing;
using System.IO;
using System.Windows;

namespace Convert_PDF_page_into_image
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PdfToImageConverter pdfToImageConvertor = new PdfToImageConverter();
            FileStream inputPDFStream = new FileStream(@"../../../../Pdf/Arrow.pdf", FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            pdfToImageConvertor.Load(inputPDFStream);
            Stream image = pdfToImageConvertor.Convert(0, false, false);
            Bitmap bitmap = new Bitmap(image);
            bitmap.Save("output.png");
        }
    }
}
