using System;
using System.IO;
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

        int[] GetPages()
        {
            try
            {
                int[] pageIndexes = null;
                string[] pageText=  textBox.Text.Split(new char[] {','});
                pageIndexes = new int[pageText.Length];
                for(int i=0;i<pageText.Length;i++)
                {
                    pageIndexes[i] = int.Parse(pageText[i].Trim());
                }
                return pageIndexes;
            }
            catch (Exception exception)
            {
                string message = "Error Occurred: " + exception.Message + "\n";
                message += "Plese provide page numbers in the required format";
                MessageBox.Show(message);
                return null;
            }
        }

        private void Organize_Click(object sender, RoutedEventArgs e)
        {
            int[] pages = GetPages();
            if (pages != null)
            {
                if (Clockwise.IsChecked.Value)
                    pdfViewer.PageOrganizer.RotateClockwise(pages);
                else if (Counterclockwise.IsChecked.Value)
                    pdfViewer.PageOrganizer.RotateCounterclockwise(pages);
                else if (Remove.IsChecked.Value)
                {
                    if (pages.Length < pdfViewer.PageCount)
                        pdfViewer.PageOrganizer.RemovePages(pages);
                    else
                        MessageBox.Show("Selected pages count should be less than that of the orginal file");
                }
                else if (Rearrange.IsChecked.Value)
                {
                    if (pages.Length == pdfViewer.PageCount)
                        pdfViewer.PageOrganizer.ReArrange(pages);
                    else
                        MessageBox.Show("Pages count should be equal to that of original file");
                }
                if (Save.IsChecked.Value)
                    pdfViewer.Save("Organized.pdf");
            }
        }
    }
}
