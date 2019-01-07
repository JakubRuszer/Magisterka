using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StreamingService
{
    public class Service : IService
    {
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

            if (string.IsNullOrWhiteSpace(fileName))
            {
                result = 0;
                message = "Nie podano nazwy pliku!";
            }
            else
            {
                try
                {
                    string filePath = @"C:\Streaming\Data\" + fileName;
                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
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
