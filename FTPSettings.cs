using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RijndaelEncryptDecrypt;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SaveFileSync
{
    public partial class FTPSettings : Form
    {
        private static Form1 _form1;

        public static string FTP_URL = ""; //The FTP url need to be empty/only contain files uploaded by this program.
        public static string FTP_USERNAME = "";
        public static string FTP_PASSWORD = ""; //No encryption in this program so make sure to only share to friends.

        private static string ENCRYPTION_SALT = "";

        private static string SETTINGS_FILE = "savefilesync.dat";

        public FTPSettings(Form1 form1)
        {
            InitializeComponent();
            _form1 = form1;
        }

        void CreateFile()
        {
            var settings_file = new JObject {
                { "ftp_settings", new JObject {
                    { "url", EncryptDecryptUtils.Encrypt(FTP_URL, SETTINGS_FILE, ENCRYPTION_SALT, "SHA1") },
                    { "username", EncryptDecryptUtils.Encrypt(FTP_USERNAME, SETTINGS_FILE, ENCRYPTION_SALT, "SHA1") },
                    { "password", EncryptDecryptUtils.Encrypt(FTP_PASSWORD, SETTINGS_FILE, ENCRYPTION_SALT, "SHA1") }
                    }
                },
                { "encryption_settings", new JObject {
                    { "salt", ENCRYPTION_SALT }
                } }
            };

            File.WriteAllText(SETTINGS_FILE, settings_file.ToString());

            using (var file = File.CreateText(SETTINGS_FILE))
            using (var writer = new JsonTextWriter(file))
            {
                settings_file.WriteTo(writer);
            }
        }

        void LoadSettings()
        {
            using (var file = File.OpenText(SETTINGS_FILE))
            using (var reader = new JsonTextReader(file))
            {
                var o2 = (JObject)JToken.ReadFrom(reader);
                ENCRYPTION_SALT = (string)o2["encryption_settings"]["salt"];
                if (((string)o2["ftp_settings"]["url"]).Length != 0)
                    FTP_URL = EncryptDecryptUtils.Decrypt((string)o2["ftp_settings"]["url"], SETTINGS_FILE, ENCRYPTION_SALT, "SHA1");
                if (((string)o2["ftp_settings"]["username"]).Length != 0)
                    FTP_USERNAME = EncryptDecryptUtils.Decrypt((string)o2["ftp_settings"]["username"], SETTINGS_FILE, ENCRYPTION_SALT, "SHA1");
                if (((string)o2["ftp_settings"]["password"]).Length != 0)
                    FTP_PASSWORD = EncryptDecryptUtils.Decrypt((string)o2["ftp_settings"]["password"], SETTINGS_FILE, ENCRYPTION_SALT, "SHA1");

                FTPUrl.Text = FTP_URL;
                FTPUsername.Text = FTP_USERNAME;
                FTPPassword.Text = FTP_PASSWORD;
                EncryptionSalt.Text = ENCRYPTION_SALT;
            }
        }

        private void FTPSettings_Load(object sender, EventArgs e)
        {
            if (File.Exists(SETTINGS_FILE))
            { 
                _form1.Log("Found settings file.");
                LoadSettings();
            }
            else
            {
                _form1.Log("No settings file found... Creating new.");
                CreateFile();
            }
        }

        private void SaveSettings_Click(object sender, EventArgs e)
        {
            FTP_URL = FTPUrl.Text;
            FTP_USERNAME = FTPUsername.Text;
            FTP_PASSWORD = FTPPassword.Text;
            ENCRYPTION_SALT = EncryptionSalt.Text;

            if (FTP_URL.Length == 0 || FTP_USERNAME.Length == 0 || FTP_PASSWORD.Length ==  0 || ENCRYPTION_SALT.Length == 0)
            {
                _form1.Log("Please enter all fields.");
                return;
            }

            CreateFile();
        }
    }
}
