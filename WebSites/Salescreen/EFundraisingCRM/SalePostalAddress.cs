using System;
using System.Xml;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for SalePostalAddress.
	/// </summary>
	public class SalePostalAddress: Core.BusinessBase
	{

		private int saleId;
		private int postalAddressId;
		private DateTime createDate;
		private bool active;


		public SalePostalAddress() : this(int.MinValue) { }
		public SalePostalAddress(int SaleId) : this(SaleId, int.MinValue) { }
		public SalePostalAddress(int SaleId, int postalAddressId) : this(SaleId, postalAddressId, DateTime.MinValue) { }
		public SalePostalAddress(int SaleId, int postalAddressId, DateTime createDate): this(SaleId, postalAddressId,  createDate,  true) 
		{
		}

		public SalePostalAddress(int SaleId, int postalAddressId, DateTime createDate, bool bActive) 
		{
			this.SalesId = SaleId;
			this.postalAddressId = postalAddressId;
			this.createDate = createDate;
			this.Active = bActive;
		}

		public bool IsDifferent(SalePostalAddress theSaleAddress)
		{
//			int iTmpThisSaleId = this.SalesId;
//			this.SalesId = theSaleAddress.SalesId;
			bool result = (this.GenerateXML() != theSaleAddress.GenerateXML());
//			this.SalesId = iTmpThisSaleId;
			return result;
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
			return "<SalePostalAddress>\r\n" +
				"	<SaleId>" + SalesId + "</SaleId>\r\n" +
				"	<PostalAddressId>" + postalAddressId + "</PostalAddressId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</SalePostalAddress>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) 
		{
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
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
				if(string.Compare(node.Name, "saleid", true) == 0) 
				{
					SetXmlValue(ref saleId, node.InnerText);
				}
				if(string.Compare(node.Name, "postaladdressid", true) == 0) 
				{
					SetXmlValue(ref postalAddressId, node.InnerText);
				}
				if(string.Compare(node.Name,"createdate", true) == 0) 
				{
					SetXmlValue(ref createDate, node.InnerText);
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
		public static SalePostalAddress[] GetSalePostalAddressBySaleId(int sId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalePostalAddressBySaleId(sId);
		}
		public static SalePostalAddress GetActiveSalePostalAddressBySaleId(int sId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetActiveSalePostalAddressBySaleId(sId);
		}

		public int Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalePostalAddress(this);
		}

		public int Update() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			SalePostalAddress theActiveSalePostalAddress = dbo.GetActiveSalePostalAddressBySaleId(this.SalesId);
			if (theActiveSalePostalAddress != null && this.IsDifferent(theActiveSalePostalAddress))
				return dbo.InsertSalePostalAddress(this);
			else
				return -1;
		}
		#endregion

		#region Properties
		public int SalesId
		{
			set { saleId = value; }
			get { return saleId; }
		}

		public int PostalAddressId 
		{
			set { postalAddressId = value; }
			get { return postalAddressId; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		public bool Active
		{
			set { active = value;}
			get { return active;}
		}

		#endregion
	}

}
