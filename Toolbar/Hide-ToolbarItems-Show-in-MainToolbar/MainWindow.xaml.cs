using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PrintAndSaveButtonInMainToolbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;
        private Button saveAsButton;
        private Button printButton;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if NETCOREAPP
            filePath = @"../../../Data/1-output.pdf";
#else
            filePath = @"../../Data/1-output.pdf";
#endif
            pdfViewer.Load(filePath);
            pdfViewer.Loaded += PdfViewer_Loaded;
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
            pdfViewer.DocumentUnloaded += PdfViewer_DocumentUnloaded;

        }

        private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
        {
            saveAsButton.IsEnabled = false;
            printButton.IsEnabled = false;
        }

        private void PdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            saveAsButton.IsEnabled = true;
            printButton.IsEnabled = true;
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;
            Button signatureButton = (Button)toolbar.Template.FindName("PART_ButtonSignature", toolbar);
            signatureButton.Visibility = Visibility.Collapsed;
            Button searchButton = (Button)toolbar.Template.FindName("PART_ButtonTextSearch", toolbar);
            searchButton.Visibility = Visibility.Collapsed;
            ToggleButton annotationsButton = (ToggleButton)toolbar.Template.FindName("PART_Annotations", toolbar);
            annotationsButton.Visibility = Visibility.Collapsed;
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                    FileMenuItem.Visibility = Visibility.Collapsed;
                if (FileMenuItem.Name == "PART_SaveAsMenuItem")
                    FileMenuItem.Visibility = Visibility.Collapsed;
                if (FileMenuItem.Name == "PART_PrintMenuItem")
                    FileMenuItem.Visibility = Visibility.Collapsed;
            }
            saveAsButton = new Button();
            saveAsButton.Height = 25;
            saveAsButton.Width = 25;
            System.Windows.Shapes.Path saveAsPath = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("M10.919004,9.5349956 L12.456006,11.068996 7.5370017,15.999999 6.0000001,15.999999 6.0000001,14.466998 z M12.631999,8.0000043 C12.736498,8.0000043 12.840998,8.0397506 12.920998,8.1192422 L13.879993,9.076138 C14.039991,9.2361207 14.039991,9.493093 13.879993,9.6530757 L13.129996,10.400995 11.593004,8.8681607 12.343,8.1192422 C12.423,8.0397506 12.527499,8.0000043 12.631999,8.0000043 z M7.0000001,4.7683714E-07 L9.0000001,4.7683714E-07 9.0000001,3.0000005 7.0000001,3.0000005 z M0.99999999,0 L4.0000001,0 4.0000001,3.9999943 10,3.9999943 10,0 10.287994,0 14,3.2020049 14,7.7830009 13.626007,7.4099994 C13.358994,7.1460071 13.007004,6.9999957 12.632004,6.9999957 12.255997,6.9999957 11.901993,7.1460071 11.636993,7.4110065 L10.921997,8.1240048 10.916,8.1209989 10.039993,8.9999967 1.9999999,8.9999967 1.9999999,13.999999 5.0540009,13.999999 5.0000001,14.054 5.0000001,16 0.99999999,16 C0.44700628,16 5.2262678E-08,15.553009 0,15 L0,1.0000004 C5.2262678E-08,0.44799826 0.44700628,0 0.99999999,0 z"), // Example shape
                Width = 20,
                Height = 20,
                Style = (Style)this.pdfViewer.FindResource("MenuIconStyle"),
                Stretch = Stretch.Uniform, // Set the stretch mode
            };
            saveAsButton.Style = (Style)this.pdfViewer.FindResource("newButtonStyle");
            saveAsButton.Content = saveAsPath;
            saveAsButton.Click += SaveAsButton_Click;
            saveAsButton.ToolTip = "SaveAs";
            saveAsButton.BorderThickness = new Thickness(0);
            saveAsButton.Margin = new Thickness(10, 0, 0, 0);

            printButton = new Button();
            printButton.Height = 25;
            printButton.Width = 25;
            System.Windows.Shapes.Path printPath = new System.Windows.Shapes.Path
            {
                Data = Geometry.Parse("F1M235.967,99.88L229.46,99.88L229.46,96.688L235.967,96.688z M229.46,84.563L235.967,84.563L235.967,86.793L229.46,86.793z M241.783,86.793L237.046,86.793L237.046,83.371L228.361,83.371L228.361,86.793L223.783,86.793L223.783,96.688L228.361,96.688L228.361,101.072L237.046,101.072L237.046,96.688L241.783,96.688z"), // Example shape
                Width = 20,
                Height = 20,
                Style = (Style)this.pdfViewer.FindResource("MenuIconStyle"),
                Stretch = Stretch.Uniform, // Set the stretch mode
            };
            printButton.Content = printPath;
            printButton.Style = (Style)this.pdfViewer.FindResource("newButtonStyle");
            printButton.Click += PrintButton_Click;
            printButton.ToolTip = "Print";
            printButton.BorderThickness = new Thickness(0);
            printButton.Margin = new Thickness(10, 0, 0, 0);

            StackPanel stackPanel = (StackPanel)toolbar.Template.FindName("PART_FileMenuStack", toolbar);

            stackPanel.Children.Insert(1, printButton);
            stackPanel.Children.Insert(1, saveAsButton);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Print();
        }

        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog save = new Microsoft.Win32.SaveFileDialog();
            save.Filter = "PDF Files (*.pdf)|*.pdf";
            if (save.ShowDialog() == true)
            {
                FileInfo saveFi = new FileInfo(save.FileName);
                if (save.FileName != string.Empty)
                {
                    pdfViewer.LoadedDocument.Save(save.FileName);
                    System.Windows.MessageBox.Show("File saved succesfully");
                }
            }
        }
    }
}
