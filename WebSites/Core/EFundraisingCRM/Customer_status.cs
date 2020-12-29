using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CustomerStatus: EFundraisingCRMDataObject {

		private int customerStatusId;
		private string customerStatusDesc;
		private DateTime createDate;


		public CustomerStatus() : this(int.MinValue) { }
		public CustomerStatus(int customerStatusId) : this(customerStatusId, null) { }
		public CustomerStatus(int customerStatusId, string customerStatusDesc) : this(customerStatusId, customerStatusDesc, DateTime.MinValue) { }
		public CustomerStatus(int customerStatusId, string customerStatusDesc, DateTime createDate) {
			this.customerStatusId = customerStatusId;
			this.customerStatusDesc = customerStatusDesc;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CustomerStatus>\r\n" +
			"	<CustomerStatusId>" + customerStatusId + "</CustomerStatusId>\r\n" +
			"	<CustomerStatusDesc>" + System.Web.HttpUtility.HtmlEncode(customerStatusDesc) + "</CustomerStatusDesc>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</CustomerStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("customerStatusId")) {
					SetXmlValue(ref customerStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("customerStatusDesc")) {
					SetXmlValue(ref customerStatusDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("createDate")) {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CustomerStatus[] GetCustomerStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCustomerStatuss();
		}

		public static CustomerStatus GetCustomerStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCustomerStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCustomerStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCustomerStatus(this);
		}
		#endregion

		#region Properties
		public int CustomerStatusId {
			set { customerStatusId = value; }
			get { return customerStatusId; }
		}

		public string CustomerStatusDesc {
			set { customerStatusDesc = value; }
			get { return customerStatusDesc; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
