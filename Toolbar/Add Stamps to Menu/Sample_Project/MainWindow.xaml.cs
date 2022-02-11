using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

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
            PdfViewer.Load("../../F Sharp Succinctly.pdf");
        }

        private void pdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            //Get the instance of the toolbar using its template name.
            DocumentToolbar toolbar = PdfViewer.Template.FindName("PART_Toolbar", PdfViewer) as DocumentToolbar;

            //Get the instance of the stamp button using its template name.
            ToggleButton StampButton = (ToggleButton)toolbar.Template.FindName("PART_Stamp", toolbar);

            //Get the instance of custom stamp menu item.
            MenuItem cutomMenuItem = (MenuItem)StampButton.ContextMenu.Items[1];

            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            BitmapImage bitmapImage = new BitmapImage();

            //Creates the image from the desired path
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.UriSource = new Uri("../../Confidential.png", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            image.Source = bitmapImage;
            Grid grid = new Grid();

            //The standard stamp size is assigned to the grid size in order to maintain uniform size for stamp menu items.
            grid.Width = 200;
            grid.Height = 50;

            if (image.Width.Equals(double.NaN) && image.Height.Equals(double.NaN))
            {
                image.Height = image.Source.Height;
                image.Width = image.Source.Width;
            }

            Viewbox viewbox = new Viewbox();
            viewbox.Child = image;

            grid.Children.Add(viewbox);
            grid.Margin = new Thickness(4, 4, 4, 8);

            cutomMenuItem.Items.Add(grid);
        }
    }
}
