using Syncfusion.PdfToImageConverter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PDF_To_Image_WF
{
    public partial class Form1 : Form
    {
        string filePath = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }

        private void ChoosePDF_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "PDF documents (*.pdf)|*.PDF";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = openFileDialog.FileName;
                if (filePath != string.Empty)
                    Convert.Enabled = true;
            }
        }

        private void Convert_Click(object sender, EventArgs e)
        {
            FileStream inputPDFStream = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite);
            PdfToImageConverter imageConverter = new PdfToImageConverter(inputPDFStream);
            Stream[] outputStream = new Stream[imageConverter.PageCount];
            // Convert all the PDF pages to images
            if (imageConverter.PageCount > 1)
            {
                outputStream = imageConverter.Convert(0, imageConverter.PageCount - 1, false, false);
            }
            else if (imageConverter.PageCount == 1)
            {
                outputStream[0] = imageConverter.Convert(0, false, false);
            }
            if (outputStream != null)
            {
                if (!Directory.Exists("PdfToImage"))
                {
                    Directory.CreateDirectory("PdfToImage");
                }
                foreach (Stream stream in outputStream)
                {
                    if (stream != null)
                    {
                        Bitmap bitmap = new Bitmap(stream);
                        bitmap.Save(@"PdfToImage\Image" + Guid.NewGuid().ToString() + ".png", ImageFormat.Png);
                    }
                }
                Convert.Enabled = false;
                imageConverter.Dispose();
                if (MessageBox.Show("Do you want to view the converted image?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    //Launching the PDF file using the default Application.[Acrobat Reader]
                    System.Diagnostics.Process process = new System.Diagnostics.Process();
                    process.StartInfo = new System.Diagnostics.ProcessStartInfo(System.IO.Directory.GetCurrentDirectory() + @"\PdfToImage\");
                    process.Start();
                }
            }
        }
    }
}
