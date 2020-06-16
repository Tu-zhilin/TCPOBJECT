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
            this.components = new System.ComponentModel.Container();
            this.SendText = new System.Windows.Forms.Button();
            this.SendFile = new System.Windows.Forms.Button();
            this.ServerOpen = new System.Windows.Forms.Button();
            this.ServerIp = new System.Windows.Forms.TextBox();
            this.ServerPort = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xxxxx = new System.Windows.Forms.GroupBox();
            this.ClientSoftVer = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ClientPdtType = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ClientIPendPort = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.SoftVersion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ProductName = new System.Windows.Forms.TextBox();
            this.SendWord = new System.Windows.Forms.TextBox();
            this.TestWindow = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.Dele = new System.Windows.Forms.Button();
            this.Change = new System.Windows.Forms.Button();
            this.Add = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pdtVer = new System.Windows.Forms.TextBox();
            this.pdtName = new System.Windows.Forms.TextBox();
            this.Load = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.xxxxx.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // SendText
            // 
            this.SendText.Location = new System.Drawing.Point(482, 549);
            this.SendText.Name = "SendText";
            this.SendText.Size = new System.Drawing.Size(75, 23);
            this.SendText.TabIndex = 0;
            this.SendText.Text = "发送文字";
            this.SendText.UseVisualStyleBackColor = true;
            this.SendText.Click += new System.EventHandler(this.SendText_Click);
            // 
            // SendFile
            // 
            this.SendFile.Enabled = false;
            this.SendFile.Location = new System.Drawing.Point(574, 549);
            this.SendFile.Name = "SendFile";
            this.SendFile.Size = new System.Drawing.Size(75, 23);
            this.SendFile.TabIndex = 1;
            this.SendFile.Text = "发送文件夹";
            this.SendFile.UseVisualStyleBackColor = true;
            this.SendFile.Click += new System.EventHandler(this.SendFile_Click);
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
            this.ServerIp.ReadOnly = true;
            this.ServerIp.Size = new System.Drawing.Size(100, 21);
            this.ServerIp.TabIndex = 3;
            // 
            // ServerPort
            // 
            this.ServerPort.Location = new System.Drawing.Point(17, 61);
            this.ServerPort.Name = "ServerPort";
            this.ServerPort.ReadOnly = true;
            this.ServerPort.Size = new System.Drawing.Size(100, 21);
            this.ServerPort.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ServerPort);
            this.groupBox1.Controls.Add(this.ServerOpen);
            this.groupBox1.Controls.Add(this.ServerIp);
            this.groupBox1.Location = new System.Drawing.Point(789, 12);
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
            this.xxxxx.Location = new System.Drawing.Point(789, 114);
            this.xxxxx.Name = "xxxxx";
            this.xxxxx.Size = new System.Drawing.Size(221, 141);
            this.xxxxx.TabIndex = 6;
            this.xxxxx.TabStop = false;
            this.xxxxx.Text = "客户端";
            // 
            // ClientSoftVer
            // 
            this.ClientSoftVer.Location = new System.Drawing.Point(68, 93);
            this.ClientSoftVer.Name = "ClientSoftVer";
            this.ClientSoftVer.ReadOnly = true;
            this.ClientSoftVer.Size = new System.Drawing.Size(146, 21);
            this.ClientSoftVer.TabIndex = 6;
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 5;
            this.label4.Text = "产品型号";
            // 
            // ClientPdtType
            // 
            this.ClientPdtType.Location = new System.Drawing.Point(68, 56);
            this.ClientPdtType.Name = "ClientPdtType";
            this.ClientPdtType.ReadOnly = true;
            this.ClientPdtType.Size = new System.Drawing.Size(146, 21);
            this.ClientPdtType.TabIndex = 2;
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
            this.ClientIPendPort.ReadOnly = true;
            this.ClientIPendPort.Size = new System.Drawing.Size(146, 21);
            this.ClientIPendPort.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.SoftVersion);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.ProductName);
            this.groupBox2.Location = new System.Drawing.Point(789, 261);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(221, 118);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "软件信息";
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
            // SoftVersion
            // 
            this.SoftVersion.Location = new System.Drawing.Point(68, 57);
            this.SoftVersion.Name = "SoftVersion";
            this.SoftVersion.ReadOnly = true;
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
            this.ProductName.ReadOnly = true;
            this.ProductName.Size = new System.Drawing.Size(146, 21);
            this.ProductName.TabIndex = 2;
            // 
            // SendWord
            // 
            this.SendWord.Location = new System.Drawing.Point(67, 549);
            this.SendWord.Name = "SendWord";
            this.SendWord.Size = new System.Drawing.Size(406, 21);
            this.SendWord.TabIndex = 3;
            this.SendWord.Text = "向客户端发送一条信息";
            // 
            // TestWindow
            // 
            this.TestWindow.Location = new System.Drawing.Point(670, 549);
            this.TestWindow.Name = "TestWindow";
            this.TestWindow.Size = new System.Drawing.Size(75, 23);
            this.TestWindow.TabIndex = 2;
            this.TestWindow.Text = "客户端测试窗口";
            this.TestWindow.UseVisualStyleBackColor = true;
            this.TestWindow.Click += new System.EventHandler(this.TestWindow_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(1016, 20);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(447, 568);
            this.listBox1.TabIndex = 9;
            // 
            // timer1
            // 
            this.timer1.Interval = 5;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.Dele);
            this.groupBox4.Controls.Add(this.Change);
            this.groupBox4.Controls.Add(this.Add);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.pdtVer);
            this.groupBox4.Controls.Add(this.pdtName);
            this.groupBox4.Controls.Add(this.Load);
            this.groupBox4.Location = new System.Drawing.Point(789, 385);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(221, 203);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "XML操作";
            // 
            // Dele
            // 
            this.Dele.Location = new System.Drawing.Point(22, 131);
            this.Dele.Name = "Dele";
            this.Dele.Size = new System.Drawing.Size(75, 23);
            this.Dele.TabIndex = 12;
            this.Dele.Text = "删除";
            this.Dele.UseVisualStyleBackColor = true;
            this.Dele.Click += new System.EventHandler(this.Dele_Click);
            // 
            // Change
            // 
            this.Change.Location = new System.Drawing.Point(135, 131);
            this.Change.Name = "Change";
            this.Change.Size = new System.Drawing.Size(75, 23);
            this.Change.TabIndex = 11;
            this.Change.Text = "修改";
            this.Change.UseVisualStyleBackColor = true;
            this.Change.Click += new System.EventHandler(this.Change_Click);
            // 
            // Add
            // 
            this.Add.Location = new System.Drawing.Point(22, 164);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(75, 23);
            this.Add.TabIndex = 10;
            this.Add.Text = "添加";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "软件版本";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 24);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "产品型号";
            // 
            // pdtVer
            // 
            this.pdtVer.Location = new System.Drawing.Point(22, 94);
            this.pdtVer.Name = "pdtVer";
            this.pdtVer.Size = new System.Drawing.Size(188, 21);
            this.pdtVer.TabIndex = 9;
            // 
            // pdtName
            // 
            this.pdtName.Location = new System.Drawing.Point(22, 48);
            this.pdtName.Name = "pdtName";
            this.pdtName.Size = new System.Drawing.Size(188, 21);
            this.pdtName.TabIndex = 8;
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(135, 164);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 4;
            this.Load.Text = "加载";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // Server
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1364, 604);
            this.Controls.Add(this.SendWord);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.TestWindow);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.SendFile);
            this.Controls.Add(this.SendText);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.xxxxx);
            this.Controls.Add(this.groupBox1);
            this.Name = "Server";
            this.Text = "服务器";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.xxxxx.ResumeLayout(false);
            this.xxxxx.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.TextBox ClientSoftVer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox ClientPdtType;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button TestWindow;
        private System.Windows.Forms.TextBox SendWord;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button Dele;
        private System.Windows.Forms.Button Change;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox pdtVer;
        private System.Windows.Forms.TextBox pdtName;
        private System.Windows.Forms.Button Load;
    }
}