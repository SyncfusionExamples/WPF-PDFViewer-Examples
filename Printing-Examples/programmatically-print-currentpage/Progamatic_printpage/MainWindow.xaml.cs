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

namespace Progamatic_printpage
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
            BitmapSource bitmapImage = pdfViewer.ExportAsImage(pdfViewer.CurrentPageIndex - 1);
            Image image = new Image();
            image.Source = bitmapImage;
            PrintDialog printDialog = new PrintDialog();
            bool? result = printDialog.ShowDialog();
            if (result == true)
            {
                printDialog.PrintVisual(image, "PDF current page");
            }
        }

    }

}