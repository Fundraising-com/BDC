using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CreditApprovalMethod: EFundraisingCRMDataObject {

		private int creditApprovalMethodID;
		private string description;


		public CreditApprovalMethod() : this(int.MinValue) { }
		public CreditApprovalMethod(int creditApprovalMethodID) : this(creditApprovalMethodID, null) { }
		public CreditApprovalMethod(int creditApprovalMethodID, string description) {
			this.creditApprovalMethodID = creditApprovalMethodID;
			this.description = description;
		}

		#region Static Data
		
		public static CreditApprovalMethod RepeatCustomer
		{
			get { return new CreditApprovalMethod(1, "Repeat customer"); }
		}

		public static CreditApprovalMethod ScratchcardBelow2000
		{
			get { return new CreditApprovalMethod(2, "Scratchcard below 2000$"); }
		}
		
		public static CreditApprovalMethod PaymentByCreditCard
		{
			get { return new CreditApprovalMethod(3, "Payment by credit card"); }
		}
		
		public static CreditApprovalMethod CreditApprovedByAR
		{
			get { return new CreditApprovalMethod(4, "Credit approved by AR"); }
		}
		
		public static CreditApprovalMethod CreditApprovedByManagement
		{
			get { return new CreditApprovalMethod(5, "Credit approved by management"); }
		}
		
		public static CreditApprovalMethod CreditDenied
		{
			get { return new CreditApprovalMethod(6, "Credit denied"); }
		}
		
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CreditApprovalMethod>\r\n" +
			"	<CreditApprovalMethodID>" + creditApprovalMethodID + "</CreditApprovalMethodID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</CreditApprovalMethod>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("creditApprovalMethodId")) {
					SetXmlValue(ref creditApprovalMethodID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CreditApprovalMethod[] GetCreditApprovalMethods() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditApprovalMethods();
		}

		public static CreditApprovalMethod GetCreditApprovalMethodByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCreditApprovalMethodByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCreditApprovalMethod(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCreditApprovalMethod(this);
		}
		#endregion

		#region Properties
		public int CreditApprovalMethodID {
			set { creditApprovalMethodID = value; }
			get { return creditApprovalMethodID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
