using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf.Security;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Bitmap = System.Drawing.Bitmap;
using Path = System.Windows.Shapes.Path;

namespace WPF_Sample_FW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "../../../Data/";
        public MainWindow()
        {
            InitializeComponent();

            //Load a PDF ldoc
            PdfLoadedDocument ldoc = new PdfLoadedDocument(filePath + "Input.pdf");
            for (int itr = 0; itr < ldoc.Pages.Count; itr++)
            {
                for (int index = 0; index < ldoc.Pages[itr].Annotations.Count; index++)
                {
                    if (ldoc.Pages[itr].Annotations[index] is PdfLoadedWatermarkAnnotation)
                    {
                        ldoc.Pages[itr].Annotations[index].Flatten = true;
                    }
                }
            }
            PDFViewer.Load(ldoc);
        }
    }
}
