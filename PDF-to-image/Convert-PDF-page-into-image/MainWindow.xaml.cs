using Microsoft.Win32;
using Syncfusion.PdfToImageConverter;
using System.Drawing;
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
            openFileDialog.DefaultExt = ".pdf";
            openFileDialog.Filter = "PDF documents (.pdf)|*.pdf";
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                if(filePath != string.Empty)
                    convert.IsEnabled= true;
            }
        }


        private void convert_Click(object sender, RoutedEventArgs e)
        {
            PdfToImageConverter pdfToImageConvertor = new PdfToImageConverter();
            FileStream inputPDFStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            pdfToImageConvertor.Load(inputPDFStream);
            Stream image = pdfToImageConvertor.Convert(0, false, false);
            if (image != null)
            {
                var result = MessageBox.Show("Do you want to view the converted image?", "Confirmation", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = image;
                    bitmapImage.EndInit();
                    resultantImage.Source = bitmapImage;
                }
                Bitmap bitmap = new Bitmap(image);
                bitmap.Save("output.png");
            }
        }
    }
}
