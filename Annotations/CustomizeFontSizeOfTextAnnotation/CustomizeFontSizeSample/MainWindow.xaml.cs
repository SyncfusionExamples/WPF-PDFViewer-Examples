﻿using Syncfusion.Windows.PdfViewer;
using System.Windows;
using System.Windows.Controls;

namespace CustomizeFontSizeSample
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            pdfViewer.Load("../../../../../PDF/HTTP Succinctly.pdf");
            pdfViewer.Loaded += pdfViewer_Loaded;
        }
        private void pdfViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Get the instance of the FontPropertiesDialog using its template name.
            FontPropertiesDialog propertiesDialog = pdfViewer.Template.FindName("PART_FreeText", pdfViewer) as FontPropertiesDialog;
            // Get the instance of the font size combo box using its template name.
            ComboBox fontSize = (ComboBox)propertiesDialog.Template.FindName("FontSizeMenu", propertiesDialog);
            //Add a custom font size as string to combo box items.
            fontSize.Items.Add("24");
            fontSize.Items.Add("26");
            fontSize.Items.Add("28");
            fontSize.Items.Add("36");
            fontSize.Items.Add("48");
            fontSize.Items.Add("72");
        }
    }
}
