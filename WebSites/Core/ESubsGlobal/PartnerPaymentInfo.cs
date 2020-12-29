using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal 
{

   public class PartnerPaymentInfo : BusinessBase.BusinessBase
	{

		private int partnerPaymentInfoId;
		private int partnerId;
		private int paymentInfoId;
		private bool active;
		private DateTime createDate;


		public PartnerPaymentInfo() : this(int.MinValue) { }
		public PartnerPaymentInfo(int partnerPaymentInfoId) : this(partnerPaymentInfoId, int.MinValue) { }
		public PartnerPaymentInfo(int partnerPaymentInfoId, int partnerId) : this(partnerPaymentInfoId, partnerId, int.MinValue) { }
		public PartnerPaymentInfo(int partnerPaymentInfoId, int partnerId, int paymentInfoId) : this(partnerPaymentInfoId, partnerId, paymentInfoId, false) { }
		public PartnerPaymentInfo(int partnerPaymentInfoId, int partnerId, int paymentInfoId, bool active) : this(partnerPaymentInfoId, partnerId, paymentInfoId, active, DateTime.MinValue) { }
		public PartnerPaymentInfo(int partnerPaymentInfoId, int partnerId, int paymentInfoId, bool active, DateTime createDate) 
		{
			this.partnerPaymentInfoId = partnerPaymentInfoId;
			this.partnerId = partnerId;
			this.paymentInfoId = paymentInfoId;
			this.active = active;
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
			return "<PartnerPaymentInfo>\r\n" +
				"	<PartnerPaymentInfoId>" + partnerPaymentInfoId + "</PartnerPaymentInfoId>\r\n" +
				"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
				"	<PaymentInfoId>" + paymentInfoId + "</PaymentInfoId>\r\n" +
				"	<Active>" + active + "</Active>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PartnerPaymentInfo>\r\n";
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
				if(node.Name.ToLower() == "partnerPaymentInfoId") 
				{
					SetXmlValue(ref partnerPaymentInfoId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") 
				{
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentInfoId") 
				{
					SetXmlValue(ref paymentInfoId, node.InnerText);
				}
				if(node.Name.ToLower() == "active") 
				{
					SetXmlValue(ref active, node.InnerText);
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
		public static PartnerPaymentInfo[] GetPartnerPaymentInfos() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerPaymentInfos();
		}

		public static PartnerPaymentInfo[] GetAllPaymentInfoByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerPaymentInfoByID(id);
		}

		
		public static PartnerPaymentInfo GetPartnerPaymentInfoByID(int id) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetActivePartnerPaymentInfoByID(id);
		}

		
		public static PartnerPaymentInfo GetActivePartnerPaymentInfoByPartnerID(int partnerId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetActivePartnerPaymentInfoByPartnerID(partnerId);
		}


		public int Insert() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPartnerPaymentInfo(this);
		}

		public int Update() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePartnerPaymentInfo(this);
		}
		#endregion

		#region Properties
		public int PartnerPaymentInfoId 
		{
			set { partnerPaymentInfoId = value; }
			get { return partnerPaymentInfoId; }
		}

		public int PartnerId 
		{
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int PaymentInfoId 
		{
			set { paymentInfoId = value; }
			get { return paymentInfoId; }
		}

		public bool Active 
		{
			set { active = value; }
			get { return active; }
		}

		public DateTime CreateDate 
		{
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
