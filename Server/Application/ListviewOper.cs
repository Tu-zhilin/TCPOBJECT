﻿using System;
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

        //修改
        public static void ListviewChangeData(ListView list,string data,int index)
        {
            int i = 1;
            if (list.SelectedItems.Count > 0)
            {
                if (index == 1)
                    list.SelectedItems[0].Text = data;
                else if(index == 2)
                    list.SelectedItems[0].SubItems[1].Text = data;

            }
            else
                return;
        }

        //加载上位机信息
        public static void LoadSoftInfo(ListView listview,List<Config.Data> dataList)
        {
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
                    listview.EndUpdate();
                    listview.Items.Add(lvi);
                }
            });

            listview.Invoke(action);
        }
        //添加地址
        public  static void Add_Address(ListView listview,string data)
        {                       
            int Number = listview.Items.Count;
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

        //修改产品名字和版本
        public static void Change_Info(ListView listview,string ipaddress,string name,string Version)
        {
            Action action = new Action(() => {
                foreach (ListViewItem item in listview.Items)
                {
                    if (item.Text == ipaddress)
                    {
                        item.SubItems[1].Text = name;
                        item.SubItems[2].Text = Version;
                        item.Selected = true;
                        item.EnsureVisible();
                        return;
                    }
                }
            });

            listview.Invoke(action);
        }

        //删除
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
    }
}
