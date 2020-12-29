using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for MetaTags.
	/// </summary>
	[Serializable, XmlInclude(typeof(MetaTag))]
	public class MetaTags {
		private ArrayList metaTagList;
		
		public MetaTags() {
			metaTagList = new ArrayList();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "MetaTag".ToLower()) {
					MetaTag metaTag = new MetaTag();
					metaTag.Load(child);
					metaTagList.Add(metaTag);
				}
			}
		}

		public ArrayList MetaTagList {
			set { metaTagList = value; }
			get { return metaTagList; }
		}
	}
}
