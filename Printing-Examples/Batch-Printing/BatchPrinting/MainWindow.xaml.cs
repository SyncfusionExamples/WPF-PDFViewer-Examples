using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace BatchPrinting
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[] files;
        PdfDocumentView pdfViewer;
        PdfLoadedDocument ldoc;
        # region Constructor
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer = new PdfDocumentView();
        }

        # endregion

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            char[] charSplitter = new char[] { '/' };
            // Get the PDF files from the directory
#if NETFRAMEWORK
            files = Directory.GetFiles("../../Data/", "*.pdf");
#else
            files = Directory.GetFiles("../../../Data", "*.pdf");
#endif
           
            foreach (string file in files)
            {
                //Splitting the filename from the file path
                string[] fileName = file.Split(charSplitter);
                //Adding the file into the list view
                list.Items.Add(fileName[3]);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (list.SelectedItems.Count > 0)
            {
                for (int i = 0; i < list.SelectedItems.Count; i++)
                {
                    string fileName = list.SelectedItems[i].ToString();
                    for (int j = 0; j < files.Length; j++)
                    {
                        if (files[j].Contains(fileName))
                        {
                            //Initialize the PdfLoadedDocument
                            ldoc = new PdfLoadedDocument(files[j]);
                            //Load the PdfDocument into PdfViewerControl/PdfDocumentView
                            pdfViewer.Load(ldoc);
                            //Silent print the PDF document
                            pdfViewer.Print();
                            //Unload the PdfViewerControl after printing
                            pdfViewer.Unload();
                            //Close the PdfLoadedDocument
                            ldoc.Close(true);
                        }
                    }
                }
            }
        }
    }
}
