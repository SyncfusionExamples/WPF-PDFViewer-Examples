using Microsoft.Win32;
using Syncfusion.Windows.PdfViewer;
using System;
using System.IO;
using System.Security;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Controls;

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
            pdfViewer.Loaded += PdfViewer_Loaded;
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

            //Iterating the File Context menu and hides the Open button
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                    FileMenuItem.Visibility = Visibility.Collapsed;
            }
        }

        private void openPDFbutton_Click(object sender, RoutedEventArgs e)
        {
            GetPDFfromLocalStorage();
        }
      
        private void GetPDFfromLocalStorage()
        {
            try
            {
                var openPdfDialog = new OpenFileDialog()
                {
                    DefaultExt = "pdf",
                    Filter = "Pdf files|*.pdf",
                    Title = "Search a PDF",
                    FilterIndex = 1,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                };
                if (openPdfDialog.ShowDialog()==true && File.Exists(openPdfDialog.FileName))
                {
                    preLoadPDF = openPdfDialog.FileName;

                    pdfViewer.ReferencePath = AppDomain.CurrentDomain.BaseDirectory;
                    pdfViewer.Load(preLoadPDF);
                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("Unexpected error" + ex.Message);
            }
        }
        /// <summary>
        /// Occurs every time when you try to open a password protected PDF file in run-time
        /// </summary>
        private void pdfViewer_GetDocumentPassword(object sender, Syncfusion.Windows.PdfViewer.GetDocumentPasswordEventArgs e)
        {
            //Initiating the added Custom password dialog box
            passwordDialog = new PasswordDialog();
            passwordDialog.ShowDialog();
            if (passwordDialog.DialogResult == true && !string.IsNullOrEmpty(passwordDialog.password))
            {
                SecureString secureString = ConvertToSecureString(passwordDialog.password);
                e.Password = secureString;
            }
            //Hnadling the GetDocumentPasssword event so that the internal event did not execute
            e.Handled = true;
        }

        /// <summary>
        /// Triggered whenever the wrong password is entered for the PDF
        /// </summary>
        private void pdfViewer_ErrorOccurred(object sender, Syncfusion.Windows.PdfViewer.ErrorOccurredEventArgs args)
        {
            if (passwordDialog.DialogResult == true && args.Message == "Can't open an encrypted document. The password is invalid.")
            {
                MessageBox.Show(args.Message);
                //Reloads the same document whenever entered the wrong password until correct one is given
                pdfViewer.Load(preLoadPDF);
            }
        }
        /// <summary>
        /// Converts the recieved string to a SecureString object
        /// </summary>
        /// <param name="secureString">Password of the document in string format</param>
        /// <returns>Converted SecureString</returns>

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
