using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using Syncfusion.SfSkinManager;
using Syncfusion.Windows.PdfViewer;
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

namespace WPF_PDFViewer_Annotations
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
public MainWindow()
{
    SfSkinManager.SetTheme(this, new Theme("FluentLight"));
    InitializeComponent();
    pdfViewer.DocumentLoaded += PdfViewer_DocumentLoaded;
    pdfViewer.FreeTextAnnotationChanged += PdfViewer_FreeTextAnnotationChanged;
    pdfViewer.StickyNoteAnnotationChanged += PdfViewer_StickyNoteAnnotationChanged;
    pdfViewer.InkAnnotationChanged += PdfViewer_InkAnnotationChanged;
    pdfViewer.TextMarkupAnnotationChanged += PdfViewer_TextMarkupAnnotationChanged;
    pdfViewer.ShapeAnnotationChanged += PdfViewer_ShapeAnnotationChanged;
    pdfViewer.StampAnnotationChanged += PdfViewer_StampAnnotationChanged;
    pdfViewer.Load("../../Data/Sample Document.pdf");
    treeView.Background = new SolidColorBrush(Color.FromRgb(235,235,238));
}

private void PdfViewer_StampAnnotationChanged(object sender, StampAnnotationChangedEventArgs e)
{
    PerformAddorRemoveAnnotations("RubberStampAnnotation", e.Name, e.Action, e.PageNumber);
}

private void PdfViewer_ShapeAnnotationChanged(object sender, ShapeAnnotationChangedEventArgs e)
{
    if(e.Type == ShapeAnnotationType.Arrow || e.Type == ShapeAnnotationType.Line)
        PerformAddorRemoveAnnotations("LineAnnotation", e.Name, e.Action, e.PageNumber);
    else if(e.Type == ShapeAnnotationType.Circle)
        PerformAddorRemoveAnnotations("EllipseAnnotation", e.Name, e.Action, e.PageNumber);
    else if (e.Type == ShapeAnnotationType.Polygon)
        PerformAddorRemoveAnnotations("PolygonAnnotation", e.Name, e.Action, e.PageNumber);
    else if (e.Type == ShapeAnnotationType.Rectangle)
        PerformAddorRemoveAnnotations("SquareAnnotation", e.Name, e.Action, e.PageNumber);
    else if (e.Type == ShapeAnnotationType.Polyline)
        PerformAddorRemoveAnnotations("PolylineAnnotation", e.Name, e.Action, e.PageNumber);
}

private void PdfViewer_TextMarkupAnnotationChanged(object sender, TextMarkupAnnotationChangedEventArgs e)
{
    if(e.Type == TextMarkupAnnotationType.Highlight)
        PerformAddorRemoveAnnotations("HighlightAnnotation", e.Name, e.Action, e.PageNumber);
    else if(e.Type == TextMarkupAnnotationType.Strikeout)
        PerformAddorRemoveAnnotations("StrikeoutAnnotation", e.Name, e.Action, e.PageNumber);
    else if(e.Type == TextMarkupAnnotationType.Underline)
        PerformAddorRemoveAnnotations("UnderlineAnnotation", e.Name, e.Action, e.PageNumber);
    else if(e.Type == TextMarkupAnnotationType.Squiggly)
        PerformAddorRemoveAnnotations("SquigglyAnnotation", e.Name, e.Action, e.PageNumber);
}

private void PdfViewer_InkAnnotationChanged(object sender, InkAnnotationChangedEventArgs e)
{
    PerformAddorRemoveAnnotations("InkAnnotation",e.Name,e.Action,e.PageNumber);
}

private void PerformAddorRemoveAnnotations(string name ,string annotationName ,AnnotationChangedAction action ,int annotationPageNumber)
{
    if (action == AnnotationChangedAction.Add)
    {
        PdfLoadedDocument lDoc = pdfViewer.LoadedDocument;
        if (treeView.Items.Count > 0)
        {
            for (int i = 0; i < treeView.Items.Count; i++)
            {
                string[] page = (treeView.Items[i] as TreeViewItem).Header.ToString().Split(' ');
                int pageNumber = int.Parse(page[page.Length - 1]);
                if (pageNumber == annotationPageNumber)
                {
                    TreeViewItem item = treeView.Items[i] as TreeViewItem;
                    TreeViewItem childItem = new TreeViewItem();
                    for (int j = 0; j < lDoc.Pages[annotationPageNumber - 1].Annotations.Count; j++)
                    {
                        if (lDoc.Pages[annotationPageNumber - 1].Annotations[j].Name == annotationName)
                        {
                            childItem.Header = name;
                            childItem.Tag = lDoc.Pages[annotationPageNumber - 1].Annotations[j];
                            break;
                        }
                    }
                    item.Items.Add(childItem);
                    break;
                }
                else if (pageNumber > annotationPageNumber)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = "PAGE - " + annotationPageNumber;
                    treeView.Items.Insert(i, item);
                    TreeViewItem childItem = new TreeViewItem();
                    for (int j = 0; j < lDoc.Pages[annotationPageNumber - 1].Annotations.Count; j++)
                    {
                        if (lDoc.Pages[annotationPageNumber - 1].Annotations[j].Name == annotationName)
                        {
                            childItem.Header = name;
                            childItem.Tag = lDoc.Pages[annotationPageNumber - 1].Annotations[j];
                            break;
                        }
                    }
                    item.Items.Add(childItem);
                }
            }
        }
        else
        {
            TreeViewItem item = new TreeViewItem();
            item.Header = "PAGE - " + annotationPageNumber;
            item.FontSize = 16;
            treeView.Items.Add(item);
            TreeViewItem childItem = new TreeViewItem();
            for (int j = 0; j < lDoc.Pages[annotationPageNumber - 1].Annotations.Count; j++)
            {
                if (lDoc.Pages[annotationPageNumber - 1].Annotations[j].Name == annotationName)
                {
                    childItem.Header = name;
                    childItem.Tag = lDoc.Pages[annotationPageNumber - 1].Annotations[j];
                    break;
                }
            }
            item.Items.Add(childItem);
        }
    }
    else if (action == AnnotationChangedAction.Remove)
    {
        for (int i = 0; i < treeView.Items.Count; i++)
        {
            string[] page = (treeView.Items[i] as TreeViewItem).Header.ToString().Split(' ');
            int pageNumber = int.Parse(page[page.Length - 1]);
            if (pageNumber == annotationPageNumber)
            {
                TreeViewItem item = treeView.Items[i] as TreeViewItem;
                for (int j = 0; j < item.Items.Count; j++)
                {
                    PdfLoadedAnnotation loadedAnnotation = (item.Items[j] as TreeViewItem).Tag as PdfLoadedAnnotation;
                    PdfAnnotation annotation = (item.Items[j] as TreeViewItem).Tag as PdfAnnotation;
                    if (loadedAnnotation != null)
                    {
                        if (annotationName == loadedAnnotation.Name)
                        {
                            item.Items.RemoveAt(j);
                            break;
                        }
                    }
                    else if (annotation != null)
                    {
                        if (annotationName == annotation.Name)
                        {
                            item.Items.RemoveAt(j);
                            break;
                        }
                    }
                }
                if (item.Items.Count == 0)
                {
                    treeView.Items.RemoveAt(i);
                }
                break;
            }
        }
    }
}

private void PdfViewer_StickyNoteAnnotationChanged(object sender, StickyNoteAnnotationChangedEventArgs e)
{
    PerformAddorRemoveAnnotations("PopupAnnotation", e.Name, e.Action, e.PageNumber);
}

private void PdfViewer_FreeTextAnnotationChanged(object sender, FreeTextAnnotationChangedEventArgs e)
{
    PerformAddorRemoveAnnotations("FreeTextAnnotation", e.Name, e.Action, e.PageNumber);
}

private void PdfViewer_DocumentLoaded(object sender, EventArgs args)
{
    PdfLoadedDocument loadedDocument = pdfViewer.LoadedDocument;
    treeView.Items.Clear();
    for (int i = 0; i < loadedDocument.Pages.Count; i++)
    {
        TreeViewItem viewItem = new TreeViewItem();
        if (loadedDocument.Pages[i].Annotations.Count > 0)
        {
            viewItem.Header = "PAGE - " + (i + 1);
            viewItem.FontSize = 16;
            treeView.Items.Add(viewItem);
            viewItem.IsExpanded = true;
            for (int j = 0; j < loadedDocument.Pages[i].Annotations.Count; j++)
            {
                PdfLoadedAnnotation annotation = loadedDocument.Pages[i].Annotations[j] as PdfLoadedAnnotation;
                if (annotation is PdfLoadedInkAnnotation || annotation is PdfLoadedTextMarkupAnnotation || annotation is PdfLoadedEllipseAnnotation
                    || annotation is PdfLoadedLineAnnotation || annotation is PdfLoadedRectangleAnnotation || annotation is PdfLoadedSquareAnnotation
                    || annotation is PdfLoadedCircleAnnotation || annotation is PdfLoadedFreeTextAnnotation || annotation is PdfLoadedRubberStampAnnotation
                    || annotation is PdfLoadedPopupAnnotation || annotation is PdfLoadedPolygonAnnotation || annotation is PdfLoadedPolyLineAnnotation)
                {
                    string name = annotation.ToString();
                    string[] annotNames = name.Split('.');
                    TreeViewItem childItem = new TreeViewItem();
                    childItem.Header = annotation.Type.ToString();
                    childItem.Tag = annotation;
                    viewItem.Items.Add(childItem);
                }
            }
            if (viewItem.Items.Count == 0)
                treeView.Items.Remove(viewItem);
        }
    }
}

private void treeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
{
    var selectedItem = treeView.SelectedItem as TreeViewItem;
    if (selectedItem != null && !selectedItem.Header.ToString().Contains("PAGE"))
    {
        PdfLoadedAnnotation loadedAnnotation = selectedItem.Tag as PdfLoadedAnnotation;
        PdfAnnotation annotation = selectedItem.Tag as PdfAnnotation;
        if (loadedAnnotation != null)
            pdfViewer.SelectAnnotation(loadedAnnotation.Name, true);
        else if (annotation != null)
            pdfViewer.SelectAnnotation(annotation.Name, true);
    }
}
    }
}
