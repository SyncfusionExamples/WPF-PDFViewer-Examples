using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace WPF_ScrollNavigateSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string _fileNameWithPath = "";
        private Dictionary<string, DocPosition> _savedPosition = new Dictionary<string, DocPosition>();
        private DocPosition _currentPosition;
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 50; i <= 800; i = i + 50)
            {
                ZoomListView.Items.Add(i);
            }
            this.Loaded += MainWindow_Loaded;
            this.Closed += Window_Closed;
            pdfViewerControl.ShowToolbar = false;
            pdfViewerControl.MaximumZoomPercentage = 800;
            pdfViewerControl.MinimumZoomPercentage = 10;
            pdfViewerControl.ThumbnailSettings.IsVisible = false;
            pdfViewerControl.IsBookmarkEnabled = false;
            pdfViewerControl.EnableLayers = false;
            pdfViewerControl.PageOrganizerSettings.IsIconVisible = false;
            pdfViewerControl.EnableRedactionTool = false;
            pdfViewerControl.FormSettings.IsIconVisible = false;
            pdfViewerControl.ScrollChanged += PdfViewerControl_ScrollChanged;
            pdfViewerControl.DocumentLoaded += PdfViewerControl_DocumentLoaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Transfer_ScrollData_FromFileToDict();
        }

        private void Transfer_ScrollData_FromFileToDict()
        {
            string scrollPositions;
#if NETFRAMEWORK
            scrollPositions = System.IO.File.ReadAllText("../../Data/scrolledposition.txt");
#else
            scrollPositions = System.IO.File.ReadAllText("../../../Data/scrolledposition.txt");
#endif
            if (!scrollPositions.IsNullOrWhiteSpace() && scrollPositions != "")
            {
                string[] scrollInfo = scrollPositions.Split('\n');
                foreach (string scrollInfo_Pdf in scrollInfo)
                {
                    if (scrollInfo_Pdf != "")
                    {
                        string[] lastSavedValue = scrollInfo_Pdf.Split(',');
                        int zoomPercentage = Convert.ToInt32(lastSavedValue[1]);
                        double horizonalPosition = Convert.ToDouble(lastSavedValue[2]);
                        double verticalPostion = Convert.ToDouble(lastSavedValue[3]);
                        _savedPosition.Add(lastSavedValue[0], new DocPosition(zoomPercentage, horizonalPosition, verticalPostion));
                    }
                }
            }
        }
        private void ZoomList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            int zoomvalue = Convert.ToInt32(ZoomListView.SelectedItem.ToString());
            pdfViewerControl.ZoomTo(zoomvalue);
        }

        private void PdfViewerControl_DocumentLoaded(object sender, System.EventArgs args)
        {

            if (_savedPosition.ContainsKey(_fileNameWithPath))
            {
                _currentPosition = _savedPosition[_fileNameWithPath];
            }
            else
            {
                _currentPosition = new DocPosition(100, 0, 0);
                _savedPosition.Add(_fileNameWithPath, _currentPosition);
            }
            pdfViewerControl.ZoomTo(_currentPosition.ZoomPercent);
            //calculate the offset based on zoom value
            double zoomfactor = (float)pdfViewerControl.ZoomPercentage / 100f;
            pdfViewerControl.ScrollTo(_currentPosition.Horizontal / zoomfactor, _currentPosition.Vertical / zoomfactor);

        }

        private void PdfViewerControl_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs args)
        {
            //To check if it not intially scrolled values
            if (!(args.VerticalOffset == 0 && args.HorizontalOffset != 0) && (args.VerticalOffset != 0 || args.HorizontalOffset != 0))
            {
                if (_savedPosition.ContainsKey(_fileNameWithPath))
                {
                    _savedPosition.Remove(_fileNameWithPath);
                }
                _savedPosition.Add(_fileNameWithPath, new DocPosition(pdfViewerControl.ZoomPercentage, args.HorizontalOffset, args.VerticalOffset));
            }

        }

        private void OpenBtn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.DefaultExt = ".pdf";
            openFileDlg.Filter = "Pdf documents (.pdf)|*.pdf";
            if (openFileDlg.ShowDialog() == true)
            {
                _fileNameWithPath = System.IO.Path.GetFileNameWithoutExtension(openFileDlg.FileName);
                pdfViewerControl.Load(openFileDlg.FileName);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Transfer_ScrollData_FromDictToFile();
        }
        private void Transfer_ScrollData_FromDictToFile()
        {

#if NETFRAMEWORK
            System.IO.File.WriteAllText("../../Data/scrolledposition.txt", "");
#else
            System.IO.File.WriteAllText("../../../Data/scrolledposition.txt", "");
#endif
            foreach (var PositionValue in _savedPosition)
            {
                string filepath = PositionValue.Key;
                DocPosition lastSavedPosition = PositionValue.Value;
#if NETFRAMEWORK
                System.IO.File.AppendAllText("../../Data/scrolledposition.txt", filepath + "," + lastSavedPosition.ZoomPercent + "," + lastSavedPosition.Horizontal.ToString() + "," + lastSavedPosition.Vertical.ToString() + "\n");
#else
                System.IO.File.AppendAllText("../../../Data/scrolledposition.txt", filepath + "," + lastSavedPosition.ZoomPercent + "," + lastSavedPosition.Horizontal.ToString() + "," + lastSavedPosition.Vertical.ToString() + "\n");
#endif 
            }
        }
    }
}
