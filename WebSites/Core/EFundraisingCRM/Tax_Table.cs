using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TaxTable: EFundraisingCRMDataObject {

		private string taxCode;
		private string description;
		private string taxAccountNumber;
		private string descriptionFrancaise;


		public TaxTable() : this(null) { }
		public TaxTable(string taxCode) : this(taxCode, null) { }
		public TaxTable(string taxCode, string description) : this(taxCode, description, null) { }
		public TaxTable(string taxCode, string description, string taxAccountNumber) : this(taxCode, description, taxAccountNumber, null) { }
		public TaxTable(string taxCode, string description, string taxAccountNumber, string descriptionFrancaise) {
			this.taxCode = taxCode;
			this.description = description;
			this.taxAccountNumber = taxAccountNumber;
			this.descriptionFrancaise = descriptionFrancaise;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TaxTable>\r\n" +
			"	<TaxCode>" + System.Web.HttpUtility.HtmlEncode(taxCode) + "</TaxCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<TaxAccountNumber>" + System.Web.HttpUtility.HtmlEncode(taxAccountNumber) + "</TaxAccountNumber>\r\n" +
			"	<DescriptionFrancaise>" + System.Web.HttpUtility.HtmlEncode(descriptionFrancaise) + "</DescriptionFrancaise>\r\n" +
			"</TaxTable>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("taxCode")) {
					SetXmlValue(ref taxCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxAccountNumber")) {
					SetXmlValue(ref taxAccountNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("descriptionFrancaise")) {
					SetXmlValue(ref descriptionFrancaise, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TaxTable[] GetTaxTables() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTaxTables();
		}

		/*
		public static TaxTable GetTaxTableByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTaxTableByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTaxTable(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTaxTable(this);
		}*/
		#endregion

		#region Properties
		public string TaxCode {
			set { taxCode = value; }
			get { return taxCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public string TaxAccountNumber {
			set { taxAccountNumber = value; }
			get { return taxAccountNumber; }
		}

		public string DescriptionFrancaise {
			set { descriptionFrancaise = value; }
			get { return descriptionFrancaise; }
		}

		#endregion
	}
}
