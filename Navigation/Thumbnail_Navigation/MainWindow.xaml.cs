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

namespace Bookmark_Navigation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Expands the thumbnail pane
            pdfViewer.ThumbnailSettings.IsExpanded = true;
            //Loads the PDF document
#if NETFRAMEWORK
            pdfViewer.Load("../../../Data/HTTP_Succinctly.pdf");
#else
            pdfViewer.Load("../../../../Data/HTTP_Succinctly.pdf");
#endif
        }
    }
}
