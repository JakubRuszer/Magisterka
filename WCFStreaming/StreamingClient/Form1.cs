using StreamingClient.StreamingService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StreamingClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {

                String path = openFileDialog.FileName;
                string nazwa = Path.GetFileName(path);
                label1.Text = nazwa;

                using (StreamReader reader = new StreamReader(new FileStream(path, FileMode.Open), new UTF8Encoding()))
                {
                    UploadFileToService(nazwa);
                }
            }
        }
        public void DownloadFileFromService(string fileName)
        {
            DownloadFileRequest request = new DownloadFileRequest();
            DownloadFileRequestHeader requestHeader = new DownloadFileRequestHeader();

            DownloadFileResponse response;

            using (ServiceClient sc = new ServiceClient())
            {
                requestHeader.FileName = fileName;
                request.downloadFileRequestHeader = requestHeader;
                response = sc.DownloadFile(request);
            }

            if (response.downloadFileResponseHeader.Result == 1)
            {
                try
                {
                    string filePath = ConfigurationManager.AppSettings["ClientFilePath"].ToString() + fileName;

                    using (FileStream fs = new FileStream(filePath, FileMode.Create))
                    {
                        //1MB buffer
                        int bufferSize = 1048576;
                        byte[] buffer = new byte[bufferSize];
                        int bytes;

                        while ((bytes = response.File.Read(buffer, 0, bufferSize)) > 0)
                        {
                            fs.Write(buffer, 0, bytes);
                            fs.Flush();
                        }
                    }
                    MessageBox.Show("Zakończono pobieranie pliku.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            else
                MessageBox.Show(response.downloadFileResponseHeader.Message);
        }

        public void UploadFileToService(string fileName)
        {
            UploadFileRequest request = new UploadFileRequest();
            UploadFileRequestHeader requestHeader = new UploadFileRequestHeader();
            UploadFileResponse response;

            string filePath = ConfigurationManager.AppSettings["ClientFilePath"].ToString() + fileName;

            if (!File.Exists(filePath))
            {
                MessageBox.Show("Nie odnaleziono pliku!");
                return;
            }

            requestHeader.FileName = fileName;
            request.uploadFileRequestHeader = requestHeader;

            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            {
                request.File = fs;
                using (ServiceClient sc = new ServiceClient())
                {
                    response = sc.UploadFile(request);
                }
            }

            if (response.uploadFileResponseHeader.Result == 1)
            {
                MessageBox.Show("Wysłano plik!");
            }
            else
            {
                MessageBox.Show(response.uploadFileResponseHeader.Message);
            }
        }


    }
}
