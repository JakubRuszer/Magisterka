﻿using System;
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
    public class FilesUploadSyncController : ApiController
    {
        private static string destinationFolderName = "uploads";
        [HttpPost]
        public List<FileUploadedInfo> UploadSingleFile()
        {
            List<FileUploadedInfo> result = new List<FileUploadedInfo>();
            var httpRequest = HttpContext.Current.Request;
            for (int i = 0; i < httpRequest.Files.Count; i++)
            {
                HttpPostedFile httpPostedFile = httpRequest.Files[i];
                string httpPostedFileName = (httpPostedFile.FileName.LastIndexOf("\\") >= 0) 
                    ? httpPostedFile.FileName.Substring(httpPostedFile.FileName.LastIndexOf("\\") + 1)
                    : httpPostedFile.FileName;
                int pos = httpPostedFileName.LastIndexOf(".");
                if (pos >= 0)
                {
                    httpPostedFileName = httpPostedFileName
                        .Insert(pos, String.Format("_{0}", Interlocked.Increment(ref FilesUploadAsyncController.counter)));
                }
                else
                {
                    httpPostedFileName += String.Format("_{0}", Interlocked.Increment(ref FilesUploadAsyncController.counter));
                }
                string httpPostedFileTargetPathDir = HttpContext.Current.Server.MapPath("~/" + FilesUploadSyncController.destinationFolderName);
                string httpPostedFileTargetPath = httpPostedFileTargetPathDir + "\\" + httpPostedFileName;
                Stream inputStream = httpPostedFile.InputStream;
                FileStream fs = null;
                try
                {
                    if (!Directory.Exists(httpPostedFileTargetPathDir))
                        Directory.CreateDirectory(httpPostedFileTargetPathDir);
                    // na kliencie webApi aktualny sposób komunikowania się jest przez hhttp client i ona do wysąłnia ma tylko metode asynk z
                    fs = new FileStream(httpPostedFileTargetPath, FileMode.Create);
                    byte[] buffer = new byte[1048576];
                    int numberOfBytesRead = 0;
                    while ((numberOfBytesRead = inputStream.Read(buffer, 0, 1048576)) != 0)
                        fs.Write(buffer, 0, numberOfBytesRead);

                    fs.Flush();

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
