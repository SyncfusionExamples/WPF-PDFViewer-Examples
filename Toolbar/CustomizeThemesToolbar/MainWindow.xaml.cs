using Syncfusion.SfSkinManager;
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

namespace CustomizeThemesToolbar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            pdfViewer.Load("../../../Data/Barcode.pdf");
#else
			pdfViewer.Load("../../Data/Barcode.pdf");
#endif
        }
        private void FluentLight(object sender, RoutedEventArgs e)
        {
            SfSkinManager.SetTheme(pdfViewer, new Theme { ThemeName = "FluentLight" });
            SfSkinManager.SetTheme(this, new Theme { ThemeName = "FluentLight" });
        }
        private void HideTools(object sender, RoutedEventArgs e)
        {
            //Get the instance of the toolbar using its template name.
            DocumentToolbar toolbar = pdfViewer.Template.FindName("PART_Toolbar", pdfViewer) as DocumentToolbar;

            //Get the instance of the file menu button using its template name.
            ToggleButton FileButton = (ToggleButton)toolbar.Template.FindName("PART_FileToggleButton", toolbar);

            //Get the instance of the file menu button context menu and the item collection.
            ContextMenu FileContextMenu = FileButton.ContextMenu;
            foreach (MenuItem FileMenuItem in FileContextMenu.Items)
            {
                //Get the instance of the open menu item using its template name and disable its visibility.
                if (FileMenuItem.Name == "PART_OpenMenuItem")
                    FileMenuItem.Visibility = System.Windows.Visibility.Collapsed;
                if (FileMenuItem.Name == "PART_SaveMenuItem")
                    FileMenuItem.Visibility = System.Windows.Visibility.Collapsed;
                if (FileMenuItem.Name == "PART_SaveAsMenuItem")
                    FileMenuItem.Visibility = System.Windows.Visibility.Collapsed;
                if (FileMenuItem.Name == "PART_PrintMenuItem")
                    FileMenuItem.Visibility = System.Windows.Visibility.Collapsed;
            }
            FileButton.Visibility = Visibility.Collapsed;
        }

    }
}
