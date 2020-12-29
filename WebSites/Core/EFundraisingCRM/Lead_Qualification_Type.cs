using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadQualificationType: EFundraisingCRMDataObject {

		private int leadQualificationTypeID;
		private string description;
		private int weight;


		public LeadQualificationType() : this(int.MinValue) { }
		public LeadQualificationType(int leadQualificationTypeID) : this(leadQualificationTypeID, null) { }
		public LeadQualificationType(int leadQualificationTypeID, string description) : this(leadQualificationTypeID, description, int.MinValue) { }
		public LeadQualificationType(int leadQualificationTypeID, string description, int weight) {
			this.leadQualificationTypeID = leadQualificationTypeID;
			this.description = description;
			this.weight = weight;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadQualificationType>\r\n" +
			"	<LeadQualificationTypeID>" + leadQualificationTypeID + "</LeadQualificationTypeID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<Weight>" + weight + "</Weight>\r\n" +
			"</LeadQualificationType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadQualificationTypeId")) {
					SetXmlValue(ref leadQualificationTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("weight")) {
					SetXmlValue(ref weight, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LeadQualificationType[] GetLeadQualificationTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadQualificationTypes();
		}

		public static LeadQualificationType GetLeadQualificationTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadQualificationTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadQualificationType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadQualificationType(this);
		}
		#endregion

		#region Properties
		public int LeadQualificationTypeID {
			set { leadQualificationTypeID = value; }
			get { return leadQualificationTypeID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int Weight {
			set { weight = value; }
			get { return weight; }
		}

		#endregion
	}
}
