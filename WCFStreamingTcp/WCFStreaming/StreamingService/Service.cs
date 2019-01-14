using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace StreamingService
{
    public class Service : IService
    {
        public static int counter = 0;
        private string destinationFolderName = "UploadsFile";
        public DownloadFileResponse DownloadFile(DownloadFileRequest request)
        {
            int result = 1;
            string message = "";
            DownloadFileResponse response = new DownloadFileResponse();
            DownloadFileResponseHeader responseHeader = new DownloadFileResponseHeader();

            string filePath = ConfigurationManager.AppSettings["FilePath"].ToString() +
                request.downloadFileRequestHeader.FileName;

            if (File.Exists(filePath))
                response.File = File.OpenRead(filePath);
            else
            {
                response.File = Stream.Null;
                result = 0;
                message = "Nie odnaleziono pliku!";
            }

            responseHeader.Result = result;
            responseHeader.Message = message;
            response.downloadFileResponseHeader = responseHeader;

            return response;
        }

        public UploadFileResponse UploadFile(UploadFileRequest request)
        {
            int result = 1;
            string message = "";
            UploadFileResponse response = new UploadFileResponse();
            UploadFileResponseHeader responseHeader = new UploadFileResponseHeader();

            string fileName = request.uploadFileRequestHeader.FileName;
            string SendFileName = (fileName.LastIndexOf("\\") >= 0) ? fileName.Substring(fileName.LastIndexOf("\\") + 1) : fileName;
            int pos = fileName.LastIndexOf(".");

            if (pos >= 0)
            {
                SendFileName = SendFileName.Insert(pos, String.Format("_{0}", Interlocked.Increment(ref Service.counter)));
            }
            else
            {
                SendFileName += String.Format("_{0}", Interlocked.Increment(ref Service.counter));
            }

            if (string.IsNullOrWhiteSpace(fileName))
            {
                result = 0;
                message = "Nie podano nazwy pliku!";
            }
            else
            {
                string fileTargetPathDir = "~/" + destinationFolderName;
                string postedFileTargetPath = fileTargetPathDir + "\\" + SendFileName;

                try
                {
                    if (!Directory.Exists(fileTargetPathDir))
                        Directory.CreateDirectory(fileTargetPathDir);

                    //string filePath = @"C:\Streaming\Data\" + fileName;
                    using (FileStream fs = new FileStream(postedFileTargetPath, FileMode.Create))
                    {
                        int bufferSize = 1048576;
                        byte[] buffer = new byte[bufferSize];
                        int bytes;

                        while ((bytes = request.File.Read(buffer, 0, bufferSize)) > 0)
                        {
                            fs.Write(buffer, 0, bytes);
                            fs.Flush();
                        }
                    }
                }
                catch (Exception e)
                {
                    result = 0;
                    message = e.Message;
                }
            }

            responseHeader.Result = result;
            responseHeader.Message = message;
            response.uploadFileResponseHeader = responseHeader;

            return response;
        }
    }
}
