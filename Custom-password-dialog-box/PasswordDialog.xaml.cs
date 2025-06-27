using System.Windows;

namespace CustomPasswordDialog
{
    /// <summary>
    /// Interaction logic for PasswordDialog.xaml
    /// </summary>
    public partial class PasswordDialog : Window
    {
        public string password;
        public PasswordDialog()
        {
            InitializeComponent();
            password = string.Empty;
            paswordInput.Password = string.Empty;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            password = paswordInput.Password;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
