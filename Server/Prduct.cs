using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template;
namespace Server
{
    class Product
    {
        //控件
        public ControlFactory factor;
        //服务端
        public TCPServer server;
        //客户端
        public TCPClient client;
        //XML
        public Config config;
        //存储本地软件信息

        public Product()
        {
            //softDic = new Dictionary<string, string>();
            factor = new ServerFactory();
            server = new TestService(factor.myTabPage.dictionary["Device"]);
            client = new TestClient();
            config = new Config("SoftInfo");
        }

        //加载产品信息
        public void LoadData()
        {
            server.softDic = ListviewOper.LoadSoftInfo(factor.myTabPage.dictionary["SoftVersion"], config.ReadNode());
        }
    }
}
