using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Server;

namespace Server
{
    public partial class Frem : Form
    {
        string sip;
        Product pdt;

        public Frem(string sip)
        {
            this.sip = sip;
            pdt = new Product();
            InitializeComponent();
            timer1.Start();
            Sip.Text = sip;
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            while (pdt.client.info.Count != 0)
            {
                listBox2.Items.Add(pdt.client.info.Dequeue());
                listBox2.SelectedIndex = listBox2.Items.Count - 1;
            }
        }

        private void SStart_Click(object sender, EventArgs e)
        {
            Sip.Text = pdt.server.localIP;
            Sport.Text = pdt.server.localPort.ToString();
            pdt.server.ServerSetting(pdt.server.localIP, pdt.server.localPort,10);
        }

        private void Conn_Click(object sender, EventArgs e)
        {
            if (pdt.client.Connect(ref pdt.client.Main_tcpClient,Sip.Text, int.Parse(Sport.Text)))
            {
                //pdt.client.SendPdtInfo("五菱CTF28E", "V1.1.1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Checked)
                pdt.client.fileType = TCPClient.FileType.File;
            else if (Directory.Checked)
                pdt.client.fileType = TCPClient.FileType.Directory;

            pdt.client.SendPdtInfo(textBox2.Text,textBox3.Text);
        }

        private void Ssend_Click(object sender, EventArgs e)
        {
            pdt.client.SendMsg(textBox1.Text);
        }

        private void Frem_FormClosing(object sender, FormClosingEventArgs e)
        {
            pdt.client.Main_tcpClient.Close();
            this.Dispose();
        }
    }
}
