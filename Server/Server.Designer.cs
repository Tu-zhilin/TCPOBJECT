namespace Server
{
    partial class Server
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
            this.SendText = new System.Windows.Forms.Button();
            this.SendFile = new System.Windows.Forms.Button();
            this.ServerOpen = new System.Windows.Forms.Button();
            this.ServerIp = new System.Windows.Forms.TextBox();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xxxxx = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientIPendPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SoftVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProductName = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ClientPdtType = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ClientSoftVer = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.xxxxx.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(17, 66);
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(75, 23);
            this.SendText.TabIndex = 0;
            this.SendText.Text = "发送文字";
            this.SendText.UseVisualStyleBackColor = true;
            // 
            // SendFile
            // 
            this.SendFile.Location = new System.Drawing.Point(135, 66);
            this.SendFile.Name = "SendFile";
            this.SendFile.Size = new System.Drawing.Size(75, 23);
            this.SendFile.TabIndex = 1;
            this.SendFile.Text = "发送文件";
            this.SendFile.UseVisualStyleBackColor = true;
            // 
            // ServerOpen
            // 
            this.ServerOpen.Location = new System.Drawing.Point(135, 25);
            this.ServerOpen.Name = "ServerOpen";
            this.ServerOpen.Size = new System.Drawing.Size(75, 23);
            this.ServerOpen.TabIndex = 2;
            this.ServerOpen.Text = "开启服务器";
            this.ServerOpen.UseVisualStyleBackColor = true;
            this.ServerOpen.Click += new System.EventHandler(this.ServerOpen_Click);
            // 
            // ServerIp
            // 
            this.ServerIp.Location = new System.Drawing.Point(17, 25);
            this.ServerIp.Name = "ServerIp";
            this.ServerIp.Size = new System.Drawing.Size(100, 21);
            this.ServerIp.TabIndex = 3;
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(17, 61);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.Size = new System.Drawing.Size(100, 21);
            this.ServerPort.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ServerPort);
            this.groupBox1.Controls.Add(this.ServerOpen);
            this.groupBox1.Controls.Add(this.ServerIp);
            this.groupBox1.Location = new System.Drawing.Point(873, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(221, 96);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Server";
            // 
            // xxxxx
            // 
            this.xxxxx.Controls.Add(this.ClientSoftVer);
            this.xxxxx.Controls.Add(this.label3);
            this.xxxxx.Controls.Add(this.label4);
            this.xxxxx.Controls.Add(this.ClientPdtType);
            this.xxxxx.Controls.Add(this.label1);
            this.xxxxx.Controls.Add(this.ClientIPendPort);
            this.xxxxx.Location = new System.Drawing.Point(873, 114);
            this.xxxxx.Name = "xxxxx";
            this.xxxxx.Size = new System.Drawing.Size(221, 141);
            this.xxxxx.TabIndex = 6;
            this.xxxxx.TabStop = false;
            this.xxxxx.Text = "客户端";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "IPendPort";
            // 
            // ClientIPendPort
            // 
            this.ClientIPendPort.Location = new System.Drawing.Point(68, 20);
            this.ClientIPendPort.Name = "ClientIPendPort";
            this.ClientIPendPort.Size = new System.Drawing.Size(146, 21);
            this.ClientIPendPort.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.SoftVersion);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ProductName);
            this.groupBox2.Location = new System.Drawing.Point(873, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "软件信息";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 98);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "软件版本";
            // 
            // SoftVersion
            // 
            this.SoftVersion.Location = new System.Drawing.Point(68, 57);
            this.SoftVersion.Name = "SoftVersion";
            this.SoftVersion.Size = new System.Drawing.Size(146, 21);
            this.SoftVersion.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "产品型号";
            // 
            // ProductName
            // 
            this.ProductName.Location = new System.Drawing.Point(68, 20);
            this.ProductName.Name = "ProductName";
            this.ProductName.Size = new System.Drawing.Size(146, 21);
            this.ProductName.TabIndex = 2;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.SendFile);
            this.groupBox3.Controls.Add(this.SendText);
            this.groupBox3.Location = new System.Drawing.Point(873, 385);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(221, 136);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "手动操作";
            // 
            // ClientPdtType
            // 
            this.ClientPdtType.Location = new System.Drawing.Point(68, 56);
            this.ClientPdtType.Name = "ClientPdtType";
            this.ClientPdtType.Size = new System.Drawing.Size(146, 21);
            this.ClientPdtType.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "产品型号";
            // 
            // ClientSoftVer
            // 
            this.ClientSoftVer.Location = new System.Drawing.Point(68, 93);
            this.ClientSoftVer.Name = "ClientSoftVer";
            this.ClientSoftVer.Size = new System.Drawing.Size(146, 21);
            this.ClientSoftVer.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 62);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "软件版本";
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1106, 533);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.xxxxx);
            this.Controls.Add(this.groupBox1);
            this.Name = "Server";
            this.Text = "客户端";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.xxxxx.ResumeLayout(false);
            this.xxxxx.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SendText;
        private System.Windows.Forms.Button SendFile;
        private System.Windows.Forms.Button ServerOpen;
        private System.Windows.Forms.TextBox ServerIp;
        private System.Windows.Forms.TextBox ServerPort;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox xxxxx;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox ClientIPendPort;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox SoftVersion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ProductName;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ClientSoftVer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ClientPdtType;
        private System.Windows.Forms.Label label5;
    }
}