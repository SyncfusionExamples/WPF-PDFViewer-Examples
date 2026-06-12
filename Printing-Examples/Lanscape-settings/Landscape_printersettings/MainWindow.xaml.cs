using Syncfusion.Windows.PdfViewer;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Landscape_printersettings
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../Input.pdf");
        }
        private void Print_Click(object sender, RoutedEventArgs e)
        {
            // Set the page orientation to landscape
            pdfViewer.PrinterSettings.PageOrientation = PdfViewerPrintOrientation.Landscape;
            // Print the PDF document
            pdfViewer.Print();
        }
    }
}