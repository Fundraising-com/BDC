using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace GA.BDC.Core.eFundraisingCommon
{
    public class Listing {

		private int _listing_id;
		private string _description;
		private float _listing_amount;
		private float _listing_period;
		private bool _is_visible;


		public Listing() : this(int.MinValue) { }
		public Listing(int listing_id) : this(listing_id, null) { }
		public Listing(int listing_id, string description) : this(listing_id, description, float.MinValue) { }
		public Listing(int listing_id, string description, float listing_amount) : this(listing_id, description, listing_amount, float.MinValue) { }
		public Listing(int listing_id, string description, float listing_amount, float listing_period) : this(listing_id, description, listing_amount, listing_period, false) { }
		public Listing(int listing_id, string description, float listing_amount, float listing_period, bool is_visible) {
			_listing_id = listing_id;
			_description = description;
			_listing_amount = listing_amount;
			_listing_period = listing_period;
			_is_visible = is_visible;
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
			return "<Listing>\r\n" +
			"	<Listing_id>" + _listing_id + "</Listing_id>\r\n" +
			"	<Description>" + _description + "</Description>\r\n" +
			"	<Listing_amount>" + _listing_amount + "</Listing_amount>\r\n" +
			"	<Listing_period>" + _listing_period + "</Listing_period>\r\n" +
			"	<Is_visible>" + _is_visible + "</Is_visible>\r\n" +
			"</Listing>\r\n";
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

        private void SetXmlValue(ref float obj, string val)
        {
            if (val == "") { obj = float.MinValue; return; }
            obj = float.Parse(val);
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
				if(node.Name.ToLower() == "listing_id") {
					SetXmlValue(ref _listing_id, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref _description, node.InnerText);
				}
				if(node.Name.ToLower() == "listing_amount") {
					SetXmlValue(ref _listing_amount, node.InnerText);
				}
				if(node.Name.ToLower() == "listing_period") {
					SetXmlValue(ref _listing_period, node.InnerText);
				}
				if(node.Name.ToLower() == "is_visible") {
					SetXmlValue(ref _is_visible, node.InnerText);
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
		public int listing_id {
			set { _listing_id = value; }
			get { return _listing_id; }
		}

		public string description {
			set { _description = value; }
			get { return _description; }
		}

		public float listing_amount {
			set { _listing_amount = value; }
			get { return _listing_amount; }
		}

		public float listing_period {
			set { _listing_period = value; }
			get { return _listing_period; }
		}

		public bool is_visible {
			set { _is_visible = value; }
			get { return _is_visible; }
		}

		#endregion
	}
}