using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
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

namespace DisableSpellcheckTextbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            String filePath = "../../Data/Allianz-EUKV_Antrag Pflege.pdf";
            //load the PDF into the PdfLoadedDocument object
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(filePath);
            //disable spellcheck for the textbox fields
            foreach (PdfField field in loadedDocument.Form.Fields)
            {
                if(field is PdfLoadedTextBoxField)
                {
                    PdfLoadedTextBoxField loadedTextBoxField = field as PdfLoadedTextBoxField;
                    loadedTextBoxField.SpellCheck = false;
                }
            }
            //save the modified pdfLoadedDocument into memory stream
            MemoryStream stream = new MemoryStream();
            stream.Position = 0;
            loadedDocument.Save(stream);
            //load the memorystream into the pdfviewercontrol
            pdfViewer.Load(stream);
        }
    }
}
