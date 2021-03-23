using Syncfusion.Windows.PdfViewer;
using System.Collections.Generic;
using System.Drawing;
using System.Windows;

namespace PdfViewerDemo
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        # region Constructor
        public MainWindow()
        {
            InitializeComponent();
        }
        #endregion

        /// <summary>
        /// On Apply redaction button is clicked.
        /// </summary>
        private void ApplyRedaction_Click(object sender, RoutedEventArgs e)
        {
            if (!MarkForRedaction.IsChecked.Value)
            {
                if (textBox.Text.Length > 0)
                {
                    Dictionary<int, List<RectangleF>> textBounds = GetTextBounds(textBox.Text);
                    MarkRegions(textBounds);
                }
                else
                    MessageBox.Show("Please enter the text");
            }
            pdfViewer.PageRedactor.ApplyRedaction();
            textBox.Clear();
        }

        /// <summary>
        /// Gets all the bounds of the text present in the PDF document.
        /// </summary>
        /// <param name="text">text to be searched</param>
        /// <returns>The collection of page index and the bounds collection of the searched text</returns>
        private Dictionary<int, List<RectangleF>> GetTextBounds(string text)
        {
            text = text.ToLower();
            Dictionary<int, List<RectangleF>> textBounds = new Dictionary<int, List<RectangleF>>();

            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                List<RectangleF> bounds = new List<RectangleF>();

                List<TextData> textDataCollection = new List<TextData>();
                string extractedText = pdfViewer.ExtractText(i, out textDataCollection).ToLower();

                int start = 0;
                int indexOfText = 0;
                int end = extractedText.Length;
                int count = 0;

                while ((start <= end) && (indexOfText > -1))
                {
                    count = end - start;
                    indexOfText = extractedText.IndexOf(text, start, count);
                    if (indexOfText == -1)
                        break;                  

                    // Holds the bounds of the first character in the text
                    RectangleF startCharacterBounds = textDataCollection[indexOfText].Bounds;

                    // Holds the bounds of the last character in the text
                    RectangleF endCharacterBounds = textDataCollection[indexOfText + text.Length - 1].Bounds;

                    RectangleF rectangle = new RectangleF(startCharacterBounds.X, startCharacterBounds.Y,
                        endCharacterBounds.X - startCharacterBounds.X + endCharacterBounds.Width,
                        startCharacterBounds.Height > endCharacterBounds.Height ? startCharacterBounds.Height : endCharacterBounds.Height);
                    bounds.Add(rectangle);

                    start = indexOfText + text.Length;
                }
                textBounds.Add(i, bounds);
            }
            return textBounds;
        }

        /// <summary>
        /// Marks the rectangle regions to be redacted in the PDF pages
        /// </summary>
        /// <param name="bounds">It has the collection of information about the page index and the bounds of the areas to be redacted</param>
        void MarkRegions(Dictionary<int, List<RectangleF>> bounds)
        {
            for (int i = 0; i < bounds.Count; i++)
            {
                pdfViewer.PageRedactor.MarkRegions(i, bounds[i]);
            }
            pdfViewer.PageRedactor.EnableRedactionMode = true;
        }

        /// <summary>
        /// Clears the existing regions.
        /// </summary>
        void ClearRegions()
        {
            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                pdfViewer.PageRedactor.MarkRegions(i, new List<RectangleF>());
            }
            pdfViewer.PageRedactor.EnableRedactionMode = false;
        }

        /// <summary>
        /// On Mark for redaction is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkForRedaction_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                Dictionary<int, List<RectangleF>> textBounds = GetTextBounds(textBox.Text);
                MarkRegions(textBounds);
            }
            else
                MessageBox.Show("Please enter the text");
        }

        /// <summary>
        /// On Mark for Redaction is unchecked.
        /// </summary>
        private void MarkForRedaction_Unchecked(object sender, RoutedEventArgs e)
        {
            ClearRegions();
        }

        /// <summary>
        /// On Save button is clicked.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Save("Redacted.Pdf");
            MessageBox.Show("The document is saved in the application folder");
        }
    }
}
