using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace HPE.Kruta.Common.Utils
{
    /// <summary>
    /// Utilities for building an XmlDocument
    /// <remarks>Ed Hastings</remarks>
    /// </summary>
    public static class XmlUtils
    {
        public static void AddXmlDeclaration(this XmlDocument xmlDoc)
        {
            var xmlNode = xmlDoc.CreateNode(XmlNodeType.XmlDeclaration, "", "");
            xmlDoc.AppendChild(xmlNode);
        }

        public static XmlElement AddXmlParentElement(this XmlDocument xmlDoc, string elementName)
        {
            var xmlElem = AddNode(xmlDoc, elementName);
            return xmlElem;
        }

        public static void AddNode(this XmlElement parent, string tag, int val)
        {
            var child = parent.OwnerDocument.CreateElement("", tag, "");
            child.AppendChild(parent.OwnerDocument.CreateTextNode(val.ToString()));
            parent.AppendChild(child);
        }
        public static void AddNode(this XmlElement parent, string tag, string val)
        {
            var child = parent.OwnerDocument.CreateElement("", tag, "");
            child.AppendChild(parent.OwnerDocument.CreateTextNode(val));
            parent.AppendChild(child);
        }
        public static void AddNode(this XmlElement parent, ref XmlElement child)
        {
            parent.AppendChild(child);
        }
        public static XmlElement AddNode(this XmlDocument x, string tag)
        {
            return x.CreateElement("", tag, "");
        }
        public static void AddNodes(this XmlElement parent, XmlNodeList children)
        {
            foreach (XmlNode child in children)
                parent.AppendChild(child);
        }
        public static void AddNode(this XmlElement parent, XmlNode child)
        {
            var child2 = parent.OwnerDocument.CreateElement("", child.Name, "");
            child2.InnerXml = child.InnerXml;
            parent.AppendChild(child2);
        }
        public static void AddAttribute(this XmlElement node, string name, string value)
        {
            var att = node.OwnerDocument.CreateAttribute(name);
            att.Value = value;
            node.Attributes.Append(att);
        }
        public static string GetNamedAttribute(this XmlNode columnNode, string name, string defaultValue)
        {
            if (columnNode.Attributes.GetNamedItem(name) != null)
                return columnNode.Attributes.GetNamedItem(name).Value;
            return defaultValue;
        }

        public static XmlDocument GetXmlDocumentFromFile(string filePath)
        {
            var xd = new XmlDocument();
            xd.Load(filePath);
            return xd;
        }


        public static bool GetBoolAttribute(this XmlNode xmlNode, string attribute)
        {
            try
            {
                return Convert.ToBoolean(xmlNode.Attributes.GetNamedItem(attribute).Value.ToString());
            }
            catch
            {
                return false;
            }
        }
        public static string GetStringAttribute(this XmlNode xmlNode, string attribute)
        {
            try
            {
                return xmlNode.Attributes.GetNamedItem(attribute).Value.ToString();
            }
            catch
            {
                return "";
            }
        }
        public static int GetIntAttribute(this XmlNode xmlNode, string attribute)
        {
            try
            {
                return Convert.ToInt32(xmlNode.Attributes.GetNamedItem(attribute).Value.ToString());
            }
            catch
            {
                return 0;
            }
        }


        public static bool TryGetInnerText(this XmlAttribute attribute, out string innerText)
        {
            if (attribute != null)
            {
                innerText = attribute.InnerText;
                return true;
            }
            innerText = null;
            return false;
        }
    }
}
