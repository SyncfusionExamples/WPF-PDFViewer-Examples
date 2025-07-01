using Microsoft.Win32;
using Syncfusion.Pdf.Parsing;
using System;
using System.IO;
using System.Windows;

namespace LoadFundamentalSyntaxErrorDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PdfLoadedDocument pdfDocument;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            if (dialog.ShowDialog() == true)
            {
                LoadPdfFile(dialog.FileName);
            }
        }
        private void LoadPdfFile(string value)
        {
            if (!string.IsNullOrEmpty(value) && File.Exists(value))
            {
                //Repair the PDF document with basic syntax errors
                pdfDocument = new PdfLoadedDocument(new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true);
                pdfViewer.Load(pdfDocument);
            }
            else
            {
                pdfDocument = null;
                return;
            }
        }
    }
}
