using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Windows;
using System.Windows.Controls;

namespace HideComments
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../Data/Annotations.pdf");
        }

        private void User_Checked(object sender, RoutedEventArgs e)
        {
            if (pdfViewer != null)
            {
                CheckBox checkBox = (CheckBox)sender;
                PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;
                for (int i = 0; i < pdfLoadedDocument.Pages.Count; i++)
                {
                    for (int j = 0; j < pdfLoadedDocument.Pages[i].Annotations.Count; j++)
                    {
                        var annotation = pdfLoadedDocument.Pages[i].Annotations[j];
                        if (annotation.Name.Contains(checkBox.Name))
                            pdfViewer.ShowAnnotation(annotation.Name);
                    }
                }
            }
        }

        private void User_Unchecked(object sender, RoutedEventArgs e)
        {
            if (pdfViewer != null)
            {
                CheckBox checkBox = (CheckBox)sender;
                PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;
                for (int i = 0; i < pdfLoadedDocument.Pages.Count; i++)
                {
                    for (int j = 0; j < pdfLoadedDocument.Pages[i].Annotations.Count; j++)
                    {
                        var annotation = pdfLoadedDocument.Pages[i].Annotations[j];
                        if (annotation.Name.Contains(checkBox.Name))
                            pdfViewer.HideAnnotation(annotation.Name);
                    }
                }
            }
        }
    }
}
