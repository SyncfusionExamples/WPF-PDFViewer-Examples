using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Windows;

namespace CheckPDFSignatureformFieldsSigned
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filepath;
        public MainWindow()
        {
#if NETCOREAPP

            filepath = @"../../../Data/Doc_with_sign.pdf";
#else
            filepath = @"../../Data/Doc_with_sign.pdf";
#endif
            InitializeComponent();
            pdfViewer.Load(filepath);
        }

        private void validateSignature_Click(object sender, RoutedEventArgs e)
        {
            bool isSigned = false;
            PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;
            if (loadedDocument.Form != null && loadedDocument.Form.Fields.Count > 0)
            {
                foreach (var field in loadedDocument.Form.Fields)
                {
                    if (field is PdfLoadedSignatureField)
                    {
                        PdfLoadedSignatureField signatureField = field as PdfLoadedSignatureField;
                        if (signatureField.Page != null && signatureField.Page.Annotations.Count > 0)
                        {
                            foreach (var annotation in signatureField.Page.Annotations)
                            {
                                if (annotation is PdfLoadedInkAnnotation)
                                {
                                    PdfLoadedInkAnnotation signature = annotation as PdfLoadedInkAnnotation;
                                    if (signature.Name != signatureField.Name)
                                    {
                                        isSigned = false;
                                    }
                                    else
                                    {
                                        isSigned = true;
                                        break;
                                    }
                                }
                                else if (annotation is PdfInkAnnotation)
                                {
                                    PdfInkAnnotation signature = annotation as PdfInkAnnotation;
                                    if (signature.Name != signatureField.Name)
                                    {
                                        isSigned = false;
                                    }
                                    else
                                    {
                                        isSigned = true;
                                        break;
                                    }
                                }
                            }
                            if (!isSigned)
                            {
                                break;
                            }
                        }
                        else
                        {
                            isSigned = false;
                            break;
                        }
                    }
                }

                if (!isSigned)
                {
                    MessageBox.Show("There is some signature required");
                }
                else
                {
                    MessageBox.Show("All signatures fields are signed!");
                }
            }
        }
    }
}
