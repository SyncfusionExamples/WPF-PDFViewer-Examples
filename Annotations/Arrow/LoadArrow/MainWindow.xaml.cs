using System.Windows;

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

            // Load PDF document in the viewer
#if NETFRAMEWORK
            pdfViewer.Load("../../../../../PDF/Arrow.pdf");
#else
            pdfViewer.Load("../../../../../../PDF/Arrow.pdf");
#endif
        }
    }
}
