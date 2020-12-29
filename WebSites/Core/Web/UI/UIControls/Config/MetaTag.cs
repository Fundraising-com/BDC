using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for MetaTags.
	/// </summary>
	[Serializable]
	public class MetaTag {
		private string name;
		private PartnersID partnersIds;
		
		public MetaTag() {
			partnersIds = new PartnersID();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Name".ToLower()) {
					name = child.InnerText;
				} else if(child.Name.ToLower() == "PartnersID".ToLower()) {
					partnersIds.Load(child);
				}
			}
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public PartnersID PartnersIds {
			set { partnersIds = value; }
			get { return partnersIds; }
		}
	}
}
