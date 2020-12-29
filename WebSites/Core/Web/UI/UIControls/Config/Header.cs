using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for Header.
	/// </summary>
	[Serializable]
	public class Header {
		private MetaTags metaTags;

		public Header() {
			metaTags = new MetaTags();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "MetaTags".ToLower()) {
					metaTags.Load(child);
				}
			}
		}

		public MetaTags MetaTags {
			set { metaTags = value; }
			get { return metaTags; }
		}
	}
}
