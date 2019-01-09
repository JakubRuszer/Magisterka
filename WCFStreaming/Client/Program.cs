using Client.StreamingService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static string pathFile = string.Empty;
        private static int numberOfConcurrentRequest = 20;

        //parametr ściażka
        //parametr ilość 
        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect program invocation command, please try the following one:  ");
                return;
            }

            Program.pathFile = args[0];
            
            if (Int32.TryParse(args[1], out Program.numberOfConcurrentRequest))
            {
                if ((Program.numberOfConcurrentRequest < 1) || (Program.numberOfConcurrentRequest > 100))
                    Program.numberOfConcurrentRequest = 20;
            }
            ProcesWithSyncStreamingService(pathFile, Program.numberOfConcurrentRequest);

        }

        private static void ProcesWithSyncStreamingService(string pathFile, int numberOfConcurrent)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                 Program.UploadFileToService(pathFile, numberOfConcurrent);

            stopwatch.Stop();
            Console.WriteLine("WCF ... {0} ms", stopwatch.ElapsedMilliseconds);
        }

        public static void UploadFileToService(string filePath, int numberOfConcurrent)
        {
            if(filePath != string.Empty)
            {
                UploadFileRequest request = new UploadFileRequest();
                UploadFileRequestHeader requestHeader = new UploadFileRequestHeader();
               // UploadFileResponse response ;

                requestHeader.FileName = filePath.Substring(filePath.LastIndexOf("\\")+1);
                request.uploadFileRequestHeader = requestHeader;

                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    request.File = fs;
                    using (ServiceClient sc = new ServiceClient())
                    {
                        sc.UploadFileCompleted += Sc_UploadFileCompleted;
                        sc.UploadFileAsync(request);
                    }
                }
            }
        }

        private static void Sc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            if (e.Result.uploadFileResponseHeader.Result == 1)
            {
                Console.Out.WriteLine("Plik wysłano!!!");
            }
            else
            {
                Console.Out.WriteLine(e.Result.uploadFileResponseHeader.Message);
            }
        }
    }
}
