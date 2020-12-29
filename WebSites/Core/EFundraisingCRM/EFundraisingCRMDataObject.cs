using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for EFundraisingCRMDataObject.
	/// </summary>
	public abstract class EFundraisingCRMDataObject : EFundraisingCRMObject
	{
		public EFundraisingCRMDataObject()
		{
			
		}

		protected string ToLowerCase(string item) {
			return item.ToLower();
		}

		#region XML Methods

		public virtual string GenerateXML() {
			return base.ToXmlString();
		}
		public virtual EFundraisingCRMDataObject ShallowCopy()
		{
			return (EFundraisingCRMDataObject)this.MemberwiseClone();
		}
		#region Set Xml Values
		protected void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		protected void SetXmlValue(ref long obj, string val) 
		{
			if(val == "") { obj = long.MinValue; return; }
			obj = long.Parse(val);
		}

		protected void SetXmlValue(ref double obj, string val) 
		{
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		protected void SetXmlValue(ref short obj, string val) 
		{
			if(val == "") { obj = short.MinValue; return; }
			obj = short.Parse(val);
		}

		protected void SetXmlValue(ref float obj, string val) {
			if(val == "") { obj = float.MinValue; return; }
			obj = float.Parse(val);
		}

		protected void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
		}
		
		protected void SetXmlValue(ref bool obj, string val) {
			if(val == "") { obj = false; return; }
			obj = (val == "true");
		}

		protected void SetXmlValue(ref Decimal obj, string val) {
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		protected void SetXmlValue(ref DateTime obj, string val) {
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) {

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

	}
}
