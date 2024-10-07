using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool keyFromTextBoxes = false;
        public MainWindow()
        {
            InitializeComponent();
#if NETFRAMEWORK 
            pdfViewer.Load(@"../../Data/PDF_Succinctly.pdf");
#else
            pdfViewer.Load(@"../../../Data/PDF_Succinctly.pdf");
#endif           
            pdfViewer.KeyDown += PdfViewer_KeyDown;
        }
        private void PdfViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (!keyFromTextBoxes)
            {
                double zoomFactor = (double)pdfViewer.ZoomPercentage / 100;
                double scroll = 200;
                double hScroll = 100;
                double offset = pdfViewer.VerticalOffset / zoomFactor;
                double hOffset = pdfViewer.HorizontalOffset / zoomFactor;
                if (e.Key == Key.J) // Instead of down arrow
                {
                    pdfViewer.ScrollTo(offset + scroll);
                }
                else if (e.Key == Key.K) // Instead of up arrow
                {
                    pdfViewer.ScrollTo(offset - scroll);
                }
                else if (e.Key == Key.L) // Instead of right arrow
                {
                    pdfViewer.ScrollTo(hOffset + hScroll, offset);
                }
                else if (e.Key == Key.H) // Instead of left arrow
                {
                    pdfViewer.ScrollTo(hOffset - hScroll, offset);
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;
            TextSearchBar textSearch_Toolbar = pdfViewer.Template.FindName("PART_TextSearchBar", pdfViewer) as TextSearchBar;
            textSearch_Toolbar.PreviewKeyDown += TextSearch_Toolbar_PreviewKeyDown;
            Button closeButton = (Button)textSearch_Toolbar.Template.FindName("PART_ButtonClose", textSearch_Toolbar);
            closeButton.Click += CloseButton_Click;
            TextBox currentPageTextBox = (TextBox)toolbar.Template.FindName("PART_TextCurrentPageIndex", toolbar);
            currentPageTextBox.GotFocus += CurrentPageTextBox_GotFocus;
            currentPageTextBox.LostFocus += CurrentPageTextBox_LostFocus;
            ComboBox comboBox = (ComboBox)toolbar.Template.FindName("PART_ComboBoxCurrentZoomLevel", toolbar);
            comboBox.GotFocus += ComboBox_GotFocus;
            comboBox.LostFocus += ComboBox_LostFocus;
        }
        private void ComboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            keyFromTextBoxes = false;
        }
        private void ComboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keyFromTextBoxes = true;
        }
        private void CurrentPageTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            keyFromTextBoxes = false;
        }
        private void CurrentPageTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            keyFromTextBoxes = true;
        }
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            keyFromTextBoxes = false;
        }
        private void TextSearch_Toolbar_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            keyFromTextBoxes = true;
            if(e.Key == Key.Escape)
            {
                keyFromTextBoxes = false;
            }
        }
    }
}
