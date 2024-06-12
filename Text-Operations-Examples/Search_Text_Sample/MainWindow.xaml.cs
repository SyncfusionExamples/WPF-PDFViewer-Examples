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

namespace Search_Text_Sample
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
            pdfViewer.Load("../../Data/Fsharp_Succinctly.pdf");
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(searchtext.Text))
            {
                MessageBox.Show("Please enter the text to search");
                searchtext.Focus();
                return;
            }

            pdfViewer.SearchText(searchtext.Text);
        }
    }
}
