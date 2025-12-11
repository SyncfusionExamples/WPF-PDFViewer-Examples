using SkiaSharp;
using Syncfusion.PdfToImageConverter;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace ConsoleApp
{
    internal class Program
    {
        static int itr = 0;
        static Bitmap[] bitmaps;
        static void Main(string[] args)
        {
            //Initialize PDF to Image converter.
            PdfToImageConverter imageConverter = new PdfToImageConverter();
            //Load the PDF document as a stream
            FileStream inputStream = new FileStream("../../../Input.pdf", FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);
            bitmaps = new Bitmap[imageConverter.PageCount - 1];
            //Convert PDF to Image.
            Stream[] outputStream = imageConverter.Convert(0, imageConverter.PageCount - 1, false, false);
            bitmaps = BitmapConverter.ConvertStreamsToBitmaps(outputStream);
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;
            printDocument.Print();
        }

        private static void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bitmaps[itr], 0, 0, e.PageBounds.Width, e.PageBounds.Height);
            e.HasMorePages = itr < bitmaps.Length - 1;
            itr = itr + 1;
        }
    }

    public class BitmapConverter
    {
        public static Bitmap[] ConvertStreamsToBitmaps(Stream[] streams)
        {
            Bitmap[] bitmaps = new Bitmap[streams.Length];

            for (int i = 0; i < streams.Length; i++)
            {
                bitmaps[i] = new Bitmap(streams[i]);
            }

            return bitmaps;
        }
    }

}
