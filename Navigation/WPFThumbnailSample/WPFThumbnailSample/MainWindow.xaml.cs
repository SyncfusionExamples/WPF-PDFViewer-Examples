using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace WPFThumbnailSample
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pdfViewerControl.Load(@"../../Data/JavaScript Succinctly.pdf");
            ThumbnailGrid.Background = System.Windows.Media.Brushes.Gray;
            pdfViewerControl.DocumentLoaded += PdfViewerControl_DocumentLoaded;
        }

        /// <summary>
        /// Add the converted PDF page images into grid
        /// </summary>
        private void PdfViewerControl_DocumentLoaded(object sender, EventArgs args)
        {
            ThumbnailGrid.ColumnDefinitions.Clear();
            //Export pdf pages as image using PdfViewer ExportAsImage method
            BitmapSource[] Pages = pdfViewerControl.ExportAsImage(0, pdfViewerControl.PageCount - 1, new SizeF(200, 400), true);
            //Add the image into Grid
            for (int i = 0; i < Pages.Length; i++)
            {
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = Pages[i];
                image.Margin = new Thickness(5);
                image.HorizontalAlignment = HorizontalAlignment.Center;
                image.MouseUp += Image_MouseUp;
                ColumnDefinition column = new ColumnDefinition();
                ThumbnailGrid.ColumnDefinitions.Add(column);
                Grid.SetColumn(image, i);
                ThumbnailGrid.Children.Add(image);
            }
        }

        /// <summary>
        /// Navigate to the page based on the thumbnail grid image selection
        /// </summary>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image img = sender as System.Windows.Controls.Image;
            if(img != null)
            {
                int pageNumber = (int)img.GetValue(Grid.ColumnProperty);
                //Navigate to the particular page
                pdfViewerControl.GotoPage(pageNumber + 1);
            }            
        }
    }
}
