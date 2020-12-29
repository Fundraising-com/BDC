// SupportedCulture.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class SupportedCulture {
		private string id;
		private string name;

		public SupportedCulture() {

		}

		public void LoadSupportedCulture(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					ID = child.InnerText;
				}else if(child.Name.ToLower() == "Name".ToLower()) {
					Name = child.InnerText;
				}
			}
		}

		public string ID {
			set { id = value; }
			get { return id; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}
	}
}





// -----------------------



