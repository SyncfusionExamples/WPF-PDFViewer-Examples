using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
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

namespace DisableToolbarItems
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PdfViewerControl pdfViewer;

        public MainWindow()
        {
            InitializeComponent();
            // Create a new instance of PdfViewerControl.
            pdfViewer = new PdfViewerControl();
            // Add the PdfViewerControl to the HomeGrid.
            HomeGrid.Children.Add(pdfViewer);
            // Load the specified PDF file.
            pdfViewer.Load("../../Data/F#.pdf");
            // Attach an event handler to the Loaded event of the PdfViewerControl.
            pdfViewer.Loaded += pdfViewer_Loaded;
        }

        private void pdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Find the DocumentToolbar element within the PdfViewerControl template.
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            // Hide the Text Search button within the toolbar.
            Button textSearchButton = toolbar.Template.FindName("PART_ButtonTextSearch", toolbar) as Button;
            textSearchButton.Visibility = Visibility.Collapsed;
        }
    }
}
