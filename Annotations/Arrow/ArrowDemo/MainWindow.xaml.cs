using Syncfusion.Pdf.Interactive;
using Syncfusion.Windows.PdfViewer;
using System.IO;
using System.Reflection;
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

            //load your PDF file.
            pdfViewer.Load("../../../../../../PDF/HTTP Succinctly.pdf");
            
            // wire the shape annotation changed event
            pdfViewer.ShapeAnnotationChanged += PdfViewer_ShapeAnnotationChanged;
        }

        /// <summary>
        /// Occurs when a shape annotation is changed
        /// </summary>
        private void PdfViewer_ShapeAnnotationChanged(object sender, ShapeAnnotationChangedEventArgs e)
        {
            // Reset the mode when an arrow is added.
            if (e.Type == ShapeAnnotationType.Arrow && e.Action== AnnotationChangedAction.Add)
            {
                // Reset the annotation mode to None.
                pdfViewer.AnnotationMode = PdfViewerAnnotationMode.None;
            }
        }

        /// <summary>
        /// Customize the default appearance of an arrow.
        /// </summary>
        public void CustomizeArrowSettings()
        {
            // set the end styles of an arrow.
            pdfViewer.ArrowAnnotationSettings.BeginLineStyle = PdfLineEndingStyle.Circle;
            pdfViewer.ArrowAnnotationSettings.EndLineStyle = PdfLineEndingStyle.ClosedArrow;
            
            // set the color for arrow.
            pdfViewer.ArrowAnnotationSettings.StrokeColor = Colors.Blue;

            // set the opacity of arrow.
            pdfViewer.ArrowAnnotationSettings.Opacity = 0.5f;

            // set the thickness for arrow.
            pdfViewer.ArrowAnnotationSettings.Thickness = 4;
        }

        private void DefaultAppearance_Click(object sender, RoutedEventArgs e)
        {
            // set arrow mode.
            if (pdfViewer.AnnotationCommand.CanExecute("Arrow"))
                pdfViewer.AnnotationCommand.Execute("Arrow");

            // customize the default appearance of an arrow.
            CustomizeArrowSettings();
        }

        /// <summary>
        /// Saves the PDF document
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save the PDF document in the application path
            pdfViewer.Save("output.pdf");

            // Display message box to show the folder
            MessageBox.Show("Document is saved to the folder: " + Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }
    }
}
