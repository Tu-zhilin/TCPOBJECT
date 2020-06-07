using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Dongzr.MidiLite;
using System.Threading;

namespace Server
{
    #region 服务端抽象类

    public abstract class TCPServer
    {
        //服务端套接字
        public Socket serverSocket;
        //客户端套接字
        public List<IPAddress> clientsSocket;
        //监听线程
        public Thread listenThread;
        //接收线程
        public Thread reviveThread;

        //绑定
        public abstract void ServerSetting(IPAddress ip,IPEndPoint point);
        //监听
        public abstract void Listenning(object obj);
        //接收
        public abstract void Receiving(Socket socket);
        //发送信息
        public abstract void Send(string msg);
        
    }
    #endregion

    public class TextService : TCPServer
    {
        
    }
}
