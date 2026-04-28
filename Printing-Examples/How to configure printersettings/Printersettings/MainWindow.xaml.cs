using Syncfusion.PdfToImageConverter;
using System.Collections.Generic;
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
            List<System.Drawing.Image> pages = ConvertPdfToImages("../../../testing.pdf");
            PrintImage(pages);
        }

        private List<System.Drawing.Image> ConvertPdfToImages(string pdfPath)
        {
            PdfToImageConverter converter = new PdfToImageConverter();

            FileStream fs = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);
            converter.Load(fs);

            List<System.Drawing.Image> images = new List<System.Drawing.Image>();

            for (int i = 0; i < converter.PageCount; i++)
            {
                Stream stream = converter.Convert(i, false, false);
                images.Add(System.Drawing.Image.FromStream(stream));
            }

            return images;
        }

        private void PrintImage(List<System.Drawing.Image> images)
        {

            PrintDocument printDoc = new PrintDocument();
            int pageIndex = 0;
            printDoc.DefaultPageSettings.Color = true;
            printDoc.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169);
            printDoc.DefaultPageSettings.Margins = new Margins(100, 200, 150, 150);
            printDoc.DefaultPageSettings.PaperSource.RawKind = (int)PaperSourceKind.LargeFormat;

            if (printDoc.PrinterSettings.CanDuplex)
                printDoc.PrinterSettings.Duplex = Duplex.Horizontal;

            printDoc.PrinterSettings.DefaultPageSettings.PrinterResolution.Kind =
                PrinterResolutionKind.High;

            printDoc.PrintPage += (s, e) =>
            {
                System.Drawing.Image img = images[pageIndex];

                if (!e.PageSettings.Color)
                    img = ToGrayscale(img);

                e.Graphics.DrawImage(img, e.MarginBounds);

                pageIndex++;
                e.HasMorePages = pageIndex < images.Count;
            };

            printDoc.Print();
            foreach (var img in images)
                img.Dispose();

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
