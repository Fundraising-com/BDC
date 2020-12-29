using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public enum PaymentExceptionTypeStatus
	{
		Error,
		Ok
	}
	
	public enum PaymentExceptionTypeComparable 
	{
		PaymentID,
		ExceptionTypeID,
		CreateDate,
		ValidateDate,
		IsCorrected
	}

	public class PaymentExceptionType : EnvironmentBase, IComparable {

		private int paymentId;
		private int exceptionTypeId;
		private DateTime createDate;
		private DateTime validatedDate;
		private bool isCorrected;

		private PaymentExceptionTypeComparable paymentExceptionTypeComparable  = PaymentExceptionTypeComparable.PaymentID;
	    private bool sortAssending = true;

		public PaymentExceptionType() : this(int.MinValue) { }
		public PaymentExceptionType(int paymentId) : this(paymentId, int.MinValue) { }
		public PaymentExceptionType(int paymentId, int exceptionTypeId) : this(paymentId, exceptionTypeId, DateTime.MinValue) { }
		public PaymentExceptionType(int paymentId, int exceptionTypeId, DateTime createDate) : this(paymentId, exceptionTypeId, createDate, DateTime.MinValue) { }
		public PaymentExceptionType(int paymentId, int exceptionTypeId, DateTime createDate, DateTime validatedDate) : this(paymentId, exceptionTypeId, createDate, validatedDate, false) { }
		public PaymentExceptionType(int paymentId, int exceptionTypeId, DateTime createDate, DateTime validatedDate, bool isCorrected) {
			this.paymentId = paymentId;
			this.exceptionTypeId = exceptionTypeId;
			this.createDate = createDate;
			this.validatedDate = validatedDate;
			this.isCorrected = isCorrected;

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
			return "<PaymentExceptionType>\r\n" +
			"	<PaymentId>" + paymentId + "</PaymentId>\r\n" +
			"	<ExceptionTypeId>" + exceptionTypeId + "</ExceptionTypeId>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"	<ValidatedDate>" + validatedDate + "</ValidatedDate>\r\n" +
			"</PaymentExceptionType>\r\n";
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
				if(node.Name.ToLower() == "exceptionTypeId") {
					SetXmlValue(ref exceptionTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
				if(node.Name.ToLower() == "validatedDate") {
					SetXmlValue(ref validatedDate, node.InnerText);
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
		public static PaymentExceptionType[] GetPaymentExceptionTypes() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypes();
		}
	
		public static PaymentExceptionType[] GetPaymentExceptionTypesUncorrected(DateTime period) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypesUncorrected(period);
		}

		public static PaymentExceptionType[] GetPaymentExceptionTypesByGroupID(int groupID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypesByGroupID(groupID);
		}

		public static PaymentExceptionType[] GetPaymentExceptionTypesByCheckNo(int checkNo) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypesByCheckNo(checkNo);
		}

		public static PaymentExceptionType GetPaymentExceptionTypeByID(int exceptionTypeId, int paymentID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypeByID(exceptionTypeId, paymentID);
		}

		
		public static PaymentExceptionTypeCollection GetPaymentExceptionTypeByPaymentID(int paymentID) 
		{
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentExceptionTypeByPaymentID(paymentID);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentExceptionType(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentExceptionType(this);
		}
		#endregion

		#region Properties
		public int PaymentId {
			set { paymentId = value; }
			get { return paymentId; }
		}

		public int ExceptionTypeId {
			set { exceptionTypeId = value; }
			get { return exceptionTypeId; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		public DateTime ValidatedDate {
			set { validatedDate = value; }
			get { return validatedDate; }
		}
		
		public bool IsCorrected 
		{
			set { isCorrected = value; }
			get { return isCorrected; }
		}

		#endregion

		#region IComparable Members

		public int CompareTo(object obj)
		{
			PaymentExceptionType paymentExceptionType = obj as PaymentExceptionType;
			if (paymentExceptionType != null)
			{
				switch (paymentExceptionTypeComparable)
				{
			
					case PaymentExceptionTypeComparable.PaymentID:
						return PaymentExceptionTypeComparable.PaymentID.CompareTo(paymentExceptionType.PaymentId);
					case PaymentExceptionTypeComparable.ExceptionTypeID:
						return PaymentExceptionTypeComparable.ExceptionTypeID.CompareTo(paymentExceptionType.ExceptionTypeId);
					case PaymentExceptionTypeComparable.ValidateDate:
						return PaymentExceptionTypeComparable.ValidateDate.CompareTo(paymentExceptionType.ValidatedDate);
					case PaymentExceptionTypeComparable.CreateDate:
						return PaymentExceptionTypeComparable.CreateDate.CompareTo(paymentExceptionType.CreateDate);
					case PaymentExceptionTypeComparable.IsCorrected:
						return PaymentExceptionTypeComparable.IsCorrected.CompareTo(paymentExceptionType.isCorrected);
				
				}
			}
			return 0;
		}

		#endregion
	}
}
