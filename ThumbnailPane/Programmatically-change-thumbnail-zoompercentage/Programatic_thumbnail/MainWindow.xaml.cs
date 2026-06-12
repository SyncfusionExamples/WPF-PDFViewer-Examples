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

namespace Programatic_thumbnail
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            pdfViewer.Load("../../../F#.pdf");
            pdfViewer.ThumbnailSettings.IsExpanded = true;
        }
        private void pdfViewer_DocumentLoaded(object sender, EventArgs args)
        {
            Grid LeftToolGrid = pdfViewer.Template.FindName("PART_Grid", pdfViewer) as Grid;
            if (LeftToolGrid != null)
            {
                foreach (var LeftToolChild in LeftToolGrid.Children)
                {
                    if (LeftToolChild is ThumbnailPane)
                    {
                        StackPanel stackPanel = (LeftToolChild as ThumbnailPane).Template.FindName("Thumb_StackPanel", (LeftToolChild as ThumbnailPane)) as StackPanel;
                        foreach (var stackPanelChild in stackPanel.Children)
                        {
                            if (stackPanelChild is Slider)
                            {
                                (stackPanelChild as Slider).Value = 12;
                            }
                        }
                    }
                }
            }
        }
    }
}