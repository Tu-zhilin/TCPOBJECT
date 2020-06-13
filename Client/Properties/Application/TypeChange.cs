using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    
}
