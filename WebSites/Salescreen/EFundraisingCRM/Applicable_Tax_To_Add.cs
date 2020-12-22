using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ApplicableTaxToAdd: EFundraisingCRMDataObject {

		private string taxCode;
		private int saleToAddID;
		private float taxAmount;


		public ApplicableTaxToAdd() : this(null) { }
		public ApplicableTaxToAdd(string taxCode) : this(taxCode, int.MinValue) { }
		public ApplicableTaxToAdd(string taxCode, int saleToAddID) : this(taxCode, saleToAddID, float.MinValue) { }
		public ApplicableTaxToAdd(string taxCode, int saleToAddID, float taxAmount) {
			this.taxCode = taxCode;
			this.saleToAddID = saleToAddID;
			this.taxAmount = taxAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ApplicableTaxToAdd>\r\n" +
			"	<TaxCode>" + System.Web.HttpUtility.HtmlEncode(taxCode) + "</TaxCode>\r\n" +
			"	<SaleToAddID>" + saleToAddID + "</SaleToAddID>\r\n" +
			"	<TaxAmount>" + taxAmount + "</TaxAmount>\r\n" +
			"</ApplicableTaxToAdd>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("taxCode")) {
					SetXmlValue(ref taxCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleToAddId")) {
					SetXmlValue(ref saleToAddID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxAmount")) {
					SetXmlValue(ref taxAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ApplicableTaxToAdd[] GetApplicableTaxToAdds() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableTaxToAdds();
		}

		/*
		public static ApplicableTaxToAdd GetApplicableTaxToAddByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetApplicableTaxToAddByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertApplicableTaxToAdd(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateApplicableTaxToAdd(this);
		}*/
		#endregion

		#region Properties
		public string TaxCode {
			set { taxCode = value; }
			get { return taxCode; }
		}

		public int SaleToAddID {
			set { saleToAddID = value; }
			get { return saleToAddID; }
		}

		public float TaxAmount {
			set { taxAmount = value; }
			get { return taxAmount; }
		}

		#endregion
	}
}
