using Syncfusion.Windows.PdfViewer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Imaging;

namespace Sample_Project
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
            //Load the PDF file
            PdfViewer.Load("../../F Sharp Succinctly.pdf");
# else
            //Load the PDF file
            PdfViewer.Load("../../../F Sharp Succinctly.pdf");
#endif
        }

        private void pdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the instance of the toolbar using its template name.
            DocumentToolbar toolbar = PdfViewer.Template.FindName("PART_Toolbar", PdfViewer) as DocumentToolbar;

            //Get the instance of the stamp button using its template name.
            ToggleButton StampButton = (ToggleButton)toolbar.Template.FindName("PART_Stamp", toolbar);

            //Get the instance of custom stamp menu item.
            MenuItem cutomMenuItem = (MenuItem)StampButton.ContextMenu.Items[1];

            //Create the instance of the image
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            BitmapImage bitmapImage = new BitmapImage();

            //Creates the image from the desired path
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
#if NETFRAMEWORK
            bitmapImage.UriSource = new Uri("../../Confidential.png", UriKind.RelativeOrAbsolute);
#else
            bitmapImage.UriSource = new Uri("../../../Confidential.png", UriKind.RelativeOrAbsolute);
#endif
            bitmapImage.EndInit();
            image.Source = bitmapImage;

            //Creates the instance of the grid
            Grid grid = new Grid();

            //The standard stamp size is assigned to the grid size in order to maintain uniform size for stamp menu items.
            grid.Width = 200;
            grid.Height = 50;

            if (image.Width.Equals(double.NaN) && image.Height.Equals(double.NaN))
            {
                image.Height = image.Source.Height;
                image.Width = image.Source.Width;
            }

            //Create and add the viewbox to the grid
            Viewbox viewbox = new Viewbox();
            viewbox.Child = image;
            grid.Children.Add(viewbox);

            //Sets the margin to the grid
            //Margin is set inorder to seperate two images
            grid.Margin = new Thickness(4, 4, 4, 8);

            cutomMenuItem.Items.Add(grid);
        }
    }
}
