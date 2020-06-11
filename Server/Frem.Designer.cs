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
            this.Ssend = new System.Windows.Forms.Button();
            this.Sport = new System.Windows.Forms.TextBox();
            this.Sip = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Conn = new System.Windows.Forms.Button();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ssend
            // 
            this.Ssend.Location = new System.Drawing.Point(566, 381);
            this.Ssend.Name = "Ssend";
            this.Ssend.Size = new System.Drawing.Size(75, 23);
            this.Ssend.TabIndex = 6;
            this.Ssend.Text = "发送文本";
            this.Ssend.UseVisualStyleBackColor = true;
            this.Ssend.Click += new System.EventHandler(this.Ssend_Click);
            // 
            // Sport
            // 
            this.Sport.Location = new System.Drawing.Point(520, 31);
            this.Sport.Name = "Sport";
            this.Sport.Size = new System.Drawing.Size(100, 21);
            this.Sport.TabIndex = 3;
            this.Sport.Text = "5000";
            // 
            // Sip
            // 
            this.Sip.Location = new System.Drawing.Point(395, 31);
            this.Sip.Name = "Sip";
            this.Sip.Size = new System.Drawing.Size(100, 21);
            this.Sip.TabIndex = 2;
            this.Sip.Text = "192.168.43.107";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.Ssend);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.Sip);
            this.groupBox2.Controls.Add(this.Sport);
            this.groupBox2.Controls.Add(this.Conn);
            this.groupBox2.Controls.Add(this.listBox2);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(776, 426);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "客户端";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(649, 381);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(105, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "发送上位机信息";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Conn
            // 
            this.Conn.Location = new System.Drawing.Point(645, 29);
            this.Conn.Name = "Conn";
            this.Conn.Size = new System.Drawing.Size(75, 23);
            this.Conn.TabIndex = 4;
            this.Conn.Text = "连接服务器";
            this.Conn.UseVisualStyleBackColor = true;
            this.Conn.Click += new System.EventHandler(this.Conn_Click);
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 12;
            this.listBox2.Location = new System.Drawing.Point(17, 87);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(624, 280);
            this.listBox2.TabIndex = 1;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(39, 382);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(510, 21);
            this.textBox1.TabIndex = 7;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(654, 196);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "五菱CTF28E";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(654, 250);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 21);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "V1.1.1";
            // 
            // Frem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox2);
            this.Name = "Frem";
            this.Text = "Form1";
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox Sport;
        private System.Windows.Forms.TextBox Sip;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button Conn;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Ssend;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
    }
}

