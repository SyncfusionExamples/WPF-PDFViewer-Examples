using Syncfusion.PdfToImageConverter;
using SkiaSharp;


PdfToImageConverter imageConverter = new PdfToImageConverter();

using (FileStream inputStream = new FileStream("../../../Input.pdf",FileMode.Open, FileAccess.Read))
{
    imageConverter.Load(inputStream);
    int pageCount = imageConverter.PageCount;
        using (Stream stream = imageConverter.Convert(0, false, false))
        {
            stream.Position = 0;
            using (SKBitmap originalBitmap = SKBitmap.Decode(stream))
            {
                int cropX = 400;
                int cropY = 600;
                int cropWidth = 400;
                int cropHeight = 300;
                SKRectI cropRect = new SKRectI(cropX, cropY, cropX + cropWidth, cropY + cropHeight);
                using (SKBitmap croppedBitmap = new SKBitmap(cropWidth, cropHeight))
                {
                    using (SKCanvas canvas = new SKCanvas(croppedBitmap))
                    {
                        canvas.DrawBitmap(
                            originalBitmap,
                            cropRect,
                            new SKRect(0, 0, cropWidth, cropHeight));
                    }

                    using (SKImage image = SKImage.FromBitmap(croppedBitmap))
                    using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                    {
                        string outputPath = $"Cropped.png";

                        using (FileStream fs = File.OpenWrite(outputPath))
                        {
                            data.SaveTo(fs);
                        }

                        Console.WriteLine($"Saved: {outputPath}");
                    }
                }
            }
        }
}
Console.WriteLine("Cropping completed successfully!");