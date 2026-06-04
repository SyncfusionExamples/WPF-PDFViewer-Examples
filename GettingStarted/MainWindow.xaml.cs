using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;

namespace WpfPDFViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //Loads the document in PDF Viewer
#if NETFRAMEWORK
            pdfViewer.Load("../../Data/F#.pdf");
#else
            pdfViewer.Load("../../../Data/F#.pdf");
#endif
        }       
    }
}
