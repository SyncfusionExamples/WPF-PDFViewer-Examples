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
        string path;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            //Setting the path based on the framework
#if NETFRAMEWORK
            path = "../../Data/";
#else
            path = "../../../Data/";
#endif
            pdfViewer.Load(System.IO.Path.Combine(path, "F#.pdf"));
        }
        /// <summary>
        /// A button click event that creates a stamp from a local image and adds the standard stamp to the toolbar.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data associated with the click event.</param>
        private void addStamp_Click(object sender, RoutedEventArgs e)
        {
            // Clear the existing standard stamps if it is not needed.
            pdfViewer.ToolbarSettings.StampAnnotations.Clear();
            //Load the custom image from the local disk.
            System.Windows.Controls.Image image = new System.Windows.Controls.Image();
            image.Source = new BitmapImage(new Uri(System.IO.Path.Combine(path,"Adventure_Cycle.jpg"), UriKind.RelativeOrAbsolute));
            //Create a new standard stamp from the image.
            PdfStampAnnotation newStandardStamp = new PdfStampAnnotation(image);
            //Add the custom stamp in the toolbar.
            pdfViewer.ToolbarSettings.StampAnnotations.Add(newStandardStamp);
        }
    }
}
