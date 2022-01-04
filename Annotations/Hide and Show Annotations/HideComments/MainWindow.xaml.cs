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
                //Show the annotation using author name. The checkbox name is set to that of author in this example.
                ToggleAnnotationVisibility(checkBox.Name, true);
            }
        }

        private void User_Unchecked(object sender, RoutedEventArgs e)
        {
            if (pdfViewer != null)
            {
                CheckBox checkBox = (CheckBox)sender;
                //Hide the annotation using author name. The checkbox name is set to that of author in this example.
                ToggleAnnotationVisibility(checkBox.Name, false);
            }
        }

        void ToggleAnnotationVisibility(string authorName, bool showAnnotation)
        {
            //Access the LoadedDocument property of PdfViewer to get the annotations details.
            PdfLoadedDocument pdfLoadedDocument = pdfViewer.LoadedDocument;
            
            //Iterate through the pages to check for the annotations.
            for (int i = 0; i < pdfLoadedDocument.Pages.Count; i++)
            {
                //Iterate through the annotations in the page.
                for (int j = 0; j < pdfLoadedDocument.Pages[i].Annotations.Count; j++)
                {
                    var annotation = pdfLoadedDocument.Pages[i].Annotations[j];

                    //Identify if the annotation is created by the given author.
                    if (annotation.Author == authorName)
                    {
                        if (showAnnotation == true)
                        {
                            //Show annotation using the ShowAnnotation functionality.
                            pdfViewer.ShowAnnotation(annotation.Name);
                        }
                        else
                        {
                            //Hide annotation using the HideAnnotation functionality.
                            pdfViewer.HideAnnotation(annotation.Name);
                        }
                    }
                }
            }
        }
    }
}
