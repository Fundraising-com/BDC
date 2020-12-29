// GlobalizerConfigs.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class GlobalizerConfigs {

		private ArrayList globalizerconfiglist = new ArrayList();
		private string xml = "";

		public GlobalizerConfigs() {

		}

		public void LoadXML() {
			LoadXML(@"C:\GlobalizerConfig\GlobalizerConfig.xml");
		}

		public void LoadXML(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);
			foreach(XmlNode node in doc.ChildNodes) {
				this.LoadGlobalizerConfigs(node);
			}
		}

		private void Add(string newLine) {
			xml += newLine + "\r\n";
		}

		public string GenerateXMLString() {
			xml = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n";
			Add("<GlobalizerConfigs>");
			foreach(GlobalizerConfig gc in GlobalizerConfigList) {
				Add("\t<GlobalizerConfig>");
				Add("\t\t<ID>" + gc.ID + "</ID>");
				Add("\t\t<Name>" + gc.Name + "</Name>");
				Add("\t\t<CurrentWorkingProject>" + gc.CurrentWorkingProject + "</CurrentWorkingProject>");
				Add("\t\t<BaseProjectFileName>" + gc.BaseProjectFileName + "</BaseProjectFileName>");
				Add("\t\t<Debug>" + gc.Debug + "</Debug>");
				Add("\t\t<DesignDefaultPartnerID>" + gc.DesignDefaultPartnerID + "</DesignDefaultPartnerID>");
				Add("\t\t<DesignDefaultCulture>" + gc.DesignDefaultCulture + "</DesignDefaultCulture>");
				Add("\t\t<SupportedCultures>");
				foreach(SupportedCulture sc in gc.SupportedCultures.SupportedCultureList) {
					Add("\t\t\t<SupportedCulture>");
					Add("\t\t\t\t<ID>" + sc.ID + "</ID>");
					Add("\t\t\t\t<Name>" + sc.Name + "</Name>");
					Add("\t\t\t</SupportedCulture>");
				}
				Add("\t\t</SupportedCultures>");
				Add("\t\t<Contacts>");
				foreach(Contact c in gc.Contacts.ContactList) {
					Add("\t\t\t<Contact>");
					Add("\t\t\t\t<ID>" + c.ID + "</ID>");
					Add("\t\t\t\t<Name>" + c.Name + "</Name>");
					Add("\t\t\t\t<EmailAddress>" + c.EmailAddress + "</EmailAddress>");
					Add("\t\t\t</Contact>");
				}
				Add("\t\t</Contacts>");
				Add("\t</GlobalizerConfig>");
			}
			Add("</GlobalizerConfigs>");
			return xml;
		}

		public bool Save() {
			try {
				if(System.IO.File.Exists(@"C:\GlobalizerConfig\GlobalizerConfig.xml.bak")) {
					System.IO.File.Delete(@"C:\GlobalizerConfig\GlobalizerConfig.xml.bak");
				}

				if(System.IO.File.Exists(@"C:\GlobalizerConfig\GlobalizerConfig.xml")) {
					System.IO.File.Move(@"C:\GlobalizerConfig\GlobalizerConfig.xml",
						@"C:\GlobalizerConfig\GlobalizerConfig.xml.bak");
				}

				//GA.BDC.Core.Xml.Serialization.Serializer.SaveObjectToXmlFile(@"C:\GlobalizerConfig\GlobalizerConfig.xml",
				//	typeof(GlobalizerConfigs));
				// Utils.SaveFile(@"C:\GlobalizerConfig\GlobalizerConfig.xml", GenerateXMLString());
				GA.BDC.Core.Utilities.IO.FileHandler.CreateNewFile(
					@"C:\GlobalizerConfig\GlobalizerConfig.xml", GenerateXMLString());
			} catch {
				return false;
			}
			return true;
		}

		public GlobalizerConfig GetGlobalizerConfigByID(string childName) {
			foreach(GlobalizerConfig globalizerconfig in globalizerconfiglist) {
				if(globalizerconfig.ID.ToLower() == childName.ToLower()) {
					return globalizerconfig;
				}
			}
			return null;
		}


		public GlobalizerConfig GetGlobalizerConfigByName(string childName) {
			foreach(GlobalizerConfig globalizerconfig in globalizerconfiglist) {
				if(globalizerconfig.Name.ToLower() == childName.ToLower()) {
					return globalizerconfig;
				}
			}
			return null;
		}

		public GlobalizerConfig GetCurrentWorkingConfig() {
			foreach(GlobalizerConfig globalizerconfig in globalizerconfiglist) {
				if(globalizerconfig.CurrentWorkingProject.ToLower() == "true") {
					return globalizerconfig;
				}
			}
			return null;
		}

		public static string GetBasePath() {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			GlobalizerConfig c = gcs.GetCurrentWorkingConfig();
			return c.BaseProjectFileName;
		}

		public static string GetDesignDefaultCulture() {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			GlobalizerConfig c = gcs.GetCurrentWorkingConfig();
			return c.DesignDefaultCulture;
		}

		public static string GetDesignDefaultPartnerID() {
			GlobalizerConfigs gcs = new GlobalizerConfigs();
			gcs.LoadXML();
			GlobalizerConfig c = gcs.GetCurrentWorkingConfig();
			return c.DesignDefaultPartnerID;
		}

		public void LoadGlobalizerConfigs(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "GlobalizerConfig".ToLower()) {
					GlobalizerConfig globalizerconfig = new GlobalizerConfig();
					globalizerconfig.LoadGlobalizerConfig(child);
					AddGlobalizerConfig(globalizerconfig);
				}
			}
		}

		public void AddGlobalizerConfig(GlobalizerConfig globalizerconfig) {
			GlobalizerConfigList.Add(globalizerconfig);
		}

		public ArrayList GlobalizerConfigList {
			set { globalizerconfiglist = value; }
			get { return globalizerconfiglist; }
		}

	}
}





// -----------------------



