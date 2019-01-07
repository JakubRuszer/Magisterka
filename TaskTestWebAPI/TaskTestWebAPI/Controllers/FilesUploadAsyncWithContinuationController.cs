using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.IO;
using System.Web;
using System.Threading;
using System.Threading.Tasks;
using TaskTestWebAPI.Models;

//Setting the RequestLengthDiskThreshold(to anything, up to MaxRequestSize) in web.config resolves the problem.
//Apparently ASP.Net buffers the first chunk of the input stream, then treats the stream as closed.
//This can happen if you set MaxRequestLength (say, 1536) but let RequestLengthDiskThreshold default. 
//This apparently trips over some internal code, which makes it a Microsoft bug.Setting both values resolves the problem
namespace TaskTestWebAPI.Controllers
{
    public class FilesUploadAsyncWithContinuationController : ApiController
    {
        public static int counter = 0;
        private static string destinationFolderName = "uploads";
        [HttpPost]
        public async Task<List<FileUploadedInfo>> UploadSingleFile()
        {
            List<FileUploadedInfo> result = new List<FileUploadedInfo>();

            var httpRequest = HttpContext.Current.Request;
            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                HttpPostedFile httpPostedFile = httpRequest.Files[i];
                string httpPostedFileName = (httpPostedFile.FileName.LastIndexOf("\\") >= 0) ? httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf("\\") + 1) : httpPostedFile.FileName;
                int pos = httpPostedFileName.LastIndexOf(".");
                if (pos >= 0)
                    httpPostedFileName = httpPostedFileName.Insert(pos, String.Format("_{0}", Interlocked.Increment(ref FilesUploadAsyncController.counter)));
                else
                    httpPostedFileName += String.Format("_{0}", Interlocked.Increment(ref FilesUploadAsyncController.counter));

                string httpPostedFileTargetPathDir = HttpContext.Current.Server.MapPath("~/" + FilesUploadAsyncWithContinuationController.destinationFolderName);
                string httpPostedFileTargetPath = httpPostedFileTargetPathDir + "\\" + httpPostedFileName;
                Stream inputStream = httpPostedFile.InputStream;
                FileStream fs = null;
                try
                {
                    if (!Directory.Exists(httpPostedFileTargetPathDir))
                        Directory.CreateDirectory(httpPostedFileTargetPathDir);

                    fs = new FileStream(httpPostedFileTargetPath, FileMode.Create);
                    byte[] buffer = new byte[1048576];

                    ContextData ctxData = new ContextData()
                    {
                        Result = result,
                        FileUploadedInfo = new FileUploadedInfo() { Path = httpPostedFileTargetPath, Size = 0 },
                        InputStream = inputStream,
                        OutputStream = fs,
                        Buffer = buffer,
                    };
                    var task = inputStream.ReadAsync(buffer, 0, 1048576);
                    await task.ContinueWith(readTask => ContinueWithAsyncWrite(readTask, ctxData));
                }
                catch (Exception ex)
                { }
            }

            return result;
        }

        private void ContinueWithAsyncRead(Task writeTask, ContextData ctxData)
        {
            if (writeTask.IsCompleted)
            {
                var task = ctxData.InputStream.ReadAsync(ctxData.Buffer, 0, 1048576).ContinueWith(readTask => ContinueWithAsyncWrite(readTask, ctxData));
            }
        }

        private void ContinueWithAsyncWrite(Task<int> readTask, ContextData ctxData)
        {
            if (readTask.IsCompleted)
            {
                int numberOfBytesRead = readTask.Result;
                if (numberOfBytesRead > 0)
                {
                    var task = ctxData.OutputStream.WriteAsync(ctxData.Buffer, 0, numberOfBytesRead).ContinueWith(writeTask => ContinueWithAsyncRead(writeTask, ctxData));
                }
                else
                {
                    var task = ctxData.OutputStream.FlushAsync().ContinueWith(flushTask => {
                        ctxData.OutputStream.Close();
                        ctxData.FileUploadedInfo.Size = (int)new FileInfo(ctxData.FileUploadedInfo.Path).Length;
                        ctxData.Result.Add(ctxData.FileUploadedInfo);
                    });
                }
            }
        }
    }

    class ContextData
    {
        public List<FileUploadedInfo> Result { get; set; }
        public FileUploadedInfo FileUploadedInfo { get; set; }
        public Stream InputStream { get; set; }
        public Stream OutputStream { get; set; }
        public byte[] Buffer { get; set; }
    }
}
