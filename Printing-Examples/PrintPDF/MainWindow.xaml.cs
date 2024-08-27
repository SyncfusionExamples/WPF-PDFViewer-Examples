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

namespace PrintPDF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Load the PDF document in pdfViewer.
        #if NETFRAMEWORK
            pdfViewer.Load("../../Data/Fsharp_Succinctly.pdf");
        #else
            pdfViewer.Load("../../../Data/Fsharp_Succinctly.pdf");
        #endif
        }

        private void print_Click(object sender, RoutedEventArgs e)
        {
            // Print the PDF document with the print dialog.
            pdfViewer.Print(true);
        }

        private void silent_Click(object sender, RoutedEventArgs e)
        {
            // Print the PDF document without the print dialog with the default printer.
            pdfViewer.Print();
        }
    }
}
