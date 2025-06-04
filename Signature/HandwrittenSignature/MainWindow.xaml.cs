using System.Windows;

namespace HandwrittenSignature
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            pdfViewer.Load("../../../../../PDF/HTTP Succinctly.pdf");
#else
            pdfViewer.Load("../../../../PDF/HTTP Succinctly.pdf");
#endif
        }

        private void Add_Sign_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.AnnotationMode = Syncfusion.Windows.PdfViewer.PdfDocumentView.PdfViewerAnnotationMode.HandwrittenSignature;
        }
    }
}
