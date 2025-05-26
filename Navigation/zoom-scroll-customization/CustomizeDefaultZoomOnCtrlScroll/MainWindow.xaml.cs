using Syncfusion.Windows.PdfViewer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace CustomizeDefaultZoomOnCtrlScroll
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath;
        public MainWindow()
        {
            InitializeComponent();
#if NETCOREAPP
            filePath = "../../../Data/Input.pdf";
#else
            filePath = "../../Data/Input.pdf";
#endif
            PDFViewer.Load(filePath);

            PDFViewer.Loaded += PDFViewer_Loaded;

            PDFViewer.PreviewMouseWheel += PDFViewer_PreviewMouseWheel;

        }

        private void PDFViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (e.Delta > 0)
                {
                    int currentZoom = (PDFViewer.ZoomPercentage + 5) - 25;
                    PDFViewer.ZoomTo(currentZoom);
                }
                else
                {
                    int currentZoom = (PDFViewer.ZoomPercentage - 5) + 25;
                    PDFViewer.ZoomTo(currentZoom);
                }
            }
            e.Handled = true;
        }


        private void PDFViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Access the toolbar from PDF Viewer template.  
            DocumentToolbar toolbar = PDFViewer.Template.FindName("PART_Toolbar", PDFViewer) as DocumentToolbar;

            //Get the buttons from the toolbar
            Button zoominbutton = (Button)toolbar.Template.FindName("PART_ButtonZoomIn", toolbar);
            Button zoomoutbutton = (Button)toolbar.Template.FindName("PART_ButtonZoomOut", toolbar);
            
            //Remove the existing click event for button
            RemoveClickEvents(zoomoutbutton);
            RemoveClickEvents(zoominbutton);
            
            //Add the new click event for the buttons
            zoominbutton.Click += Zoominbutton_Click;
            zoomoutbutton.Click += Zoomoutbutton_Click;
        }


        //Method used to reomve the existing click event
        private void RemoveClickEvents(System.Windows.Controls.Button b)
        {
            RoutedEventHandlerInfo[] routedEventHandlers = GetRoutedEventHandlerss(b, System.Windows.Controls.Button.ClickEvent);
            foreach (RoutedEventHandlerInfo routedEventHandler in routedEventHandlers)
                b.Click -= (RoutedEventHandler)routedEventHandler.Handler;
        }

        public static RoutedEventHandlerInfo[] GetRoutedEventHandlerss(UIElement element, RoutedEvent routedEvent)
        {
            PropertyInfo eventHandlersStoreProperty = typeof(UIElement).GetProperty("EventHandlersStore", BindingFlags.Instance | BindingFlags.NonPublic);
            object eventHandlersStore = eventHandlersStoreProperty.GetValue(element, null/* TODO Change to default(_) if this is not a reference type */);

            MethodInfo getRoutedEventHandler = eventHandlersStore.GetType().GetMethod("GetRoutedEventHandlers", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            RoutedEventHandlerInfo[] routedEventHandlers = (RoutedEventHandlerInfo[])getRoutedEventHandler.Invoke(eventHandlersStore, new object[] { routedEvent });

            return routedEventHandlers;
        }


        private void Zoomoutbutton_Click(object sender, RoutedEventArgs e)
        {
            //set the zoom out percentage
            int currentZoom = PDFViewer.ZoomPercentage - 5;
            PDFViewer.ZoomTo(currentZoom);
        }

        private void Zoominbutton_Click(object sender, RoutedEventArgs e)
        {
            // set the zoom in percentage
            int currentZoom = PDFViewer.ZoomPercentage + 5;
            PDFViewer.ZoomTo(currentZoom);
        }
    }
}

