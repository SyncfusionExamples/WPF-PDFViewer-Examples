using Syncfusion.Windows.PdfViewer;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace PdfViewerWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.ToolbarSettings.ShowAnnotationTools = false;
            pdfViewer.Load(@"../../../Data/GIS Succinctly.pdf");           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddUnloadTool();            
        }

        /// <summary>
        /// Add unload tool in the toolbar.
        /// </summary>
        private void AddUnloadTool()
        {
            // Access the toolbar from PDF Viewer template. 
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            // Create the custom unload button.
            Button unloadButton = new Button();
            unloadButton.Margin = new Thickness(8, 0, 0, 0);
            unloadButton.BorderThickness = new Thickness(0);
            unloadButton.Height = 32;
            unloadButton.Padding = new Thickness(4);
            unloadButton.BorderThickness = new Thickness(1);
            unloadButton.Content = "Unload";
            // wire the click event.
            unloadButton.Click += UnloadButton_Click;

            // Get the wrap panel from the toolbar.
            WrapPanel wrapPanel = (WrapPanel)toolbar.Template.FindName("toolBar", toolbar);

            // Add the unload button in the toolbar.
            wrapPanel.Children.Add(unloadButton);
        }

        /// <summary>
        /// Click event handler for unload button.
        /// </summary>
        private void UnloadButton_Click(object sender, RoutedEventArgs e)
        {
            // unload the PDF document.
            pdfViewer.Unload(true);
        }
    }
}
