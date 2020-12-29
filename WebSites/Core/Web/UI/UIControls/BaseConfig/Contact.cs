// Contact.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class Contact {

		private string id;
		private string name;
		private string emailaddress;

		public Contact() {

		}

		public void LoadContact(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "ID".ToLower()) {
					ID = child.InnerText;
				}else if(child.Name.ToLower() == "Name".ToLower()) {
					Name = child.InnerText;
				}else if(child.Name.ToLower() == "EmailAddress".ToLower()) {
					EmailAddress = child.InnerText;
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

		public string EmailAddress {
			set { emailaddress = value; }
			get { return emailaddress; }
		}

	}
}





// -----------------------



