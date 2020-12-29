using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ApplicableTax: EFundraisingCRMDataObject {

		private int salesID;
		private string taxCode;
		private decimal taxAmount;


		public ApplicableTax() : this(int.MinValue) { }
		public ApplicableTax(int salesID) : this(salesID, null) { }
		public ApplicableTax(int salesID, string taxCode) : this(salesID, taxCode, decimal.MinValue) { }
		public ApplicableTax(int salesID, string taxCode, decimal taxAmount) {
			this.salesID = salesID;
			this.taxCode = taxCode;
			this.taxAmount = taxAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ApplicableTax>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<TaxCode>" + System.Web.HttpUtility.HtmlEncode(taxCode) + "</TaxCode>\r\n" +
			"	<TaxAmount>" + taxAmount + "</TaxAmount>\r\n" +
			"</ApplicableTax>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxCode")) {
					SetXmlValue(ref taxCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxAmount")) {
					SetXmlValue(ref taxAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ApplicableTax[] GetApplicableTaxs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableTaxs();
		}

		public static ApplicableTax[] GetApplicableTaxByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableTaxByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertApplicableTax(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateApplicableTax(this);
		}
		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public string TaxCode {
			set { taxCode = value; }
			get { return taxCode; }
		}

		public decimal TaxAmount {
			set { taxAmount = value; }
			get { return taxAmount; }
		}

		#endregion
	}
}
