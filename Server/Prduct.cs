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

        public Product()
        {
            factor = new ServerFactory();
            server = new TestService(factor.myTabPage.dictionary["Device"]);
            client = new TestClient();
        }
    }
}
