using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
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
            pdfViewer.Load("../../Data/F Sharp Succinctly.pdf");
        }

        #region Helper Methods
        private void HideOpenTool()
        {
            // Get the instance of the toolbar using its template name 
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            // Get the instance of the file menu button using its template name.
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

            //Get the instance of the file menu button context menu and the item collection.
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                //Get the instance of the open menu item using its template name and disable its visibility.
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                {
                    //Set the visibility of the item to collapsed.
                    FileMenuItem.Visibility = Visibility.Collapsed;
                }
            }
        }

        private void HideThumbnailTool()
        {
            //Get the instance of the left pane using its template name 
            OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;
            
            //Get the instance of the thumbnail button using its template name 
            ToggleButton thumbnailButton = (ToggleButton)outlinePane.Template.FindName("PART_ThumbnailButton", outlinePane);

            //Set the visibility of the button to collapsed.
            thumbnailButton.Visibility = Visibility.Collapsed;
        }

        private void HideSearchTool()
        {
            //Get the instance of the toolbar using its template name.
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            //Get the instance of the open file button using its template name.
            Button textSearchButton = (Button)toolbar.Template.FindName("PART_ButtonTextSearch", toolbar);

            //Set the visibility of the button to collapsed.
            textSearchButton.Visibility = System.Windows.Visibility.Collapsed;
        }
        #endregion

        #region Handlers
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HideOpenTool();
            HideThumbnailTool();
            HideSearchTool();
        }
        #endregion
    }
}