using System.ComponentModel;
using System.IO;

namespace PdfViewerDemo
{
    public class PdfReport : INotifyPropertyChanged
    {
        private Stream docStream;

        public event PropertyChangedEventHandler PropertyChanged;

        public Stream DocumentStream
        {
            get
            {
                return docStream;
            }
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public PdfReport()
        {
            //Load the stream from the local system.
#if NETCORE
            docStream = new FileStream(@"../../../Data/HTTP Succinctly.pdf", FileMode.OpenOrCreate);
#else
            docStream = new FileStream(@"../../Data/HTTP Succinctly.pdf", FileMode.OpenOrCreate);
#endif
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
