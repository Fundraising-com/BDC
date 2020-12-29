using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal 
{

	public class PartnerTypeCulture 
	{

		private int partnerTypeId;
		private string cultureCode;
		private string partnerTypeName;
		private DateTime createDate;


		public PartnerTypeCulture() : this(int.MinValue) { }
		public PartnerTypeCulture(int partnerTypeId) : this(partnerTypeId, null) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode) : this(partnerTypeId, cultureCode, null) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode, string partnerTypeName) : this(partnerTypeId, cultureCode, partnerTypeName, DateTime.MinValue) { }
		public PartnerTypeCulture(int partnerTypeId, string cultureCode, string partnerTypeName, DateTime createDate) 
		{
			this.partnerTypeId = partnerTypeId;
			this.cultureCode = cultureCode;
			this.partnerTypeName = partnerTypeName;
			this.createDate = createDate;
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
			return "<PartnerTypeCulture>\r\n" +
				"	<PartnerTypeId>" + partnerTypeId + "</PartnerTypeId>\r\n" +
				"	<CultureCode>" + cultureCode + "</CultureCode>\r\n" +
				"	<PartnerTypeName>" + System.Web.HttpUtility.HtmlEncode(partnerTypeName) + "</PartnerTypeName>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PartnerTypeCulture>\r\n";
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
				if(node.Name.ToLower() == "partnerTypeId") 
				{
					SetXmlValue(ref partnerTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") 
				{
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerTypeName") 
				{
					SetXmlValue(ref partnerTypeName, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") 
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
		public static PartnerTypeCulture[] GetPartnerTypeCultures() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerTypeCultures();
		}

		public static PartnerTypeCulture[] GetPartnerTypeCultureByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerTypeCultureByID(id);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPartnerTypeCulture(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePartnerTypeCulture(this);
		}
		#endregion

		#region Properties
		public int PartnerTypeId 
		{
			set { partnerTypeId = value; }
			get { return partnerTypeId; }
		}

		public string CultureCode 
		{
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public string PartnerTypeName 
		{
			set { partnerTypeName = value; }
			get { return partnerTypeName; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
