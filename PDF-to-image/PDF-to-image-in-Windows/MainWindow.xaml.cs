using Microsoft.Win32;
using Syncfusion.PdfToImageConverter;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Convert_PDF_page_into_image
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = string.Empty;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenPDF_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF documents (*.pdf)|*.PDF";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if(filePath != string.Empty)
                    convert.IsEnabled = true;
            }
        }


        private void convert_Click(object sender, RoutedEventArgs e)
        {
            FileStream inputPDFStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            PdfToImageConverter imageConverter = new PdfToImageConverter(inputPDFStream);
            Stream[] outputStream = new Stream[imageConverter.PageCount];
            // Convert all the PDF pages to images
            if (imageConverter.PageCount > 1)
            {
                outputStream = imageConverter.Convert(0, imageConverter.PageCount - 1, false, false);
            }
            else if (imageConverter.PageCount == 1)
            {
                outputStream[0] = imageConverter.Convert(0, false, false);
            }
            if (outputStream != null)
            {
                if (!Directory.Exists("PdfToImage"))
                {
                    Directory.CreateDirectory("PdfToImage");
                }
                foreach (Stream stream in outputStream)
                {
                    if (stream != null)
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        bitmap.Save(@"PdfToImage\Image" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png);
                    }
                }
                convert.IsEnabled = false;
                imageConverter.Dispose();
                if(MessageBox.Show("Do you want to view the converted image?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    //Launching the PDF file using the default Application.[Acrobat Reader]
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo(System.IO.Directory.GetCurrentDirectory() + @"\PdfToImage\");
                    process.Start();
                }
                
            }
        }
    }
}
