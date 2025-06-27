using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using Syncfusion.Pdf.Parsing;
using System;
using System.IO;

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
                //Repair the PDF document with basic syntax errors
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
                Filter = "PDF files (*.pdf)|*.pdf",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
            };
            if (dialog.ShowDialog() == true)
            {
                FilePath = dialog.FileName;
            }
        }
    }
}
