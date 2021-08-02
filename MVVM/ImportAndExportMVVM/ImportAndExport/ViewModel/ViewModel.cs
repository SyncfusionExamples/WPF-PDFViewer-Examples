using Syncfusion.Pdf.Parsing;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Shared;
using System.ComponentModel;
using System.IO;
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
            documentStream = new FileStream(@"..\..\Data\Simple Shapes.pdf", FileMode.OpenOrCreate);
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
                pdfViewerControl.ExportAnnotations(@"../../Data/Exported Annotations.fdf", annotationDataFormat);
            }
            else
            {
                annotationDataFormat = AnnotationDataFormat.XFdf;
                pdfViewerControl.ExportAnnotations(@"../../Data/Exported Annotations.xfdf", annotationDataFormat);
            }
            
        }

        void ImportAnnotations(PdfViewerControl pdfViewerControl)
        {
            AnnotationDataFormat annotationDataFormat;
            if (FormatIndex == 0)
            {
                annotationDataFormat = AnnotationDataFormat.Fdf;
                pdfViewerControl.ImportAnnotations(@"../../Data/Annotations.fdf", annotationDataFormat);
            }
            else
            {
                annotationDataFormat = AnnotationDataFormat.XFdf;
                pdfViewerControl.ImportAnnotations(@"../../Data/Annotations.xfdf", annotationDataFormat);
            }
            
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
