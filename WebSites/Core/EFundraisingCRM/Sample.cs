using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM
{
    public class Sample
    {
        private int _sampleID;
		private string _sampleName;
		private string _description;
		private bool _active;


		public Sample() : this(int.MinValue) { }
		public Sample(int sampleID) : this(sampleID, null) { }
		public Sample(int sampleID, string sampleName) : this(sampleID, sampleName, null) { }
		public Sample(int sampleID, string sampleName, string description) : this(sampleID, sampleName, description, false) { }
		public Sample(int sampleID, string sampleName, string description, bool active) {
			_sampleID = sampleID;
			_sampleName = sampleName;
			_description = description;
			_active = active;
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
			return "<Sample>\r\n" +
			"	<SampleID>" + _sampleID + "</SampleID>\r\n" +
			"	<SampleName>" + _sampleName + "</SampleName>\r\n" +
			"	<Description>" + _description + "</Description>\r\n" +
			"	<Active>" + _active + "</Active>\r\n" +
			"</Sample>\r\n";
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
				if(node.Name.ToLower() == "sampleid") {
					SetXmlValue(ref _sampleID, node.InnerText);
				}
				if(node.Name.ToLower() == "samplename") {
					SetXmlValue(ref _sampleName, node.InnerText);
				}
				if(node.Name.ToLower() == "description") {
					SetXmlValue(ref _description, node.InnerText);
				}
				if(node.Name.ToLower() == "active") {
					SetXmlValue(ref _active, node.InnerText);
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

        #region Data Source Functions
        public static List<Sample> GetSamples()
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetSamples();
        }
        #endregion

		#endregion

		#region Properties
		public int SampleID {
			set { _sampleID = value; }
			get { return _sampleID; }
		}

		public string SampleName {
			set { _sampleName = value; }
			get { return _sampleName; }
		}

		public string Description {
			set { _description = value; }
			get { return _description; }
		}

		public bool Active {
			set { _active = value; }
			get { return _active; }
		}

		#endregion
	}
}
