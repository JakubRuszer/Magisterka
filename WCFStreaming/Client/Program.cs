using Client.StreamingService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        private static string pathFile = string.Empty;
        private static int numberOfConcurrentRequest = 20;
        private static int counter = 0;
        private static Mutex mtx = new Mutex();
        private static AutoResetEvent ars = new AutoResetEvent(false);

        static void Main(string[] args)
        {

            if (args.Length != 2)
            {
                Console.WriteLine("Incorrect program invocation command, please try the following one: path_to_file numberOfConcurrentRequest");
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

            using (ServiceClient sc = new ServiceClient())
            {
                sc.UploadFileCompleted += Sc_UploadFileCompleted;
                for (int i = 0; i < Program.numberOfConcurrentRequest; i++)
                    Program.UploadFileToService(pathFile, sc);

                Program.ars.WaitOne();
                stopwatch.Stop();
            }

            Console.WriteLine("WCF ... {0} ms", stopwatch.ElapsedMilliseconds);
        }

        public static void UploadFileToService(string filePath, ServiceClient sc)
        {
            if (filePath != string.Empty)
            {
                UploadFileRequest request = new UploadFileRequest();
                UploadFileRequestHeader requestHeader = new UploadFileRequestHeader();

                requestHeader.FileName = filePath.Substring(filePath.LastIndexOf("\\") + 1);
                request.uploadFileRequestHeader = requestHeader;

                FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                request.File = fs;

                //Program.mtx.WaitOne();
                //Program.counter++;
                //Program.mtx.ReleaseMutex();
                Interlocked.Increment(ref Program.counter);
                sc.UploadFileAsync(request, fs);
            }
        }

        private static void Sc_UploadFileCompleted(object sender, UploadFileCompletedEventArgs e)
        {
            //Program.mtx.WaitOne();
            //Program.counter--;
            //if (Program.counter == 0)
            //    Program.ars.Set();
            //Program.mtx.ReleaseMutex();
            if ((e.UserState != null) && ((e.UserState as FileStream) != null))
                ((FileStream)e.UserState).Close();

            int counterLocalCopy = Interlocked.Decrement(ref Program.counter);
            if (counterLocalCopy == 0)
                Program.ars.Set();
        }
    }
}
