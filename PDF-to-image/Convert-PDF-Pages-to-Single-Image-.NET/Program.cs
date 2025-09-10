using SkiaSharp;
using Syncfusion.PdfToImageConverter;
using System;
using System.IO;

namespace Convert_PDF_to_Image
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Initialize PDF to Image converter.
            PdfToImageConverter imageConverter = new PdfToImageConverter();
            //Load the PDF document as a stream
            FileStream inputStream = new FileStream("../../../Input.pdf", FileMode.Open, FileAccess.ReadWrite);
            imageConverter.Load(inputStream);
            //Convert PDF to Image.
            Stream[] imageStreams = imageConverter.Convert(0, imageConverter.PageCount - 1, false, false);
            CombineImages(imageStreams, "Output.png");

            //Dispose the image streams.
            foreach (Stream imageStream in imageStreams)
                imageStream.Dispose();
        }
        /// <summary>
        /// Combines multiple images from streams into a single image.
        /// </summary>
        /// <param name="imageStreams">Streams containing the images to be combined.</param>
        /// <param name="outputPath">Output path where the combined image will be saved.</param>
        public static void CombineImages(Stream[] imageStreams, string outputPath)
        {
            if (imageStreams == null || imageStreams.Length == 0)
                throw new ArgumentException("No images to combine.");

            // Load all images and get their dimensions
            SKBitmap[] bitmaps = new SKBitmap[imageStreams.Length];
            int maxWidth = 0;
            int totalHeight = 0;
            int margin = 20;

            for (int i = 0; i < imageStreams.Length; i++)
            {
                imageStreams[i].Position = 0;
                bitmaps[i] = SKBitmap.Decode(imageStreams[i]);
                maxWidth = Math.Max(maxWidth, bitmaps[i].Width);
                totalHeight += bitmaps[i].Height + margin;
            }

            // Add margins to the total width and height
            int combinedWidth = maxWidth + 2 * margin;
            // Add margin at the bottom
            totalHeight += margin;

            // Create a new bitmap with the combined dimensions
            using (SKBitmap combinedBitmap = new SKBitmap(combinedWidth, totalHeight))
            {
                using (SKCanvas canvas = new SKCanvas(combinedBitmap))
                {
                    // Set background color to the specified color
                    canvas.Clear(new SKColor(240, 240, 240));

                    // Draw each bitmap onto the canvas
                    int yOffset = margin;
                    for (int i = 0; i < bitmaps.Length; i++)
                    {
                        int xOffset = (combinedWidth - bitmaps[i].Width) / 2; // Center the image horizontally
                        canvas.DrawBitmap(bitmaps[i], new SKPoint(xOffset, yOffset));
                        yOffset += bitmaps[i].Height + margin; // Add margin between rows
                    }

                    // Save the combined bitmap to the output stream
                    using (SKImage image = SKImage.FromBitmap(combinedBitmap))
                    {
                        using (SKData data = image.Encode(SKEncodedImageFormat.Png, 100))
                        {
                            using (FileStream stream = File.OpenWrite(outputPath))
                                data.SaveTo(stream);
                        }
                    }
                }
            }
        }
    }
}
