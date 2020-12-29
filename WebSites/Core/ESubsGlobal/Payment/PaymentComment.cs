using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public class PaymentComment {

		private int paymentCommentId;
		private int paymentId;
		private string comment;
		private string ntLogin;
		private DateTime createDate;


		public PaymentComment() : this(int.MinValue) { }
		public PaymentComment(int paymentCommentId) : this(paymentCommentId, int.MinValue) { }
		public PaymentComment(int paymentCommentId, int paymentId) : this(paymentCommentId, paymentId, null) { }
		public PaymentComment(int paymentCommentId, int paymentId, string comment) : this(paymentCommentId, paymentId, comment, null) { }
		public PaymentComment(int paymentCommentId, int paymentId, string comment, string ntLogin) : this(paymentCommentId, paymentId, comment, ntLogin, DateTime.MinValue) { }
		public PaymentComment(int paymentCommentId, int paymentId, string comment, string ntLogin, DateTime createDate) {
			this.paymentCommentId = paymentCommentId;
			this.paymentId = paymentId;
			this.comment = comment;
			this.ntLogin = ntLogin;
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
			return "<PaymentComment>\r\n" +
				"	<PaymentCommentId>" + paymentCommentId + "</PaymentCommentId>\r\n" +
				"	<PaymentId>" + paymentId + "</PaymentId>\r\n" +
				"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
				"	<NtLogin>" + System.Web.HttpUtility.HtmlEncode(ntLogin) + "</NtLogin>\r\n" +
				"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
				"</PaymentComment>\r\n";
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
				if(node.Name.ToLower() == "paymentCommentId") {
					SetXmlValue(ref paymentCommentId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentId") {
					SetXmlValue(ref paymentId, node.InnerText);
				}
				if(node.Name.ToLower() == "comment") {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(node.Name.ToLower() == "ntLogin") {
					SetXmlValue(ref ntLogin, node.InnerText);
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
		public static PaymentComment[] GetPaymentComments() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentComments();
		}

		public static PaymentComment[] GetPaymentCommentsByPaymentID(int paymentID) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentCommentsByPaymentID(paymentID);
		}

		public static PaymentComment[] GetPaymentCommentsByNTLogin(string ntLogin) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentCommentsByNtLogin(ntLogin);
		}

		public static PaymentComment GetPaymentCommentByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentCommentByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentComment(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentComment(this);
		}
		#endregion

		#region Properties
		public int PaymentCommentId {
			set { paymentCommentId = value; }
			get { return paymentCommentId; }
		}

		public int PaymentId {
			set { paymentId = value; }
			get { return paymentId; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public string NtLogin {
			set { ntLogin = value; }
			get { return ntLogin; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
