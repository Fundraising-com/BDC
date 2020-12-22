using System;
using System.Xml;
using System.Collections;

namespace EFundraisingCRMWeb.Components.Server.Menu {
	public class Menus {

		private ArrayList _menus;

		public Menus() {
			_menus = new ArrayList();
		}

		#region Object Manipulation

		// add a new Menu
		public void AddMenu(Menu menu) {
			_menus.Add(menu);
		}

		// remove a Menu
		public void RemoveMenu(Menu menu) {
			_menus.Remove(menu);
		}

		// check if this specified Menu exists
		public bool Exists(Menu menu) {
			return _menus.Contains(menu);
		}

		// clear the list of Menus
		public void Clear() {
			_menus.Clear();
		}

		// get all Menus
		public Menu[] GetAllMenus() {
			Menu[] menu = new Menu[_menus.Count];
			for(int i=0;i<_menus.Count;i++) {
				menu[i] = (Menu)_menus[i];
			}
			return menu;
		}

		// get one Menu by ID
		public Menu GetMenuByID(string id) {
			for(int i=0;i<_menus.Count;i++) {
				Menu menu = (Menu)_menus[i];
				if(menu.ID == id) {
					return menu;
				}
			}
			return null;
		}

		// get all Menu by ID
		public Menu[] GetMenusByID(string id) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_menus.Count;i++) {
				Menu menu = (Menu)_menus[i];
				if(menu.ID == id) {
					list.Add(menu);
				}
			}
			if(list.Count < 1) return null;
			Menu[] menus = new Menu[list.Count];
			for(int i=0;i<list.Count;i++) {
				menus[i] = (Menu)list[i];
			}
			return menus;
		}

		// get one Menu by Name
		public Menu GetMenuByName(string name) {
			for(int i=0;i<_menus.Count;i++) {
				Menu menu = (Menu)_menus[i];
				if(menu.Name == name) {
					return menu;
				}
			}
			return null;
		}

		// get all Menu by Name
		public Menu[] GetMenusByName(string name) {
			ArrayList list = new ArrayList();
			for(int i=0;i<_menus.Count;i++) {
				Menu menu = (Menu)_menus[i];
				if(menu.Name == name) {
					list.Add(menu);
				}
			}
			if(list.Count < 1) return null;
			Menu[] menus = new Menu[list.Count];
			for(int i=0;i<list.Count;i++) {
				menus[i] = (Menu)list[i];
			}
			return menus;
		}

		#endregion

		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) {
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) {
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() {
			string rstr = "<Menus>\r\n";
			foreach(Menu menu in _menus) {
				rstr += IdentXML(menu.GenerateXML());
			}
			rstr += "</Menus>\r\n";
			return rstr;
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "menu") {
					Menu menu = new Menu();
					menu.Load(node);
					_menus.Add(menu);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) {
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) {
			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) {
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) {
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) {
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) {
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Properties
		public ArrayList Menu {
			set { _menus = value; }
			get { return _menus; }
		}
		#endregion
	}
}
