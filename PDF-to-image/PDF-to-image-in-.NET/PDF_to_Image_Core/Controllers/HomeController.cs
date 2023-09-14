using Microsoft.AspNetCore.Mvc;
using PDF_to_Image_Core.Models;
using System.Diagnostics;
using Syncfusion.PdfToImageConverter;
using Microsoft.AspNetCore.Hosting.Server;

namespace PDF_to_Image_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _webEnvironment;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment webEnvironment)
        {
            _logger = logger;
            _webEnvironment = webEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ConvertToImage(MyPdfUploadViewModel model)
        {
            if (model.PdfFile != null && model.PdfFile.Length > 0)
            {
                string filePath = Path.Combine(_webEnvironment.WebRootPath, "Sample.pdf");

                using (var stream = new FileStream(filePath, FileMode.OpenOrCreate))
                {
                    await model.PdfFile.CopyToAsync(stream);
                    PdfToImageConverter pdfToImageConverter = new PdfToImageConverter();
                    pdfToImageConverter.Load(stream);
                    Stream imageStream = pdfToImageConverter.Convert(0, false, false);
                    imageStream.Position = 0;
                    FileStreamResult fileStreamResult = new FileStreamResult(imageStream, "image/png");
                    fileStreamResult.FileDownloadName = "Sample.png";
                    return fileStreamResult;
                }

                return RedirectToAction("Index"); // Redirect to the upload page
            }
            else
            {
                // Handle invalid file or no file selected
                ViewBag.Message = "Please select a valid PDF file.";
                return View("Index");
            }
        }
        private IActionResult File(object value, object png, string v)
        {
            throw new NotImplementedException();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}