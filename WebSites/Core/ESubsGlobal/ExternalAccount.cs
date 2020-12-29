using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal
{
	[Serializable]
	public class ExternalAccount 
	{

		private int externalAccountId;
		private int foodAccountId;
		private string fsmId;
		private int onlineAccountId;
		private int eventParticipationId;
		private DateTime createDate;


		public ExternalAccount() : this(int.MinValue) { }
		public ExternalAccount(int externalAccountId) : this(externalAccountId, int.MinValue) { }
		public ExternalAccount(int externalAccountId, int foodAccountId) : this(externalAccountId, foodAccountId, null) { }
		public ExternalAccount(int externalAccountId, int foodAccountId, string fsmId) : this(externalAccountId, foodAccountId, fsmId, int.MinValue) { }
		public ExternalAccount(int externalAccountId, int foodAccountId, string fsmId, int onlineAccountId) : this(externalAccountId, foodAccountId, fsmId, onlineAccountId, int.MinValue) { }
		public ExternalAccount(int externalAccountId, int foodAccountId, string fsmId, int onlineAccountId, int eventParticipationId) : this(externalAccountId, foodAccountId, fsmId, onlineAccountId, eventParticipationId, DateTime.MinValue) { }
		public ExternalAccount(int externalAccountId, int foodAccountId, string fsmId, int onlineAccountId, int eventParticipationId, DateTime createDate) 
		{
			this.externalAccountId = externalAccountId;
			this.foodAccountId = foodAccountId;
			this.fsmId = fsmId;
			this.onlineAccountId = onlineAccountId;
			this.eventParticipationId = eventParticipationId;
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
			return "<ExternalAccount>\r\n" +
				"	<ExternalAccountId>" + externalAccountId + "</ExternalAccountId>\r\n" +
				"	<FoodAccountId>" + foodAccountId + "</FoodAccountId>\r\n" +
				"	<FsmId>" + fsmId + "</FsmId>\r\n" +
				"	<OnlineAccountId>" + onlineAccountId + "</OnlineAccountId>\r\n" +
				"	<EventParticipationId>" + eventParticipationId + "</EventParticipationId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</ExternalAccount>\r\n";
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
				if(node.Name.ToLower() == "externalAccountId") 
				{
					SetXmlValue(ref externalAccountId, node.InnerText);
				}
				if(node.Name.ToLower() == "foodAccountId") 
				{
					SetXmlValue(ref foodAccountId, node.InnerText);
				}
				if(node.Name.ToLower() == "fsmId") 
				{
					SetXmlValue(ref fsmId, node.InnerText);
				}
				if(node.Name.ToLower() == "onlineAccountId") 
				{
					SetXmlValue(ref onlineAccountId, node.InnerText);
				}
				if(node.Name.ToLower() == "eventParticipationId") 
				{
					SetXmlValue(ref eventParticipationId, node.InnerText);
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
		public static ExternalAccount[] GetExternalAccounts() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetExternalAccounts();
		}

		public static ExternalAccount GetExternalAccountByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetExternalAccountByID(id);
		}
		public static ExternalAccount GetExternalAccountByFoodAccountID(int id)
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetExternalAccountByFoodAccountID(id);
		}
		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertExternalAccount(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateExternalAccount(this);
		}
		#endregion

		#region Properties
		public int ExternalAccountId 
		{
			set { externalAccountId = value; }
			get { return externalAccountId; }
		}

		public int FoodAccountId 
		{
			set { foodAccountId = value; }
			get { return foodAccountId; }
		}

		public string FsmId 
		{
			set { fsmId = value; }
			get { return fsmId; }
		}

		public int OnlineAccountId 
		{
			set { onlineAccountId = value; }
			get { return onlineAccountId; }
		}

		public int EventParticipationId 
		{
			set { eventParticipationId = value; }
			get { return eventParticipationId; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
