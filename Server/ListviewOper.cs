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
        public ListviewOper()
        {
            
        }
        //添加地址
        public  static void Add(ListView listview,string data)
        {                       
            int Number = listview.Items.Count;
            ListViewItem lvi = new ListViewItem();

            listview.BeginUpdate();
            lvi.Text = data;
            listview.EndUpdate();
            listview.Items.Add(lvi);
        }
        //删除
        public static void ListviewDeleteData(string TableName, ListView listView)
        {
            //listView.Items.Remove(listView.SelectedItems[0]);
        }
    }
}
