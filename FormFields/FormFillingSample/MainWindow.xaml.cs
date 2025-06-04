using System.Windows;

namespace FormFillingSample
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
            pdfViewer.Load("../../../Data/FormFillingDocument.pdf");
#else
            pdfViewer.Load("../../Data/FormFillingDocument.pdf");
#endif
        }
    }
}
