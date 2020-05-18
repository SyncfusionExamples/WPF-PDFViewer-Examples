using System.Collections.Generic;

namespace PDFToImage
{
    public class Model
    {
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
        bool m_isMaintainAspectRatio = true;

        public Model()
        {
            m_dpiValues = new List<float> { 50, 100, 200, 300 };
            m_dpiX = m_dpiValues[0];
            m_dpiY = m_dpiValues[0];
            m_sizeValues = new List<float> { 300, 500, 1000, 2000 };
            m_imageWidth = m_sizeValues[0];
            m_imageHeight = m_sizeValues[0];
        }

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
    }
}
