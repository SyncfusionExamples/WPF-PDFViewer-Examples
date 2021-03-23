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
            pdfViewer.PageBorder.Color = Color.Gray;
            pdfViewer.PageRedactor.RedactionApplied += PageRedactor_RedactionApplied;
        }

        #endregion

        /// <summary>
        /// Reset control values after redaction applied.
        /// </summary>
        private void PageRedactor_RedactionApplied(object sender, RedactionEventArgs e)
        {
            Reset();
        }

        /// <summary>
        /// Resets the control values
        /// </summary>
        private void Reset()
        {
            textBox.Clear();
            MarkForRedaction.IsChecked = false;
        }

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
                    // Mark the regions from the bounds of the text.
                    MarkRegions(textBounds);
                }
                else
                    MessageBox.Show("Please enter the text.");
            }
            // Apply redaction to the marked bounds.
            pdfViewer.PageRedactor.ApplyRedaction();
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

                // Extract text and its bounds from the PDF document.
                List<TextData> textDataCollection = new List<TextData>();
                string extractedText = pdfViewer.ExtractText(i, out textDataCollection).ToLower();

                int start = 0;
                int indexOfText = 0;
                int end = extractedText.Length;
                int count = 0;

                // Iterate and get all the instance of the given text.
                while ((start <= end) && (indexOfText > -1))
                {
                    count = end - start;
                    // Get the next index of the text to be searched 
                    indexOfText = extractedText.IndexOf(text, start, count);
                    if (indexOfText == -1)
                        break;                  

                    // Holds the bounds of the first character in the text
                    RectangleF startCharacterBounds = textDataCollection[indexOfText].Bounds;

                    // Holds the bounds of the last character in the text
                    RectangleF endCharacterBounds = textDataCollection[indexOfText + text.Length - 1].Bounds;

                    // Get the bounds of the whole text
                    RectangleF rectangle = new RectangleF(startCharacterBounds.X, startCharacterBounds.Y,
                        endCharacterBounds.X - startCharacterBounds.X + endCharacterBounds.Width,
                        startCharacterBounds.Height > endCharacterBounds.Height ? startCharacterBounds.Height : endCharacterBounds.Height);
                    bounds.Add(rectangle);

                    start = indexOfText + text.Length;
                }
                // Add to the collection if any text is obtained.
                if (bounds.Count > 0)
                    textBounds.Add(i, bounds);
            }
            return textBounds;
        }

        /// <summary>
        /// Marks the rectangle regions to be redacted in the PDF pages
        /// </summary>
        /// <param name="bounds">It has the collection of information about the page index and the bounds of the areas to be redacted</param>
        private void MarkRegions(Dictionary<int, List<RectangleF>> bounds)
        {
            if (bounds.Count > 0)
            {
                // Iterate the collection and mark regions
                foreach (KeyValuePair<int, List<RectangleF>> textBounds in bounds)
                {
                    pdfViewer.PageRedactor.MarkRegions(textBounds.Key, textBounds.Value);
                }
                pdfViewer.PageRedactor.EnableRedactionMode = true;
            }
            else
            {
                // If no bounds are present for the given text.
                MessageBox.Show("The text is not found in the document");
                Reset();
            }
        }

        /// <summary>
        /// Clears the existing regions.
        /// </summary>
        private void ClearMarkedRegions()
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
        /// On Mark for redaction is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MarkForRedaction_Checked(object sender, RoutedEventArgs e)
        {
            if (textBox.Text.Length > 0)
            {
                // Get the text bounds and mark regions.
                Dictionary<int, List<RectangleF>> textBounds = GetTextBounds(textBox.Text);
                MarkRegions(textBounds);
            }
            else
            {
                MessageBox.Show("Please enter the text");
                MarkForRedaction.IsChecked = false;
            }
        }

        /// <summary>
        /// On Save button is clicked.
        /// </summary>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
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
            MarkForRedaction.IsChecked = false;
        }
    }
}
