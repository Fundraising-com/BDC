using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public enum PaymentPaymentStatusStatus
	{
		Error,
		Ok
	}

	public class PaymentPaymentStatus {

		private int paymentId;
		private int paymentStatusId;
		private DateTime createDate;


		public PaymentPaymentStatus() : this(int.MinValue) { }
		public PaymentPaymentStatus(int paymentId) : this(paymentId, int.MinValue) { }
		public PaymentPaymentStatus(int paymentId, int paymentStatusId) : this(paymentId, paymentStatusId, DateTime.MinValue) { }
		public PaymentPaymentStatus(int paymentId, int paymentStatusId, DateTime createDate) {
			this.paymentId = paymentId;
			this.paymentStatusId = paymentStatusId;
			this.createDate = createDate;
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
			return "<PaymentPaymentStatus>\r\n" +
			"	<PaymentId>" + paymentId + "</PaymentId>\r\n" +
			"	<PaymentStatusId>" + paymentStatusId + "</PaymentStatusId>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PaymentPaymentStatus>\r\n";
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
				if(node.Name.ToLower() == "paymentId") {
					SetXmlValue(ref paymentId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentStatusId") {
					SetXmlValue(ref paymentStatusId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
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
		public static PaymentPaymentStatus[] GetPaymentPaymentStatuss() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentPaymentStatuss();
		}

		public static PaymentPaymentStatus GetPaymentPaymentStatusByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLastPaymentPaymentStatusByPaymentID(id);
		}

		public static PaymentPaymentStatus GetLastPaymentPaymentStatusByPaymentID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetLastPaymentPaymentStatusByPaymentID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentPaymentStatus(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentPaymentStatus(this);
		}
		#endregion

		#region Properties
		public int PaymentId {
			set { paymentId = value; }
			get { return paymentId; }
		}

		public int PaymentStatusId {
			set { paymentStatusId = value; }
			get { return paymentStatusId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
