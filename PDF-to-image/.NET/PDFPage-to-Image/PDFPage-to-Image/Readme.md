# Create or Generate images from PDF in ASP.NET Core

## Steps to create images from PDF in ASP.NET Core

Step 1: Create a new C# ASP.NET Core Web Application project.

Step 2: Select Web Application pattern (Model-View-Controller) for the project.

Step 3: Install the [Syncfusion.Pdf.Net.Core](https://www.nuget.org/packages/Syncfusion.PdfToImageConverter.Net/) [NuGet package](https://help.syncfusion.com/document-processing/nuget-packages) as reference to your ASP.NET Core applications from [NuGet.org](https://www.nuget.org/).

Step 4: A default controller with name HomeController.cs gets added on creation of ASP.NET Core project. Include the following namespaces in that HomeController.cs file.


```
using Syncfusion.PdfToImageConverter;
using System.IO;
```

Step 5: A default action method named Index will be present in HomeController.cs. Right click on Index method and select Go To View where you will be directed to its associated view page Index.cshtml. Add a new button in the Index.cshtml as shown below.

```
@{Html.BeginForm("CreateImage", "Home", FormMethod.Get);
    {
        <div>
            <input type="submit" value="Create PDF to Image" style="width:200px;height:27px" />
        </div>
    }
    Html.EndForm();
}
```

Step 6: Add a new action method named ``CreateImage`` in HomeController.cs file and include the below code example to generate a image from PDF document using the PdfToImageConverter.
```
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