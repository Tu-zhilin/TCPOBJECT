using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Template
{

    #region TabControl
 
    class MyTabcontrol
    {
        public TabControl TabControl;
        public MyTabcontrol()
        {
            TabControl = new TabControl();
            Setting(this.TabControl);
        }

        public void Setting(TabControl tabControl)
        {
            tabControl.Name = "tabControl";
            tabControl.Location = new Point(6, 6);
            tabControl.Size = new Size(776, 503);
        }
    }
    #endregion

    #region TabPage
    class MyTabPage
    {
        //这个是用于绑定Listview和Tabpage用的字典
        public Dictionary<string, ListView> dictionary;

        public MyTabPage()
        {
            dictionary = new Dictionary<string, ListView>();
        }

        public void Setting(TabPage tabpage,string name,string text)
        {
            //设置控件名字
            tabpage.Name = name;
            //设置页面名
            tabpage.Text = text;
        }

        public TabPage AddListview(ListView listview, string name, string text)
        {
            TabPage page = new TabPage();
            Setting(page,name,text);
            page.Controls.Add(listview);
            dictionary.Add(name,listview);
            return page;
        }
    }
    #endregion

    #region Listview配置抽象类
    abstract class MyListview
    {
        //配置属性
        public virtual void SettingListView(ListView listview)
        {
            listview.Location = new Point(6, 6);
            listview.Height = 465;
            listview.Width = 756;
            listview.GridLines = true;
            listview.FullRowSelect = true;
            listview.View = View.Details;
            listview.Scrollable = true;
            listview.MultiSelect = false;
            listview.HideSelection = false;
            listview.HeaderStyle = ColumnHeaderStyle.Nonclickable;
        }

        //ListView头标题设置
        public virtual void SetTitle(ListView list, string Title, int Width = 80, HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center)
        {
            ColumnHeader cn = new ColumnHeader();
            cn.Width = Width;
            cn.TextAlign = horizontalAlignment;
            cn.Text = Title;
            list.Columns.Add(cn);
        }

        public abstract ListView GetAdmine();            
    }
    #endregion

    /// <summary>
    /// 账号数据管理Listview 
    /// </summary>
    class UserListview : MyListview
    {
        //User Password Nmae Sex
        public override ListView GetAdmine()
        {
            ListView listview = new ListView();
            SettingListView(listview);
            SetTitle(listview, "number");
            SetTitle(listview, "User");
            SetTitle(listview, "Password");
            SetTitle(listview, "id");
            SetTitle(listview, "name");
            SetTitle(listview, "Sex");
            return listview;
        }
    }

    /// <summary>
    ///成绩数据管理Listview 
    /// </summary>
    class SorceListview : MyListview
    {
        //User Password Nmae Sex
        public override ListView GetAdmine()
        {
            ListView listview = new ListView();
            SettingListView(listview);
            SetTitle(listview, "number");
            SetTitle(listview, "id");
            SetTitle(listview, "name");
            SetTitle(listview, "class");
            SetTitle(listview, "sex");
            SetTitle(listview, "English");
            SetTitle(listview, "Math");
            return listview;
        }
    }

    /// <summary>
    /// 设备在线状态
    /// </summary>
    class ServerDevice : MyListview
    {
        public override ListView GetAdmine()
        {
            ListView listview = new ListView();
            SettingListView(listview);
            SetTitle(listview, "设备地址",200);
            SetTitle(listview, "产品型号",200);
            SetTitle(listview, "当前版本",200);
            SetTitle(listview, "在线状态");
            return listview;
        }
    }

    /// <summary>
    /// 上位机版本控制
    /// </summary>
    class ServerSoftCtr : MyListview
    {
        public override ListView GetAdmine()
        {
            ListView listview = new ListView();
            SettingListView(listview);
            SetTitle(listview, "产品型号",200);
            SetTitle(listview, "当前版本",200);
            SetTitle(listview, "软件",200);
            return listview;
        }
    }

    #region 工厂

    abstract class ControlFactory
    {
        public MyTabcontrol myTabcontrol;
        public MyListview myListview;
        public MyTabPage myTabPage;
        public TabControl tabControl;
        public ControlFactory()
        {
            myTabcontrol = new MyTabcontrol();
            myTabPage = new MyTabPage();
        }

        public abstract TabControl GetTab();
    }
    #endregion

    /// <summary>
    /// 服务端控件生成
    /// </summary>
    class ServerFactory : ControlFactory
    {
        public ServerFactory():base()
        {
            tabControl = GetTab();
        }

        public override TabControl GetTab()
        {
            myListview = new ServerDevice();
            myTabcontrol.TabControl.Controls.Add(myTabPage.AddListview(myListview.GetAdmine(), "Device", "设备显示"));
            myListview = new ServerSoftCtr();
            myTabcontrol.TabControl.Controls.Add(myTabPage.AddListview(myListview.GetAdmine(), "SoftVersion", "软件版本"));
            return myTabcontrol.TabControl;
        }
    }
}
