using Syncfusion.PdfToImageConverter;
using SkiaSharp;


PdfToImageConverter imageConverter = new PdfToImageConverter();

using (FileStream inputStream = new FileStream("../../../Input.pdf",
    FileMode.Open, FileAccess.Read))
{
    imageConverter.Load(inputStream);

    int pageCount = imageConverter.PageCount;

    for (int i = 0; i < pageCount; i++)
    {
        using (Stream stream = imageConverter.Convert(i, false, false))
        {
            stream.Position = 0;
            using (SKBitmap originalBitmap = SKBitmap.Decode(stream))
            {
                int cropX = 400;
                int cropY = 600;
                int cropWidth = 400;
                int cropHeight = 300;
                SKRectI cropRect = new SKRectI(cropX, cropY, cropX + cropWidth, cropY + cropHeight);

                if (cropRect.Right > originalBitmap.Width ||
                    cropRect.Bottom > originalBitmap.Height)
                {
                    Console.WriteLine($"Invalid crop area on page {i + 1}");
                    continue;
                }

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
                        string outputPath = $"Cropped_Page_{i + 1}.png";

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
}

Console.WriteLine("Cropping completed successfully!");