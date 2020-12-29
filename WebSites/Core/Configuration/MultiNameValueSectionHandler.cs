//
// 2005-07-12 - Stephen Lim - New class.
//

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.Xml;
using GA.BDC.Core.Collections;

namespace GA.BDC.Core.Configuration
{
	public class MultiNameValueSectionHandler: IConfigurationSectionHandler
	{
		public object Create(object parent, object context, XmlNode section)
		{
			MultiNameValueCollection col = new MultiNameValueCollection();

			foreach (XmlNode xmlNode in section.ChildNodes)
			{
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					switch (xmlNode.Name)
					{
						case "add":
							NameValueCollection nvCol = new NameValueCollection();

							// Add each attribute into our NameValueCollection
							for (int i = 0; i < xmlNode.Attributes.Count; i++)
							{
								XmlAttribute att = xmlNode.Attributes[i];
								nvCol.Add(att.Name, att.Value);
							}
							col.Add(xmlNode.Attributes["key"].Value, nvCol);
							break;
						case "remove":
							col.Remove(xmlNode.Attributes["key"].Value);
							break;
						case "clear":
							col.Clear();
							break;
					}
				}
			}
			return col;
		}
	}
}
