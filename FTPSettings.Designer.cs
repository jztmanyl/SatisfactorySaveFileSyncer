namespace SaveFileSync
{
    partial class FTPSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FTPUrl = new System.Windows.Forms.TextBox();
            this.FTPPassword = new System.Windows.Forms.TextBox();
            this.FTPUsername = new System.Windows.Forms.TextBox();
            this.EncryptionSalt = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FTPUrl
            // 
            this.FTPUrl.Location = new System.Drawing.Point(12, 25);
            this.FTPUrl.Name = "FTPUrl";
            this.FTPUrl.Size = new System.Drawing.Size(186, 20);
            this.FTPUrl.TabIndex = 0;
            // 
            // FTPPassword
            // 
            this.FTPPassword.Location = new System.Drawing.Point(12, 103);
            this.FTPPassword.Name = "FTPPassword";
            this.FTPPassword.Size = new System.Drawing.Size(186, 20);
            this.FTPPassword.TabIndex = 2;
            this.FTPPassword.UseSystemPasswordChar = true;
            // 
            // FTPUsername
            // 
            this.FTPUsername.Location = new System.Drawing.Point(12, 64);
            this.FTPUsername.Name = "FTPUsername";
            this.FTPUsername.Size = new System.Drawing.Size(186, 20);
            this.FTPUsername.TabIndex = 1;
            // 
            // EncryptionSalt
            // 
            this.EncryptionSalt.Location = new System.Drawing.Point(12, 142);
            this.EncryptionSalt.Name = "EncryptionSalt";
            this.EncryptionSalt.Size = new System.Drawing.Size(186, 20);
            this.EncryptionSalt.TabIndex = 3;
            this.EncryptionSalt.UseSystemPasswordChar = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(113, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "FTP Url (include path):";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "FTP Username:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "FTP Password:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Encryption salt:";
            // 
            // SaveSettings
            // 
            this.SaveSettings.Location = new System.Drawing.Point(12, 168);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(113, 23);
            this.SaveSettings.TabIndex = 4;
            this.SaveSettings.Text = "Save settings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // FTPSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 199);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EncryptionSalt);
            this.Controls.Add(this.FTPUsername);
            this.Controls.Add(this.FTPPassword);
            this.Controls.Add(this.FTPUrl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FTPSettings";
            this.Text = "FTPSettings";
            this.Load += new System.EventHandler(this.FTPSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FTPUrl;
        private System.Windows.Forms.TextBox FTPPassword;
        private System.Windows.Forms.TextBox FTPUsername;
        private System.Windows.Forms.TextBox EncryptionSalt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button SaveSettings;
    }
}