using Syncfusion.Pdf;
using Syncfusion.Pdf.Interactive;
using System;
using System.Collections.Generic;
using System.Drawing;
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

namespace FindAndHighlightTextInDesiredPage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string file;
        int desiredPageIndex = 1;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
#if NETCOREAPP
            file = "../../../Data/F#.pdf";
#else
            file = "../../Data/F#.pdf";
#endif
            //Load the document
            pdfViewer.Load(file);

        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            //Navigating to desired page
            pdfViewer.GotoPage(desiredPageIndex + 1);

            //To store the occurrences of the text found in the page
            List<RectangleF> textbounds = new List<RectangleF>();

            //Find the text in the specific page returns true if the text is found in the particular page
            bool isMatchFound = pdfViewer.FindText("F#", desiredPageIndex, out textbounds);
            if (isMatchFound)
            {
                foreach(RectangleF rect in textbounds)
                {
                    //Creating the markup annotation
                    PdfTextMarkupAnnotation markUpannotation = new PdfTextMarkupAnnotation(rect);
                    PdfLoadedPage page = pdfViewer.LoadedDocument.Pages[desiredPageIndex] as PdfLoadedPage;
                    //Asigning the type of Text Mark up annotation
                    markUpannotation.TextMarkupAnnotationType = PdfTextMarkupAnnotationType.Highlight;
                    //Adding the mark up annotation to the particular page
                    page.Annotations.Add(markUpannotation);

                }
            }


        }
    }
}
