using System;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for PartnerID.
	/// </summary>
	[Serializable]
	public class PartnerID {
		private string id;
		private string partnerType = "";
		private Cultures cultures;

		public PartnerID() {
			cultures = new Cultures();
		}

		public void SetPartnerType(string _partnerType) {
			partnerType = _partnerType.ToLower();
		}

		public string GetPartnerType() {
			return partnerType;
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					id = child.InnerText;
				} else if(child.Name.ToLower() == "Cultures".ToLower()) {
					cultures.Load(child);
				}
			}
		}

		public string ID {
			set { id = value; }
			get { return id; }
		}

		public Cultures Cultures {
			set { cultures = value; }
			get { return cultures; }
		}
	}
}
