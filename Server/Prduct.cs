using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Product
    {
        public TCPServer server;
        public TCPClient client;

        public Product()
        {
            server = new TestService();
            client = new TestClient();
        }
    }
}
