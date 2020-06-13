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
        //发送文件的标识
        public enum Type
        {
            T = 84,
            R = 82,
            H = 72,
            M = 77,
            F = 70
        }
        //上位机绑定信息
        public class ProductInfo
        {
            //客户端套接字
            public Socket clientSocket;
            //产品名称
            public string PdtName;
            //版本信息
            public string pdtVer;
        }
        //读取的文件信息
        public struct FileInfo
        {
            //文件地址
            public string FilePath;
            //文件名字
            public string FileName;
            //文件大小
            public int Size;
            //缓存
            public byte[] buffer;
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
        //发送缓存大小
        public int SendBufferLength = (int)0xFFFFFF;
        //每次发送字节  开头三个字节用于校验所以要去掉
        public int SendLength { get { return SendBufferLength - 3; } }
        //表示字节数(F+校验数据字节)
        public int PreDataByte = 4;
        //表示字节数(校验数据字节)
        public int DataByte { get { return PreDataByte - 1; } }



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
        public abstract void SendFile(object obj);
        //发送更新提示
        public abstract void SendUpdataInfo(string ipaddress,string msg);
        //自动发送文件
        public abstract void SendFile(string ipaddress,string path);
        //发送文件 线程开启函数
        public abstract void DoSendFile(string ipaddress);
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
            Socket socket = (Socket)obj;
            string ipadreess = clientSocket.RemoteEndPoint.ToString();

            while (true)
            {
                int length = 0;
                byte[] revBuffer = new byte[512];
                try
                {
                    //Recive函数会阻塞,直到收到信息为止
                    length = socket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                    info.Enqueue(ipadreess + "主接收线程");
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

        //开一个文件发送线程
        public override void DoSendFile(string ipaddress)
        {
            Thread thread = new Thread(new ParameterizedThreadStart(SendFile));
            thread.IsBackground = true;
            thread.TrySetApartmentState(ApartmentState.STA);
            thread.Start(ipaddress);
        }

        //自动选择文件发送
        public override void SendFile(string ipaddress, string path)
        {
            //这里需要跟下边抽出一个相同的函数使用
        }

        //发送文件之前的通知
        public int SendPreData(string ipaddress,string FileName,int Size)
        {
            byte[] data;
            int DataBlock = 0;
            //表示字节数
            int DataByte = this.PreDataByte;
            //文件名字
            byte[] dataFileName = Encoding.Default.GetBytes(FileName);
            //计算数据块
            if (Size % (SendLength) == 0)
                DataBlock = Size / (SendLength);
            else
                DataBlock = Size / (SendLength) + 1;
            //发送类型+数据块
            data = new byte[DataByte + dataFileName.Length];
            data[0] = (byte)Type.F;
            for (int i = 1;i < DataByte;i++)
            {
                data[i] = (byte)((DataBlock >> ((DataByte - 1 - i) * 8)) & 0xFF);
            }
            dataFileName.CopyTo(data, DataByte);
            clientsDictionary[ipaddress].clientSocket.Send(data);
            info.Enqueue("发送数据信息过去");
            return DataBlock;
        }

        //发送文件数据
        public void SendData(string ipaddress,byte[] dataBuffer,int sendLength,ref int number,ref int Size)
        {
            byte[] sendBuffer;
            int DataByte = this.DataByte;
            sendBuffer = new byte[sendLength];
            Array.Copy(dataBuffer, number, sendBuffer, DataByte, sendLength - DataByte);
            //数据长度
            for (int i = 0; i < DataByte; i++)
            {
                sendBuffer[i] = (byte)((sendLength >> ((DataByte - 1 - i) * 8)) & 0xFF);
            }

            clientsDictionary[ipaddress].clientSocket.Send(sendBuffer);
            number += (SendLength);
            Size -= (SendLength);
        }

        //发送文件
        public override void SendFile(object obj)
        {
            string ipaddress = (string)obj;
            //发送的字节块
            int DataBlock = 0;
            //接收ACK缓存
            byte[] revBuffer = new byte[512];
            //发送数据缓存
            byte[] sendBuffer = new byte[SendLength];
            //设定一个跟踪标识
            int number = 0;
            //函数返回值
            int len;
            //文件信息
            FileInfo fileinfo = new FileInfo();

            try
            {  //读取文件
                if (!MyFile.GetFileData(ref fileinfo.FileName, ref fileinfo.FilePath, ref fileinfo.Size, ref fileinfo.buffer))
                {
                    info.Enqueue("文件读取失败");
                    return;
                }

                DataBlock = SendPreData(ipaddress, fileinfo.FileName, fileinfo.Size);

                //算上头消息的应答
                DataBlock++;

                //发送数据块
                while (DataBlock != 0)
                {
                    //清空缓存
                    revBuffer = new byte[512];
                    //等待应答
                    len = clientsDictionary[ipaddress].clientSocket.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                    //防止速度太快
                    Thread.Sleep(5);
                    if (len <= 0)
                    {
                        info.Enqueue("客户端 " + ipaddress + " 退出");
                        return;
                    }
                    if (revBuffer[0] == 1)
                    {
                        //应答成功了代码块-1;
                        DataBlock--;
                        if (DataBlock > 0)
                        {
                            //当数据块>=1024*1024的时候
                            if (fileinfo.Size >= SendLength)
                            {
                                SendData(ipaddress, fileinfo.buffer, SendBufferLength, ref number, ref fileinfo.Size);
                            }
                            //当数据块<1024*1024的时候
                            else
                            {
                                SendData(ipaddress, fileinfo.buffer, fileinfo.Size + 3, ref number, ref fileinfo.Size);
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
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }



    #region 客户端抽象类 

    public abstract class TCPClient
    {
        //发送类型
        public enum Type
        {
            T = 84,
            R = 82,
            H = 72,
            M = 77,
            F = 70
        }

        //客户端套接字
        public Socket tcpClient;
        //接收
        public Thread revThread;
        //信息
        public Queue<string> info;
        //地址端口号
        public string IpEndPort;
        //委托
        public delegate void Action(int data);
        //每次最多接收的字节
        public int RecvDataByte = (int)0xFFFFFF;
        //表示字节数(F+校验数据字节)
        public int PreDataByte = 4;
        //表示字节数(校验数据字节)
        public int DataByte { get { return PreDataByte - 1; } }

        public event Action ChangeHandle;

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
            //byte[] type = Encoding.Default.GetBytes("H");
            byte[] name = Encoding.Default.GetBytes(pdtName);
            byte[] version = Encoding.Default.GetBytes(Version);
            byte[] sendData = new byte[2+name.Length+version.Length];
            int len = name.Length + 2;
            //type.CopyTo(sendData,0);
            sendData[0] = (byte)Type.H;
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
            byte[] revBuffer;
            while (true)
            {
                int length = 0;
                revBuffer = new byte[RecvDataByte];
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
                                }
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
                                try
                                {
                                    int len = 0;
                                    int DataBlock = 0;
                                    string pathName = Encoding.Default.GetString(revBuffer, PreDataByte, length - PreDataByte);
                                    info.Enqueue(pathName);
                                    if (File.Exists(pathName))
                                        File.Delete(pathName);
                                    FileStream fswrite = new FileStream(pathName, FileMode.Append);
                                    for (int i = 1; i < PreDataByte; i++)
                                    {
                                        DataBlock += revBuffer[i] << ((PreDataByte - 1 - i) * 8);
                                    }
                                    //发送1请求数据开始传送
                                    tcpClient.Send(new byte[1] { (byte)1 });
                                    info.Enqueue("发送应答1");

                                    //Download formDownLoad = new Download(DataBlock);
                                    //formDownLoad.Show();
                                    //Thread.Sleep(2000);

                                    while (DataBlock != 0)
                                    {
                                        len = 0;
                                        length = tcpClient.Receive(revBuffer, revBuffer.Length, SocketFlags.None);
                                        //防止速度太快
                                        Thread.Sleep(5);
                                        if (length <= 0)
                                        {
                                            info.Enqueue("服务器退出");
                                            return;
                                        }
                                        //数据字节匹配(后面应该进一步改成校验码)
                                        for (int i = 0; i < DataByte; i++)
                                        {
                                            len += revBuffer[i] << ((DataByte - 1 - i) * 8);
                                        }

                                        if (length == len)
                                        {
                                            fswrite.Write(revBuffer, DataByte, length - DataByte);
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
                                catch (Exception ex)
                                {
                                    throw new Exception(ex.Message);
                                }
                                finally
                                {
                                    tcpClient.Close();
                                    tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
                                }
                            }
                            break;
                    }                    
                }
            }
        }
    }
}
