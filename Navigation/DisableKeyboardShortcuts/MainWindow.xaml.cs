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

namespace DisableKeyboardShortcuts
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            filePath = @"../../../Data/F#.pdf";
#else
            filePath = @"../../Data/F#.pdf";
#endif
            //Load the PDF
            pdfViewer.Load(filePath);
            //Hook the PreviewKeyDown event
            pdfViewer.PreviewKeyDown += PdfViewerControl_PreviewKeyDown;
            //Open the window in maximized state
            this.WindowState = WindowState.Maximized;
        }

        private void PdfViewerControl_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //Handling the event so that default WPF logic didn't intercept
            e.Handled = true;
        }
    }
}
