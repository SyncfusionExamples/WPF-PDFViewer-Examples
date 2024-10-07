using Syncfusion.Pdf.Parsing;
using System.Windows;

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
#if NETFRAMEWORK
            PdfLoadedDocument document = new PdfLoadedDocument("../../Data/BlankPage.pdf");
            pdfViewer.Load(document);
#else
            PdfLoadedDocument document = new PdfLoadedDocument( "../../../Data/BlankPage.pdf");
            pdfViewer.Load(document);
#endif
            
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (fdfRadioButton.IsChecked.Value)
                openFileDlg.Filter = "FDF Documents|*.fdf";
            else
                openFileDlg.Filter = "XFDF Documents|*.xfdf";
            if (openFileDlg.ShowDialog().Value)
            {
                AnnotationDataFormat annotationDataFormat;
                if (fdfRadioButton.IsChecked.Value)
                    annotationDataFormat = AnnotationDataFormat.Fdf;
                else
                    annotationDataFormat = AnnotationDataFormat.XFdf;
                pdfViewer.ImportAnnotations(openFileDlg.FileName, annotationDataFormat);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();
            if (fdfRadioButton.IsChecked.Value)
                saveFileDlg.Filter = "FDF Documents|*.fdf";
            else
                saveFileDlg.Filter = "XFDF Documents|*.xfdf";
            if (saveFileDlg.ShowDialog().Value)
            {
                AnnotationDataFormat annotationDataFormat;
                if (fdfRadioButton.IsChecked.Value)
                    annotationDataFormat = AnnotationDataFormat.Fdf;
                else
                    annotationDataFormat = AnnotationDataFormat.XFdf;
                pdfViewer.ExportAnnotations(saveFileDlg.FileName, annotationDataFormat);
            }
        }
    }
}