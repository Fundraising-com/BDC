using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal 
{

	public class StoreTemplate 
	{

        private int storeTemplateId = int.MinValue;
		private string cultureCode;
        private int storeId = int.MinValue;
        private int aggregatorId = int.MinValue;
        private int accountNumber = int.MinValue;
        private int opportunityId = int.MinValue;
		private string description;
		private DateTime createDate;


		public StoreTemplate() : this(int.MinValue) { }
		public StoreTemplate(int storeTemplateId) : this(storeTemplateId, null) { }
		public StoreTemplate(int storeTemplateId, string cultureCode) : this(storeTemplateId, cultureCode, int.MinValue) { }
		public StoreTemplate(int storeTemplateId, string cultureCode, int storeId) : this(storeTemplateId, cultureCode, storeId, int.MinValue) { }
		public StoreTemplate(int storeTemplateId, string cultureCode, int storeId, int aggregatorId) : this(storeTemplateId, cultureCode, storeId, aggregatorId, int.MinValue) { }
		public StoreTemplate(int storeTemplateId, string cultureCode, int storeId, int aggregatorId, int accountNumber) : this(storeTemplateId, cultureCode, storeId, aggregatorId, accountNumber, null) { }
		public StoreTemplate(int storeTemplateId, string cultureCode, int storeId, int aggregatorId, int accountNumber, string description) : this(storeTemplateId, cultureCode, storeId, aggregatorId, accountNumber, description, DateTime.MinValue) { }
        public StoreTemplate(int storeTemplateId, string cultureCode, int storeId, int aggregatorId, int accountNumber, string description, DateTime createDate) : this(storeTemplateId, cultureCode, storeId, aggregatorId, accountNumber, int.MinValue, description, DateTime.MinValue) { }
        public StoreTemplate(int storeTemplateId, string cultureCode, int storeId, int aggregatorId, int accountNumber, int opportunityId, string description, DateTime createDate) 
		{
			this.storeTemplateId = storeTemplateId;
			this.cultureCode = cultureCode;
			this.storeId = storeId;
			this.aggregatorId = aggregatorId;
			this.accountNumber = accountNumber;
            this.opportunityId = opportunityId;
			this.description = description;
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
			return "<StoreTemplate>\r\n" +
				"	<StoreTemplateId>" + storeTemplateId + "</StoreTemplateId>\r\n" +
				"	<CultureCode>" + cultureCode + "</CultureCode>\r\n" +
				"	<StoreId>" + storeId + "</StoreId>\r\n" +
				"	<AggregatorId>" + aggregatorId + "</AggregatorId>\r\n" +
				"	<AccountNumber>" + accountNumber + "</AccountNumber>\r\n" +
                "	<OpportunityID>" + opportunityId + "</OpportunityID>\r\n" +
				"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</StoreTemplate>\r\n";
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
				if(node.Name.ToLower() == "storeTemplateId") 
				{
					SetXmlValue(ref storeTemplateId, node.InnerText);
				}
				if(node.Name.ToLower() == "cultureCode") 
				{
					SetXmlValue(ref cultureCode, node.InnerText);
				}
				if(node.Name.ToLower() == "storeId") 
				{
					SetXmlValue(ref storeId, node.InnerText);
				}
				if(node.Name.ToLower() == "aggregatorId") 
				{
					SetXmlValue(ref aggregatorId, node.InnerText);
				}
				if(node.Name.ToLower() == "accountNumber") 
				{
					SetXmlValue(ref accountNumber, node.InnerText);
				}
                if (node.Name.ToLower() == "opportunityId")
                {
                    SetXmlValue(ref opportunityId, node.InnerText);
                }
				if(node.Name.ToLower() == "description") 
				{
					SetXmlValue(ref description, node.InnerText);
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
		public static StoreTemplate[] GetStoreTemplates() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStoreTemplates();
		}

		public static StoreTemplate GetStoreTemplateByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStoreTemplateByID(id);
		}

		public static StoreTemplate GetStoreTemplateByPartnerID(int partnerId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetStoreTemplateByPartnerID(partnerId);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertStoreTemplate(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateStoreTemplate(this);
		}
		#endregion

		#region Properties
		public int StoreTemplateId 
		{
			set { storeTemplateId = value; }
			get { return storeTemplateId; }
		}

		public string CultureCode 
		{
			set { cultureCode = value; }
			get { return cultureCode; }
		}

		public int StoreId 
		{
			set { storeId = value; }
			get { return storeId; }
		}

		public int AggregatorId 
		{
			set { aggregatorId = value; }
			get { return aggregatorId; }
		}

		public int AccountNumber 
		{
			set { accountNumber = value; }
			get { return accountNumber; }
		}

        public int OpportunityID
        {
            set { opportunityId = value; }
            get { return opportunityId; }
        }

		public string Description 
		{
			set { description = value; }
			get { return description; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
