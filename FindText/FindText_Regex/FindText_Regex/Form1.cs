using System.Collections.Generic;
using System.Windows.Forms;
using Syncfusion.Pdf.Parsing;
using System.Text.RegularExpressions;

namespace FindText_Regex
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(@"../../Data/Bank statement.pdf");

            // Extract text from the PDF file to get matched texts present in the file based on regex pattern.
            string extractedText = string.Empty;
            for (int i = 0; i < loadedDocument.Pages.Count; i++)
            {
                extractedText += loadedDocument.Pages[i].ExtractText(true);
            }

            // Get matched text collection for the required pattern by passing the extracted text to System.Text.RegularExpressions.Regex.
            MatchCollection matchCollection = Regex.Matches(extractedText, @"\$\d");

            // List out the matched text.
            List<string> matchedText = new List<string>();
            for (int i=0;i<matchCollection.Count;i++)
            {
                // Filter the repeated text.
                if (!matchedText.Contains(matchCollection[i].Value))
                    matchedText.Add(matchCollection[i].Value);
            }

            // Pass the list of matched pattern strings as a parameter find text for getting its bounds.
            TextSearchResultCollection searchResults;
            bool isMatchFound= loadedDocument.FindText(matchedText, out searchResults);
            
            // Clear all the resources.
            extractedText = string.Empty;
            matchedText.Clear();
        }
    }
}
