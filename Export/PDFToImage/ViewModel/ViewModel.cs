using Syncfusion.Windows.PdfViewer;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace PDFToImage
{
    public class ViewModel
    {
        #region Fields
        ICommand m_closeWindowCommand;
        ICommand m_exportPagesCommand;
        PdfViewerControl pdfViewerControl;
        public Model Model
        {
            get;
            set;
        }

        #endregion

        #region Constructor
        public ViewModel()
        {
            pdfViewerControl = new PdfViewerControl();
            Model = new Model();
            LoadPdf();
        }
        #endregion

        #region Commands
        public ICommand CloseWindowCommand
        {
            get
            {
                if (m_closeWindowCommand == null)
                    m_closeWindowCommand = new DelegateCommand<Window>(CloseWindow);
                return m_closeWindowCommand;
            }
        }

        public ICommand ExportPagesCommand
        {
            get
            {
                if (m_exportPagesCommand == null)
                {
                    m_exportPagesCommand = new DelegateCommand<Window>(ExportPages);
                }
                return m_exportPagesCommand;
            }
        }
        #endregion

        #region Helper Methods
        void CloseWindow(Window window)
        {
            if (window != null)
                window.Close();
        }

        void ExportPages(Window window)
        {
            int startIndex = 0;
            int endIndex = 0;
            if (Model.IsExportAllPages)
                endIndex = pdfViewerControl.PageCount - 1;
            else
            {
                startIndex = Model.FromPageNumber - 1;
                endIndex = Model.ToPageNumber - 1;
            }

            if (endIndex >= pdfViewerControl.PageCount)
                MessageBox.Show("Entered page number exceeds the page count", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (endIndex < startIndex)
                MessageBox.Show("The End Value should not be less than the Start Value", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                BitmapSource[] images = null;
                if (Model.IsCustomDpi && Model.IsCustomSize)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, 
                        new SizeF(Model.ImageWidth, Model.ImageHeight), Model.DpiX, Model.DpiY,
                        Model.IsMaintainAspectRatio);
                else if (Model.IsCustomDpi)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, Model.DpiX, Model.DpiY);
                else if (Model.IsCustomSize)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, 
                        new SizeF(Model.ImageWidth, Model.ImageHeight), Model.IsMaintainAspectRatio);
                else
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex);

                SaveImages(images);

                if (window != null)
                    window.Close();
            }
        }

        void SaveImages(BitmapSource[] images)
        {
            string outputFolder;
#if NETCORE
            outputFolder = @"..\..\..\Output\";
#else
            outputFolder = @"..\..\Output\";
#endif
            if (images != null)
            {
                for (int i = 0; i < images.Length; i++)
                {
                    //Initialize the new Jpeg bitmap encoder
                    BitmapEncoder encoder = new JpegBitmapEncoder();
                    //Create the bitmap frame using the bitmap source and add it to the encoder
                    encoder.Frames.Add(BitmapFrame.Create(images[i]));
                    //Create the file stream for the output in the desired image format
                    FileStream stream = new FileStream(outputFolder + "Image_"+ i.ToString() + ".Jpeg", FileMode.Create);
                    //Save the stream, so that the image will be generated in the output location
                    encoder.Save(stream);
                }
            }
            MessageBox.Show("Images are exported successfully in the Output folder: " + Path.GetFullPath(outputFolder),"Success", MessageBoxButton.OK,MessageBoxImage.Information);
        }

        void LoadPdf()
        {
#if NETCORE
            pdfViewerControl.Load("../../../Data/HTTP Succinctly.pdf");
#else
            pdfViewerControl.Load("../../Data/HTTP Succinctly.pdf");
#endif
        }
        #endregion
    }
}
