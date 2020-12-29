using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public class PartnerPaymentConfig {

		private int partnerId;
		private int profitPercentage;
		private int paymentTo;
		private int emailTemplateId;
		private int firstEmailTemplateId;
		private bool isDefault;
		private int partnerPaymentInfoId;
        private bool excluded;
        private int profitId;
        private int profitRangeId;

        public PartnerPaymentConfig() : this(int.MinValue) { }
        public PartnerPaymentConfig(int partnerId) : this(partnerId, int.MinValue) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage) : this(partnerId, profitPercentage, int.MinValue) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo) : this(partnerId, profitPercentage, paymentTo, int.MinValue) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo, int emailTemplateId) : this(partnerId, profitPercentage, paymentTo, emailTemplateId, int.MinValue) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo, int emailTemplateId, int firstEmailTemplateId) : this(partnerId, profitPercentage, paymentTo, emailTemplateId, firstEmailTemplateId, false) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo, int emailTemplateId, int firstEmailTemplateId, bool isDefault) : this(partnerId, profitPercentage, paymentTo, emailTemplateId, firstEmailTemplateId, false, false) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo, int emailTemplateId, int firstEmailTemplateId, bool isDefault, bool excluded) : this(partnerId, profitPercentage, paymentTo, emailTemplateId, firstEmailTemplateId, false, false, int.MinValue, int.MinValue) { }
        public PartnerPaymentConfig(int partnerId, int profitPercentage, int paymentTo, int emailTemplateId, int firstEmailTemplateId, bool isDefault, bool excluded, int profitId, int profitRangeId)
        {
            this.partnerId = partnerId;
            this.profitPercentage = profitPercentage;
            this.paymentTo = paymentTo;
            this.emailTemplateId = emailTemplateId;
            this.firstEmailTemplateId = firstEmailTemplateId;
            this.isDefault = isDefault;
            this.excluded = excluded;
            this.profitId = profitId;
            this.profitRangeId = profitRangeId;
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
			return "<PartnerPaymentConfig>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<ProfitPercentage>" + profitPercentage + "</ProfitPercentage>\r\n" +
			"	<PaymentTo>" + paymentTo + "</PaymentTo>\r\n" +
			"	<EmailTemplateId>" + emailTemplateId + "</EmailTemplateId>\r\n" +
			"	<FirstEmailTemplateId>" + firstEmailTemplateId + "</FirstEmailTemplateId>\r\n" +
			"	<IsDefault>" + isDefault + "</IsDefault>\r\n" +
			"</PartnerPaymentConfig>\r\n";
		}
		#endregion

		#region Set Xml Values
		private void SetXmlValue(ref int obj, string val) {
			if(val == "") { obj = int.MinValue; return; }
			obj = int.Parse(val);
		}

		private void SetXmlValue(ref string obj, string val) {
			if(val == "") { obj = null; return; }
			obj = System.Web.HttpUtility.HtmlDecode(val);
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
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "profitPercentage") {
					SetXmlValue(ref profitPercentage, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentTo") {
					SetXmlValue(ref paymentTo, node.InnerText);
				}
				if(node.Name.ToLower() == "emailTemplateId") {
					SetXmlValue(ref emailTemplateId, node.InnerText);
				}
				if(node.Name.ToLower() == "firstEmailTemplateId") {
					SetXmlValue(ref firstEmailTemplateId, node.InnerText);
				}
				if(node.Name.ToLower() == "isDefault") {
					SetXmlValue(ref isDefault, node.InnerText);
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

		#endregion

		#region Data Source Methods
		public static PartnerPaymentConfig[] GetPartnerPaymentConfigs() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerPaymentConfigs();
		}

		public static PartnerPaymentConfig GetPartnerPaymentConfigByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerPaymentConfigByID(id);
		}

		public static PartnerPaymentConfig GetPartnerPaymentConfigByPaymentID(int paymentId) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPartnerPaymentConfigByPaymentID(paymentId);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPartnerPaymentConfig(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePartnerPaymentConfig(this);
		}

		public static PartnerPaymentConfig GetDefaultPartnerPaymentConfig() 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetDefaultPartnerPaymentConfig();
		}

		#endregion

		#region Properties
		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public int ProfitPercentage {
			set { profitPercentage = value; }
			get { return profitPercentage; }
		}

		public int PaymentTo {
			set { paymentTo = value; }
			get { return paymentTo; }
		}

		public int EmailTemplateId {
			set { emailTemplateId = value; }
			get { return emailTemplateId; }
		}

		public int FirstEmailTemplateId {
			set { firstEmailTemplateId = value; }
			get { return firstEmailTemplateId; }
		}

		public bool IsDefault {
			set { isDefault = value; }
			get { return isDefault; }
		}

		public int PartnerPaymentInfoId
		{
			set {partnerPaymentInfoId = value;}
			get {return partnerPaymentInfoId;}
		}

		public bool IsPayToGroup()
		{
			return PaymentTo == (int)PaymentToList.Group;
		}

		public bool IsPayToPartner()
		{
			return PaymentTo == (int)PaymentToList.Partner;
		}

        public bool Excluded
        {
            set { this.excluded = value; }
            get { return this.excluded; }
        }

        public int ProfitId
        {
            set { profitId = value; }
            get { return profitId; }
        }


        public int ProfitRangeId
        {
            set { profitRangeId = value; }
            get { return profitRangeId; }
        }


		#endregion

		public enum PaymentToList : int
		{
			Group = 0,
			Partner = 1
		}
	}
}
