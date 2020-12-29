// GlobalizerConfig.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class GlobalizerConfig {

		private SupportedCultures supportedcultures = new SupportedCultures();

		private string id;
		private string name;
		private string currentworkingproject;
		private string baseprojectfilename;
		private string productionbaseprojectfilename;
		private string debug;
		private string designDefaultPartnerID;
		private string designDefaultCulture;
		private Contacts contacts = new Contacts();

		public GlobalizerConfig() {

		}

		public void LoadGlobalizerConfig(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					ID = child.InnerText;
				}else if(child.Name.ToLower() == "Name".ToLower()) {
					Name = child.InnerText;
				}else if(child.Name.ToLower() == "CurrentWorkingProject".ToLower()) {
					CurrentWorkingProject = child.InnerText;
				}else if(child.Name.ToLower() == "BaseProjectFileName".ToLower()) {
					BaseProjectFileName = child.InnerText;
				}else if(child.Name.ToLower() == "productionbaseprojectfilename") {
					ProductionBaseProjectFilename = child.InnerText;
				}else if(child.Name.ToLower() == "Debug".ToLower()) {
					Debug = child.InnerText;
				} else if(child.Name.ToLower() == "SupportedCultures".ToLower()) {
					supportedcultures.LoadSupportedCultures(child);
				} else if(child.Name.ToLower() == "DesignDefaultPartnerID".ToLower()) {
					designDefaultPartnerID = child.InnerText;
				} else if(child.Name.ToLower() == "DesignDefaultCulture".ToLower()) {
					designDefaultCulture = child.InnerText;
				} else if(child.Name.ToLower() == "Contacts".ToLower()) {
					contacts.LoadContacts(child);
				}
			}
		}

		public string DesignDefaultPartnerID {
			set { designDefaultPartnerID = value; }
			get { return designDefaultPartnerID; }
		}

		public string DesignDefaultCulture {
			set { designDefaultCulture = value; }
			get { return designDefaultCulture; }
		}

		public string ID {
			set { id = value; }
			get { return id; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string CurrentWorkingProject {
			set { currentworkingproject = value; }
			get { return currentworkingproject; }
		}

		public string BaseProjectFileName {
			set { baseprojectfilename = value; }
			get { return baseprojectfilename; }
		}

		public string ProductionBaseProjectFilename {
			set { productionbaseprojectfilename = value; }
			get { return productionbaseprojectfilename; }
		}

		public string Debug {
			set { debug = value; }
			get { return debug; }
		}

		public SupportedCultures SupportedCultures {
			set { supportedcultures = value; }
			get { return supportedcultures; }
		}

		public Contacts Contacts {
			set { contacts = value; }
			get { return contacts; }
		}
	}
}





// -----------------------



