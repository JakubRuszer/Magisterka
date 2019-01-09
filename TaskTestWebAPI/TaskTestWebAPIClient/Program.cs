using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace TaskTestWebAPIClient
{
    class Program
    {
        private static string webApiUrl = String.Empty;
        private static string fileToUpload = String.Empty;
        private static string mode = String.Empty;
        private static int numberOfConcurrentRequest = 20;
        private static string filesUploadAsyncUri = "http://localhost:81/TaskTestWebAPI/api/FilesUploadAsync";
        private static string filesUploadSyncUri = "http://localhost:81/TaskTestWebAPI/api/FilesUploadSync";

        static void Main(string[] args)
        {
            if (args.Length != 4)
            {
                Console.WriteLine("Incorrect program invocation command, please try the following one: TaskTestWebAPIClient web_api_url path_to_file mode numberOfConcurrentRequest ");
                Console.WriteLine("where:");
                Console.WriteLine("\tweb_api_url parameter is for example \"http://localhost:81/TaskTestWebAPI/api/FilesUploadAsync\"");
                Console.WriteLine("\tmode is one of the following: sync, async, asyncWithContinuation where sync is default");
                Console.WriteLine("\tnumberOfConcurrentRequest which is integer value from 1 to 100 with 20 default");
                return;
            }

            Program.webApiUrl = args[0];
            Program.fileToUpload = args[1];
            Program.mode = (args[2] == "sync" || args[2] == "async") ? args[2] : "sync";
            if (Int32.TryParse(args[3], out Program.numberOfConcurrentRequest))
            {
                if ((Program.numberOfConcurrentRequest < 1) || (Program.numberOfConcurrentRequest > 100))
                    Program.numberOfConcurrentRequest = 20;
            }

            HttpClient httpClient = CreateHttpClientInstance(/*webApiUrl*/);
            switch (Program.mode)
            {
                case "sync":
                    ProcessWithSyncService(httpClient);
                    break;
                case "async":
                    ProcessWithAsyncService(httpClient);
                    break;
            }

            Console.WriteLine("Press any key to finish");
            Console.ReadKey();
            if (httpClient != null)
            {
                httpClient.Dispose();
                httpClient = null;
            }
        }

        private static HttpClient CreateHttpClientInstance(/*string webApiUrl*/)
        {
            HttpClient httpClient = null;
            try
            {
                httpClient = new HttpClient();
                //httpClient.BaseAddress = new Uri(webApiUrl);
                httpClient.Timeout = Timeout.InfiniteTimeSpan;
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/plain"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return httpClient;
        }

        private static void ProcessWithSyncService(HttpClient httpClient)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task[] tabOfTasks = new Task[Program.numberOfConcurrentRequest];
            for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                tabOfTasks[i] = Program.SendFileToWebAPI(Program.fileToUpload, httpClient, Program.filesUploadSyncUri);

            for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                tabOfTasks[i].Wait();

            stopwatch.Stop();
            Console.WriteLine("Processing with SYNC service took {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static void ProcessWithAsyncService(HttpClient httpClient)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task[] tabOfTasks = new Task[Program.numberOfConcurrentRequest];
            for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                tabOfTasks[i] = Program.SendFileToWebAPI(Program.fileToUpload, httpClient, Program.filesUploadAsyncUri);

            for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                tabOfTasks[i].Wait();

            stopwatch.Stop();
            Console.WriteLine("Processing with ASYNC service took {0} ms", stopwatch.ElapsedMilliseconds);
        }

        private static async Task SendFileToWebAPI(string file, HttpClient httpClient, string webApiUrl)
        {
            if (httpClient != null)
            {
                MultipartFormDataContent content = null;
                FileStream fs = null;

                try
                {
                    fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read);

                    /*
                    Treść requestu HTTP typu multiparted post zawierająca przesyłany plik
                    -----------------------------265001916915724
                    Content-Disposition: form-data; name="somefile"; filename="total2-5_Deployment_v2_WithBEGroup.pdf"
                    Content-Type: application/pdf
                    */
                    StreamContent streamContent = new StreamContent(fs);
                    streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                    streamContent.Headers.ContentDisposition.Name = "somefile";
                    streamContent.Headers.ContentDisposition.FileName = file.Substring(file.LastIndexOf("\\") + 1);
                    streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                    content = new MultipartFormDataContent();
                    content.Add(streamContent);
                    await httpClient.PostAsync(webApiUrl, content);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
                finally
                {
                    if (fs != null)
                        fs.Close();

                    if (content != null)
                        content.Dispose();
                }
            }
        }
    }
}
