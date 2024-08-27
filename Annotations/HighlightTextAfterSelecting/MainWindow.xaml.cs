using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace HighlightTextAfterSelecting
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
            pdfViewer.Load("../../Data/HTTP_Succinctly.pdf");
#else
            pdfViewer.Load("../../../Data/HTTP_Succinctly.pdf");
#endif
            pdfViewer.TextSelectionCompleted += PdfViewer_TextSelectionCompleted;
        }
        private void PdfViewer_TextSelectionCompleted(object sender, TextSelectionCompletedEventArgs args)
        {
            //Get the whole selected text 
            string selectedText = args.SelectedText;

            PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;

            //Get the selected text and its rectangular bounds for each page separately if the selection is made on multiple pages 
            Dictionary<int, Dictionary<string, System.Drawing.Rectangle>> selectedTextInformation = args.SelectedTextInformation;

            foreach (var SelectedValue in selectedTextInformation)
            {
                int pageIndex = SelectedValue.Key;

                Dictionary<string, System.Drawing.Rectangle> innerDictionary = SelectedValue.Value;

                foreach (var innerValue in innerDictionary)
                {
                    string innerKey = innerValue.Key;
                    RectangleF rect = new RectangleF(innerValue.Value.X, innerValue.Value.Y, innerValue.Value.Width, innerValue.Value.Height);

                    PdfTextMarkupAnnotation markupAnnotation = new PdfTextMarkupAnnotation(rect);
                    markupAnnotation.TextMarkupColor = new PdfColor(System.Drawing.Color.Red);
                    markupAnnotation.TextMarkupAnnotationType = PdfTextMarkupAnnotationType.Highlight;
                    loadedDocument.Pages[pageIndex - 1].Annotations.Add(markupAnnotation);
                }
            }
        }
    }
}
