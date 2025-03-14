﻿using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PDFToTIFFSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if NETFRAMEWORK
            ExportPDFtoTiff("../../Data/Barcode.pdf");
#else
            ExportPDFtoTiff("../../../Data/Barcode.pdf");
#endif
        }

        /// <summary>
        /// Exports the PDF pages as Tiff Images.
        /// </summary>
        /// <param name="fileName">The PDF file name</param>
        private void ExportPDFtoTiff(string fileName)
        {
            PdfDocumentView pdfViewer = new PdfDocumentView();
            //Load the input PDF file
            pdfViewer.Load(fileName);
            //Export the images From the input PDF file at the page range of 0 to 1 .
            BitmapSource[] image = pdfViewer.ExportAsImage(0, pdfViewer.PageCount - 1);
            if (image != null)
            {
                for (int i = 0; i < image.Length; i++)
                {
                    //Initialize the new Tiff bitmap encoder
                    TiffBitmapEncoder encoder = new TiffBitmapEncoder();
                    //Set the compression to zip to reduce the file size.
                    encoder.Compression = TiffCompressOption.Zip;
                    //Create the bitmap frame using the bitmap source and add it to the encoder.
                    encoder.Frames.Add(BitmapFrame.Create(image[i]));
                    //Create the file stream for the output in the desired image format.
                    using (FileStream stream = new FileStream("Image_" + i.ToString() + ".Tiff", FileMode.Create))
                    {
                        //Save the stream, so that the image will be generated in the output location.
                        encoder.Save(stream);
                    }
                }
            }
        }
    }
}
