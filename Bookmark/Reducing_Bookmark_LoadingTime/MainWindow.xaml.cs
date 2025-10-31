using Syncfusion.Pdf.Interactive;
using System;
using System.Collections.Generic;
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

namespace ReduceBookmarkLoadingTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load(@"../../../Data/Input.pdf");
            pdfViewer.Loaded += PdfViewer_Loaded;
        }

        private void PdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            PdfBookmarkBase m_bookmarkBase = pdfViewer.LoadedDocument.Bookmarks;
            for (int i = 0; i < m_bookmarkBase.Count; i++)
            {
                PdfBookmark m_bookmark = m_bookmarkBase[i] as PdfLoadedBookmark;
                ReadBookmark(m_bookmark);
            }
        }
        private void ReadBookmark(PdfBookmark bookmark)
        {
            if (bookmark != null)
            {
                if (bookmark.Count != 0)
                {
                    for (int i = 0; i < bookmark.Count; i++)
                    {
                        ReadBookmark(bookmark[i]);
                    }
                }
            }
        }
    }
}
