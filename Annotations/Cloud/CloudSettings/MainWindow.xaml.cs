using Syncfusion.Windows.PdfViewer;
using System.Windows;
using System.Windows.Media;
using static Syncfusion.Windows.PdfViewer.PdfDocumentView;

namespace PDFViewerNuget
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //load the PDF file
#if NETFRAMEWORK
            pdfViewer.Load("../../Data/Empty.pdf");
#else
            pdfViewer.Load("../../../Data/Empty.pdf");
#endif
            // wire the shape annotation changed event
            pdfViewer.ShapeAnnotationChanged += PdfViewer_ShapeAnnotationChanged;
        }

        /// <summary>
        /// Occurs when a shape annotation is changed
        /// </summary>
        private void PdfViewer_ShapeAnnotationChanged(object sender, ShapeAnnotationChangedEventArgs e)
        {
            // Reset the mode when a polygon is added.
            if (e.Type == ShapeAnnotationType.Polygon && e.Action== AnnotationChangedAction.Add)
            {
                // Reset the annotation mode from Polygon to None.
                pdfViewer.AnnotationMode = PdfViewerAnnotationMode.None;
            }
        }

        /// <summary>
        /// Enables mode to add cloud
        /// </summary>
        private void AddCloud_Click(object sender, RoutedEventArgs e)
        {
            // Enable the cloud drawing mode.
            EnableCloudDrawingMode();
        }

        /// <summary>
        /// Enable the cloud drawing mode.
        /// </summary>
        public void EnableCloudDrawingMode()
        {
            // Set the polygon mode with cloud border style
            pdfViewer.AnnotationMode = PdfViewerAnnotationMode.Polygon;
            pdfViewer.PolygonAnnotationSettings.BorderEffect = BorderEffect.Cloudy;
        }

        /// <summary>
        /// Sets the default appearance of cloud
        /// </summary>
        public void ApplyDefaultCloudSettings()
        {
            // Set the polygon mode with cloud border style
            pdfViewer.AnnotationMode = PdfViewerAnnotationMode.Polygon;
            pdfViewer.PolygonAnnotationSettings.BorderEffect = BorderEffect.Cloudy;

            // Set the color
            pdfViewer.PolygonAnnotationSettings.StrokeColor = Colors.Blue;
            pdfViewer.PolygonAnnotationSettings.FillColor = Colors.Yellow;

            // Set the thickness of cloud border
            pdfViewer.PolygonAnnotationSettings.Thickness = 2;

            // Set the opacity
            pdfViewer.PolygonAnnotationSettings.Opacity = 0.5f;
        }

        private void DefaultAppearance_Click(object sender, RoutedEventArgs e)
        {
            // Set the default appearance of cloud
            ApplyDefaultCloudSettings();
        }

        /// <summary>
        /// Saves the PDF document
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save the PDF document in the application path
            pdfViewer.Save("output.pdf");
        }
    }
}
