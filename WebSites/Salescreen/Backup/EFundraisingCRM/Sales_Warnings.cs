using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SalesWarnings: EFundraisingCRMDataObject {

		private int salesID;
		private int salesItemNo;
		private int salesConstraintId;


		public SalesWarnings() : this(int.MinValue) { }
		public SalesWarnings(int salesID) : this(salesID, int.MinValue) { }
		public SalesWarnings(int salesID, int salesItemNo) : this(salesID, salesItemNo, int.MinValue) { }
		public SalesWarnings(int salesID, int salesItemNo, int salesConstraintId) {
			this.salesID = salesID;
			this.salesItemNo = salesItemNo;
			this.salesConstraintId = salesConstraintId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesWarnings>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"	<SalesConstraintId>" + salesConstraintId + "</SalesConstraintId>\r\n" +
			"</SalesWarnings>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesConstraintId")) {
					SetXmlValue(ref salesConstraintId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesWarnings[] GetSalesWarningss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesWarningss();
		}

		public static SalesWarnings GetSalesWarningsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesWarningsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesWarnings(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesWarnings(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		public int SalesConstraintId {
			set { salesConstraintId = value; }
			get { return salesConstraintId; }
		}

		#endregion
	}
}
