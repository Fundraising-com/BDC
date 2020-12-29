using System;
using System.Xml;

using GA.BDC.Core.ESubsGlobal;

namespace GA.BDC.Core.ESubsGlobal.FlagPole {

	public class QspMatchingCode: DataObject {

		private int qspMatchingCodeId;
		private int accountId;
		private string custBillingMatchingCode;
		private string custShippingMatchingCode;
		private string accountMatchingCode;


		public QspMatchingCode() : this(int.MinValue) { }
		public QspMatchingCode(int qspMatchingCodeId) : this(qspMatchingCodeId, int.MinValue) { }
		public QspMatchingCode(int qspMatchingCodeId, int accountId) : this(qspMatchingCodeId, accountId, null) { }
		public QspMatchingCode(int qspMatchingCodeId, int accountId, string custBillingMatchingCode) : this(qspMatchingCodeId, accountId, custBillingMatchingCode, null) { }
		public QspMatchingCode(int qspMatchingCodeId, int accountId, string custBillingMatchingCode, string custShippingMatchingCode) : this(qspMatchingCodeId, accountId, custBillingMatchingCode, custShippingMatchingCode, null) { }
		public QspMatchingCode(int qspMatchingCodeId, int accountId, string custBillingMatchingCode, string custShippingMatchingCode, string accountMatchingCode) {
			this.qspMatchingCodeId = qspMatchingCodeId;
			this.accountId = accountId;
			this.custBillingMatchingCode = custBillingMatchingCode;
			this.custShippingMatchingCode = custShippingMatchingCode;
			this.accountMatchingCode = accountMatchingCode;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<QspMatchingCode>\r\n" +
				"	<QspMatchingCodeId>" + qspMatchingCodeId + "</QspMatchingCodeId>\r\n" +
				"	<AccountId>" + accountId + "</AccountId>\r\n" +
				"	<CustBillingMatchingCode>" + System.Web.HttpUtility.HtmlEncode(custBillingMatchingCode) + "</CustBillingMatchingCode>\r\n" +
				"	<CustShippingMatchingCode>" + System.Web.HttpUtility.HtmlEncode(custShippingMatchingCode) + "</CustShippingMatchingCode>\r\n" +
				"	<AccountMatchingCode>" + System.Web.HttpUtility.HtmlEncode(accountMatchingCode) + "</AccountMatchingCode>\r\n" +
				"</QspMatchingCode>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "qspMatchingCodeId") {
					SetXmlValue(ref qspMatchingCodeId, node.InnerText);
				}
				if(node.Name.ToLower() == "accountId") {
					SetXmlValue(ref accountId, node.InnerText);
				}
				if(node.Name.ToLower() == "custBillingMatchingCode") {
					SetXmlValue(ref custBillingMatchingCode, node.InnerText);
				}
				if(node.Name.ToLower() == "custShippingMatchingCode") {
					SetXmlValue(ref custShippingMatchingCode, node.InnerText);
				}
				if(node.Name.ToLower() == "accountMatchingCode") {
					SetXmlValue(ref accountMatchingCode, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static QspMatchingCode[] GetQspMatchingCodes() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetQspMatchingCodes();
		}

		public static QspMatchingCode GetQspMatchingCodeByID(int id) {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.GetQspMatchingCodeByID(id);
		}

		public int Insert() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.InsertQspMatchingCode(this);
		}

		public int Update() {
			DataAccess.ESubsGlobalDatabase dbo = new DataAccess.ESubsGlobalDatabase();
			return dbo.UpdateQspMatchingCode(this);
		}
		#endregion

		#region Properties
		public int QspMatchingCodeId {
			set { qspMatchingCodeId = value; }
			get { return qspMatchingCodeId; }
		}

		public int AccountId {
			set { accountId = value; }
			get { return accountId; }
		}

		public string CustBillingMatchingCode {
			set { custBillingMatchingCode = value; }
			get { return custBillingMatchingCode; }
		}

		public string CustShippingMatchingCode {
			set { custShippingMatchingCode = value; }
			get { return custShippingMatchingCode; }
		}

		public string AccountMatchingCode {
			set { accountMatchingCode = value; }
			get { return accountMatchingCode; }
		}

		#endregion
	}
}
