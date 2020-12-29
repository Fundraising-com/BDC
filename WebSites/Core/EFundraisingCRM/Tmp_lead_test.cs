using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TmpLeadTest: EFundraisingCRMDataObject {

		private int leadId;


		public TmpLeadTest() : this(int.MinValue) { }
		public TmpLeadTest(int leadId) {
			this.leadId = leadId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TmpLeadTest>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"</TmpLeadTest>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static TmpLeadTest[] GetTmpLeadTests() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTmpLeadTests();
		}

		public static TmpLeadTest GetTmpLeadTestByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTmpLeadTestByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTmpLeadTest(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTmpLeadTest(this);
		}*/
		#endregion

		#region Properties
		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		#endregion
	}
}
