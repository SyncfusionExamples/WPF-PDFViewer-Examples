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
        readonly List<float> m_dpiValues;
        readonly List<float> m_sizeValues;
        float m_dpiX;
        float m_dpiY;
        float m_imageWidth;
        float m_imageHeight;
        bool m_isExportAllPages = true;
        int m_fromPageNumber = 1;
        int m_toPageNumber = 1;
        bool m_isCustomSize = false;
        bool m_isCustomDpi = false;
        PdfViewerControl pdfViewerControl;
        bool m_isMaintainAspectRatio = true;

        #endregion

        #region Constructor
        public ViewModel()
        {
            m_dpiValues = new List<float> { 50, 100, 200, 300 };
            m_dpiX = m_dpiValues[0];
            m_dpiY = m_dpiValues[0];
            m_sizeValues = new List<float> { 300, 500, 1000, 2000 };
            m_imageWidth = m_sizeValues[0];
            m_imageHeight = m_sizeValues[0];
            pdfViewerControl = new PdfViewerControl();
            LoadPdf();
        }
        #endregion

        #region Properties
        public List<float> DpiValues
        {
            get
            {
                return m_dpiValues;
            }
        }

        public List<float> SizesValues
        {
            get
            {
                return m_sizeValues;
            }
        }

        public float DpiX
        {
            get
            {
                return m_dpiX;
            }
            set
            {
                m_dpiX = value;
            }
        }

        public float DpiY
        {
            get
            {
                return m_dpiY;
            }
            set
            {
                m_dpiY = value;
            }
        }

        public bool IsExportAllPages
        {
            get
            {
                return m_isExportAllPages;
            }
            set
            {
                m_isExportAllPages = value;
                if (!value)
                    ToPageNumber = pdfViewerControl.PageCount;
            }
        }

        public float ImageWidth
        {
            get
            {
                return m_imageWidth;
            }
            set
            {
                m_imageWidth = value;
            }
        }

        public float ImageHeight
        {
            get
            {
                return m_imageHeight;
            }
            set
            {
                m_imageHeight = value;
            }
        }

        public int FromPageNumber
        {
            get
            {
                return m_fromPageNumber;
            }
            set
            {
                m_fromPageNumber = value;
            }
        }

        public int ToPageNumber
        {
            get
            {
                return m_toPageNumber;
            }
            set
            {
                m_toPageNumber = value;
            }
        }

        public bool IsCustomSize
        {
            get
            {
                return m_isCustomSize;
            }
            set
            {
                m_isCustomSize = value;
            }
        }

        public bool IsCustomDpi
        {
            get
            {
                return m_isCustomDpi;
            }
            set
            {
                m_isCustomDpi = value;
            }
        }

        public bool IsMaintainAspectRatio
        {
            get
            {
                return m_isMaintainAspectRatio;
            }
            set
            {
                m_isMaintainAspectRatio = value;
            }
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
            if (IsExportAllPages)
                endIndex = pdfViewerControl.PageCount - 1;
            else
            {
                startIndex = FromPageNumber - 1;
                endIndex = ToPageNumber - 1;
            }

            if (endIndex >= pdfViewerControl.PageCount)
                MessageBox.Show("Entered page number exceeds the page count", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            else if (endIndex < startIndex)
                MessageBox.Show("The End Value should not be less than the Start Value", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                BitmapSource[] images = null;
                if (IsCustomDpi && IsCustomSize)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, new SizeF(ImageWidth, ImageHeight), DpiX, DpiY, IsMaintainAspectRatio);
                else if (IsCustomDpi)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, DpiX, DpiY);
                else if (IsCustomSize)
                    images = pdfViewerControl.ExportAsImage(startIndex, endIndex, new SizeF(ImageWidth, ImageHeight), IsMaintainAspectRatio);
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
