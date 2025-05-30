using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddButtonsInVerticalToolbar
{
    internal class PdfReport : INotifyPropertyChanged
    {
        string filePath;
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
#if NETCOREAPP
            filePath = @"../../../Data/PDF_Succinctly.pdf";
#else
            filePath = @"../../Data/PDF_Succinctly.pdf";
#endif
            //Load the stream from the local system.
            docStream = new FileStream(filePath, FileMode.OpenOrCreate);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }
    }
}
