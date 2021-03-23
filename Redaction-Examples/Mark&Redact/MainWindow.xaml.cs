using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace PdfViewerDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool m_isRegionsMarked;

        # region Constructor
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.PageBorder.Color = Color.Gray;
        }

        #endregion

        /// <summary>
        /// On Apply redaction button is clicked.
        /// </summary>
        private void ApplyRedaction_Click(object sender, RoutedEventArgs e)
        {
            // if regions are not already marked, mark the regions before applying redaction.
            if(!m_isRegionsMarked)
                MarkRegions();

            // Apply redaction to the specific regions
            pdfViewer.PageRedactor.ApplyRedaction();
            m_isRegionsMarked = false;
        }

        /// <summary>
        /// Clears the existing regions.
        /// </summary>
        void ClearMarkedRegions()
        {
            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                // Pass empty rectangle collection to override (clear) the existing marked regions.
                pdfViewer.PageRedactor.MarkRegions(i, new List<RectangleF>());
            }
            // Disable the redaction mode.
            pdfViewer.PageRedactor.EnableRedactionMode = false;
        }

        /// <summary>
        /// On Save button is clicked.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Save the PDF
            pdfViewer.Save("Redacted.Pdf");
            MessageBox.Show("The document is saved in the application folder");
        }

        /// <summary>
        /// On Clear button is clicked.
        /// </summary>
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // Clear all the marked regions.
            ClearMarkedRegions();
        }

        /// <summary>
        /// On Mark Regions button is clicked
        /// </summary>
        private void MarkRegions_Click(object sender, RoutedEventArgs e)
        {
            // Mark regions
            MarkRegions();
        }

        /// <summary>
        /// Marks regions based on the given rectangles
        /// </summary>
        private void MarkRegions()
        {
            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                // create a rectagle regions collection to be redacted at the page at the index of i. 
                List<RectangleF> rectangles = new List<RectangleF>();

                // Add a rectangle with required dimensions. For example, X=50, Y=80, Width =150, Height=100.
                rectangles.Add(new RectangleF(50, 80, 150, 100));

                // Mark the regions.
                pdfViewer.PageRedactor.MarkRegions(i, rectangles);
            }
            // Enable the redaction mode.
            pdfViewer.PageRedactor.EnableRedactionMode = true;
        }
    }
}
