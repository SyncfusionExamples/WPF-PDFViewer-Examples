using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace PdfViewer
{
    public class ViewModel : INotifyPropertyChanged
    {
        private Stream documentStream;
        private int formatIndex = 1;
        ICommand exportAnnotationsCommand;
        ICommand importAnnotationsCommand;

        public ViewModel()
        {
#if NETFRAMEWORK
            documentStream = new FileStream(@"..\..\Data\Simple Shapes.pdf", FileMode.OpenOrCreate);
#else
            documentStream = new FileStream(@"..\..\..\Data\Simple Shapes.pdf", FileMode.OpenOrCreate);
#endif 
    }

        public event PropertyChangedEventHandler PropertyChanged;

        public Stream DocumentStream
        {
            get
            {
                return documentStream;
            }
            set
            {
                documentStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public int FormatIndex
        {
            get
            {
                return formatIndex;
            }
            set
            {
                formatIndex = value;
                OnPropertyChanged(new PropertyChangedEventArgs("FormatIndex"));
            }
        }

        public ICommand ExportAnnotationsCommand
        {
            get
            {
                if (exportAnnotationsCommand == null)
                {
                    exportAnnotationsCommand = new DelegateCommand<PdfViewerControl>(ExportAnnotations);
                }
                return exportAnnotationsCommand;
            }
        }

        public ICommand ImportAnnotationsCommand
        {
            get
            {
                if (importAnnotationsCommand == null)
                {
                    importAnnotationsCommand = new DelegateCommand<PdfViewerControl>(ImportAnnotations);
                }
                return importAnnotationsCommand;
            }
        }

        void ExportAnnotations(PdfViewerControl pdfViewerControl)
        {
            AnnotationDataFormat annotationDataFormat;
            if (FormatIndex == 0)
            {
                annotationDataFormat = AnnotationDataFormat.Fdf;
#if NETFRAMEWORK
                pdfViewerControl.ExportAnnotations(@"../../Data/Exported Annotations.fdf", annotationDataFormat);
#else
                pdfViewerControl.ExportAnnotations(@"../../../Data/Exported Annotations.fdf", annotationDataFormat);
#endif
            }
            else
            {
                annotationDataFormat = AnnotationDataFormat.XFdf;
#if NETFRAMEWORK
                pdfViewerControl.ExportAnnotations(@"../../Data/Exported Annotations.xfdf", annotationDataFormat);
#else
                pdfViewerControl.ExportAnnotations(@"../../../Data/Exported Annotations.xfdf", annotationDataFormat);
#endif
            }
            
        }

        void ImportAnnotations(PdfViewerControl pdfViewerControl)
        {
            AnnotationDataFormat annotationDataFormat;
            if (FormatIndex == 0)
            {
                annotationDataFormat = AnnotationDataFormat.Fdf;
#if NETFRAMEWORK
                pdfViewerControl.ImportAnnotations(@"../../Data/Annotations.fdf", annotationDataFormat);
#else
                pdfViewerControl.ImportAnnotations(@"../../../Data/Annotations.fdf", annotationDataFormat);
#endif
            }
            else
            {
                annotationDataFormat = AnnotationDataFormat.XFdf;
#if NETFRAMEWORK
                pdfViewerControl.ImportAnnotations(@"../../Data/Annotations.xfdf", annotationDataFormat);
#else
                pdfViewerControl.ImportAnnotations(@"../../../Data/Annotations.xfdf", annotationDataFormat);
#endif
            }
            
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
