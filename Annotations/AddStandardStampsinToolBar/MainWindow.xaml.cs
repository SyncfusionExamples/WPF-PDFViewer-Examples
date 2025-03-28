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
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
        }

            /// <summary>
            /// Document loaded event that handles the creation of stamps from the local images and adds the standard stamps to the toolbar.
            /// </summary>
            /// <param name="sender">Source of the event</param>
            /// <param name="args">Event arguments</param>
            private void PdfViewer_DocumentLoaded(object sender, EventArgs args)
            {
                // Clear the existing standard stamps if it is not needed.
                pdfViewer.ToolbarSettings.StampAnnotations.Clear();
                //Load the custom image from the local disk.
                System.Windows.Controls.Image image1 = new System.Windows.Controls.Image();
                image1.Source = new BitmapImage(new Uri(System.IO.Path.Combine(path, "Adventure_Cycle.jpg"), UriKind.RelativeOrAbsolute));
                System.Windows.Controls.Image image2 = new System.Windows.Controls.Image();
                image2.Source = new BitmapImage(new Uri(System.IO.Path.Combine(path, "Confidential.png"), UriKind.RelativeOrAbsolute));
                //Create a new standard stamp from the image.
                PdfStampAnnotation newStandardStamp1 = new PdfStampAnnotation(image1);
                PdfStampAnnotation newStandardStamp2 = new PdfStampAnnotation(image2);
                //Add the custom stamp in the toolbar.
                pdfViewer.ToolbarSettings.StampAnnotations.Add(newStandardStamp1);
                pdfViewer.ToolbarSettings.StampAnnotations.Add(newStandardStamp2 );
            }
    }
}
