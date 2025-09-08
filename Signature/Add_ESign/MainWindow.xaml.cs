using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Security;
using Syncfusion.Windows.PdfViewer;
using System;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Path = System.Windows.Shapes.Path;
using Bitmap = System.Drawing.Bitmap;

namespace WPF_Sample_FW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string filePath = "../../../Data/";
        bool addSignature = false;
        public MainWindow()
        {
            InitializeComponent();
            PDFViewer.Load(filePath + "Ink signature.pdf");
            PDFViewer.Loaded += PDFViewer_Loaded;
            PDFViewer.ZoomMode = ZoomMode.FitPage;
        }

        private void PDFViewer_Loaded(object sender, RoutedEventArgs e)
        {
            // Find the default toolbar from the PDFViewer control template.
            DocumentToolbar toolbar = PDFViewer.Template.FindName("PART_Toolbar", PDFViewer) as DocumentToolbar;

            // Find the stack panel inside the toolbar where buttons are arranged.
            StackPanel stackPanel = (StackPanel)toolbar.Template.FindName("PART_ToolbarStack", toolbar);

            // Get the last stack panel in the toolbar stack (usually contains default buttons).
            StackPanel stack = stackPanel.Children[stackPanel.Children.Count - 1] as StackPanel;

            // Get the first button from the stack to copy its style and icon.
            Button button = stack.Children[0] as Button;
            Path oldStylePath = button.Content as Path;

            Button eSignButton = GetButton(oldStylePath, button);

            // Add the new eSign button to the toolbar stack.
            stackPanel.Children.Add(eSignButton);
        }

        private void eSignButton_Click(object sender, RoutedEventArgs e)
        {
            addSignature = true;
        }

        private Button GetButton(System.Windows.Shapes.Path oldPath, Button button)
        {
            // Create a new custom button for eSign functionality.
            Button eSignButton = new Button();

            // Create a new Path object to use as the icon for the eSign button.
            Path path = new Path();
            path.Data = System.Windows.Media.Geometry.Parse("M218.17 424.14c-2.95-5.92-8.09-6.52-10.17-6.52s-7.22.59-10.02 6.19l-7.67 15.34c-6.37 12.78-25.03 11.37-29.48-2.09L144 386.59l-10.61 31.88c-5.89 17.66-22.38 29.53-41 29.53H80c-8.84 0-16-7.16-16-16s7.16-16 16-16h12.39c4.83 0 9.11-3.08 10.64-7.66l18.19-54.64c3.3-9.81 12.44-16.41 22.78-16.41s19.48 6.59 22.77 16.41l13.88 41.64c19.75-16.19 54.06-9.7 66 14.16 1.89 3.78 5.49 5.95 9.36 6.26v-82.12l128-127.09V160H248c-13.2 0-24-10.8-24-24V0H24C10.7 0 0 10.7 0 24v464c0 13.3 10.7 24 24 24h336c13.3 0 24-10.7 24-24v-40l-128-.11c-16.12-.31-30.58-9.28-37.83-23.75zM384 121.9c0-6.3-2.5-12.4-7-16.9L279.1 7c-4.5-4.5-10.6-7-17-7H256v128h128v-6.1zm-96 225.06V416h68.99l161.68-162.78-67.88-67.88L288 346.96zm280.54-179.63l-31.87-31.87c-9.94-9.94-26.07-9.94-36.01 0l-27.25 27.25 67.88 67.88 27.25-27.25c9.95-9.94 9.95-26.07 0-36.01z");

            // Copy the fill color from the existing button's icon.
            path.Fill = oldPath.Fill;

            // Set the icon as the content of the new button.
            eSignButton.Content = path;

            // Match the icon's size and layout to the original button.
            path.Stretch = Stretch.Uniform;
            path.Height = oldPath.Height;
            path.Width = oldPath.Width;

            // Match the button's background and layout properties to the original button.
            eSignButton.Height = button.Height;
            eSignButton.Width = button.Width;
            eSignButton.Margin = button.Margin;
            eSignButton.Style = button.Style;
            // Attach the click event handler for eSign functionality.
            eSignButton.Click += eSignButton_Click;

            return eSignButton;
        }

        private void PDFViewer_PageClicked(object sender, PageClickedEventArgs args)
        {
            if (addSignature)
            {
                int pageIndex = PDFViewer.CurrentPageIndex - 1;
                CreateSignatureImage();

                //Gets the first page of the document
                PdfLoadedPage page = PDFViewer.LoadedDocument.Pages[pageIndex] as PdfLoadedPage;

                //Retrieve the clicked client area position
                System.Windows.Point clientPoint = args.Position;

                // Convert client point to page point, which accounts for zoom mode.
                System.Windows.Point pagePoint = PDFViewer.ConvertClientPointToPagePoint(clientPoint, pageIndex + 1);
                double x = pagePoint.X;
                double y = pagePoint.Y;

                //Creates a certificate instance from PFX file with private key.
                PdfCertificate pdfCert = new PdfCertificate( filePath + "PDF.pfx", "password123");

                //Creates a digital signature
                PdfSignature Signature = new PdfSignature(PDFViewer.LoadedDocument, page, pdfCert, "Signature");

                //Sets an image for signature field
                PdfBitmap signatureImage = new PdfBitmap( filePath + "ESign.png");

                // Center the signature on the click position using dimensions in points.
                float signWidth = signatureImage.PhysicalDimension.Width;
                float signHeight = signatureImage.PhysicalDimension.Height;
                Signature.Bounds = new System.Drawing.RectangleF((float)(x), (float)(y), signWidth, signHeight);

                Signature.ContactInfo = "johndoe@owned.us";
                Signature.LocationInfo = "Honolulu, Hawaii";
                Signature.Reason = "I am author of this document.";

                //Draws the signature image
                Signature.Appearance.Normal.Graphics.DrawImage(signatureImage, 0, 0);

                //Save the document into stream.
                MemoryStream stream = new MemoryStream();
                PDFViewer.LoadedDocument.Save(stream);
                stream.Position = 0;

                //Reloads the document
                PDFViewer.Load(stream);
                addSignature = false;
            }

        }

        private void CreateSignatureImage()
        {
            int pageIndex = PDFViewer.CurrentPageIndex - 1;
            if(pageIndex < 0)
                return;

            //Creating image for current date and time details
            CreateCurrentDataImage();

            //Combine the name image and date-time image into a single image
            CombineSignatureAndDataImage();
          
        }

        private void CombineSignatureAndDataImage()
        {
            // Load the two images
            using (System.Drawing.Image nameImage = System.Drawing.Image.FromFile(filePath + "John.png"))
            using (System.Drawing.Image signImage = System.Drawing.Image.FromFile(filePath + "DigitalSignatureBlock.png"))
            {
                // Create a new bitmap with combined width and max height
                int signatureWidth = nameImage.Width + signImage.Width;
                int signatureHeight = Math.Max(nameImage.Height, signImage.Height);
                using (Bitmap combinedImage = new Bitmap(signatureWidth, signatureHeight))
                using (Graphics g = System.Drawing.Graphics.FromImage(combinedImage))
                {
                    // Draw both images side by side
                    g.DrawImage(nameImage, 0, 0);
                    g.DrawImage(signImage, nameImage.Width, 0);
                    // Save the result
                    combinedImage.Save(filePath + "ESign.png", System.Drawing.Imaging.ImageFormat.Png);
                }
            }
        }

        private void CreateCurrentDataImage()
        {
            int width = 200;
            int height = 100;
            string signerName = "John";
            string dateTime = DateTime.Now.ToString("yyyy.MM.dd\nHH:mm:ss zzz");
            string text = $"Digitally signed by {signerName}\nDate: {dateTime}\n\n";
            string outputPath = filePath + "DigitalSignatureBlock.png";
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(outputPath));
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics graphics = System.Drawing.Graphics.FromImage(bitmap))
                {
                    using (Font font = new Font("Arial", 9))
                    using (SolidBrush backgroundBrush = new SolidBrush(System.Drawing.Color.White))
                    {
                        graphics.FillRectangle(backgroundBrush, 0, 0, width, height);
                        RectangleF layoutRect = new RectangleF(10, 10, width - 20, height - 20);
                        graphics.DrawString(text, font, System.Drawing.Brushes.Black, layoutRect);
                        bitmap.Save(outputPath, System.Drawing.Imaging.ImageFormat.Png);
                    }
                }
            }
        }
    }
}
