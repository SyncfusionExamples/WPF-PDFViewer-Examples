using Syncfusion.Windows.PdfViewer;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace PrintAndSaveButtonInMainToolbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filepath;
        private Button printButton;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if NETCOREAPPP
            filepath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filepath = @"../../Data/PDF_Succinctly.pdf";
#endif
            //Load the Document
            pdfViewer.Load(filepath);
            pdfViewer.Loaded += PdfViewer_Loaded;
            pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
            pdfViewer.DocumentUnloaded += PdfViewer_DocumentUnloaded;

        }

        private void PdfViewer_DocumentUnloaded(object sender, EventArgs e)
        {
            printButton.IsEnabled = false;
        }

        private void PdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            printButton.IsEnabled = true;
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);
            //Accessing the File Toggle context Menu's iterating and hiding tthe Print button
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                if (FileMenuItem.Name == "PART_PrintMenuItem")
                    FileMenuItem.Visibility = Visibility.Collapsed;
            }

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
            //Adding the Print button in the toolbar
            stackPanel.Children.Insert(1, printButton);
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Print();
        }
    }
}
