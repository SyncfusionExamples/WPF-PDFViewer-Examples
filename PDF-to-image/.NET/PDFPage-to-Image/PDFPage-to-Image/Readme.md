# Create or Generate images from PDF in ASP.NET Core

## Steps to create images from PDF in ASP.NET Core

Step 1: Create a new .NET Console application.

Step 2: Install the [Syncfusion.PdfToImageConverter.Net](https://www.nuget.org/packages/Syncfusion.PdfToImageConverter.Net/) [NuGet package]

Step 3: Add the Data folder and Output folder  parallel to the Program.cs file.Data folder will hold the PDF files that you want to convert.Output Folder will store the resulting image files after conversion.

Step 4: Add the following namespaces and code in application Programe.cs file.

```
using Syncfusion.PdfToImageConverter;
using System.IO;

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
```
Step 5 : Run the application, and the images will be saved in the Output folder.