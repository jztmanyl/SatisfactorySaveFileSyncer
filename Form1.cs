using Skogstekniskt;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace SaveFileSync
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        private static string FTP_URL = ""; //The FTP url need to be empty/only contain files uploaded by this program.
        private static string FTP_USERNAME = ";
        private static string FTP_PASSWORD = ""; //No encryption in this program so make sure to only share to friends.
        private static NetworkCredential FTP_CREDENTIALS = new NetworkCredential(FTP_USERNAME, FTP_PASSWORD);

        private static string SERVER_NAME = Properties.Settings.Default.SERVER_NAME;
        private static string BASE_PATH = $@"{Environment.GetEnvironmentVariable("LocalAppData")}\FactoryGame\Saved\SaveGames\";
        private static string FILE_NAME = "";
        private static long FILE_CREATION_TIME = 1;
        private static List<string> FILE_LIST;
        

        void Log(string text)
        {
            logbox.Text += $"[{DateTime.Now.ToString("h:mm:sstt")}] {text}\n";
        }

        //Find all files in the FTP directory so we later can compare them to local ones.
        void ListOnlineFiles()
        {
            try
            {
                var request = (FtpWebRequest)WebRequest.Create(FTP_URL);

                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = FTP_CREDENTIALS;

                using (var response = (FtpWebResponse)request.GetResponse())
                using (var responseStream = response.GetResponseStream())
                {
                    var reader = new StreamReader(responseStream);
                    var names = reader.ReadToEnd();
                    FILE_LIST = names.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                }
            }
            catch (Exception ex)
            {
                Log($"(101) {ex.Message}");
            }
        }

        //Find the directory in "SaveGames" that contains all the saves.
        void FindDirectory()
        {
            try
            {
                var directories = Directory.GetDirectories(BASE_PATH);
                if (directories.Length > 2)
                {
                    Log("More than 2 directories in SaveGames... You need to manually select directory.");
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
            catch (Exception ex)
            {
                Log("Couldn't find directory... Try manually selecting it.");
                Log($"(102) {ex.Message}");
            }
        }

        //Find the latest save file so we can decide wheter to upload or download.
        void FindFile()
        {
            try
            {
                var files = Directory.GetFiles(BASE_PATH);
                var kill_switch = true;
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
                        kill_switch = false;
                    }
                }

                if (kill_switch)
                {
                    Log("Couldn't find file...");
                    return;
                }

                Log($"Found file: {FILE_NAME}");

                if (CheckOnline())
                    DownloadFile();
                else
                    UploadFile();
            }
            catch (Exception ex)
            {
                Log($"(103) {ex.Message}");
            }
        }

        //Compare the latest online save file with the latest local save file.
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

        //Download the save file
        void DownloadFile()
        {
            var new_file = $"{SERVER_NAME}_{FILE_CREATION_TIME}.sync.sav";
            var file_name = Path.Combine(BASE_PATH, new_file);

            var request = (FtpWebRequest)WebRequest.Create($"{FTP_URL}/{new_file}");

            request.Method = WebRequestMethods.Ftp.DownloadFile;
            request.Credentials = FTP_CREDENTIALS;

            using (var fileStream = File.Create(file_name))
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.CopyTo(fileStream);
            }

            using (var response = (FtpWebResponse)request.GetResponse())
                Log($"Download Complete, status {response.StatusDescription}");

        }

        //Upload the save file
        void UploadFile()
        {
            var new_file = $"{SERVER_NAME}_{FILE_CREATION_TIME}.sync.sav";
            var file_name = Path.Combine(BASE_PATH, FILE_NAME);

            var request = (FtpWebRequest)WebRequest.Create($"{FTP_URL}/{new_file}");

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = FTP_CREDENTIALS;

            using (var fileStream = File.OpenRead(file_name))
            using (var requestStream = request.GetRequestStream())
            {
                fileStream.CopyTo(requestStream);
            }
            using (var response = (FtpWebResponse)request.GetResponse())
                Log($"Upload File Complete, status {response.StatusDescription}");
            
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (ServerName.Text == "")
            {
                Log("You need to set a server name...");
                return;
            }
            Properties.Settings.Default.SERVER_NAME = ServerName.Text;
            Properties.Settings.Default.Save();
            SERVER_NAME = ServerName.Text; 
            FindFile();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.SERVER_NAME != "")
                ServerName.Text = Properties.Settings.Default.SERVER_NAME;
            SearchPath.Text = BASE_PATH;
            FindDirectory();
        }

        private void FolderButton_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                Log($"(103) {ex.Message}");
            }
        }
        #region "placeholders"
        private void ServerName_Enter(object sender, EventArgs e)
        {
            if (ServerName.Text == "Server name here")
                ServerName.Text = "";
        }

        private void ServerName_Leave(object sender, EventArgs e)
        {
            if (ServerName.Text == "")
                ServerName.Text = "Server name here";
        }

        private void SearchPath_Enter(object sender, EventArgs e)
        {
            if (SearchPath.Text == "Save file directory here")
                SearchPath.Text = "";
        }

        private void SearchPath_Leave(object sender, EventArgs e)
        {
            if (SearchPath.Text == "")
                SearchPath.Text = "Save file directory here";
        }
        #endregion
    }
}
