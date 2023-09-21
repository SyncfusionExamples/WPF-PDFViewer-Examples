using PDF_to_Image_MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows;
using Syncfusion.PdfToImageConverter;

namespace PDF_to_Image_MVC
{
    public partial class UploadPDF : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PDFToImage_Click(object sender, EventArgs e)
        {
            if (pdfFile != null && pdfFile.Value != "")
            {
                // Get the uploaded file
                HttpPostedFile uploadedFile = pdfFile.PostedFile;

                // Check if it's a PDF file
                if (Path.GetExtension(uploadedFile.FileName).Equals(".pdf", StringComparison.OrdinalIgnoreCase))
                {
                    // Save or process the PDF file
                    string filePath = Server.MapPath("~/" + "Sample.pdf");
                    uploadedFile.SaveAs(filePath);
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
                        if (!Directory.Exists(Server.MapPath("~/PdfToImage/")))
                        {
                            Directory.CreateDirectory(Server.MapPath("~/PdfToImage/"));
                        }
                        foreach (Stream stream in outputStream)
                        {
                            if (stream != null)
                            {
                                Bitmap bitmap = new Bitmap(stream);
                                bitmap.Save(Server.MapPath("~/PdfToImage/Image"+ Guid.NewGuid().ToString() + ".png"), ImageFormat.Png);
                            }
                        }
                        imageConverter.Dispose();
                        if (MessageBox.Show("Do you want to view the converted image?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                        {
                            //Launching the PDF file using the default Application.[Acrobat Reader]
                            System.Diagnostics.Process process = new System.Diagnostics.Process();
                            process.StartInfo = new System.Diagnostics.ProcessStartInfo(Server.MapPath("~/PdfToImage/"));
                            process.Start();
                        }
                    }
                    // You can perform further processing with the PDF file here
                }
            }
        }
    }
}