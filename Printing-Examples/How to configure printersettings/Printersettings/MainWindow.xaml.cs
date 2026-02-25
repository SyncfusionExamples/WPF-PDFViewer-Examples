using Syncfusion.PdfToImageConverter;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Printersettings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PdfToImageConverter imageConverter = new PdfToImageConverter();
            //Load the PDF document as a stream
            FileStream inputStream = new FileStream("../../../testing.pdf", FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);
            //Convert PDF to Image.
            Stream outputStream = imageConverter.Convert(0, false, true);
            Bitmap image = new Bitmap(outputStream);
            image.Save("tmp_without.png");
            PrintImage("tmp_without.png");
        }

        private void PrintImage(string imagePath)
        {
            using (System.Drawing.Image original = System.Drawing.Image.FromFile(imagePath))
            {
                PrintDocument printDoc = new PrintDocument();
                var img = original;
                printDoc.PrinterSettings.DefaultPageSettings.Color = true;
                //customize printer settings
                printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
                // Set margins
                printDoc.DefaultPageSettings.Margins = new Margins(100, 200, 150, 150);
                //Set paper source
                printDoc.DefaultPageSettings.PaperSource.RawKind = (int)PaperSourceKind.LargeFormat;
                //// Set paper kind
                //printDoc.PrinterSettings.DefaultPageSettings.PaperSize.RawKind = (int)PaperKind.A4;
                // Set duplex printing
                if (printDoc.PrinterSettings.CanDuplex)
                    printDoc.PrinterSettings.Duplex = Duplex.Horizontal;
                // Set printer resolution
                printDoc.PrinterSettings.DefaultPageSettings.PrinterResolution.Kind = PrinterResolutionKind.High;
                // Check if the printer supports color printing
                if (!printDoc.PrinterSettings.DefaultPageSettings.Color)
                {
                    img = ToGrayscale(original);
                }
                printDoc.PrintPage += (s, e) =>
                {
                    e.Graphics.DrawImage(img, e.MarginBounds);
                };
                printDoc.Print();
            }
        }
        private static System.Drawing.Image ToGrayscale(System.Drawing.Image source)
        {
            var bmp = new Bitmap(source.Width, source.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (var g = Graphics.FromImage(bmp))
            {
                var matrix = new ColorMatrix(new float[][]
                {
            new float[] {.299f, .299f, .299f, 0, 0},
            new float[] {.587f, .587f, .587f, 0, 0},
            new float[] {.114f, .114f, .114f, 0, 0},
            new float[] {0,      0,      0,      1, 0},
            new float[] {0,      0,      0,      0, 1}
                });
                var ia = new ImageAttributes();
                ia.SetColorMatrix(matrix);
                g.DrawImage(source, new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height),
                    0, 0, source.Width, source.Height, GraphicsUnit.Pixel, ia);
            }
            return bmp;
        }
    }
}