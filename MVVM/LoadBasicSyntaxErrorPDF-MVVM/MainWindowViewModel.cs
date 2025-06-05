using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Syncfusion.Pdf.Parsing;
using System.IO;
using System.Xml.Linq;

namespace LoadFundamentalSyntaxErrorDocument
{
    public partial class MainWindowViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _filePath;

        [ObservableProperty]
        private PdfLoadedDocument _pdfDocument;

        partial void OnFilePathChanged(string value)
        {
            if (!string.IsNullOrEmpty(value) && File.Exists(value))
            {
                PdfDocument = new PdfLoadedDocument(new FileStream(value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite), true);
            }
            else
            {
                PdfDocument = null;
            }
        }

        [RelayCommand]
        private void OpenPDFFile()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf"
            };
            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
            }
        }
    }
}
