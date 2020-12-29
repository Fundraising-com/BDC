using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LocalSponsorSteps: EFundraisingCRMDataObject {

		private int stepId;
		private string description;


		public LocalSponsorSteps() : this(int.MinValue) { }
		public LocalSponsorSteps(int stepId) : this(stepId, null) { }
		public LocalSponsorSteps(int stepId, string description) {
			this.stepId = stepId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LocalSponsorSteps>\r\n" +
			"	<StepId>" + stepId + "</StepId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LocalSponsorSteps>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("stepId")) {
					SetXmlValue(ref stepId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LocalSponsorSteps[] GetLocalSponsorStepss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorStepss();
		}

		public static LocalSponsorSteps GetLocalSponsorStepsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorStepsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLocalSponsorSteps(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLocalSponsorSteps(this);
		}
		#endregion

		#region Properties
		public int StepId {
			set { stepId = value; }
			get { return stepId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
