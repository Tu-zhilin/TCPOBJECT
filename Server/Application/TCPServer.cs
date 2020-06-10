using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Data;
using Template;
using System.ComponentModel;

namespace Server
{
    #region 服务端抽象类

    public abstract class TCPServer
    {
        public class ProductInfo
        {
            //客户端套接字
            public Socket clientSocket;
            //产品名称
            public string PdtName;
            //版本信息
            public string pdtVer;
        }

        //服务端套接字
        public Socket serverSocket;
        //客户端套接字集合
        public Dictionary<string, ProductInfo> clientsDictionary;
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
        //Listview
        public ListView listview;

        public TCPServer(ListView listview)
        {            
            GetIPandPort();
            this.listview = listview;
            clientsDictionary = new Dictionary<string, ProductInfo>();
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
        //发送更新提示
        public abstract void SendUpdataInfo(string ipaddress,string msg);
    }

    #endregion

    /// <summary>
    /// 测试类
    /// </summary>
    public class TestService : TCPServer
    {
        public TestService(ListView listview) : base(listview)
        {

        }

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
                ProductInfo productInfo = new ProductInfo();
                productInfo.clientSocket = clientSocket;
                string ipadreess = clientSocket.RemoteEndPoint.ToString();
                clientsDictionary.Add(ipadreess, productInfo);
                info.Enqueue(ipadreess + "已连接");
                ListviewOper.Add_Address(this.listview, ipadreess);
                //开启接收线程
                Thread thread = new Thread(new ParameterizedThreadStart(Receiving));
                thread.Start(clientSocket);
            }
        }
        //接收
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
                    info.Enqueue(string.Format("客户端{0}退出:{1}返回值{2}", ipadreess,ex.Message,length));
                    clientsDictionary.Remove(ipadreess);
                    ListviewOper.Dele_Info(listview, ipadreess);
                    return;
                }

                if (length == 0 || length == -1)
                {
                    info.Enqueue(string.Format("客户端{0}退出:返回值{1}",ipadreess,length));
                    clientsDictionary.Remove(ipadreess);
                    ListviewOper.Dele_Info(listview,ipadreess);
                    return;
                }

                else if (length == -1)
                {
                    info.Enqueue(ipadreess + "异常退出");
                    return;
                }

                else
                {
                    string str = TypeChange.GetASCII(revBuffer[0]);
                    switch (str)
                    {
                        //客户端的更新请求
                        case "R":
                            //TODO:发送响应的文件过去
                            info.Enqueue("发送文件过去");
                            break;
                        //接收软件信息
                        case "H":
                            {
                                //存储产品名和软件版本
                                clientsDictionary[ipadreess].PdtName = Encoding.Default.GetString(revBuffer, 2, revBuffer[1] - 2);                          
                                clientsDictionary[ipadreess].pdtVer = Encoding.Default.GetString(revBuffer, revBuffer[1], length);                      
                                //添加进Listview
                                ListviewOper.Change_Info(listview,ipadreess, clientsDictionary[ipadreess].PdtName, clientsDictionary[ipadreess].pdtVer);
                                //进行比对版本
                                //TODO：如果版本一致，回复无需更新，如果不一致，发送需要更新
                                SendUpdataInfo(ipadreess, "有最新版本,是否更新");
                            }
                            break;
                        //Message
                        case "M":
                            info.Enqueue(Encoding.Default.GetString(revBuffer, 1, length));
                            break;
                    }
                }
            }
        }
        //发送更新信息
        public override void SendUpdataInfo(string ipaddress, string msg)
        {
            try
            {
                msg = "T" + msg;
                clientsDictionary[ipaddress].clientSocket.Send(Encoding.Default.GetBytes(msg));
            }
            catch (Exception ex)
            {
                info.Enqueue(ex.Message);
            }
        }
        //发送文字
        public override void SendMsg(string ipaddress, string msg)
        {
            try
            {
                msg = "M" + msg;
                clientsDictionary[ipaddress].clientSocket.Send(Encoding.Default.GetBytes(msg));
            }
            catch (Exception ex)
            {
                info.Enqueue(ex.Message);
            }
        }
        //发送文件
        public override void SendFile(string ipaddress)
        {
            string filePath = null;
            string fileName = null;
            OpenFileDialog openFile = new OpenFileDialog();
            //选择文件
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                filePath = openFile.FileName;
                fileName = openFile.SafeFileName;
            }
            //读取文件
            try
            {
                FileStream fsRead = new FileStream(filePath, FileMode.Open);
                int datalength = (int)fsRead.Length;
                byte[] buffer = new byte[datalength];
                int len = fsRead.Read(buffer, 0, datalength);
                //确保读取全部字节
                if (!(len == datalength))
                {
                    info.Enqueue("读取文件异常");
                    return;
                }
                //拼接发送类型 头字节 文件名字 数据
                byte[] dataFileName = Encoding.Default.GetBytes(fileName);
                int dataHeadLen = dataFileName.Length + 2;
                byte[] data = new byte[dataHeadLen + datalength];
                byte[] type = Encoding.Default.GetBytes("F");
                type.CopyTo(data,0);
                data[1] = (byte)dataHeadLen;
                dataFileName.CopyTo(data,2);
                buffer.CopyTo(data, dataHeadLen);
                //发送
                len = clientsDictionary[ipaddress].clientSocket.Send(data);
                info.Enqueue(string.Format("发送文件：返回值{0}", len));
                //关闭
                fsRead.Close();
            }
            catch(Exception ex)
            {
                info.Enqueue(ex.Message);
            }
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
        public abstract bool Connect(string ip,int port);
        //接受
        public abstract void Reciving(object obj);
        //发送文本
        public abstract void SendMsg(string msg);
        //发送软件信息,版本
        public abstract void SendPdtInfo(string pdtName,string Version);
        //更新请求
        public abstract void SendReq(string msg);
    }

    #endregion

    /// <summary>
    /// 测试类
    /// </summary>
    public class TestClient : TCPClient
    {
        public override bool Connect(string ip, int port)
        {
            IPAddress this_ip = IPAddress.Parse(ip);

            if (tcpClient != null && tcpClient.Connected)
            {
                MessageBox.Show("已存在,断开连接，请重新连接");
                tcpClient.Close();
                tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                return false;
            }

            try
            {
                tcpClient.Connect(this_ip, port);
                IpEndPort = tcpClient.RemoteEndPoint.ToString();
                revThread = new Thread(new ParameterizedThreadStart(Reciving));
                revThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message+" 连接失败");
                return false;
            }
            return true;
        }
        //发送产品名称和版本
        public override void SendPdtInfo(string pdtName, string Version)
        {
            byte[] type = Encoding.Default.GetBytes("H");
            byte[] name = Encoding.Default.GetBytes(pdtName);
            byte[] version = Encoding.Default.GetBytes(Version);
            byte[] sendData = new byte[2+name.Length+version.Length];
            int len = name.Length + 2;
            type.CopyTo(sendData,0);
            sendData[1] = (byte)len;
            name.CopyTo(sendData, 2);
            version.CopyTo(sendData,len);
            tcpClient.Send(sendData);
        }
        //发送文本
        public override void SendMsg(string msg)
        {
            tcpClient.Send(Encoding.Default.GetBytes("M"+msg));
        }

        //发送更新请求
        public override void SendReq(string msg)
        {
            tcpClient.Send(Encoding.Default.GetBytes("R" + msg));
        }

        //接受
        public override void Reciving(object obj)
        {
            while (true)
            {
                int length = 0;
                byte[] revBuffer = new byte[40000000];
                try
                {
                    //Recive函数会阻塞,直到收到信息为止
                    length = tcpClient.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                }
                catch (Exception ex)
                {
                    info.Enqueue(ex.Message + "服务器异常退出");
                    return;
                }

                if (length == 0 || length == -1)
                {
                    info.Enqueue(string.Format("服务器退出:返回值{0}",length));
                    return;
                }

                else
                {
                    string str = TypeChange.GetASCII(revBuffer[0]);
                    switch (str)
                    {
                        //客户端的更新提示
                        case "T":
                            {
                                string updataInfo = Encoding.Default.GetString(revBuffer, 1, length-1);
                                if (updataInfo == "有最新版本,是否更新")
                                {
                                    if (MessageBox.Show(updataInfo, "更新提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                                    {
                                        SendReq("请求下载最新版本软件");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("已是最新版本，无需更新","更新提示");
                                }
                            }
                            break;
                        //Message
                        case "M":
                            info.Enqueue(Encoding.Default.GetString(revBuffer, 1, length));
                            break;
                        //File
                        case "F":
                            {

                                string pathName = Encoding.Default.GetString(revBuffer,2,revBuffer[1]-2);
                                info.Enqueue(pathName);
                                if (File.Exists(pathName))
                                    File.Delete(pathName);
                                FileStream fswrite = new FileStream(pathName,FileMode.Append);

                                fswrite.Write(revBuffer, revBuffer[1], length - revBuffer[1]);

                                fswrite.Close();
                            }                             
                            break;
                    }                    
                }
            }
        }
    }
}
