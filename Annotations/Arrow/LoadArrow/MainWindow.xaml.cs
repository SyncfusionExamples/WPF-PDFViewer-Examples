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
            pdfViewer.Load("../../../../../../PDF/Arrow.pdf");
        }
    }
}
