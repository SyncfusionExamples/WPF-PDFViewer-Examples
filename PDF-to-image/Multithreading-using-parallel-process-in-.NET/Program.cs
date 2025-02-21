using Syncfusion.PdfToImageConverter;

namespace Multithreading_using_parallel_process
{
    class MultiThreading
    {
        static void Main(string[] args)
        {
            //Indicates the number of threads to be create.
            int limit = 5;
            Console.WriteLine("Parallel For Loop");
            Parallel.For(0, limit, count =>
            {
                Console.WriteLine("Task {0} started", count);
                //Create multiple PDF document, one document on each thread.
                ConvertPdfToImage(count);
                Console.WriteLine("Task {0} is done", count);
            });
        }
        //Open and save a PDF document using multi-threading.
        static void ConvertPdfToImage(int count)
        {
            using (FileStream inputStream = new FileStream(@"Data/Input.pdf", FileMode.Open, FileAccess.Read))
            {
                //Load an existing PDF document.
                using (PdfToImageConverter imageConverter = new PdfToImageConverter(inputStream))
                {
                    Stream outputStream = imageConverter.Convert(0, false, false);

                    //Rewind the stream position to the beginning before copying.
                    outputStream.Position = 0;

                    //Create file stream.
                    using (FileStream outputFileStream = new FileStream("Output" + count + ".jpeg", FileMode.Create, FileAccess.Write))
                    {
                        //Save the image to file stream.
                        outputStream.CopyTo(outputFileStream);
                    }
                }
            }
        }
    }
}
