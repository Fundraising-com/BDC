// SupportedCultures.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class SupportedCultures {

		private ArrayList supportedculturelist = new ArrayList();

		public SupportedCultures() {

		}

		public SupportedCulture GetSupportedCultureByID(string childName) {
			foreach(SupportedCulture supportedculture in supportedculturelist) {
				if(supportedculture.ID.ToLower() == childName.ToLower()) {
					return supportedculture;
				}
			}
			return null;
		}


		public SupportedCulture GetSupportedCultureByName(string childName) {
			foreach(SupportedCulture supportedculture in supportedculturelist) {
				if(supportedculture.Name.ToLower() == childName.ToLower()) {
					return supportedculture;
				}
			}
			return null;
		}


		public void LoadSupportedCultures(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "SupportedCulture".ToLower()) {
					SupportedCulture supportedculture = new SupportedCulture();
					supportedculture.LoadSupportedCulture(child);
					AddSupportedCulture(supportedculture);
				}
			}
		}

		public void AddSupportedCulture(SupportedCulture supportedculture) {
			SupportedCultureList.Add(supportedculture);
		}

		public ArrayList SupportedCultureList {
			set { supportedculturelist = value; }
			get { return supportedculturelist; }
		}

	}
}





// -----------------------



