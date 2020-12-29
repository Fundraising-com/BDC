using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for Title.
	/// </summary>
	[Serializable]
	public class Title {
		private PartnersID partnersId;

		public Title() {
			partnersId = new PartnersID();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "PartnersID".ToLower()) {
					partnersId.Load(child);
				}
			}
		}
		
		public PartnersID PartnersId {
			set { partnersId = value; }
			get { return partnersId; }
		}

	}
}
