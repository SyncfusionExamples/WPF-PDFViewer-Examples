using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Drawing;

namespace CheckPDFSignatureformFieldsSigned
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string inputFilePath, outputFilePath;
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoadDocument(string inputFile, string outputFile)
        {
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(inputFile);

            var pages = loadedDocument.Pages;
            PdfLoadedPage page = pages[0] as PdfLoadedPage;

            PdfSignatureField signature = new PdfSignatureField(page, "Signature");
            signature.Bounds = new RectangleF(155, 590, 150, 24);
            signature.ToolTip = "Sign Here";
            loadedDocument.Form.Fields.Add(signature);

            PdfTextBoxField vehicleEntry = new PdfTextBoxField(page, "VehicleEntry");
            vehicleEntry.Bounds = new RectangleF(128, 400, 75, 24);
            vehicleEntry.BorderColor = new Syncfusion.Pdf.Graphics.PdfColor(0, 1, 1, 0);
            //Add the form field to the document.
            loadedDocument.Form.Fields.Add(vehicleEntry);

            PdfSignatureField initials = new PdfSignatureField(page, "Initials");
            initials.Bounds = new RectangleF(40, 775, 75, 24);
            initials.ToolTip = "Initials Here";
            loadedDocument.Form.Fields.Add(initials);

            using (FileStream output = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                loadedDocument.Save(output);
            }

            pdfViewer.Load(outputFile);
            pdfViewer.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitPage;
        }

        private void btnSignature_Click(object sender, RoutedEventArgs e)
        {
#if NETCOREAPP
            inputFilePath = @"../../../Data/1.pdf";
            outputFilePath = @"../../../Data/1-output.pdf";
#else
            inputFilePath = @"../../Data/1.pdf";
            outputFilePath = @"../../Data/1-output.pdf";
#endif
            LoadDocument(inputFilePath, outputFilePath);
            
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
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
                    MessageBox.Show("All signatures are completed!");
                }
            }
        }
    }
}
