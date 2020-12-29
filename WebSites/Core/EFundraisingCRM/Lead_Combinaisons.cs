using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadCombinaisons: EFundraisingCRMDataObject {

		private int leadCombinaisonID;
		private int conditionID;
		private int leadQualificationTypeID;


		public LeadCombinaisons() : this(int.MinValue) { }
		public LeadCombinaisons(int leadCombinaisonID) : this(leadCombinaisonID, int.MinValue) { }
		public LeadCombinaisons(int leadCombinaisonID, int conditionID) : this(leadCombinaisonID, conditionID, int.MinValue) { }
		public LeadCombinaisons(int leadCombinaisonID, int conditionID, int leadQualificationTypeID) {
			this.leadCombinaisonID = leadCombinaisonID;
			this.conditionID = conditionID;
			this.leadQualificationTypeID = leadQualificationTypeID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadCombinaisons>\r\n" +
			"	<LeadCombinaisonID>" + leadCombinaisonID + "</LeadCombinaisonID>\r\n" +
			"	<ConditionID>" + conditionID + "</ConditionID>\r\n" +
			"	<LeadQualificationTypeID>" + leadQualificationTypeID + "</LeadQualificationTypeID>\r\n" +
			"</LeadCombinaisons>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadCombinaisonId")) {
					SetXmlValue(ref leadCombinaisonID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("conditionId")) {
					SetXmlValue(ref conditionID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadQualificationTypeId")) {
					SetXmlValue(ref leadQualificationTypeID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadCombinaisons[] GetLeadCombinaisonss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadCombinaisonss();
		}

		public static LeadCombinaisons GetLeadCombinaisonsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadCombinaisonsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadCombinaisons(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadCombinaisons(this);
		}
		#endregion

		#region Properties
		public int LeadCombinaisonID {
			set { leadCombinaisonID = value; }
			get { return leadCombinaisonID; }
		}

		public int ConditionID {
			set { conditionID = value; }
			get { return conditionID; }
		}

		public int LeadQualificationTypeID {
			set { leadQualificationTypeID = value; }
			get { return leadQualificationTypeID; }
		}

		#endregion
	}
}
