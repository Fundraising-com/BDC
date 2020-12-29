using System;
using System.Xml;

namespace GA.BDC.Core.Email.MassMail
{
	/// <summary>
	/// Summary description for ExtraInfo.
	/// </summary>
	public class ExtraInfo
	{
		private XmlDocument xmlDocument;

		public ExtraInfo(){
			xmlDocument = new XmlDocument();
			xmlDocument.PreserveWhitespace = true;

			XmlNode tempXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, "ExtraInfo", "");
			xmlDocument.AppendChild(tempXmlNode);
		}

		public void LoadFromXmlString(string xmlString){
			if(xmlString != null && xmlString.Trim() != String.Empty){
                xmlDocument.LoadXml(xmlString);
			}
		}

		public void Add(string name, string value){
			XmlNode tempXmlNode = xmlDocument.CreateNode(XmlNodeType.Element, name, "");
			tempXmlNode.InnerText = value;
			xmlDocument.DocumentElement.AppendChild(tempXmlNode);
		}

		public string GetPostString(){
			XmlNodeList xnl = xmlDocument.FirstChild.ChildNodes;
			string s = "";
			foreach(XmlNode xn in xnl){
				if(xn.NodeType.ToString() == "Element") {
					s += "&" + xn.Name + "=" + xn.InnerText;
				}
			}
			return s;
		}
		
		public string GetXmlString(){
			return xmlDocument.InnerXml;
		}
	}
}
