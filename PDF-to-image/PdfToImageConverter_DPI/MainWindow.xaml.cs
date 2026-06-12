using Syncfusion.PdfToImageConverter;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;

namespace PdfToImageConverter_DPI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PdfToImageConverter imageConverter = new PdfToImageConverter();

            FileStream inputStream = new FileStream("../../../Data/Input.pdf", FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);

            // Convert PDF page to image stream with desired DPI
            Stream outputStream = imageConverter.Convert(0, false, false);
            // Load the image
            Bitmap originalImage = new Bitmap(outputStream);

            //// Set DPI
            originalImage.SetResolution(451, 451);

            ////// Get TIFF codec
            ImageCodecInfo tiffCodec = ImageCodecInfo.GetImageEncoders().First(codec => codec.FormatID == ImageFormat.Tiff.Guid);

            // Set CCITT Group 4 compression
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Compression, (long)EncoderValue.CompressionCCITT4);

            // Save as 1-bit TIFF with CCITT G4
            originalImage.Save("sample.tif");
        }
    }
}