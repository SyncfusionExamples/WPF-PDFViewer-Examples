using Syncfusion.PdfToImageConverter;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
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

namespace PrintCollate
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

            FileStream inputStream = new FileStream("../../testing.pdf", FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);

            int pageCount = imageConverter.PageCount;
            List<System.Drawing.Image> images = new List<System.Drawing.Image>();

            for (int i = 0; i < pageCount; i++)
            {
                Stream outputStream = imageConverter.Convert(i, false, true);
                Bitmap image = new Bitmap(outputStream);
                images.Add(new Bitmap(image));
            }

            PrintImages(images);
        }

        private void PrintImages(List<System.Drawing.Image> images)
        {
            bool collate = false;
            short copies = 2;
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrinterSettings.Collate = collate;
            int totalPages = images.Count;
            int printIndex = 0;
            printDoc.PrintPage += (s, e) =>
            {
                int pageIndex;
                if (collate)
                {
                    // Print full document, then repeat
                    pageIndex = printIndex % totalPages;
                }
                else
                {
                    // Print each page multiple times before moving to next
                    pageIndex = printIndex / copies;
                }

                e.Graphics.DrawImage(images[pageIndex], e.MarginBounds);

                printIndex++;
                e.HasMorePages = printIndex < copies * totalPages;

            };
            printDoc.Print();
        }
    }
}
