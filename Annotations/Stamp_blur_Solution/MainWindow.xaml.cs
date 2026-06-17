using Syncfusion.Pdf.Graphics;
using Syncfusion.Windows.PdfViewer;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Stamp_blur_Solution
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PDFViewer.Load("../../../Data/Input.pdf");
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var unitConvertor = new PdfUnitConvertor();

            // Stamp properties
            int SX = (int)unitConvertor.ConvertToPixels(125, PdfGraphicsUnit.Point);
            int SY = (int)unitConvertor.ConvertToPixels(108, PdfGraphicsUnit.Point);
            string StampNo = "01";
            int StampSize = 100;

            int StampWidth = (int)unitConvertor.ConvertToPixels(20, PdfGraphicsUnit.Point);
            int StampHeight = (int)unitConvertor.ConvertToPixels(20, PdfGraphicsUnit.Point);

            // 1. Create a high-resolution Bitmap for the custom stamp
            using (var customStampBitmap = new Bitmap(StampSize, StampSize))
            {
                using (Graphics g = Graphics.FromImage(customStampBitmap))
                {
                    g.Clear(Color.Transparent);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    // Draw ellipse
                    using (var pen = new Pen(Color.Red, 3))
                    {
                        g.DrawEllipse(pen, 3, 3, StampSize - 6, StampSize - 6);
                    }

                    // Draw stamp number text (centered)
                    using (var font = new Font("Helvetica", 28))
                    {
                        SizeF textSize = g.MeasureString(StampNo, font);
                        var textPoint = new System.Drawing.PointF(
                            (StampSize - textSize.Width) / 2f,
                            (StampSize - textSize.Height) / 2f);
                        g.DrawString(StampNo, font, Brushes.Red, textPoint);
                    }
                }

                // 2. Convert Bitmap to BitmapImage for WPF PdfViewer
                BitmapImage bitmapImage;
                using (var ms = new MemoryStream())
                {
                    customStampBitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.Position = 0;

                    bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = ms;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                }

                // 3. Create WPF Image control
                var stampImage = new System.Windows.Controls.Image
                {
                    Source = bitmapImage
                };

                // 4. Create PdfStampAnnotation
                var stampAnnotation = new PdfStampAnnotation(stampImage);

                // 5. Define position and size for the stamp in PDF
                var position = new System.Windows.Point(SX, SY);
                var stampSizeObj = new System.Drawing.Size(StampWidth, StampHeight);

                // 6. Add the custom image stamp to the PDF viewer
                // NOTE: Ensure 'pdfViewer' is your PdfViewerControl instance defined in XAML.
                PDFViewer.AddStamp(stampAnnotation, 1, position, stampSizeObj);
            }
        }

    }
}