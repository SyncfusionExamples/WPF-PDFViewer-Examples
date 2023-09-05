using Syncfusion.PdfToImageConverter;
using System;
using System.Collections.Generic;
using System.Drawing;
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
