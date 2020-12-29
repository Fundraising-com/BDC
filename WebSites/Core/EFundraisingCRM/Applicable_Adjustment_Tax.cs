using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ApplicableAdjustmentTax: EFundraisingCRMDataObject {

		private int salesId;
		private int adjustementNo;
		private string taxCode;
		private float taxAmount;


		public ApplicableAdjustmentTax() : this(int.MinValue) { }
		public ApplicableAdjustmentTax(int salesId) : this(salesId, int.MinValue) { }
		public ApplicableAdjustmentTax(int salesId, int adjustementNo) : this(salesId, adjustementNo, null) { }
		public ApplicableAdjustmentTax(int salesId, int adjustementNo, string taxCode) : this(salesId, adjustementNo, taxCode, float.MinValue) { }
		public ApplicableAdjustmentTax(int salesId, int adjustementNo, string taxCode, float taxAmount) {
			this.salesId = salesId;
			this.adjustementNo = adjustementNo;
			this.taxCode = taxCode;
			this.taxAmount = taxAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ApplicableAdjustmentTax>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<AdjustementNo>" + adjustementNo + "</AdjustementNo>\r\n" +
			"	<TaxCode>" + System.Web.HttpUtility.HtmlEncode(taxCode) + "</TaxCode>\r\n" +
			"	<TaxAmount>" + taxAmount + "</TaxAmount>\r\n" +
			"</ApplicableAdjustmentTax>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustementNo")) {
					SetXmlValue(ref adjustementNo, node.InnerText);
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
		public static ApplicableAdjustmentTax[] GetApplicableAdjustmentTaxs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableAdjustmentTaxs();
		}

		public static ApplicableAdjustmentTax GetApplicableAdjustmentTaxByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableAdjustmentTaxByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertApplicableAdjustmentTax(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateApplicableAdjustmentTax(this);
		}
		#endregion

		#region Properties
		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public int AdjustementNo {
			set { adjustementNo = value; }
			get { return adjustementNo; }
		}

		public string TaxCode {
			set { taxCode = value; }
			get { return taxCode; }
		}

		public float TaxAmount {
			set { taxAmount = value; }
			get { return taxAmount; }
		}

		#endregion
	}
}
