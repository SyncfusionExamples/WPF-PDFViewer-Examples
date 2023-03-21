using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace customized_toolbar_sample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button fitPage;
        Button fitWidth;
        ToggleButton fileButton;
        ToggleButton marqueeZoom;
        ToggleButton handTool;
        ToggleButton selectTool;

        public MainWindow()
        {
            InitializeComponent();
            PdfLoadedDocument pdfLoadedDocument = new PdfLoadedDocument(@"D:\Succinity\PDF_Succinctly.pdf");
            pdfViewer.Load(pdfLoadedDocument);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;
            StackPanel annotationStack = (StackPanel)toolbar.Template.FindName("PART_Annotation_Stack", toolbar);
            fitWidth = (Button)toolbar.Template.FindName("PART_ButtonFitWidth", toolbar);
            fitPage = (Button)toolbar.Template.FindName("PART_ButtonFitPage", toolbar);
            marqueeZoom = (ToggleButton)toolbar.Template.FindName("PART_MarqueeZooming", toolbar);
            fileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);
            handTool = (ToggleButton)toolbar.Template.FindName("PART_HandTool", toolbar);
            selectTool = (ToggleButton)toolbar.Template.FindName("PART_SelectTool", toolbar);
            annotationStack.GotMouseCapture += AnnotationStack_GotMouseCapture;
            annotationStack.GotTouchCapture += AnnotationStack_GotTouchCapture;
            pdfViewer.ZoomChanged += PdfViewer_ZoomChanged;
            fitWidth.Click += FitWidth_Click;
            fitPage.Click += Fitpage_Click;
            marqueeZoom.Checked += MarqueeZoom_Checked;
            fileButton.Click += FileButton_Click;
            handTool.Checked += HandTool_Checked;
            selectTool.Checked += SelectTool_Checked;
        }

        private void SelectTool_Checked(object sender, RoutedEventArgs e)
        {
            if (marqueeZoom.IsChecked == true)
            {
                marqueeZoom.IsChecked = false;
                marqueeZoom.IsHitTestVisible = true;
            }
        }

        private void HandTool_Checked(object sender, RoutedEventArgs e)
        {
            if (marqueeZoom.IsChecked == true)
            {
                marqueeZoom.IsChecked = false;
                marqueeZoom.IsHitTestVisible = true;
            }
        }
        
        private void FileButton_Click(object sender, RoutedEventArgs e)
        {
            if (marqueeZoom.IsChecked == true)
            {
                marqueeZoom.IsChecked = false;
            }
            selectTool.IsChecked = true;
        }

        private void AnnotationStack_GotTouchCapture(object sender, TouchEventArgs e)
        {
            if (marqueeZoom.IsChecked == true)
            {
                marqueeZoom.IsChecked = false;
                selectTool.IsChecked = true;
                marqueeZoom.IsHitTestVisible = true;
            }
        }
        private void AnnotationStack_GotMouseCapture(object sender, MouseEventArgs e)
        {
            if (marqueeZoom.IsChecked == true)
            {
                marqueeZoom.IsChecked = false;
                selectTool.IsChecked = true;
                marqueeZoom.IsHitTestVisible = true;
            }
        }

        private void PdfViewer_ZoomChanged(object sender, ZoomEventArgs args)
        {
            fitWidth.IsEnabled = true;
            fitPage.IsEnabled = true;
            if (pdfViewer.ZoomMode == ZoomMode.FitPage)
            {
                fitPage.IsEnabled = false;
            }
            if (pdfViewer.ZoomMode == ZoomMode.FitWidth)
            {
                fitWidth.IsEnabled = false;
            }
        }

        private void MarqueeZoom_Checked(object sender, RoutedEventArgs e)
        {
            marqueeZoom.IsChecked = true;
            marqueeZoom.IsHitTestVisible = false;
            pdfViewer.CursorMode = PdfViewerCursorMode.MarqueeZoom;
            if (selectTool.IsChecked == true)
            {
                selectTool.IsChecked = false;
                selectTool.IsHitTestVisible = true;
            }
            else if (handTool.IsChecked == true)
            {
                handTool.IsChecked = false;
                handTool.IsHitTestVisible = true;
            }
        }

        private void Fitpage_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.ZoomMode = ZoomMode.FitPage;
        }

        private void FitWidth_Click(object sender, RoutedEventArgs e)
        {
            pdfViewer.ZoomMode = ZoomMode.FitWidth;
        }
    }
}
