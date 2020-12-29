using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class UnassignedConsultantSale: EFundraisingCRMDataObject {

		private int unassignationID;
		private int saleID;
		private int oldConsultantID;
		private int newConsultantID;
		private DateTime unassignedDate;


		public UnassignedConsultantSale() : this(int.MinValue) { }
		public UnassignedConsultantSale(int unassignationID) : this(unassignationID, int.MinValue) { }
		public UnassignedConsultantSale(int unassignationID, int saleID) : this(unassignationID, saleID, int.MinValue) { }
		public UnassignedConsultantSale(int unassignationID, int saleID, int oldConsultantID) : this(unassignationID, saleID, oldConsultantID, int.MinValue) { }
		public UnassignedConsultantSale(int unassignationID, int saleID, int oldConsultantID, int newConsultantID) : this(unassignationID, saleID, oldConsultantID, newConsultantID, DateTime.MinValue) { }
		public UnassignedConsultantSale(int unassignationID, int saleID, int oldConsultantID, int newConsultantID, DateTime unassignedDate) {
			this.unassignationID = unassignationID;
			this.saleID = saleID;
			this.oldConsultantID = oldConsultantID;
			this.newConsultantID = newConsultantID;
			this.unassignedDate = unassignedDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<UnassignedConsultantSale>\r\n" +
			"	<UnassignationID>" + unassignationID + "</UnassignationID>\r\n" +
			"	<SaleID>" + saleID + "</SaleID>\r\n" +
			"	<OldConsultantID>" + oldConsultantID + "</OldConsultantID>\r\n" +
			"	<NewConsultantID>" + newConsultantID + "</NewConsultantID>\r\n" +
			"	<UnassignedDate>" + unassignedDate + "</UnassignedDate>\r\n" +
			"</UnassignedConsultantSale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("unassignationId")) {
					SetXmlValue(ref unassignationID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleId")) {
					SetXmlValue(ref saleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("oldConsultantId")) {
					SetXmlValue(ref oldConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("newConsultantId")) {
					SetXmlValue(ref newConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unassignedDate")) {
					SetXmlValue(ref unassignedDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static UnassignedConsultantSale[] GetUnassignedConsultantSales() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnassignedConsultantSales();
		}

		public static UnassignedConsultantSale GetUnassignedConsultantSaleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnassignedConsultantSaleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertUnassignedConsultantSale(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateUnassignedConsultantSale(this);
		}
		#endregion

		#region Properties
		public int UnassignationID {
			set { unassignationID = value; }
			get { return unassignationID; }
		}

		public int SaleID {
			set { saleID = value; }
			get { return saleID; }
		}

		public int OldConsultantID {
			set { oldConsultantID = value; }
			get { return oldConsultantID; }
		}

		public int NewConsultantID {
			set { newConsultantID = value; }
			get { return newConsultantID; }
		}

		public DateTime UnassignedDate {
			set { unassignedDate = value; }
			get { return unassignedDate; }
		}

		#endregion
	}
}
