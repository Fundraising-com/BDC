// Contacts.cs

using System;
using System.Collections;
using System.Xml;

namespace GA.BDC.Core.Web.UI.UIControls.BaseConfig {
	[Serializable]
	public class Contacts {

		private ArrayList contactlist = new ArrayList();


		public Contacts() {

		}

		public Contact GetContactByID(string childName) {
			foreach(Contact contact in contactlist) {
				if(contact.ID.ToLower() == childName.ToLower()) {
					return contact;
				}
			}
			return null;
		}


		public Contact GetContactByName(string childName) {
			foreach(Contact contact in contactlist) {
				if(contact.Name.ToLower() == childName.ToLower()) {
					return contact;
				}
			}
			return null;
		}


		public void LoadContacts(XmlNode node) {
			
			foreach(XmlNode child in node) {
				if(child.Name.ToLower() == "Contact".ToLower()) {
					Contact contact = new Contact();
					contact.LoadContact(child);
					AddContact(contact);
				}
			}
		}

		public void AddContact(Contact contact) {
			ContactList.Add(contact);
		}

		public ArrayList ContactList {
			set { contactlist = value; }
			get { return contactlist; }
		}

	}
}





// -----------------------



