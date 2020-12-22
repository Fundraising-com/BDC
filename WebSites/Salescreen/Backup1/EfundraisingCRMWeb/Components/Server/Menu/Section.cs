using System;
using System.Xml;

namespace EFundraisingCRMWeb.Components.Server.Menu {
	public class Section {

		private string _iD;
		private string _name;

		private Roles _roles = null;
		private Menus _menus = null;

		public Section() : this(null) { }
		public Section(string iD) {
			_iD = iD;
		}

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
			return "<Section>\r\n" +
			"	<ID>" + _iD + "</ID>\r\n" +
			"	<Name>" + _name + "</Name>\r\n" +
			IdentXML(_roles.GenerateXML()) + 
			IdentXML(_menus.GenerateXML()) + 
			"</Section>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = val;
		}
		
		private void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "id") {
					SetXmlValue(ref _iD, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref _name, node.InnerText);
				}
				if(node.Name.ToLower() == "roles") {
					_roles = new Roles();
					_roles.Load(node);
				}
				if(node.Name.ToLower() == "menus") {
					_menus = new Menus();
					_menus.Load(node);
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
		public string ID {
			set { _iD = value; }
			get { return _iD; }
		}

		public string Name {
			set { _name = value; }
			get { return _name; }
		}

		public Roles Roles {
			set { _roles = value; }
			get { return _roles; }
		}

		public Menus Menus {
			set { _menus = value; }
			get { return _menus; }
		}

		#endregion
	}
}
