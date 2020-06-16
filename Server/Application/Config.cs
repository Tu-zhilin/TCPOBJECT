using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

namespace Server
{
    public class Config
    {
        public class Data 
        {
            public string _Name;
            public string _Version;
            public string _SoftName;
        }

        XmlTextReader reader;
        XmlDocument xmlDoc;
        string Path;

        public Config(string path)
        {
            Path = path + ".xml";

            xmlDoc = new XmlDocument();

            if (!File.Exists(Path))
            {
                CreatXMLFile();
            }
        }

        //删除文件
        public void Deleted()
        {
            if (File.Exists(Path))
                File.Delete(Path);
        }

        //清空数据
        public void Clear()
        {
            Deleted();
            CreatXMLFile();
        }

        //创建xml文件,创建完毕后直接关闭
        public void CreatXMLFile()
        {
            if (File.Exists(Path))
                return;

            XmlDeclaration dec = xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null);
            xmlDoc.AppendChild(dec);
            XmlElement root = xmlDoc.CreateElement("ProductList");
            xmlDoc.AppendChild(root);
            Save();
        }

        //修改节点(1)代表修改名字  (2)代表修改版本号
        public bool ChangeChildNode(string Name, string Version, string SoftName)
        {
            try
            {
                xmlDoc.Load(Path);
                var root = xmlDoc.DocumentElement;
                foreach (XmlNode item in root.ChildNodes)
                {
                    if (item.Attributes["Name"].Value == Name)
                    {
                        item.Attributes["Version"].Value = Version;
                        item.Attributes["SoftName"].Value = SoftName;
                        break;
                    }
                }
                xmlDoc.Save(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //删除节点
        public bool DeleChileNode(string Name)
        {
            try
            {
                xmlDoc.Load(Path);
                var root = xmlDoc.DocumentElement;
                foreach (XmlNode item in root.ChildNodes)
                {
                    if (item.Attributes["Name"].Value == Name)
                    {
                        root.RemoveChild(item);
                        break;
                    }
                }
                xmlDoc.Save(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //插入节点
        public bool AddChileNode(string Name, string Version,string SoftName)
        {
            try
            {
                xmlDoc.Load(Path);
                XmlElement signal = xmlDoc.CreateElement("Product");
                signal.SetAttribute("Name", Name);
                signal.SetAttribute("Version", Version);
                signal.SetAttribute("SoftName", SoftName);
                xmlDoc.DocumentElement.AppendChild(signal);
                xmlDoc.Save(Path);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //遍历节点
        public List<Data> ReadNode()
        {
            List<Data> list = new List<Data>();

            if (File.Exists(Path))
                reader = new XmlTextReader(Path);
            else
                return null;

            while (reader.Read())
            {
                if (reader.Name == "Product")
                {
                    Data data = new Data();
                    data._Name = reader.GetAttribute("Name");
                    data._Version = reader.GetAttribute("Version");
                    data._SoftName = reader.GetAttribute("SoftName");
                    list.Add(data);
                }
            }
            reader.Close();
            return list;
        }

        //保存
        public void Save()
        {
            xmlDoc.Save(Path);
        }
    }
}
