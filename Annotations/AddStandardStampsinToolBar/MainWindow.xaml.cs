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
using Syncfusion.Windows.PdfViewer;

namespace AddingStandardStampsInToolBar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //Loads the Document
#if NETFRAMEWORK
            pdfViewer.Load("../../Data/PDF_Succinctly.pdf");
#else
            pdfViewer.Load("../../../Data/PDF_Succinctly.pdf");
#endif
        }

        private void addStamp_Click(object sender, RoutedEventArgs e)
        {
            // Clear the existing standard stamps if it is not needed.
            pdfViewer.ToolbarSettings.StampAnnotations.Clear();
            //Load the custom image from the local disk.
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
#if NETFRAMEWORK
            image.Source = new BitmapImage(new Uri(@"../../Data/CustomStamp.png", UriKind.RelativeOrAbsolute));
#else
            image.Source = new BitmapImage(new Uri(@"../../../Data/CustomStamp.png", UriKind.RelativeOrAbsolute));
#endif
            //Create a new standard stamp from the image.
            PdfStampAnnotation newStandardStamp = new PdfStampAnnotation(image);
            //Add the custom stamp in the toolbar.
            pdfViewer.ToolbarSettings.StampAnnotations.Add(newStandardStamp);
        }
    }
}
