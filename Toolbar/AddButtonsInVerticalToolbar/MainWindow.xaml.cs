using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Tools.Controls;

namespace AddButtonsInVerticalToolbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RibbonWindow_Loaded(object sender, RoutedEventArgs e)
        {
            OutlinePane outlinePane = pdfViewer.Template.FindName("PART_OutlinePane", pdfViewer) as OutlinePane;

            StackPanel stackPanel = (StackPanel)outlinePane.Template.FindName("PART_OutlineStackPanel", outlinePane);

            Button button1 = new Button();
            button1.Margin = new Thickness(0, 8, 0, 8);
            button1.Width = 30;
            button1.Height = 25;
            button1.Content = "B1";
            stackPanel.Children.Add(button1);
            button1.Click += Button1_Click; ;

            Button button2 = new Button();
            button2.Margin = new Thickness(0, 4, 0, 8);
            button2.Width = 30;
            button2.Height = 25;
            button2.Content = "B2";
            stackPanel.Children.Add(button2);
            button2.Click += Button2_Click; ;
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            string urlToOpen = "https://help.syncfusion.com/wpf/pdf-viewer/overview";
            Process.Start(new ProcessStartInfo
            {
                FileName = urlToOpen,
                UseShellExecute = true
            });
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            string urlToOpen = "https://help.syncfusion.com/wpf/pdf-viewer/getting-started";
            Process.Start(new ProcessStartInfo
            {
                FileName = urlToOpen,
                UseShellExecute = true
            });
        }
    }
}
