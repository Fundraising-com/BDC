using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM 
{

	public class LeadInterestProductClass : EFundraisingCRMDataObject
	{

		private int leadId;
		private byte productClassId;


		public LeadInterestProductClass() : this(int.MinValue) { }
		public LeadInterestProductClass(int leadId) : this(leadId, byte.MinValue) { }
		public LeadInterestProductClass(int leadId, byte productClassId) 
		{
			this.leadId = leadId;
			this.productClassId = productClassId;
		}


		#region XML Methods

		#region Save XML
		private string IdentXML(string xml) 
		{
			string newXML = "";
			string[] lines = xml.Split('\r');
			foreach(string strXml in lines) 
			{
				if(strXml.Trim() == "")
					break;
				newXML += "\t" + strXml.Replace("\n", "") + "\r\n";
			}
			return newXML;
		}

		public virtual string GenerateXML() 
		{
			return "<LeadInterestProductClass>\r\n" +
				"	<LeadId>" + leadId + "</LeadId>\r\n" +
				"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
				"</LeadInterestProductClass>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) 
		{
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref byte obj, string val) 
		{
			if(val == "") { obj = byte.MinValue; return; }
			obj = byte.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) 
		{
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
		}
		
		private void SetXmlValue(ref bool obj, string val) 
		{
			if(val == "") { obj = false; return; }
			obj = (val.ToLower() == "true");
		}

		private void SetXmlValue(ref Decimal obj, string val) 
		{
			if(val == "") { obj = Decimal.MinValue; return; }
			obj = Decimal.Parse(val);
		}

		private void SetXmlValue(ref DateTime obj, string val) 
		{
			if(val == "") { obj = DateTime.MinValue; return; }
			obj = DateTime.Parse(val);
		}

		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public virtual void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(node.Name.ToLower() == "leadId") 
				{
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(node.Name.ToLower() == "productClassId") 
				{
					SetXmlValue(ref productClassId, node.InnerText);
				}
			}
		}
		// load from an xml string 
		public virtual void LoadXml(string xml) 
		{
			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from an xml document object
		public virtual void Load(System.Xml.XmlDocument doc) 
		{
			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a stream
		public virtual void Load(System.IO.Stream inStream) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(inStream);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a text reader
		public virtual void Load(System.IO.TextReader txtReader) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(txtReader);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from an xml reader
		public virtual void Load(System.Xml.XmlReader reader) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(reader);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		// load from a xml filename
		public virtual void Load(string filename) 
		{
			XmlDocument doc = new XmlDocument();
			doc.Load(filename);

			foreach(XmlNode node in doc.ChildNodes) 
			{
				Load(node);
			}
		}

		#endregion

		#endregion

		#region Data Source Methods
		public static LeadInterestProductClass[] GetLeadInterestProductClasss() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadInterestProductClasss();
		}

		public static LeadInterestProductClass GetLeadInterestProductClassByID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadInterestProductClassByID(id);
		}

		public int Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadInterestProductClass(this);
		}

		public int Update() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadInterestProductClass(this);
		}
		#endregion

		#region Properties
		public int LeadId 
		{
			set { leadId = value; }
			get { return leadId; }
		}

		public byte ProductClassId 
		{
			set { productClassId = value; }
			get { return productClassId; }
		}

		#endregion
	}
}
