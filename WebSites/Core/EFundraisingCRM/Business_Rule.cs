using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BusinessRule: EFundraisingCRMDataObject {

		private int businessRuleID;
		private int partnerID;
		private string ruleDescription;
		private string moduleName;
		private string formName;
		private string accessSubName;


		public BusinessRule() : this(int.MinValue) { }
		public BusinessRule(int businessRuleID) : this(businessRuleID, int.MinValue) { }
		public BusinessRule(int businessRuleID, int partnerID) : this(businessRuleID, partnerID, null) { }
		public BusinessRule(int businessRuleID, int partnerID, string ruleDescription) : this(businessRuleID, partnerID, ruleDescription, null) { }
		public BusinessRule(int businessRuleID, int partnerID, string ruleDescription, string moduleName) : this(businessRuleID, partnerID, ruleDescription, moduleName, null) { }
		public BusinessRule(int businessRuleID, int partnerID, string ruleDescription, string moduleName, string formName) : this(businessRuleID, partnerID, ruleDescription, moduleName, formName, null) { }
		public BusinessRule(int businessRuleID, int partnerID, string ruleDescription, string moduleName, string formName, string accessSubName) {
			this.businessRuleID = businessRuleID;
			this.partnerID = partnerID;
			this.ruleDescription = ruleDescription;
			this.moduleName = moduleName;
			this.formName = formName;
			this.accessSubName = accessSubName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BusinessRule>\r\n" +
			"	<BusinessRuleID>" + businessRuleID + "</BusinessRuleID>\r\n" +
			"	<PartnerID>" + partnerID + "</PartnerID>\r\n" +
			"	<RuleDescription>" + System.Web.HttpUtility.HtmlEncode(ruleDescription) + "</RuleDescription>\r\n" +
			"	<ModuleName>" + System.Web.HttpUtility.HtmlEncode(moduleName) + "</ModuleName>\r\n" +
			"	<FormName>" + System.Web.HttpUtility.HtmlEncode(formName) + "</FormName>\r\n" +
			"	<AccessSubName>" + System.Web.HttpUtility.HtmlEncode(accessSubName) + "</AccessSubName>\r\n" +
			"</BusinessRule>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("businessRuleId")) {
					SetXmlValue(ref businessRuleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ruleDescription")) {
					SetXmlValue(ref ruleDescription, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("moduleName")) {
					SetXmlValue(ref moduleName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("formName")) {
					SetXmlValue(ref formName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accessSubName")) {
					SetXmlValue(ref accessSubName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BusinessRule[] GetBusinessRules() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBusinessRules();
		}

		public static BusinessRule GetBusinessRuleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBusinessRuleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBusinessRule(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBusinessRule(this);
		}
		#endregion

		#region Properties
		public int BusinessRuleID {
			set { businessRuleID = value; }
			get { return businessRuleID; }
		}

		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public string RuleDescription {
			set { ruleDescription = value; }
			get { return ruleDescription; }
		}

		public string ModuleName {
			set { moduleName = value; }
			get { return moduleName; }
		}

		public string FormName {
			set { formName = value; }
			get { return formName; }
		}

		public string AccessSubName {
			set { accessSubName = value; }
			get { return accessSubName; }
		}

		#endregion
	}
}
