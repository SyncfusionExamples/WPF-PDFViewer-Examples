using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using System;
using System.Collections.Generic;
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

namespace HideAnnotationsInExportedPages
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewerControl.Load("../../Data/JavaScript Succinctly.pdf");
        }
        private void ExportAsImage_Click(object sender, RoutedEventArgs e)
        {
            int pagecount = pdfViewerControl.LoadedDocument.Pages.Count;

            //Hide the annotations from the PDF
            for (int j = 0; j < pagecount; j++)
            {
                PdfPageBase page = pdfViewerControl.LoadedDocument.Pages[j];
                foreach (PdfAnnotation annotation in page.Annotations)
                {
                    pdfViewerControl.HideAnnotation(annotation.Name, j + 1);
                }
            }

            //Export pdf pages without annotations
            BitmapSource[] image = pdfViewerControl.ExportAsImage(0, pagecount - 1);
            //Set up the output path
            string output = @"..\..\Data\Image";
            if (image != null)
            {
                for (int i = 0; i < image.Length; i++)
                {
                    //Initialize the new Jpeg bitmap encoder
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    //Create the bitmap frame using the bitmap source and add it to the encoder
                    encoder.Frames.Add(BitmapFrame.Create(image[i]));
                    //Create the file stream for the output in the desired image format
                    FileStream stream = new FileStream(output + i.ToString() + ".Jpeg", FileMode.Create);
                    //Save the stream, so that the image will be generated in the output location
                    encoder.Save(stream);
                }
            }

            //Show the hidden annotations 
            for (int j = 0; j < pagecount; j++)
            {
                PdfPageBase page = pdfViewerControl.LoadedDocument.Pages[j];
                foreach (PdfAnnotation annotation in page.Annotations)
                {
                    pdfViewerControl.ShowAnnotation(annotation.Name, j + 1);
                }
            }
        }
    }
}
