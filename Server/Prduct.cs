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
        public Product()
        {
            factor = new ServerFactory();
            server = new TestService(factor.myTabPage.dictionary["Device"]);
            client = new TestClient();
            config = new Config("SoftInfo");
        }

        //加载产品信息
        public void LoadData()
        {
            ListviewOper.LoadSoftInfo(factor.myTabPage.dictionary["SoftVersion"], config.ReadNode());
        }
    }
}
