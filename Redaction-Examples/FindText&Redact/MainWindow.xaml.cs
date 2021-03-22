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

        private void ApplyRedaction_Click(object sender, RoutedEventArgs e)
        {
            if (!MarkForRedaction.IsChecked.Value)
            {
                Dictionary<int, List<RectangleF>> textBounds = GetTextBounds(textBox.Text);
                MarkRegions(textBounds);
            }
            pdfViewer.PageRedactor.ApplyRedaction();
            textBox.Clear();
        }

        Dictionary<int, List<RectangleF>> GetTextBounds(string text)
        {
            text = text.ToLower();
            Dictionary<int, List<RectangleF>> textBounds = new Dictionary<int, List<RectangleF>>();

            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                List<RectangleF> bounds = new List<RectangleF>();

                List<TextData> textDataCollection = new List<TextData>();
                string extractedText = pdfViewer.ExtractText(i, out textDataCollection).ToLower();

                int start = 0;
                int at = 0;
                int end = extractedText.Length;
                int count = 0;

                while ((start <= end) && (at > -1))
                {
                    count = end - start;
                    at = extractedText.IndexOf(text, start, count);
                    if (at == -1)
                        break;                  

                    RectangleF startBounds = textDataCollection[at].Bounds;
                    RectangleF endBounds = textDataCollection[at + text.Length - 1].Bounds;

                    RectangleF rectangle = new RectangleF(startBounds.X, startBounds.Y,
                        endBounds.X - startBounds.X + endBounds.Width,
                        startBounds.Height > endBounds.Height ? startBounds.Height : endBounds.Height);
                    bounds.Add(rectangle);

                    start = at + text.Length;
                }
                textBounds.Add(i, bounds);
            }
            return textBounds;
        }

        void MarkRegions(Dictionary<int, List<RectangleF>> textBounds)
        {
            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                pdfViewer.PageRedactor.MarkRegions(i, textBounds[i]);
            }
            pdfViewer.PageRedactor.EnableRedactionMode = true;
        }

        void ClearRegions()
        {
            for (int i = 0; i < pdfViewer.PageCount; i++)
            {
                pdfViewer.PageRedactor.MarkRegions(i, new List<RectangleF>());
            }
            pdfViewer.PageRedactor.EnableRedactionMode = true;
        }

        private void MarkForRedaction_Checked(object sender, RoutedEventArgs e)
        {
            Dictionary<int, List<RectangleF>> textBounds = GetTextBounds(textBox.Text);
            MarkRegions(textBounds);
        }

        private void MarkForRedaction_Unchecked(object sender, RoutedEventArgs e)
        {
            ClearRegions();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.Save("Redacted.Pdf");
            MessageBox.Show("The document is saved in the application folder");
        }
    }
}
