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

namespace TaskTestWebAPI.Controllers
{
    public class FilesUploadAsyncController : ApiController
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

                string httpPostedFileTargetPathDir = HttpContext.Current.Server.MapPath("~/" + FilesUploadAsyncController.destinationFolderName);
                string httpPostedFileTargetPath = httpPostedFileTargetPathDir + "\\" + httpPostedFileName;
                Stream inputStream = httpPostedFile.InputStream;
                FileStream fs = null;
                try
                {
                    if (!Directory.Exists(httpPostedFileTargetPathDir))
                        Directory.CreateDirectory(httpPostedFileTargetPathDir);

                    fs = new FileStream(httpPostedFileTargetPath, FileMode.Create);
                    byte[] buffer = new byte[1048576];
                    int numberOfBytesRead = 0;
                    while ((numberOfBytesRead = await inputStream.ReadAsync(buffer, 0, 1048576)) != 0)
                        await fs.WriteAsync(buffer, 0, numberOfBytesRead);

                    await fs.FlushAsync();
                    
                    result.Add(new FileUploadedInfo()
                    {
                        Path = httpPostedFileName,
                        Size = (int)new FileInfo(httpPostedFileTargetPath).Length
                    });
                }
                catch (Exception ex)
                { }
                finally
                {
                    if (fs != null)
                        fs.Close();
                }
            }

            return result;
        }
    }
}
