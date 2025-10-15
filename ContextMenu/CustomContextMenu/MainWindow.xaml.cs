using Syncfusion.Windows.PdfViewer;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomContextMenu
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../Data/Input.pdf");
            pdfViewer.ContextMenuOpening += PdfViewer_ContextMenuOpening;
        }

        private void Pan_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.CursorMode = PdfViewerCursorMode.HandTool;
        }

        private void SelectZoomArea_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.CursorMode = PdfViewerCursorMode.MarqueeZoom;
        }

        private void FitToPageMenuItem_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.ZoomMode = Syncfusion.Windows.PdfViewer.ZoomMode.FitPage;
        }
        private void PdfViewer_ContextMenuOpening(object sender, ContextMenuOpeningEventArgs e)
        {
            if (e.Source == "Text Selection")
            {
                ContextMenu contextmenu = new ContextMenu();
                contextmenu.FlowDirection = FlowDirection.LeftToRight;
                contextmenu.HorizontalAlignment = HorizontalAlignment.Center;
                MenuItem selectZoomArea = new MenuItem();
                selectZoomArea.Header = "Select Zoom Area";
                selectZoomArea.Click += SelectZoomArea_Click;
                contextmenu.Items.Add(selectZoomArea);
                MenuItem fitToPageMenuItem = new MenuItem();
                fitToPageMenuItem.Header = "Fit to Page";
                fitToPageMenuItem.Click += FitToPageMenuItem_Click;
                contextmenu.Items.Add(fitToPageMenuItem);
                MenuItem pan = new MenuItem();
                pan.Header = "Pan";
                pan.Click += Pan_Click;
                contextmenu.Items.Add(pan);
                contextmenu.IsOpen = true;
                contextmenu.StaysOpen = true;
                e.Handled = true;
            }
        }
    }
}