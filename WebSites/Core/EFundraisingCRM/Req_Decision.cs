using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ReqDecision: EFundraisingCRMDataObject {

		private int decisionId;
		private int languageId;
		private string description;


		public ReqDecision() : this(int.MinValue) { }
		public ReqDecision(int decisionId) : this(decisionId, int.MinValue) { }
		public ReqDecision(int decisionId, int languageId) : this(decisionId, languageId, null) { }
		public ReqDecision(int decisionId, int languageId, string description) {
			this.decisionId = decisionId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ReqDecision>\r\n" +
			"	<DecisionId>" + decisionId + "</DecisionId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ReqDecision>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("decisionId")) {
					SetXmlValue(ref decisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ReqDecision[] GetReqDecisions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqDecisions();
		}

		public static ReqDecision GetReqDecisionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetReqDecisionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertReqDecision(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateReqDecision(this);
		}
		#endregion

		#region Properties
		public int DecisionId {
			set { decisionId = value; }
			get { return decisionId; }
		}

		public int LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
