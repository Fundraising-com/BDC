using System;
using System.Xml;
using System.Collections;

namespace GA.BDC.Core.Web.UI.UIControls.Config {
	/// <summary>
	/// Summary description for UIController.
	/// </summary>
	[Serializable]
	public class UIControl {
		private string id;
		private string name;
		private string type;
		private Parameters parameters;
		private PartnersID partnersID;

		public UIControl() {
			parameters = new Parameters();
			partnersID = new PartnersID();
		}

		/// <summary>
		/// Parse xml tags
		/// </summary>
		/// <param name="node"></param>
		public void Load(XmlNode node) {
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					id = child.InnerText;
				} else if(child.Name.ToLower() == "Name".ToLower()) {
					name = child.InnerText;
				} else if(child.Name.ToLower() == "Type".ToLower()) {
					type = child.InnerText;
				} else if(child.Name.ToLower() == "Params".ToLower()) {
					parameters.Load(child);
				} else if(child.Name.ToLower() == "PartnersID".ToLower()) {
					partnersID.Load(child);
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

		public string Type {
			set { type = value; }
			get { return type; }
		}

		public Parameters Parameters {
			set { parameters = value; }
			get { return parameters; }
		}

		public PartnersID PartnersID {
			set { partnersID = value; }
			get { return partnersID; }
		}
	}
}
