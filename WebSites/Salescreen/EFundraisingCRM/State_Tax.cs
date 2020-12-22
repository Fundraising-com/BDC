using System;
using System.Xml;
using System.Data;

namespace efundraising.EFundraisingCRM {

	public class StateTax: EFundraisingCRMDataObject {

		private string stateCode;
		private string taxCode;
		private DateTime effectiveDate;
		private float taxRate;
		private int taxOrder;


		public StateTax() : this(null) { }
		public StateTax(string stateCode) : this(stateCode, null) { }
		public StateTax(string stateCode, string taxCode) : this(stateCode, taxCode, DateTime.MinValue) { }
		public StateTax(string stateCode, string taxCode, DateTime effectiveDate) : this(stateCode, taxCode, effectiveDate, float.MinValue) { }
		public StateTax(string stateCode, string taxCode, DateTime effectiveDate, float taxRate) : this(stateCode, taxCode, effectiveDate, taxRate, int.MinValue) { }
		public StateTax(string stateCode, string taxCode, DateTime effectiveDate, float taxRate, int taxOrder) {
			this.stateCode = stateCode;
			this.taxCode = taxCode;
			this.effectiveDate = effectiveDate;
			this.taxRate = taxRate;
			this.taxOrder = taxOrder;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<StateTax>\r\n" +
			"	<StateCode>" + System.Web.HttpUtility.HtmlEncode(stateCode) + "</StateCode>\r\n" +
			"	<TaxCode>" + System.Web.HttpUtility.HtmlEncode(taxCode) + "</TaxCode>\r\n" +
			"	<EffectiveDate>" + effectiveDate + "</EffectiveDate>\r\n" +
			"	<TaxRate>" + taxRate + "</TaxRate>\r\n" +
			"	<TaxOrder>" + taxOrder + "</TaxOrder>\r\n" +
			"</StateTax>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("stateCode")) {
					SetXmlValue(ref stateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxCode")) {
					SetXmlValue(ref taxCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("effectiveDate")) {
					SetXmlValue(ref effectiveDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxRate")) {
					SetXmlValue(ref taxRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("taxOrder")) {
					SetXmlValue(ref taxOrder, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static StateTax[] GetStateTaxs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetStateTaxs();
		}

		
		public static DataTable GetStateTaxByClientID(int clientID, string clientSeqCode) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetStateTaxByClientID(clientID,clientSeqCode);
		}
/*
		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertStateTax(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateStateTax(this);
		}*/
		#endregion

		#region Properties
		public string StateCode {
			set { stateCode = value; }
			get { return stateCode; }
		}

		public string TaxCode {
			set { taxCode = value; }
			get { return taxCode; }
		}

		public DateTime EffectiveDate {
			set { effectiveDate = value; }
			get { return effectiveDate; }
		}

		public float TaxRate {
			set { taxRate = value; }
			get { return taxRate; }
		}

		public int TaxOrder {
			set { taxOrder = value; }
			get { return taxOrder; }
		}

		#endregion
	}
}
