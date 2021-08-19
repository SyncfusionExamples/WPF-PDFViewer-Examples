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
            HideHorizontalToolbar();
            HideVerticalToolbar();
        }

        #region Helper Methods
        /// <summary>
        /// Hides the top (horizontal) toolbar of PDF Viewer
        /// </summary>
        private void HideHorizontalToolbar()
        {
            // Disable the top document toolbar of PDF Viewer
            pdfViewer.ShowToolbar = false;
        }

        /// <summary>
        /// Hides the left side (vertical) toolbar of PDF Viewer.
        /// </summary>
        private void HideVerticalToolbar()
        {
            // Hides the thumbnail icon. 
            pdfViewer.ThumbnailSettings.IsVisible = false;

            // Hides the bookmark icon. 
            pdfViewer.IsBookmarkEnabled = false;

            // Hides the layer icon. 
            pdfViewer.EnableLayers = false;

            // Hides the organize page icon. 
            pdfViewer.PageOrganizerSettings.IsIconVisible = false;

            // Hides the redaction icon. 
            pdfViewer.EnableRedactionTool = false;

            // Hides the form icon. 
            pdfViewer.FormSettings.IsIconVisible = false;
        }
        #endregion
    }
}