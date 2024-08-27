using Syncfusion.Windows.PdfViewer;
using System.Runtime.InteropServices;
using System.Windows;

namespace ToggleLayers
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
            pdfViewerControl.Load("../../../../../PDF/LayersDocument.pdf");
#else
             pdfViewerControl.Load("../../../../../../PDF/LayersDocument.pdf");
#endif
        }

        private void ToggleLayer_Click(object sender, RoutedEventArgs e)
        {
            //Retrieves a PDF document's layers collection using PdfViewerControl
            LayerCollection layers = pdfViewerControl.Layers;

            // Gets a layer by its name                
            for (int i = 0; i < layers.Count; i++)
            {
                if (layers[i].Name == "Layer2")
                {
                    //Toggle the visibility of the Layer 
                    if (layers[i].IsVisible)
                        layers[i].IsVisible = false;
                    else
                        layers[i].IsVisible = true;
                }
            }
        }
    }
}
