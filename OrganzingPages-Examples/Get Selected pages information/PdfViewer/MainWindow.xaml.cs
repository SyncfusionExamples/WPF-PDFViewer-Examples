using Syncfusion.Pdf;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace PdfViewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary> 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            pdfViewer.PageSelected += PdfViewer_PageSelected;
#if NETFRAMEWORK
            pdfViewer.Load(@"../../Data/PDF_Succinctly.pdf");
#else
            pdfViewer.Load(@"../../../Data/PDF_Succinctly.pdf");
#endif
           
        }

        private void PdfViewer_PageSelected(object sender, PageSelectedEventArgs e)
        {
            string selectedPages = string.Empty;
            for(int i=0;i<e.SelectedPages.Length;i++)
            {
                selectedPages += (e.SelectedPages[i]+1).ToString();
                selectedPages += " ";
            }
            SelectedPagesTextBlock.Text = "The selected pages are: " + selectedPages.ToString();
        }
    }
}
