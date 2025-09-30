using System.Windows;

namespace LocalizationSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("fr");
            InitializeComponent();
            pdfViewer.Load("../../../HTTP Succinctly.pdf");
        }
    }
}
