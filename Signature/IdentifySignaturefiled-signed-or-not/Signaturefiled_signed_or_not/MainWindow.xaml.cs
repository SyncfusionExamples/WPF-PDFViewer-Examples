using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Signaturefiled_signed_or_not
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../DocumentForSigning.pdf");
        }
        private void validateSignature_Click(object sender, RoutedEventArgs e)
        {
            bool isSigned = false;
            var loadedDocument = pdfViewer.LoadedDocument as PdfLoadedDocument;

            if (loadedDocument != null && loadedDocument.Form != null)
            {
                for (int i = loadedDocument.Form.Fields.Count - 1; i >= 0; i--)
                {
                    var field = loadedDocument.Form.Fields[i] as PdfLoadedField;
                    var signatureField = field as PdfLoadedSignatureField;
                    if (signatureField == null)
                        continue;

                    foreach (var ann in signatureField.Page.Annotations)
                    {
                        if (ann is PdfLoadedInkAnnotation loadedInk && string.Equals(loadedInk.Name, signatureField.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            isSigned = true;
                            break;
                        }
                        else if (ann is PdfInkAnnotation ink && string.Equals(ink.Name, signatureField.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            isSigned = true;
                            break;
                        }
                    }

                     MessageBox.Show("Signature field Signed: " + isSigned);
                }
            }
            else
            {
                Console.WriteLine("No form fields found or document not loaded.");
            }
        }
    }
}