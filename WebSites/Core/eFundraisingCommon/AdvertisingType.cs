using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.eFundraisingCommon
{
    public class AdvertisingType {

		private int _advertising_type_id;
		private string _description;
		private DateTime _create_date;
        
		public AdvertisingType() : this(int.MinValue) { }
		public AdvertisingType(int advertising_type_id) : this(advertising_type_id, null) { }
		public AdvertisingType(int advertising_type_id, string description) : this(advertising_type_id, description, DateTime.MinValue) { }
		public AdvertisingType(int advertising_type_id, string description, DateTime create_date) {
			_advertising_type_id = advertising_type_id;
			_description = description;
			_create_date = create_date;
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
			return "<AdvertisingType>\r\n" +
			"	<Advertising_type_id>" + _advertising_type_id + "</Advertising_type_id>\r\n" +
			"	<Description>" + _description + "</Description>\r\n" +
			"	<Create_date>" + _create_date + "</Create_date>\r\n" +
			"</AdvertisingType>\r\n";
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
				if(node.Name.ToLower() == "advertising_type_id") {
					SetXmlValue(ref _advertising_type_id, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref _description, node.InnerText);
				}
				if(node.Name.ToLower() == "create_date") {
					SetXmlValue(ref _create_date, node.InnerText);
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
		public int Advertising_type_id {
			set { _advertising_type_id = value; }
			get { return _advertising_type_id; }
		}

		public string Description {
			set { _description = value; }
			get { return _description; }
		}

		public DateTime Create_date {
			set { _create_date = value; }
			get { return _create_date; }
		}

		#endregion
	}

}
