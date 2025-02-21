using Syncfusion.PdfToImageConverter;

namespace Multithreading_using_tasks
{
    class MultiThreading
    {
        //Indicates the number of threads to be create.
        private const int TaskCount = 1000;
        public static async Task Main()
        {
            //Create an array of tasks based on the TaskCount.
            Task[] tasks = new Task[TaskCount];
            for (int i = 0; i < TaskCount; i++)
            {
                tasks[i] = Task.Run(() => OpenPDFAndSaveImage());
            }
            //Ensure all tasks complete by waiting on each task.
            await Task.WhenAll(tasks);
        }

        //Open a PDF document and save image using multi-threading.
        static void OpenPDFAndSaveImage()
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
                    using (FileStream outputFileStream = new FileStream(Path.GetFullPath(@"Output/Output" + Guid.NewGuid().ToString() + ".jpeg"), FileMode.Create, FileAccess.ReadWrite))
                    {
                        //Save the image to file stream.
                        outputStream.CopyTo(outputFileStream);
                    }
                }
            }
        }
    }
}
