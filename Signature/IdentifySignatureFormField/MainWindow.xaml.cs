using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace IdentifySignatureFormField
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;
        public List<String> NameList;
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            filePath = @"../../../Data/FormFields.pdf";
#else
            filePath = @"../../Data/FormFields.pdf";
#endif
            //Load the PDF
            pdfViewer.Load(filePath);
            pdfViewer.DocumentLoaded += pdfViewer_DocumentLoaded;
            pdfViewer.DocumentUnloaded += pdfViewer_DocumentUnloaded;
            pdfViewer.FormFieldClicked += PdfViewer_FormFieldClicked;
        }
        /// <summary>
        /// Triggered whenever the formField is clicked
        /// </summary>
        private void PdfViewer_FormFieldClicked(object sender, Syncfusion.Windows.PdfViewer.FormFieldClickedEventArgs args)
        {
            if (args.FormField is TextBox)
            {
                //Typecast the `FormField` property to `System.Windows.Controls.TextBox`
                TextBox text = args.FormField as TextBox;
                if(NameList.Contains(text.Name))
                {
                    MessageBox.Show("Signature Form Field Clicked");
                }
            }
        }

        /// <summary>
        /// Triggered whenever the PDF document has finished loading.
        /// </summary>
        private void pdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            NameList = new List<String>();
            PdfLoadedDocument document = new PdfLoadedDocument(filePath);
            //Get the loaded form.
            PdfLoadedForm loadedForm = document.Form;
            for (int i = 0; i < loadedForm.Fields.Count; i++)
            {
                if (loadedForm.Fields[i] is PdfLoadedSignatureField)
                {
                    NameList.Add(loadedForm.Fields[i].Name);
                }
            }
        }
        /// <summary>
        /// "Triggered whenever the PDF document has unloaded from PDF Viewer control."
        /// </summary>
        private void pdfViewer_DocumentUnloaded(object sender, EventArgs e)
        {
            NameList.Clear();
        }
    }
}
