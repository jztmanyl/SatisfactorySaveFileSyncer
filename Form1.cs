using Skogstekniskt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveFileSync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static string FTP_URL = "";
        private static string FTP_USERNAME = "";
        private static string FTP_PASSWORD = "";
        private static NetworkCredential FTP_CREDENTIALS = new NetworkCredential(FTP_USERNAME, FTP_PASSWORD);

        private static string SERVER_NAME = "asd"; //implement this into UI
        private static string BASE_PATH = $@"{Environment.GetEnvironmentVariable("LocalAppData")}\FactoryGame\Saved\SaveGames\";
        private static string FILE_NAME = "";
        private static long FILE_CREATION_TIME = 1;
        private static List<string> FILE_LIST;
        void Log(string text)
        {
            logbox.Text += $"[{DateTime.Now.ToString("h:mm:sstt")}] {text}\n";
        }
        void ListOnlineFiles()
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(FTP_URL);
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                request.Credentials = FTP_CREDENTIALS;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream);
                string names = reader.ReadToEnd();

                reader.Close();
                response.Close();


                FILE_LIST = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
        void FindDirectory()
        {
            try
            {
                var directories = Directory.GetDirectories(BASE_PATH);
                if (directories.Length > 2)
                {
                    Log("More than 2 directories in SaveGames..");
                    return;
                }
                foreach (var dir in directories)
                {
                    var file_info = new FileInfo(dir);
                    if (file_info.Name == "common")
                        continue;
                    BASE_PATH = dir;
                }
                Log($"Got directory: {BASE_PATH}");
                SearchPath.Text = BASE_PATH;
            }
            catch (Exception)
            {

            }
        }
        void FindFile()
        {
            try
            {
                var files = Directory.GetFiles(BASE_PATH);
                foreach (var file in files)
                {
                    var file_info = new FileInfo(file);
                    var file_name = file_info.Name;
                    if (!file_name.StartsWith(SERVER_NAME))
                        continue;

                    var creation_time = file_info.LastWriteTime.Ticks;
                    if (creation_time > FILE_CREATION_TIME)
                    {
                        FILE_CREATION_TIME = creation_time;
                        FILE_NAME = file_name;
                    }
                }
                Log($"Found file: {FILE_NAME}");

                if (CheckOnline())
                    DownloadFile();
                else
                    UploadFile();
            }
            catch (Exception)
            {

            }
        }
        bool CheckOnline()
        {
            ListOnlineFiles();
            long temp_creation_time = 1;
            foreach (var file in FILE_LIST)
            {
                var file_info = new FileInfo(file);
                var file_name = file_info.Name;
                if (!file_name.StartsWith(SERVER_NAME))
                    continue;
                if (!file_name.Contains(".sync"))
                    continue;

                var creation_time = Convert.ToInt64(file_name.Replace($"{SERVER_NAME}_", "").Replace(".sync.sav", ""));
                if (creation_time > temp_creation_time)
                    temp_creation_time = creation_time;
            }
            if (temp_creation_time > FILE_CREATION_TIME)
            {
                FILE_CREATION_TIME = temp_creation_time;
                return true;
            }
            return false;

        }
        void DownloadFile()
        {
            var new_file = $"{SERVER_NAME}_{FILE_CREATION_TIME}.sync.sav";
            var file_name = Path.Combine(BASE_PATH, new_file);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{FTP_URL}/{new_file}");

            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = FTP_CREDENTIALS;

            using (Stream fileStream = File.Create(file_name))
            using (Stream requestStream = request.GetRequestStream())
            {
                requestStream.CopyTo(fileStream);
            }

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                Log($"Download Complete, status {response.StatusDescription}");

        }
        void UploadFile()
        {
            var new_file = $"{SERVER_NAME}_{FILE_CREATION_TIME}.sync.sav";
            var file_name = Path.Combine(BASE_PATH, FILE_NAME);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"{FTP_URL}/{new_file}");

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = FTP_CREDENTIALS;

            using (Stream fileStream = File.OpenRead(file_name))
            using (Stream requestStream = request.GetRequestStream())
            {
                fileStream.CopyTo(requestStream);
            }
            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                Log($"Upload File Complete, status {response.StatusDescription}");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            FindFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SearchPath.Text = BASE_PATH;
            FindDirectory();
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderSelectDialog
            {
                InitialDirectory = BASE_PATH,
                Title = "Select a folder to import save file from"
            };
            if (dialog.Show(Handle))
            {
                SearchPath.Text = dialog.FileName;
                BASE_PATH = dialog.FileName;
                Log($"Got directory: {BASE_PATH}");
            }
        }
    }
}
