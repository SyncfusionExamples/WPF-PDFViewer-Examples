using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using Syncfusion.Pdf.Parsing;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Shapes;

namespace UriAnnotation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public RectangleF annotationBounds = RectangleF.Empty;
        public int annotationPageIndex = -1;
        ToolTip tooltip = new ToolTip();
        PdfUnitConvertor convertor = new PdfUnitConvertor();
        public MainWindow()
        {
            InitializeComponent();
#if NETFRAMEWORK
             docView.Load("../../../../../PDF/HTTP Succinctly.pdf");
#else
             docView.Load( "../../../../../../PDF/HTTP Succinctly.pdf");
#endif
        }
        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            PdfLoadedDocument lDoc = docView.LoadedDocument;

            //Set the standard font.
            PdfFont font = new PdfStandardFont(PdfFontFamily.Helvetica, 20);

            //Draw the border.
            docView.LoadedDocument.Pages[0].Graphics.DrawRectangle(PdfPens.Red, new RectangleF(10, 10, 30, 30));
            
            //Create a new Uri Annotation.
            PdfUriAnnotation uriAnnotation = new PdfUriAnnotation(new RectangleF(10, 10, 30, 30), "http://www.google.com");
            //Set Text to uriAnnotation.
            uriAnnotation.Text = "Uri Annotation";
            //Add this annotation to a new page.
            docView.LoadedDocument.Pages[0].Annotations.Add(uriAnnotation);
            //Save the document and reload it into viewer.
            MemoryStream stream = new MemoryStream();
            lDoc.Save(stream);
            docView.Load(stream);
            //Get the annotation page index and bounds. 
            annotationPageIndex = 0;
            annotationBounds = uriAnnotation.Bounds;
            //Create the tooltip with content and placement target.
            TextBlock textBlock = new TextBlock();
            textBlock.Text = "Uri Annotation";
            tooltip.PlacementTarget = docView;
            tooltip.Content = textBlock;
            //Subscribe page mouse move event of WPF PdfViewer.
            docView.PageMouseMove += PdfViewer_PageMouseMove;
        }

        private void PdfViewer_PageMouseMove(object sender, Syncfusion.Windows.PdfViewer.PageMouseMoveEventArgs args)
        {
            //Ensure the annotation bounds are not empty.
            if (annotationBounds != RectangleF.Empty)
            {
                //Convert the position from pixels to point values.
                float x = convertor.ConvertFromPixels((float)args.Position.X, PdfGraphicsUnit.Point);
                float y = convertor.ConvertFromPixels((float)args.Position.Y, PdfGraphicsUnit.Point);
                //Ensure the retrieved page index matches the annotation page index, and then compare the converted position value with the annotation bounds.
                if (args.PageIndex == annotationPageIndex && annotationBounds.Contains(x, y))
                {
                    //change the cursor and set the tooltip position.
                    docView.Cursor = Cursors.Hand;
                    docView.ForceCursor = true;
                    if (!tooltip.IsOpen)
                    {
                        tooltip.IsOpen = true;
                        tooltip.Placement = PlacementMode.Relative;
                    }
                    System.Windows.Point point = Mouse.GetPosition(docView);
                    tooltip.HorizontalOffset = point.X + 25;
                    tooltip.VerticalOffset = point.Y + 25;
                }
                else
                {
                    docView.ForceCursor = false;
                    tooltip.IsOpen = false;
                }
            }
        }
    }
}
