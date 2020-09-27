using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace FileOperate
{
    class Program
    {
        static void Main(string[] args)
        {


        }




        static XmlDocument doc;
        static string path;


        #region CREATE XML
        /// <summary>
        /// CREATE THE XML FILE
        /// </summary>
        /// <param name="path"></param>
        public static void CreateXMLDocument(string rootName)
        {
            //  XmlDocument xmlDoc = new XmlDocument();

            //加入XML的声明段落,<?xml version="1.0" encoding="gb2312"?>
            XmlDeclaration xmlDeclar;
            xmlDeclar = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(xmlDeclar);

            //加入Employees根元素
            XmlElement xmlElement = doc.CreateElement(rootName);
            doc.AppendChild(xmlElement);
            doc.Save(path);//保存的路径
        }
        #endregion

        #region GET CHILDS lIST
        /// <summary>
        /// GET THE PARTICULAR NODE CHILDS
        /// </summary>
        /// <param name="path"></param>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public static XmlNodeList GetChildElement(string elementName)
        {
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode(elementName);
            XmlNodeList list = node.ChildNodes;
            return list;
        }
        #endregion

        #region ADD NODE
        /// <summary>
        /// ADD A SPECIAL NODE WITH ATTRIBUTE
        /// </summary>
        /// <param name="parentNodeName">THE SPECIFIED NODE</param>
        /// <param name="addedNode">THE NODE NAME</param>
        /// <param name="key">ATTRIBUTE KEY</param>
        /// <param name="value">ATTRIBUTE VALUE</param>
        public static void AddNode(string parentNodeName, string addedNode, string key, string value, string innerText)
        {
            doc.Load(path);
            XmlNode parentNode = doc.SelectSingleNode(parentNodeName);
            XmlElement ele = doc.CreateElement(addedNode);
            ele.InnerText = innerText;
            ele.SetAttribute(key, value);
            parentNode.AppendChild(ele as XmlNode);
            doc.Save(path);
        }

        public static void AddNode(string parentNodeName, string addedNode)
        {
            doc.Load(path);
            XmlNode parentNode = doc.SelectSingleNode(parentNodeName);
            XmlElement ele = doc.CreateElement(addedNode);
            parentNode.AppendChild(ele as XmlNode);
            doc.Save(path);
        }

        #endregion

        #region UPDATE NODE INTERTEXT
        public static void UpdateNodeContent(string nodeName, string content)
        {
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("//" + nodeName);
            node.InnerText = content;
            doc.Save(path);
        }
        #endregion

        #region UPDATE NODE INTERTEXT
        public static void DeleteNodeContent(string nodeName)
        {
            doc.Load(path);
            XmlNode node = doc.SelectSingleNode("//" + nodeName);
            XmlNode parentNode = node.ParentNode;
            parentNode.RemoveChild(node);
            doc.Save(path);
        }
        #endregion



        


        #region XML INIT
        /// <summary>
        /// INIT THE XML FILE
        /// IGNORE THE ANNOTATION
        /// </summary>
        static Program()
        {
            doc = new XmlDocument();

#if UNITY_EDITOR
                path = Application.dataPath + "/config.xml";
#elif UNITY_IPHONE
                platform="hi，大家好,我是IPHONE平台";  
#elif UNITY_ANDROID
                 path = Application.persistentDataPath + "/config.xml";  
#elif UNITY_STANDALONE_WIN
                 path ="file://+"Application.dataPath + "/config.xml";
#else
            path = "config.xml";
#endif


            //IGNORE THE COMMENTS
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
        }
        #endregion
    }
}
