using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Utils
{
	public class XMLHelper
	{
		public XMLHelper()
		{
		}

		public static XmlNode CreateChild(XmlDocument doc, XmlNode parent, string name)
		{
			XmlNode xmlNodes;
			try
			{
				xmlNodes = parent.AppendChild(doc.CreateElement(name));
			}
			catch
			{
				xmlNodes = null;
			}
			return xmlNodes;
		}

		public static XmlNode FindChild(XmlNode parent, string name)
		{
			XmlNode xmlNodes;
			IEnumerator enumerator = parent.ChildNodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode current = (XmlNode)enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					xmlNodes = current;
					return xmlNodes;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public static List<XmlNode> FindChildrenWithAttribute(XmlNode parent, string attribute_name, string attribute_value)
		{
			List<XmlNode> xmlNodes = new List<XmlNode>();
			foreach (XmlNode childNode in parent.ChildNodes)
			{
				if (XMLHelper.GetAttribute(childNode, attribute_name) != attribute_value)
				{
					continue;
				}
				xmlNodes.Add(childNode);
			}
			return xmlNodes;
		}

		public static XmlNode FindChildWithAttribute(XmlNode parent, string attribute_name, string attribute_value)
		{
			XmlNode xmlNodes;
			IEnumerator enumerator = parent.ChildNodes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlNode current = (XmlNode)enumerator.Current;
					if (XMLHelper.GetAttribute(current, attribute_name) != attribute_value)
					{
						continue;
					}
					xmlNodes = current;
					return xmlNodes;
				}
				return null;
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public static string GetAttribute(XmlNode node, string name)
		{
			string value;
			IEnumerator enumerator = node.Attributes.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					XmlAttribute current = (XmlAttribute)enumerator.Current;
					if (current.Name != name)
					{
						continue;
					}
					value = current.Value;
					return value;
				}
				return "";
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					disposable.Dispose();
				}
			}
		}

		public static bool GetBoolAttribute(XmlNode node, string name)
		{
			return bool.Parse(XMLHelper.GetAttribute(node, name));
		}

		public static int GetIntAttribute(XmlNode node, string name)
		{
			return int.Parse(XMLHelper.GetAttribute(node, name));
		}

		public static XmlDocument LoadSource(string xml)
		{
			XmlDocument xmlDocument;
			try
			{
				XmlReader xmlReader = XmlReader.Create(new StringReader(xml));
				XmlDocument xmlDocument1 = new XmlDocument();
				xmlDocument1.Load(xmlReader);
				xmlDocument = xmlDocument1;
			}
			catch
			{
				xmlDocument = null;
			}
			return xmlDocument;
		}

		public static string NodeText(XmlNode parent, string name)
		{
			XmlNode xmlNodes = XMLHelper.FindChild(parent, name);
			if (xmlNodes == null)
			{
				return "";
			}
			return xmlNodes.InnerText;
		}
	}
}