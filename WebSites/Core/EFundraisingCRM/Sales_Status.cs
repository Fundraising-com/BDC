using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SalesStatus: EFundraisingCRMDataObject {

		private int salesStatusID;
		private string description;


		public SalesStatus() : this(int.MinValue) { }
		public SalesStatus(int salesStatusID) : this(salesStatusID, null) { }
		public SalesStatus(int salesStatusID, string description) {
			this.salesStatusID = salesStatusID;
			this.description = description;
		}

		#region Static Data
		public static SalesStatus Default {
			get { return SalesStatus.New; }
		}
		
		public static SalesStatus New {
			get { return new SalesStatus(1, "New"); }
		}

		public static SalesStatus Confirmed {
			get { return new SalesStatus(2, "Confirmed"); }
		}

		public static SalesStatus Cancelled {
			get { return new SalesStatus(4, "Cancelled"); }
		}

		public static SalesStatus OnHold {
			get { return new SalesStatus(6, "On Hold"); }
		}

		public static SalesStatus PendingConfirmation {
			get { return new SalesStatus(7, "Pending Confirmation"); }
		}

		public static SalesStatus Unreachable {
			get { return new SalesStatus(8, "Unreachable"); }
		}

		public static SalesStatus PendingCancellation {
			get { return new SalesStatus(12, "Pending Cancellation"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesStatus>\r\n" +
			"	<SalesStatusID>" + salesStatusID + "</SalesStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</SalesStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesStatusId")) {
					SetXmlValue(ref salesStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesStatus[] GetSalesStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesStatuss();
		}

		public static SalesStatus GetSalesStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesStatus(this);
		}
		#endregion

		#region Properties
		public int SalesStatusID {
			set { salesStatusID = value; }
			get { return salesStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
