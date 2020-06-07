using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using Dongzr.MidiLite;
using System.Threading;
using System.Windows.Forms;

namespace Server
{
    #region 服务端抽象类

    public abstract class TCPServer
    {
        //服务端套接字
        public Socket serverSocket;
        //客户端套接字集合
        public Dictionary<string,Socket> clientsDictionary;
        //监听线程
        public Thread listenThread;
        //接收线程
        public Thread reviveThread;
        //客户端套接字
        public Socket clientSocket;

        //绑定
        public abstract void ServerSetting(string ip, string point, int MaxListener);
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
        public override void ServerSetting(string ipaddress, string point,int MaxListener)
        {
            try
            {
                //配置Socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                //类型转换
                IPAddress ip = IPAddress.Parse(ipaddress);
                IPEndPoint ipendpoint = new IPEndPoint(ip, int.Parse(point));
                //绑定
                serverSocket.Bind(ipendpoint);
                //最大监听数
                serverSocket.Listen(MaxListener);
                //开始监听
                MessageBox.Show("服务器开启,等待连接", "连接成功");
                listenThread = new Thread(Listenning);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"连接失败");
            }
            
        }

        public override void Listenning(object obj)
        {            
            clientSocket = serverSocket.Accept();
            while (clientSocket != null)
            {
                string ipadreess = clientSocket.RemoteEndPoint.ToString();
                clientsDictionary.Add(ipadreess,clientSocket);
                //TODO:此处应该输出信息(IP地址连接上了)
                //并且反馈回给客户端(你已连接)

                //开启接收线程
            }
        }
    }
}
