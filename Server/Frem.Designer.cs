namespace Server
{
    partial class Frem
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.SStart = new System.Windows.Forms.Button();
            this.Sip = new System.Windows.Forms.TextBox();
            this.Sport = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.Conn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Sport);
            this.groupBox1.Controls.Add(this.Sip);
            this.groupBox1.Controls.Add(this.SStart);
            this.groupBox1.Controls.Add(this.listBox1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(415, 381);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.Conn);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Location = new System.Drawing.Point(433, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(355, 381);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "groupBox2";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(6, 96);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(403, 136);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(6, 96);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(343, 136);
            this.listBox2.TabIndex = 1;
            // 
            // SStart
            // 
            this.SStart.Location = new System.Drawing.Point(311, 42);
            this.SStart.Name = "SStart";
            this.SStart.Size = new System.Drawing.Size(75, 23);
            this.SStart.TabIndex = 1;
            this.SStart.Text = "启动服务器";
            this.SStart.UseVisualStyleBackColor = true;
            this.SStart.Click += new System.EventHandler(this.SStart_Click);
            // 
            // Sip
            // 
            this.Sip.Location = new System.Drawing.Point(37, 44);
            this.Sip.Name = "Sip";
            this.Sip.Size = new System.Drawing.Size(100, 21);
            this.Sip.TabIndex = 2;
            // 
            // Sport
            // 
            this.Sport.Location = new System.Drawing.Point(163, 44);
            this.Sport.Name = "Sport";
            this.Sport.Size = new System.Drawing.Size(100, 21);
            this.Sport.TabIndex = 3;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Conn
            // 
            this.Conn.Location = new System.Drawing.Point(257, 42);
            this.Conn.Name = "Conn";
            this.Conn.Size = new System.Drawing.Size(75, 23);
            this.Conn.TabIndex = 4;
            this.Conn.Text = "连接服务器";
            this.Conn.UseVisualStyleBackColor = true;
            this.Conn.Click += new System.EventHandler(this.Conn_Click);
            // 
            // Frem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Frem";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox Sport;
        private System.Windows.Forms.TextBox Sip;
        private System.Windows.Forms.Button SStart;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Conn;
    }
}

