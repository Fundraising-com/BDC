using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadConditions: EFundraisingCRMDataObject {

		private int conditionID;
		private string description;


		public LeadConditions() : this(int.MinValue) { }
		public LeadConditions(int conditionID) : this(conditionID, null) { }
		public LeadConditions(int conditionID, string description) {
			this.conditionID = conditionID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadConditions>\r\n" +
			"	<ConditionID>" + conditionID + "</ConditionID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LeadConditions>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("conditionId")) {
					SetXmlValue(ref conditionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadConditions[] GetLeadConditionss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadConditionss();
		}

		public static LeadConditions GetLeadConditionsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadConditionsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadConditions(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadConditions(this);
		}
		#endregion

		#region Properties
		public int ConditionID {
			set { conditionID = value; }
			get { return conditionID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
