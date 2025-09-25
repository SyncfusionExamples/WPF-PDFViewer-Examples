using System.Windows;

namespace Export_ImportFormFields
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "../../../FormFillingDocument.pdf";
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load(filePath);
        }
        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            if (fdfRadioButton.IsChecked.Value)
                openFileDlg.Filter = "FDF Documents|*.fdf";
            else
                openFileDlg.Filter = "XFDF Documents|*.xfdf";
            if (openFileDlg.ShowDialog().Value)
            {
                Syncfusion.Pdf.Parsing.DataFormat dataFormat;
                if (fdfRadioButton.IsChecked.Value)
                    dataFormat = Syncfusion.Pdf.Parsing.DataFormat.Fdf;
                else
                    dataFormat = Syncfusion.Pdf.Parsing.DataFormat.XFdf;
                //Import PDF form data
                pdfViewer.ImportFormData(openFileDlg.FileName, dataFormat);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Save("Modified.pdf");
            Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();
            if (fdfRadioButton.IsChecked.Value)
                saveFileDlg.Filter = "FDF Documents|*.fdf";
            else
                saveFileDlg.Filter = "XFDF Documents|*.xfdf";
            if (saveFileDlg.ShowDialog().Value)
            {
                Syncfusion.Pdf.Parsing.DataFormat dataFormat;
                if (fdfRadioButton.IsChecked.Value)
                    dataFormat = Syncfusion.Pdf.Parsing.DataFormat.Fdf;
                else
                    dataFormat = Syncfusion.Pdf.Parsing.DataFormat.XFdf;
                //Export PDF form data
                pdfViewer.ExportFormData(saveFileDlg.FileName, dataFormat, "Modified.pdf");
            }
        }
    }
}
