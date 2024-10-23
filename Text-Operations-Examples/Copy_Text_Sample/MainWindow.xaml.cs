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

namespace Copy_Text_Sample
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
#if NETCOREAPP
            pdfviewer.Load("../../../Data/Fsharp_Succinctly.pdf");
#else
             pdfviewer.Load("../../Data/Fsharp_Succinctly.pdf");
#endif
            pdfviewer.GotoPage(2);
        }
    }
}
