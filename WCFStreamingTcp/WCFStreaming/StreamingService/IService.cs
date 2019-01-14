using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace StreamingService
{
    [ServiceContract]
    public interface IService
    {
        [OperationContract]
        DownloadFileResponse DownloadFile(DownloadFileRequest request);

        [OperationContract]
        UploadFileResponse UploadFile(UploadFileRequest request);
    }

    #region DownloadFile Contracts

    [MessageContract]
    public class DownloadFileRequest
    {
        [MessageHeader]
        public DownloadFileRequestHeader downloadFileRequestHeader;
    }

    [DataContract]
    public class DownloadFileRequestHeader
    {
        [DataMember]
        public string FileName;
    }

    [MessageContract]
    public class DownloadFileResponse : IDisposable
    {
        [MessageHeader]
        public DownloadFileResponseHeader downloadFileResponseHeader;

        [MessageBodyMember]
        public Stream File;

        public void Dispose()
        {
            if (File != null)
            {
                File.Close();
                File = null;
            }
        }
    }

    [DataContract]
    public class DownloadFileResponseHeader
    {
        [DataMember]
        public int Result;

        [DataMember]
        public string Message;
    }

    #endregion

    #region UploadFile Contracts

    [MessageContract]
    public class UploadFileRequest
    {
        [MessageHeader]
        public UploadFileRequestHeader uploadFileRequestHeader;

        [MessageBodyMember]
        public Stream File;
    }

    [DataContract]
    public class UploadFileRequestHeader
    {
        [DataMember]
        public string FileName;
    }

    [MessageContract]
    public class UploadFileResponse
    {
        [MessageHeader]
        public UploadFileResponseHeader uploadFileResponseHeader;
    }

    [DataContract]
    public class UploadFileResponseHeader
    {
        [DataMember]
        public int Result;

        [DataMember]
        public string Message;
    }

    #endregion
}
