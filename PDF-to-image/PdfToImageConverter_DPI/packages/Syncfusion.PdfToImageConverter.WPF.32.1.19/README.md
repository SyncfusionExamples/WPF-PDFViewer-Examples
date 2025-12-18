### Syncfusion® WPF PDF To Image Converter

The Syncfusion® [WPF Pdf To Image converter](https://help.syncfusion.com/file-formats/pdf-to-image/convert-pdf-file-to-image-in-wpf?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) package use PDFium to convert the PDF to image without opening the document in the PDF Viewer control. 

![WPF Pdf To Image converter](https://cdn.syncfusion.com/intranet/images/PdfToImageConverter.png)

[Docs](https://help.syncfusion.com/file-formats/pdf-to-image/convert-pdf-file-to-image-in-wpf?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | [API Reference](https://help.syncfusion.com/cr/wpf/Syncfusion.PdfToImageConverter.PdfToImageConverter.html#Syncfusion_PdfToImageConverter_PdfToImageConverter__ctor?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | [Blogs](https://www.syncfusion.com/blogs/?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | [Support](https://support.syncfusion.com/support/tickets/create?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | [Forums](https://www.syncfusion.com/forums/wpf?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | [Feedback](https://www.syncfusion.com/feedback/wpf?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget)

### Key Features

* Converts [PDF into image.](https://help.syncfusion.com/file-formats/pdf-to-image/converting-pdf-pages-into-images-in-wpf)

### System Requirements

*	[System Requirements](https://help.syncfusion.com/file-formats/installation-and-upgrade/system-requirements?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget).

### Getting started

You can fetch the Syncfusion® WPF Pdf to Image converter library NuGet by simply running the command **Install-Package Syncfusion.PdfToImageConverter.Wpf** from the Package Manager Console in Visual Studio.

Try the following code snippet to convert an PDF to Image.

```csharp
//Initialize PDF to Image converter.
PdfToImageConverter imageConverter = new PdfToImageConverter();
//Load the PDF document as a stream
FileStream inputStream = new FileStream("File Path goes here", FileMode.Open, FileAccess.ReadWrite);
imageConverter.Load(inputStream);
//Convert PDF to Image.
Stream[] outputStream = imageConvertor.Convert(0,imageConverter.PageCount-1,false,false);
for(int i=0; i < outputStream.Length; i++)
{
    Bitmap image = new Bitmap(outputStream[i]);
    image.Save("sample-"+i+".png");
}
```

For more information to get started, refer to our [Getting Started Documentation page](https://help.syncfusion.com/file-formats/pdf-to-image/convert-pdf-file-to-image-in-wpf?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget).

### License

This is a commercial product and requires a paid license for possession or use. Syncfusion® licensed software, including this component, is subject to the terms and conditions of [Syncfusion® EULA](https://www.syncfusion.com/eula/es/?utm_source=nuget&utm_medium=listing&utm_campaign=aspnetcore-pdf-nuget). You can purchase a license [here](https://www.syncfusion.com/sales/products?utm_source=nuget&utm_medium=listing&utm_campaign=aspnetcore-pdf-nuget) or start a free 30-day trial [here](https://www.syncfusion.com/account/manage-trials/start-trials?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget).

### About Syncfusion®

Founded in 2001 and headquartered in Research Triangle Park, N.C., Syncfusion® has more than 29,000 customers and more than 1 million users, including large financial institutions, Fortune 500 companies, and global IT consultancies.

Today, we provide 1800+ components and frameworks for web ([Blazor](https://www.syncfusion.com/blazor-components?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Flutter](https://www.syncfusion.com/flutter-widgets?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [ASP.NET Core](https://www.syncfusion.com/aspnet-core-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [ASP.NET MVC](https://www.syncfusion.com/aspnet-mvc-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [ASP.NET Web Forms](https://www.syncfusion.com/jquery/aspnet-webforms-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [JavaScript](https://www.syncfusion.com/javascript-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Angular](https://www.syncfusion.com/angular-ui-components?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [React](https://www.syncfusion.com/react-ui-components?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Vue](https://www.syncfusion.com/vue-ui-components?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), and [jQuery](https://www.syncfusion.com/jquery-ui-widgets?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget)), mobile ([.NET MAUI](https://www.syncfusion.com/maui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Flutter](https://www.syncfusion.com/flutter-widgets?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Xamarin](https://www.syncfusion.com/xamarin-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [UWP](https://www.syncfusion.com/uwp-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), and [JavaScript](https://www.syncfusion.com/javascript-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget)), and desktop development ([WinForms](https://www.syncfusion.com/winforms-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [WPF](https://www.syncfusion.com/wpf-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [WinUI](https://www.syncfusion.com/winui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [.NET MAUI (Preview)](https://www.syncfusion.com/maui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), [Flutter](https://www.syncfusion.com/flutter-widgets?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget),[Xamarin](https://www.syncfusion.com/xamarin-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget), and [UWP](https://www.syncfusion.com/uwp-ui-controls?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget)). We provide ready-to-deploy enterprise software for dashboards, reports, data integration, and big data processing. Many customers have saved millions in licensing fees by deploying our software.

[sales@syncfusion.com](mailto:sales@syncfusion.com?Subject=Syncfusion%20ASPNET%20Core%20DocIO%20-%20NuGet) | [www.syncfusion.com](https://www.syncfusion.com?utm_source=nuget&utm_medium=listing&utm_campaign=wpf-pdftoimageconverter-nuget) | Toll Free: 1-888-9 DOTNET
