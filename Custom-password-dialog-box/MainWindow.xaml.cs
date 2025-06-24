using Microsoft.SqlServer.Server;
using Microsoft.Win32;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
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

namespace CustomPasswordDialog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string preLoadPDF;
        PasswordDialog passwordDialog;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            pdfViewer.Visibility = Visibility.Hidden;
        }

        private void openPDFbutton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Visibility = Visibility.Visible;
            GetPDFfromLocalStorage();
        }
      
        private void GetPDFfromLocalStorage()
        {
            try
            {
                var OFD = new OpenFileDialog()
                {
                    DefaultExt = "pdf",
                    Filter = "Pdf files|*.pdf",
                    Title = "Search a PDF",
                    FilterIndex = 1,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                };
                if (OFD.ShowDialog()==true && File.Exists(OFD.FileName))
                {
                    preLoadPDF = OFD.FileName;

                    pdfViewer.ReferencePath = AppDomain.CurrentDomain.BaseDirectory;
                    pdfViewer.Load(preLoadPDF);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error" + ex.Message);
            }
        }

        private void pdfViewer_GetDocumentPassword(object sender, Syncfusion.Windows.PdfViewer.GetDocumentPasswordEventArgs e)
        {
            passwordDialog = new PasswordDialog();
            passwordDialog.ShowDialog();
            if (passwordDialog.DialogResult == true && !string.IsNullOrEmpty(passwordDialog.password))
            {
                SecureString secureString = ConvertToSecureString(passwordDialog.password);
                e.Password = secureString;
            }
            e.Handled = true;
        }

        private void pdfViewer_ErrorOccurred(object sender, Syncfusion.Windows.PdfViewer.ErrorOccurredEventArgs args)
        {
            if (passwordDialog.DialogResult == true && args.Message == "Can't open an encrypted document. The password is invalid.")
            {
                MessageBox.Show(args.Message);
                pdfViewer.Load(preLoadPDF);
            }
        }

        private SecureString ConvertToSecureString(string secureString)
        {
            SecureString passsword = new SecureString();
            foreach (char c in secureString)
            {
                passsword.AppendChar(c);
            }
            passsword.MakeReadOnly();
            return passsword;
        }
    }
}
