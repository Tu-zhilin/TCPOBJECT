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

    /// <summary>
    /// 数据转换
    /// </summary>
    public class TypeChange
    {
        //ASCII码转换
        public static string GetASCII(byte data)
        {
            byte[] newdata = new byte[1];
            newdata[0] = (byte)(Convert.ToInt32(data));
            return Encoding.ASCII.GetString(newdata);
        }
    }

    public class MyFile
    {
        /// <summary>
        /// 读取文件大小,名字，地址,文件数据缓存
        /// </summary>
        public static bool GetFileData(ref string FileName,ref string FilePath,ref int Size,ref byte[] buffer)
        {
            //文件窗体对象
            OpenFileDialog openFile = new OpenFileDialog();
            //文件流
            FileStream fsRead;

            //选择文件
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                FilePath = openFile.FileName;
                FileName = openFile.SafeFileName;
            }

            if (FilePath == null || FilePath == "")
                return false;
            //读取文件
            fsRead = new FileStream(FilePath, FileMode.Open);
            //数据长度
            Size = (int)fsRead.Length;
            buffer = new byte[Size];
            //将数据读取到Buffer缓存里面
            int len = fsRead.Read(buffer, 0, Size);
            //确保读取全部字节
            if (!(len == Size))
            {
                fsRead.Close();
                return false;
            }
            fsRead.Close();
            return true;
        }

        /// <summary>
        /// 读取文件大小,名字，地址,文件数据缓存
        /// </summary>
        public static bool GetFileData(ref string FileName, ref string FilePath, ref int Size, ref byte[] buffer,string FilefullPath,string Name)
        {
            //文件流
            FileStream fsRead;

            FilePath = FilefullPath;
            FileName = Name;
            //读取文件
            if (FilePath == null || FilePath == "" || !File.Exists(FilePath))
            {
                return false;
            }
            fsRead = new FileStream(FilePath, FileMode.Open);
            //数据长度
            Size = (int)fsRead.Length;
            buffer = new byte[Size];
            //将数据读取到Buffer缓存里面
            int len = fsRead.Read(buffer, 0, Size);
            //确保读取全部字节
            if (!(len == Size))
            {
                fsRead.Close();
                return false;
            }
            fsRead.Close();
            return true;
        }
    }
}
