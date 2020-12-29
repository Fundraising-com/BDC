using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal
{
	public class ExternalAccountAction 
	{

		private int externalAccountActionId;
		private int externalAccountId;
		private int actionId;
		private DateTime createDate;


		public ExternalAccountAction() : this(int.MinValue) { }
		public ExternalAccountAction(int externalAccountActionId) : this(externalAccountActionId, int.MinValue) { }
		public ExternalAccountAction(int externalAccountActionId, int externalAccountId) : this(externalAccountActionId, externalAccountId, int.MinValue) { }
		public ExternalAccountAction(int externalAccountActionId, int externalAccountId, int actionId) : this(externalAccountActionId, externalAccountId, actionId, DateTime.MinValue) { }
		public ExternalAccountAction(int externalAccountActionId, int externalAccountId, int actionId, DateTime createDate) 
		{
			this.externalAccountActionId = externalAccountActionId;
			this.externalAccountId = externalAccountId;
			this.actionId = actionId;
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
			return "<ExternalAccountAction>\r\n" +
				"	<ExternalAccountActionId>" + externalAccountActionId + "</ExternalAccountActionId>\r\n" +
				"	<ExternalAccountId>" + externalAccountId + "</ExternalAccountId>\r\n" +
				"	<ActionId>" + actionId + "</ActionId>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</ExternalAccountAction>\r\n";
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
				if(node.Name.ToLower() == "externalAccountActionId") 
				{
					SetXmlValue(ref externalAccountActionId, node.InnerText);
				}
				if(node.Name.ToLower() == "externalAccountId") 
				{
					SetXmlValue(ref externalAccountId, node.InnerText);
				}
				if(node.Name.ToLower() == "actionId") 
				{
					SetXmlValue(ref actionId, node.InnerText);
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
		public static ExternalAccountAction[] GetExternalAccountActions() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetExternalAccountActions();
		}

		public static ExternalAccountAction GetExternalAccountActionByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetExternalAccountActionByID(id);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertExternalAccountAction(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateExternalAccountAction(this);
		}
		#endregion

		#region Properties
		public int ExternalAccountActionId 
		{
			set { externalAccountActionId = value; }
			get { return externalAccountActionId; }
		}

		public int ExternalAccountId 
		{
			set { externalAccountId = value; }
			get { return externalAccountId; }
		}

		public int ActionId 
		{
			set { actionId = value; }
			get { return actionId; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
