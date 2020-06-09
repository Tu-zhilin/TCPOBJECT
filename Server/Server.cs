using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Template;

namespace Server
{
    public partial class Server : Form
    {
        Product pdt;
        TabControl tabControl;
        public Server()
        {
            pdt = new Product();
            tabControl = pdt.factor.tabControl;
            InitializeComponent();
            this.Controls.Add(tabControl);
        }
        //事件绑定
        public void BindEvent()
        {

        }
        //开启服务器
        private void ServerOpen_Click(object sender, EventArgs e)
        {
            ServerIp.Text = pdt.server.localIP;
            ServerPort.Text = pdt.server.localPort.ToString();
            pdt.server.ServerSetting(pdt.server.localIP, pdt.server.localPort, 10);
        }
        //设备Listview事件
        private void DeviceListview (object sender, EventArgs e)
        {
            if (pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems.Count > 0)
            {
                ClientIPendPort.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[0].Text;
                ClientPdtType.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[1].Text;
                ClientSoftVer.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[2].Text;
            }
        }
        //软件Listview事件
        private void SoftListview(object sender, EventArgs e)
        {
            if (pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems.Count > 0)
            {
                ProductName.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[0].Text;
                SoftVersion.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[1].Text;
            }
        }

        private void TestWindow_Click(object sender, EventArgs e)
        {
            Form form = new Frem();
            form.Show();
        }
    }
}
