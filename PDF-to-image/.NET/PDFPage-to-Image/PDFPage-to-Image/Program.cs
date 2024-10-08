﻿using Syncfusion.PdfToImageConverter;
using System.IO;


namespace NET
{
    class Programe
    {
        public static void Main(string[] args)
        {
            //Initialize PDF to Image converter.
            PdfToImageConverter imageConverter = new PdfToImageConverter();

            //Load the PDF document as a stream
            FileStream inputStream = new FileStream(Path.GetFullPath(@"Data/Input.pdf"), FileMode.Open, FileAccess.ReadWrite);

            imageConverter.Load(inputStream);

            //Convert PDF to Image.
            Stream outputStream = imageConverter.Convert(0, false, false);

            //Rewind the stream position to the beginning before copying.
            outputStream.Position = 0;

            //Create file stream.
            using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"Output/Output.jpeg"), FileMode.Create, FileAccess.ReadWrite))
            {
                //Save the image to file stream.
                outputStream.CopyTo(outputFileStream);
            }
            //Dispose the imageConverter
            imageConverter.Dispose();
        }
    }
}
