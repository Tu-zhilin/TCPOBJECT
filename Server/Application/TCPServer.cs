﻿using System;
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
        public abstract void SendMsg(string ipaddress,string msg);
        //发送文件
        public abstract void SendFile(string ipaddress);
    }

    #endregion

    /// <summary>
    /// 测试类
    /// </summary>
    public class TestService : TCPServer
    {
        public override void ServerSetting(string ipaddress, int point, int MaxListener)
        {
            if (serverSocket != null && serverSocket.Connected)
            {
                MessageBox.Show("服务器已打开");
                serverSocket.Close();
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                return;
            }
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
            while (true)
            {
                 clientSocket = serverSocket.Accept();
                string ipadreess = clientSocket.RemoteEndPoint.ToString();
                clientsDictionary.Add(ipadreess, clientSocket);
                info.Enqueue(ipadreess + "已连接");

                //开启接收线程
                Thread thread = new Thread(new ParameterizedThreadStart(Receiving));
                thread.Start(clientSocket);
            }
        }

        public override void Receiving(object obj)
        {
            while (true)
            {
                int length = 0;
                byte[] revBuffer = new byte[512];
                Socket socket = (Socket)obj;
                string ipadreess = clientSocket.RemoteEndPoint.ToString();
                try
                {
                    //Recive函数会阻塞,直到收到信息为止
                    length = socket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    info.Enqueue(ipadreess + "异常退出");
                    clientsDictionary.Remove(ipadreess);
                    return;
                }

                if (length == 0)
                {
                    info.Enqueue(ipadreess + "正常退出");
                    clientsDictionary.Remove(ipadreess);
                    return;
                }

                else if (length == -1)
                {
                    info.Enqueue(ipadreess + "异常退出");
                    return;
                }

                else
                {
                    info.Enqueue(Encoding.Default.GetString(revBuffer, 0, length));
                }
            }
        }

        public override void SendMsg(string ipaddress, string msg)
        {
            clientsDictionary[ipaddress].Send(Encoding.Default.GetBytes(msg));
        }

        public override void SendFile(string ipaddress)
        {
            //打开选择文件
            //传输到对应的客户端
        }
    }

    #region 客户端抽象类 

    public abstract class TCPClient
    {
        //客户端套接字
        public Socket tcpClient;
        //接收
        public Thread revThread;
        //信息
        public Queue<string> info;
        //地址端口号
        public string IpEndPort;

        public TCPClient()
        {
            info = new Queue<string>();
            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        }

        //连接服务器
        public abstract void Connect(string ip,int port);
        //接受
        public abstract void Reciving(object obj);
        //发送
        public abstract void Send(string msg);
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

            if (tcpClient != null && tcpClient.Connected)
            {
                MessageBox.Show("已存在,断开连接，请重新连接");
                tcpClient.Close();
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                return;
            }

            try
            {
                tcpClient.Connect(this_ip,port);
                IpEndPort = tcpClient.RemoteEndPoint.ToString();
                revThread = new Thread(new ParameterizedThreadStart(Reciving));
                revThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接失败");
            }
        }

        public override void Send(string msg)
        {
            tcpClient.Send(Encoding.Default.GetBytes(msg));
        }

        public override void Reciving(object obj)
        {
            while (true)
            {
                int length = 0;
                byte[] revBuffer = new byte[512];
                try
                {
                    //Recive函数会阻塞,直到收到信息为止
                    length = tcpClient.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    info.Enqueue("服务器异常退出");
                    return;
                }

                if (length == 0)
                {
                    info.Enqueue("服务器正常退出");
                    return;
                }

                else if (length == -1)
                {
                    info.Enqueue("服务器异常退出");
                    return;
                }

                else
                {
                    info.Enqueue(Encoding.Default.GetString(revBuffer, 0, length));
                }
            }
        }
    }
}
