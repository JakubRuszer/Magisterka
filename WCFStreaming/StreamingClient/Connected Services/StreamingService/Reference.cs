﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace StreamingClient.StreamingService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DownloadFileRequestHeader", Namespace="http://schemas.datacontract.org/2004/07/StreamingService")]
    [System.SerializableAttribute()]
    public partial class DownloadFileRequestHeader : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DownloadFileResponseHeader", Namespace="http://schemas.datacontract.org/2004/07/StreamingService")]
    [System.SerializableAttribute()]
    public partial class DownloadFileResponseHeader : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UploadFileRequestHeader", Namespace="http://schemas.datacontract.org/2004/07/StreamingService")]
    [System.SerializableAttribute()]
    public partial class UploadFileRequestHeader : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FileNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FileName {
            get {
                return this.FileNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FileNameField, value) != true)) {
                    this.FileNameField = value;
                    this.RaisePropertyChanged("FileName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="UploadFileResponseHeader", Namespace="http://schemas.datacontract.org/2004/07/StreamingService")]
    [System.SerializableAttribute()]
    public partial class UploadFileResponseHeader : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int ResultField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Result {
            get {
                return this.ResultField;
            }
            set {
                if ((this.ResultField.Equals(value) != true)) {
                    this.ResultField = value;
                    this.RaisePropertyChanged("Result");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="StreamingService.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/DownloadFile", ReplyAction="http://tempuri.org/IService/DownloadFileResponse")]
        StreamingClient.StreamingService.DownloadFileResponse DownloadFile(StreamingClient.StreamingService.DownloadFileRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/UploadFile", ReplyAction="http://tempuri.org/IService/UploadFileResponse")]
        StreamingClient.StreamingService.UploadFileResponse UploadFile(StreamingClient.StreamingService.UploadFileRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadFileRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadFileRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public StreamingClient.StreamingService.DownloadFileRequestHeader downloadFileRequestHeader;
        
        public DownloadFileRequest() {
        }
        
        public DownloadFileRequest(StreamingClient.StreamingService.DownloadFileRequestHeader downloadFileRequestHeader) {
            this.downloadFileRequestHeader = downloadFileRequestHeader;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="DownloadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class DownloadFileResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public StreamingClient.StreamingService.DownloadFileResponseHeader downloadFileResponseHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream File;
        
        public DownloadFileResponse() {
        }
        
        public DownloadFileResponse(StreamingClient.StreamingService.DownloadFileResponseHeader downloadFileResponseHeader, System.IO.Stream File) {
            this.downloadFileResponseHeader = downloadFileResponseHeader;
            this.File = File;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFileRequest", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileRequest {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public StreamingClient.StreamingService.UploadFileRequestHeader uploadFileRequestHeader;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public System.IO.Stream File;
        
        public UploadFileRequest() {
        }
        
        public UploadFileRequest(StreamingClient.StreamingService.UploadFileRequestHeader uploadFileRequestHeader, System.IO.Stream File) {
            this.uploadFileRequestHeader = uploadFileRequestHeader;
            this.File = File;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="UploadFileResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class UploadFileResponse {
        
        [System.ServiceModel.MessageHeaderAttribute(Namespace="http://tempuri.org/")]
        public StreamingClient.StreamingService.UploadFileResponseHeader uploadFileResponseHeader;
        
        public UploadFileResponse() {
        }
        
        public UploadFileResponse(StreamingClient.StreamingService.UploadFileResponseHeader uploadFileResponseHeader) {
            this.uploadFileResponseHeader = uploadFileResponseHeader;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : StreamingClient.StreamingService.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<StreamingClient.StreamingService.IService>, StreamingClient.StreamingService.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public StreamingClient.StreamingService.DownloadFileResponse DownloadFile(StreamingClient.StreamingService.DownloadFileRequest request) {
            return base.Channel.DownloadFile(request);
        }
        
        public StreamingClient.StreamingService.UploadFileResponse UploadFile(StreamingClient.StreamingService.UploadFileRequest request) {
            return base.Channel.UploadFile(request);
        }
    }
}