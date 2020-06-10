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

namespace Server
{
    public partial class Frem : Form
    {
        Product pdt;
        public Frem()
        {
            pdt = new Product();
            InitializeComponent();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            while (pdt.server.info.Count != 0)
            {
                listBox1.Items.Add(pdt.server.info.Dequeue());
            }

            while (pdt.client.info.Count != 0)
            {
                listBox2.Items.Add(pdt.client.info.Dequeue());
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
            if (pdt.client.Connect(Sip.Text, int.Parse(Sport.Text)))
            {
                Cip.Text = pdt.client.IpEndPort;
                pdt.client.SendPdtInfo("五菱CTF28E", "V1.1.1");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pdt.client.SendPdtInfo("五菱CTF28E","V1.1.1");
        }

        private void Ssend_Click(object sender, EventArgs e)
        {
            pdt.server.SendMsg(Cip.Text,"服务器发送一个消息");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pdt.server.SendFile(Cip.Text);
        }
    }
}
