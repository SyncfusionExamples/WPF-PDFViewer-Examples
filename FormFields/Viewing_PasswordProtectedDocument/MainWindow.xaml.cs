using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
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

namespace PDFViewer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PdfViewer pdfViewer;
        PdfViewer pdfViewer1;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer = new PdfViewer();
            if (pdfViewer1!= null && pdfViewer1.IsVisible)
            {
                pdfViewer1.Close();
            }
            pdfViewer.PDFViewer.GetDocumentPassword += PdfViewer_GetDocumentPassword;

            string filePath = "../../../Data/Input.pdf";

            pdfViewer.PDFViewer.Load(filePath);
            pdfViewer.Show();            
            pdfViewer.WindowState = WindowState.Maximized;
            pdfViewer.PDFViewer.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
        }

        private void PdfViewer_GetDocumentPassword(object sender, GetDocumentPasswordEventArgs e)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            secureString.AppendChar('o');
            secureString.AppendChar('w');//ownerPassword
            secureString.AppendChar('n');
            secureString.AppendChar('e');
            secureString.AppendChar('r');
            secureString.AppendChar('P');
            secureString.AppendChar('a');
            secureString.AppendChar('s');
            secureString.AppendChar('s');
            secureString.AppendChar('w');
            secureString.AppendChar('o');
            secureString.AppendChar('r');
            secureString.AppendChar('d');
            e.Password = secureString;

            // Enabling handled to hide the password dialog.
            e.Handled = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pdfViewer1 = new PdfViewer();
            if (pdfViewer != null && pdfViewer.IsVisible)
            {
                pdfViewer.Close();
            }
            pdfViewer1.PDFViewer.GetDocumentPassword += PdfViewer_GetDocumentPassword1;

            string filePath = "../../../Data/Input.pdf";
            pdfViewer1.PDFViewer.Load(filePath);
            pdfViewer1.Show();
            pdfViewer1.WindowState = WindowState.Maximized;
            pdfViewer1.PDFViewer.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
        }

        private void PdfViewer_GetDocumentPassword1(object sender, GetDocumentPasswordEventArgs e)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            secureString.AppendChar('u');
            secureString.AppendChar('s');
            secureString.AppendChar('e');
            secureString.AppendChar('r');
            secureString.AppendChar('P');
            secureString.AppendChar('a');
            secureString.AppendChar('s');
            secureString.AppendChar('s');
            secureString.AppendChar('w');
            secureString.AppendChar('o');
            secureString.AppendChar('r');
            secureString.AppendChar('d');
            e.Password = secureString;

            // Enabling handled to hide the password dialog.
            e.Handled = true;
        }
    }
}
