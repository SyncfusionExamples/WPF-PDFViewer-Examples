using System;
using System.Windows;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;

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
            pdfViewerControl.Load(@"../../Data/JavaScript Succinctly.pdf");
            //Rise PdfViewer document loaded event
            pdfViewerControl.DocumentLoaded += PdfViewerControl_DocumentLoaded;
            //Rise PdfViewer document unloaded event
            pdfViewerControl.DocumentUnloaded += PdfViewerControl_DocumentUnloaded;
        }

        /// <summary>
        /// Convert pdf pages as imag and add the images into thumbnail grid
        /// </summary>
        private void PdfViewerControl_DocumentLoaded(object sender, EventArgs args)
        {
            //Export pdf pages as image using PdfViewer ExportAsImage method with a custom size of 200 X 400
            BitmapSource[] Pages = pdfViewerControl.ExportAsImage(0, pdfViewerControl.PageCount - 1, new SizeF(200, 400), true);

            for (int i = 0; i < Pages.Length; i++)
            {
                //Create image control from the exported Pdf page bitmap source 
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = Pages[i];
                image.Margin = new Thickness(5);
                image.HorizontalAlignment = HorizontalAlignment.Center;
                image.MouseUp += Image_MouseUp;                
                //Create column in the thumbnail grid and add the image into the column
                ColumnDefinition column = new ColumnDefinition();
                ThumbnailGrid.ColumnDefinitions.Add(column);
                Grid.SetColumn(image, i);
                ThumbnailGrid.Children.Add(image);
            }
        }

        /// <summary>
        /// Navigate to the page in PdfViewer based on the thumbnail grid image selection
        /// </summary>
        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Controls.Image img = sender as System.Windows.Controls.Image;
            if (img != null)
            {
                //Get the pageindex from the thumbnail grid based on selected image
                int pageIndex = (int)img.GetValue(Grid.ColumnProperty);
                //Navigate to the particular page
                pdfViewerControl.GotoPage(pageIndex + 1);
            }
        }

        /// <summary>
        /// Clear the columns in the Thumbnail grid
        /// </summary>
        private void PdfViewerControl_DocumentUnloaded(object sender, EventArgs e)
        {
            ThumbnailGrid.ColumnDefinitions.Clear();
        }
    }
}
