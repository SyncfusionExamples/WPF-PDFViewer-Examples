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
