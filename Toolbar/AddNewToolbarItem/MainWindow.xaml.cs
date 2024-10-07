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

namespace AddNewToolbarItem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Button button;
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            pdfViewer.Load(@"../../../Data/PDF_Succinctly.pdf");
#else
         pdfViewer.Load(@"../../Data/PDF_Succinctly.pdf");
#endif
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AddButton();
        }

        private void AddButton()
        {
            OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;
            ToggleButton thumbnailButton = (ToggleButton)outlinePane.Template.FindName("PART_ThumbnailButton", outlinePane);
            thumbnailButton.Checked += ThumbnailButton_Checked;
            thumbnailButton.Unchecked += ThumbnailButton_Unchecked;
            ThumbnailPane thumbnailPane = pdfViewer.Template.FindName("PART_ThumbnailPanel", pdfViewer) as ThumbnailPane;
            StackPanel stack = (StackPanel)thumbnailPane.Template.FindName("Thumb_StackPanel", thumbnailPane);
            button = new Button();
            button.Margin = new Thickness(10, 0, 0, 0);
            button.Width = 35;
            button.Height = 24;
            button.Content = "New";
            stack.Children.Add(button);
        }

        private void ThumbnailButton_Unchecked(object sender, RoutedEventArgs e)
        {
            button.Visibility = Visibility.Visible;
        }

        private void ThumbnailButton_Checked(object sender, RoutedEventArgs e)
        {
            button.Visibility = Visibility.Collapsed;
        }
    }
}
