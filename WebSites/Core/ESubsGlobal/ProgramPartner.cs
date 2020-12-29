using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal 
{

	public class ProgramPartner 
	{

        private int programId = int.MinValue;
        private int partnerId = int.MinValue;
		private string programUrl;
		private DateTime createDate;


		public ProgramPartner() : this(int.MinValue) { }
		public ProgramPartner(int programId) : this(programId, int.MinValue) { }
		public ProgramPartner(int programId, int partnerId) : this(programId, partnerId, null) { }
		public ProgramPartner(int programId, int partnerId, string programUrl) : this(programId, partnerId, programUrl, DateTime.MinValue) { }
		public ProgramPartner(int programId, int partnerId, string programUrl, DateTime createDate) 
		{
			this.programId = programId;
			this.partnerId = partnerId;
			this.programUrl = programUrl;
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
			return "<ProgramPartner>\r\n" +
				"	<ProgramId>" + programId + "</ProgramId>\r\n" +
				"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
				"	<ProgramUrl>" + System.Web.HttpUtility.HtmlEncode(programUrl) + "</ProgramUrl>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</ProgramPartner>\r\n";
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
				if(node.Name.ToLower() == "programId") 
				{
					SetXmlValue(ref programId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") 
				{
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "programUrl") 
				{
					SetXmlValue(ref programUrl, node.InnerText);
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
		public static ProgramPartner[] GetProgramPartners() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetProgramPartners();
		}

		public static ProgramPartner GetProgramPartnerByPartnerID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetProgramPartnerByPartnerID(id);
		}

		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertProgramPartner(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateProgramPartner(this);
		}
		#endregion

		#region Properties
		public int ProgramId 
		{
			set { programId = value; }
			get { return programId; }
		}

		public int PartnerId 
		{
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string ProgramUrl 
		{
			set { programUrl = value; }
			get { return programUrl; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
