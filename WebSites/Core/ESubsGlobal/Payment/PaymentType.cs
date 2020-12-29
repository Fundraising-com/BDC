using System;
using System.Xml;

namespace GA.BDC.Core.ESubsGlobal.Payment {

	public class PaymentType: DataObject {

		private int paymentTypeId;
		private string paymentTypeName;
		private DateTime createDate;


		public PaymentType() : this(int.MinValue) { }
		public PaymentType(int paymentTypeId) : this(paymentTypeId, null) { }
		public PaymentType(int paymentTypeId, string paymentTypeName) : this(paymentTypeId, paymentTypeName, DateTime.MinValue) { }
		public PaymentType(int paymentTypeId, string paymentTypeName, DateTime createDate) {
			this.paymentTypeId = paymentTypeId;
			this.paymentTypeName = paymentTypeName;
			this.createDate = createDate;
		}

		#region Static Data
		public static int Check {
			get { return 1; }
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PaymentType>\r\n" +
			"	<PaymentTypeId>" + paymentTypeId + "</PaymentTypeId>\r\n" +
			"	<PaymentTypeName>" + System.Web.HttpUtility.HtmlEncode(paymentTypeName) + "</PaymentTypeName>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</PaymentType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "paymentTypeId") {
					SetXmlValue(ref paymentTypeId, node.InnerText);
				}
				if(node.Name.ToLower() == "paymentTypeName") {
					SetXmlValue(ref paymentTypeName, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PaymentType[] GetPaymentTypes() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentTypes();
		}

		public static PaymentType GetPaymentTypeByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetPaymentTypeByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertPaymentType(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdatePaymentType(this);
		}
		#endregion

		#region Properties
		public int PaymentTypeId {
			set { paymentTypeId = value; }
			get { return paymentTypeId; }
		}

		public string PaymentTypeName {
			set { paymentTypeName = value; }
			get { return paymentTypeName; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
