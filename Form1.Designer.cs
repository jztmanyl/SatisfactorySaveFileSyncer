namespace SaveFileSync
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.logbox = new System.Windows.Forms.RichTextBox();
            this.SearchPath = new System.Windows.Forms.TextBox();
            this.FolderButton = new System.Windows.Forms.Button();
            this.ServerName = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(158, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 22);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sync Save File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // logbox
            // 
            this.logbox.Location = new System.Drawing.Point(12, 38);
            this.logbox.Name = "logbox";
            this.logbox.ReadOnly = true;
            this.logbox.Size = new System.Drawing.Size(483, 229);
            this.logbox.TabIndex = 1;
            this.logbox.Text = "";
            // 
            // SearchPath
            // 
            this.SearchPath.Location = new System.Drawing.Point(12, 12);
            this.SearchPath.Name = "SearchPath";
            this.SearchPath.Size = new System.Drawing.Size(437, 20);
            this.SearchPath.TabIndex = 2;
            this.SearchPath.Tag = "";
            this.SearchPath.Text = "Save file directory here";
            this.SearchPath.Enter += new System.EventHandler(this.SearchPath_Enter);
            this.SearchPath.Leave += new System.EventHandler(this.SearchPath_Leave);
            // 
            // FolderButton
            // 
            this.FolderButton.Location = new System.Drawing.Point(455, 12);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(40, 20);
            this.FolderButton.TabIndex = 3;
            this.FolderButton.Text = "...";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderButton_Click);
            // 
            // ServerName
            // 
            this.ServerName.Location = new System.Drawing.Point(12, 273);
            this.ServerName.Name = "ServerName";
            this.ServerName.Size = new System.Drawing.Size(140, 20);
            this.ServerName.TabIndex = 4;
            this.ServerName.Tag = "";
            this.ServerName.Text = "Server name here";
            this.ServerName.Enter += new System.EventHandler(this.ServerName_Enter);
            this.ServerName.Leave += new System.EventHandler(this.ServerName_Leave);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(419, 273);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(76, 21);
            this.button2.TabIndex = 5;
            this.button2.Text = "FTP Settings";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(507, 302);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ServerName);
            this.Controls.Add(this.FolderButton);
            this.Controls.Add(this.SearchPath);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.logbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Save File Syncer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox logbox;
        private System.Windows.Forms.TextBox SearchPath;
        private System.Windows.Forms.Button FolderButton;
        private System.Windows.Forms.TextBox ServerName;
        private System.Windows.Forms.Button button2;
    }
}

