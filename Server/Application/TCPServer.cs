using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
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
        public Dictionary<string, Socket> clientsDictionary;
        //监听线程
        public Thread listenThread;
        //接收线程
        public Thread reviveThread;
        //客户端套接字
        public Socket clientSocket;
        //队列(存放显示信息)
        public Queue<string> info;
        //本地ip
        public string localIP = null;
        //本地端口
        public int localPort;


        public TCPServer()
        {            
            GetIPandPort();
            clientsDictionary = new Dictionary<string, Socket>();
            info = new Queue<string>();
        }

        //获取本地ip/port
        public void GetIPandPort()
        {
            string AddressIP = string.Empty;
            foreach (IPAddress _IPAddress in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    AddressIP = _IPAddress.ToString();
                }
            }
            localIP = AddressIP;

            localPort = 5000;
        }

        //绑定
        public abstract void ServerSetting(string ip, int point, int MaxListener);
        //监听
        public abstract void Listenning(object obj);
        //接收
        public abstract void Receiving(object obj);
        //发送信息
        public abstract void Send(string msg, Socket socket);

    }

    #endregion

    /// <summary>
    /// 测试类
    /// </summary>
    public class TestService : TCPServer
    {
        public override void ServerSetting(string ipaddress, int point, int MaxListener)
        {
            if (serverSocket !=null && serverSocket.Connected)
                return;
            try
            {
                //配置Socket
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                //类型转换
                IPAddress ip = IPAddress.Parse(ipaddress);
                IPEndPoint ipendpoint = new IPEndPoint(ip,point);
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
                MessageBox.Show(ex.Message, "连接失败");
            }

        }

        public override void Listenning(object obj)
        {
            clientSocket = serverSocket.Accept();
            while (clientSocket != null)
            {
                string ipadreess = clientSocket.RemoteEndPoint.ToString();
                clientsDictionary.Add(ipadreess, clientSocket);
                info.Enqueue(ipadreess + "已连接");

                //开启接收线程
                Thread thread = new Thread(new ParameterizedThreadStart(Receiving));
                thread.Start(clientSocket);

                break;
            }
        }

        public override void Receiving(object obj)
        {
            int length = 0;
            byte[] revBuffer = new byte[512];
            Socket socket = (Socket)obj;
            try
            {
                //Recive函数会阻塞,直到收到信息为止
                length = socket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {

            }

            if (length == 0)
            {
                //客户端正常退出
            }

            else if (length == -1)
            {
                //客户端异常退出
            }

            else
            {
                info.Enqueue(Encoding.Default.GetString(revBuffer, 0, length));
            }
        }

        public override void Send(string msg, Socket socket)
        {
            try
            {
                byte[] sendBuffer = Encoding.Default.GetBytes(msg);
                socket.Send(sendBuffer, sendBuffer.Length, SocketFlags.None);
            }
            catch (Exception ex)
            {

            }
        }
    }

    #region 客户端抽象类 

    public abstract class TCPClient
    {
        public TcpClient tcpClient;

        //连接服务器
        public abstract void Connect(string ip,int port);
        //接受
        //发送
    }

    #endregion

    /// <summary>
    /// 测试类
    /// </summary>
    public class TestClient : TCPClient
    {
        public override void Connect(string ip, int port)
        {
            IPAddress this_ip = IPAddress.Parse(ip);
            if (tcpClient != null &&tcpClient.Connected)
            {
                tcpClient.Close();
                return;
            }
            try
            {
                tcpClient = new TcpClient(ip, port);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
