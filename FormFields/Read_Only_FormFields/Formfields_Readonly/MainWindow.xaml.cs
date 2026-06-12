using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Formfields_Readonly
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<PdfField> user1List = new ObservableCollection<PdfField>();
        private ObservableCollection<PdfField> user2List = new ObservableCollection<PdfField>();
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../Input.pdf");
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
        }

        private void PdfViewer_DocumentLoaded(object sender, System.EventArgs e)
        {
            UpdateFormFields();
        }

        private void UpdateFormFields()
        {

            if (pdfViewer.LoadedDocument.Form != null)
            {
                var FormFieldCollections = pdfViewer.LoadedDocument.Form.Fields;

                if (FormFieldCollections.Count > 0)
                {
                    for (int i = 0; i < FormFieldCollections.Count; i++)
                    {
                        if (FormFieldCollections[i].Name == "User1_Name" || FormFieldCollections[i].Name == "User1_Age" || FormFieldCollections[i].Name == "User1_Country")
                        {
                            user1List.Add(FormFieldCollections[i]);
                        }
                        else
                        {
                            user2List.Add(FormFieldCollections[i]);
                        }
                    }
                }



            }
        }

        private void SetFieldsReadOnly(ObservableCollection<PdfField> fields, bool isReadOnly)
        {
            foreach (var field in fields)
            {
                if (field is PdfLoadedTextBoxField textField)
                {
                    textField.ReadOnly = isReadOnly;
                }
            }
        }

        private void UserComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (pdfViewer.LoadedDocument == null) return;

            var selectedUser = (UserComboBox.SelectedItem as ComboBoxItem)?.Content?.ToString();

            if (selectedUser == "User1")
            {
                SetFieldsReadOnly(user1List, false);
                SetFieldsReadOnly(user2List, true);
            }
            else if (selectedUser == "User2")
            {
                SetFieldsReadOnly(user2List, false);
                SetFieldsReadOnly(user1List, true);
            }
        }
    }
}