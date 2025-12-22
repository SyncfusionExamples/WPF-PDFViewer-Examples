using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace Custom_stamp
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
            string authorName = "John Doe";
            string dateTimeText = DateTime.Now.ToString("dd-MMM-yyyy HH:mm");
            string metaText = $"{authorName} | {dateTimeText}";

            // Load original stamp image
            Bitmap originalStamp = new Bitmap("Approved.png");

            using (Graphics g = Graphics.FromImage(originalStamp))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                using (Font font = new Font("Arial", 9))
                {
                    SizeF textSize = g.MeasureString(metaText, font);

                    // Position text at bottom-right inside the image
                    float x = originalStamp.Width - textSize.Width - 10; // 10px padding from right
                    float y = originalStamp.Height - textSize.Height - 5; // 5px padding from bottom

                    g.DrawString(metaText, font, System.Drawing.Brushes.Black, new PointF(x, y));
                }
            }

            // Convert to BitmapImage for WPF PdfViewer
            BitmapImage bitmapImage;
            using (MemoryStream ms = new MemoryStream())
            {
                originalStamp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                ms.Position = 0;

                bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
            }

            // Create PdfStampAnnotation
            System.Windows.Controls.Image stampImage = new System.Windows.Controls.Image() { Source = bitmapImage };
            PdfStampAnnotation stampAnnotation = new PdfStampAnnotation(stampImage);

            // Add stamp to PDF
            System.Windows.Point position = new System.Windows.Point(125, 108);
            System.Drawing.Size stampSize = new System.Drawing.Size(originalStamp.Width, originalStamp.Height);
            pdfViewer.AddStamp(stampAnnotation, 1, position, stampSize);

        }
    }
}
