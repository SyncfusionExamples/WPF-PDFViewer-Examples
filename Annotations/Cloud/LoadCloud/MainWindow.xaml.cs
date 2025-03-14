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
#if NETFRAMEWORK
            pdfViewer.Load("../../Data/Cloud.pdf");
#else
            pdfViewer.Load("../../../Data/Cloud.pdf");
#endif
        }
    }
}
