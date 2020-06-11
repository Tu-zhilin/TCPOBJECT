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
        public enum Type
        {
            T = 84,
            R = 82,
            H = 72,
            M = 77,
            F = 70
        }
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
        //产品信息存储
        public Dictionary<string, string> softDic;

        public TCPServer(ListView listview)
        {
            softDic = new Dictionary<string, string>();
            GetIPandPort();
            this.listview = listview;
            clientsDictionary = new Dictionary<string, ProductInfo>();
            info = new Queue<string>();
        }

        //获取本地ip/port
        public virtual void GetIPandPort()
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
        //自动发送文件
        public abstract void SendFile(string ipaddress,string path);
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

        //连接配置
        public override void ServerSetting(string ipaddress, int point, int MaxListener)
        {
            if (serverSocket != null && serverSocket.Connected)
            {
                MessageBox.Show("服务器已打开");
                //serverSocket.Close();
                //serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
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
                listenThread.IsBackground = true;
                listenThread.TrySetApartmentState(ApartmentState.STA);
                listenThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "连接失败");
            }

        }

        //监听
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
                thread.IsBackground = true;
                thread.TrySetApartmentState(ApartmentState.STA);
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
                            {
                                info.Enqueue("发送文件过去");
                                string PdtName = clientsDictionary[ipadreess].PdtName;
                                string PdtVer = clientsDictionary[ipadreess].pdtVer;
                                //拼接产品地址
                                SendFile(ipadreess);
                            }                         
                            break;
                        //接收软件信息
                        case "H":
                            {
                                try
                                {
                                    //存储产品名和软件版本
                                    clientsDictionary[ipadreess].PdtName = Encoding.Default.GetString(revBuffer, 2, revBuffer[1] - 2);
                                    clientsDictionary[ipadreess].pdtVer = Encoding.Default.GetString(revBuffer, revBuffer[1], length - revBuffer[1]);
                                    //添加进Listview
                                    ListviewOper.Change_Info(listview, ipadreess, clientsDictionary[ipadreess].PdtName, clientsDictionary[ipadreess].pdtVer);
                                    //TODO：如果版本一致，回复无需更新，如果不一致，发送需要更新
                                    if (clientsDictionary[ipadreess].pdtVer != softDic[clientsDictionary[ipadreess].PdtName])
                                        SendUpdataInfo(ipadreess, "有最新版本,是否更新");
                                    else

                                        SendUpdataInfo(ipadreess, "已是最新版本");
                                }
                                catch (Exception ex)
                                {
                                    info.Enqueue(ex.Message);
                                }
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

        //自动选择文件发送
        public override void SendFile(string ipaddress, string path)
        {
            //这里需要跟下边抽出一个相同的函数使用
        }

        //发送文件
        public override void SendFile(string ipaddress)
        {
            //发送缓存大小
            int SendBufferLength = 65535;
            //每次发送字节  开头两个字节用于校验
            int SendLength = SendBufferLength - 2;
            //发送的字节块
            int DataBlock = 0;
            //文件地址
            string filePath = null;
            //文件名字
            string fileName = null;
            //接收ACK缓存
            byte[] revBuffer = new byte[512];
            //发送数据缓存
            byte[] sendBuffer = new byte[SendLength];
            //文件数据缓存
            byte[] buffer;
            //文件流
            FileStream fsRead;
            //文件窗体对象
            OpenFileDialog openFile = new OpenFileDialog();

            try
            {
                //选择文件
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    filePath = openFile.FileName;
                    fileName = openFile.SafeFileName;
                }
                //读取文件
                fsRead = new FileStream(filePath, FileMode.Open);
                //数据长度
                int datalength = (int)fsRead.Length;
                buffer = new byte[datalength];
                //将数据读取到Buffer缓存里面
                int len = fsRead.Read(buffer, 0, datalength);
                //确保读取全部字节
                if (!(len == datalength))
                {
                    info.Enqueue("读取文件异常");
                    return;
                }
                //1.拼接发送类型 数据块个数 文件名字 
                //2.然后发送数据块等待客户端回应ACK,校验
                //3.处理超时等待

                //文件名字
                byte[] dataFileName = Encoding.Default.GetBytes(fileName);
                //计算数据块
                if (datalength % (SendLength) == 0)
                    DataBlock = datalength / (SendLength);
                else
                    DataBlock = datalength / (SendLength) + 1;
                //发送类型+数据块
                //TOdo:这里代码块要加大
                byte[] data = new byte[2 + dataFileName.Length];
                data[0] = (byte)Type.F;
                data[1] = (byte)DataBlock;
                dataFileName.CopyTo(data, 2);
                len = clientsDictionary[ipaddress].clientSocket.Send(data);
                //设定一个跟踪标识
                int number = 0;
                //算上头消息的应答
                DataBlock++;
                //发送数据块
                while (DataBlock != 0)
                {
                    //清空缓存
                    revBuffer = new byte[512];
                    //等待应答
                    clientsDictionary[ipaddress].clientSocket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                    //应答成功
                    info.Enqueue(string.Format("接收应答:{0}", revBuffer[0]));
                    if (revBuffer[0] == 1)
                    {
                        //应答成功了代码块-1;
                        DataBlock--;
                        if (DataBlock > 0)
                        {
                            //当数据块>=1024*1024的时候
                            if (datalength >= SendLength)
                            {
                                //清空Buffer
                                sendBuffer = new byte[SendBufferLength];
                                Array.Copy(buffer, number, sendBuffer, 2, SendLength);
                                //高8位
                                sendBuffer[0] = (byte)(SendBufferLength >> 8);
                                //低8位
                                sendBuffer[1] = (byte)(SendBufferLength & 0xFF);
                                len = clientsDictionary[ipaddress].clientSocket.Send(sendBuffer);
                                info.Enqueue(string.Format("尝试发送字节:{0} 代码块:{1}", SendLength, DataBlock));
                                number += (SendLength);
                                datalength -= (SendLength);
                                //DataBlock--;
                            }
                            //当数据块<1024*1024的时候
                            else
                            {
                                //清空Buffer
                                sendBuffer = new byte[datalength + 2];
                                Array.Copy(buffer, number, sendBuffer, 2, datalength);
                                //高8位
                                sendBuffer[0] = (byte)((datalength + 2) >> 8);
                                //低8位
                                sendBuffer[1] = (byte)((datalength + 2) & 0xFF);
                                len = clientsDictionary[ipaddress].clientSocket.Send(sendBuffer);
                                info.Enqueue(string.Format("尝试发送字节:{0} 代码块:{1}", datalength + 2, DataBlock));
                                number += datalength;
                                datalength -= datalength;
                                //DataBlock--;
                            }
                        }
                    }
                    //应答失败 再次发送数据
                    else
                    {
                        len = clientsDictionary[ipaddress].clientSocket.Send(sendBuffer);
                        info.Enqueue(string.Format("发送失败,再次发送数据"));
                    }
                }
                info.Enqueue(string.Format("发送完成"));
                fsRead.Close();
            }
            catch (Exception ex)
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
                revThread.IsBackground = true;
                revThread.TrySetApartmentState(ApartmentState.STA);
                revThread.Start();
                info.Enqueue("连接成功");
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
                byte[] revBuffer = new byte[65535];
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
                                    else
                                    {
                                        tcpClient.Close();
                                        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                                    }
                                }//
                                else
                                {
                                    MessageBox.Show("已是最新版本，无需更新","更新提示");
                                    tcpClient.Close();
                                    tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
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
                                string pathName = Encoding.Default.GetString(revBuffer,2,length - 2);
                                info.Enqueue(pathName);
                                if (File.Exists(pathName))
                                    File.Delete(pathName);
                                FileStream fswrite = new FileStream(pathName,FileMode.Append);
                                int DataBlock = revBuffer[1];
                                //发送1请求数据开始传送
                                tcpClient.Send(new byte[1] {(byte)1});
                                while (DataBlock != 0)
                                {
                                    length = tcpClient.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                                    //数据字节匹配(后面应该进一步改成校验码)
                                    if (length == (int)((revBuffer[0] << 8) + revBuffer[1]))
                                    {
                                        fswrite.Write(revBuffer, 2, length - 2);
                                        tcpClient.Send(new byte[1] { (byte)1 });
                                        info.Enqueue(string.Format(string.Format("接收代码块成功:{0}", DataBlock)));
                                        DataBlock--;
                                    }
                                    else
                                    {
                                        tcpClient.Send(new byte[1] { (byte)0 });
                                        info.Enqueue(string.Format(string.Format("接收代码块失败:{0}", DataBlock)));
                                    }
                                }
                                info.Enqueue("接收成功");
                                fswrite.Close();
                            }
                            tcpClient.Close();
                            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                            break;
                    }                    
                }
            }
        }
    }
}
