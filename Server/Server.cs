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
using System.Threading;

namespace Server
{
    public partial class Server : Form
    {
        bool Flag = false;
        Product pdt;
        TabControl tabControl;
        delegate void Action();
        Action action;

        public Server()
        {
            pdt = new Product();
            tabControl = pdt.factor.tabControl;
            InitializeComponent();
            this.Controls.Add(tabControl);
            BindEvent();
            action = new Action(ServerOpen.PerformClick);
            timer1.Enabled = true;
        }

        //事件绑定
        public void BindEvent()
        {
            pdt.factor.myTabPage.dictionary["Device"].ItemSelectionChanged += DeviceListview;
            pdt.factor.myTabPage.dictionary["SoftVersion"].ItemSelectionChanged += SoftListview;
        }

        //开启服务器
        private void ServerOpen_Click(object sender, EventArgs e)
        {
            ServerIp.Text = pdt.server.localIP;
            ServerPort.Text = pdt.server.localPort.ToString();
            pdt.server.ServerSetting(pdt.server.localIP, pdt.server.localPort, 10);
            pdt.LoadData();
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
                pdtSoftname.Text = pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems[0].SubItems[2].Text;
            }
        }

        //测试窗口
        private void TestWindow_Click(object sender, EventArgs e)
        {
            Form form = new Frem(ServerIp.Text);
            form.Show();
        }

        //发送文本按钮
        private void SendText_Click(object sender, EventArgs e)
        {
            if (pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems.Count > 0)
            {
                pdt.server.SendMsg(ClientIPendPort.Text, SendWord.Text);
            }
        }

        //发送文件按钮
        private void SendFile_Click(object sender, EventArgs e)
        {
            if (pdt.factor.myTabPage.dictionary[tabControl.SelectedTab.Name].SelectedItems.Count > 0)
            {
                pdt.server.SendDirectory(ClientIPendPort.Text);
            }
        }

        //定时器
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!Flag)
            {
                Flag = true;
                this.Invoke(action);
                ServerOpen.Enabled = false;
            }

            //这里会报错，待解决
            while (pdt.server.info.Count > 0)
            {
                string str = null;
                if ((str = pdt.server.info.Dequeue()) != null)
                    listBox1.Items.Add(str);
                if (listBox1.Items.Count > 0)
                    listBox1.SelectedIndex = listBox1.Items.Count - 1;
            }
        }

        //加载按钮
        private void Load_Click(object sender, EventArgs e)
        {
            pdt.LoadData();
        }
        
        //插入按钮
        private void Add_Click(object sender, EventArgs e)
        {

            try
            {
                if (pdt.server.softDic.ContainsKey(pdtName.Text))
                {
                    MessageBox.Show("该产品已存在");
                    return;
                }
                if (pdt.config.AddChileNode(pdtName.Text, pdtVer.Text,softName.Text))
                {
                    Config.Data data = new Config.Data();
                    data._Name = pdtName.Text;
                    data._Version = pdtVer.Text;
                    data._SoftName = softName.Text;
                    ListviewOper.Insert_Info(pdt.factor.myTabPage.dictionary["SoftVersion"], pdtName.Text, pdtVer.Text,softName.Text);
                    pdt.server.softDic.Add(pdtName.Text,data);
                }
            }
            catch (Exception ex)
            {
                listBox1.Items.Add(ex.Message);
            }
        }
        
        //修改按钮
        private void Change_Click(object sender, EventArgs e)
        {
            if (pdt.config.ChangeChildNode(ProductName.Text, pdtVer.Text,softName.Text))
            {
                ListviewOper.ChangeData(pdt.factor.myTabPage.dictionary["SoftVersion"], pdtVer.Text,softName.Text);
                pdt.server.softDic[ProductName.Text]._Version = pdtVer.Text;
                pdt.server.softDic[ProductName.Text]._SoftName = softName.Text;
            }
        }

        //删除按钮
        private void Dele_Click(object sender, EventArgs e)
        {
            if (pdt.config.DeleChileNode(ProductName.Text))
            {
                ListviewOper.Dele_Data(pdt.factor.myTabPage.dictionary["SoftVersion"]);
                pdt.server.softDic.Remove(ProductName.Text);
            }
        }
    }
}
