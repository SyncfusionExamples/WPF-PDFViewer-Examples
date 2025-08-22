using Syncfusion.Windows.PdfViewer;
using System.Windows;

namespace PDFViewer_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        PDFViewerWindow ownerPasswordWindow;
        PDFViewerWindow userPasswordWindow;
        bool isOwnerPassword;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OwnerPassword_Button_Click(object sender, RoutedEventArgs e)
        {
            ownerPasswordWindow = new PDFViewerWindow();
            if (userPasswordWindow != null && userPasswordWindow.IsVisible)
            {
                userPasswordWindow.Close();
            }
            isOwnerPassword = true;
            ChangesInPDFViewer(ownerPasswordWindow);
        }

        private void UserPassword_Button_Click(object sender, RoutedEventArgs e)
        {
            userPasswordWindow = new PDFViewerWindow();
            if (ownerPasswordWindow != null && ownerPasswordWindow.IsVisible)
            {
                ownerPasswordWindow.Close();
            }
            ChangesInPDFViewer(userPasswordWindow);
        }

        private void ChangesInPDFViewer(PDFViewerWindow window)
        {
            string filePath = "../../../Data/Input.pdf";
            window.PDFViewercontrol.GetDocumentPassword += PdfViewer_GetDocumentPassword;
            window.PDFViewercontrol.Load(filePath);
            window.Show();
            window.WindowState = WindowState.Maximized;
            window.PDFViewercontrol.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitWidth;
        }

        private void PdfViewer_GetDocumentPassword(object sender, GetDocumentPasswordEventArgs e)
        {
            System.Security.SecureString secureString = new System.Security.SecureString();
            if (isOwnerPassword)
            {
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
                isOwnerPassword = false;
            }
            else
            {
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
            }
            e.Password = secureString;

            // Enabling handled to hide the password dialog.
            e.Handled = true;
        }       
    }
}
