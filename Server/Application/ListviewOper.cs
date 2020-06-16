using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public class ListviewOper
    {
        private delegate void Action();

        //修改选中的上位机信息
        public static void ChangeData(ListView list,string Version,string SoftName)
        {
            if (list.SelectedItems.Count > 0)
            {
                list.SelectedItems[1].Text = Version;
                list.SelectedItems[2].SubItems[1].Text = SoftName;
            }
            else
                return;
        }

        //加载全部上位机信息
        public static Dictionary<string,string> LoadSoftInfo(ListView listview,List<Config.Data> dataList)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            Action action = new Action(() => {            
                //先清空所有的信息
                listview.Items.Clear();
                //循环加载
                foreach (Config.Data item in dataList)
                {
                    ListViewItem lvi = new ListViewItem();
                    listview.BeginUpdate();
                    lvi.Text = item._Name;
                    lvi.SubItems.Add(item._Version);
                    lvi.SubItems.Add(item._SoftName);
                    listview.EndUpdate();
                    listview.Items.Add(lvi);
                    dic.Add(item._Name, item._Version);
                }
            });

            listview.Invoke(action);
            return dic;
        }
        //将接受到的地址添加进去
        public  static void Add_Address(ListView listview,string data)
        {                       
            //int Number = listview.Items.Count;
            ListViewItem lvi = new ListViewItem();
            string[] str = new string[] {"待写入","待写入","在线"};
            Action action = new Action(() =>
            {
                listview.BeginUpdate();
                lvi.Text = data;
                lvi.SubItems.AddRange(str);
                listview.EndUpdate();
                listview.Items.Add(lvi);
            });
            listview.Invoke(action);
        }

        //修改客户端名字和版本
        public static void Change_Info(ListView listview,string ipaddress,string name,string Version)
        {
            Action action = new Action(() => {
                foreach (ListViewItem item in listview.Items)
                {
                    if (item.Text == ipaddress)
                    {
                        item.SubItems[1].Text = name;
                        item.SubItems[2].Text = Version;
                        //item.Selected = true;
                        //item.EnsureVisible();
                        return;
                    }
                }
            });

            listview.Invoke(action);
        }

        //删除下线设备
        public static void Dele_Info(ListView listview,string ipaddress)
        {
            Action action = new Action(() => {
                foreach (ListViewItem item in listview.Items)
                {
                    if (item.Text == ipaddress)
                    {
                        listview.Items.Remove(item);
                        return;
                    }
                }
            });

            listview.Invoke(action);
        }

        //添加上位机信息
        public static void Insert_Info(ListView listview,string name,string version,string softname)
        {
            ListViewItem lvi = new ListViewItem();
            Action action = new Action(() =>
            {
                listview.BeginUpdate();
                lvi.Text = name;
                lvi.SubItems.Add(version);
                lvi.SubItems.Add(softname);
                listview.EndUpdate();
                listview.Items.Add(lvi);
            });
            listview.Invoke(action);
        }

        //删除产品信息
        public static void Dele_Data(ListView listview)
        {
            listview.Items.Remove(listview.SelectedItems[0]);
        }
    }
}
