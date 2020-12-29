using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TemplateSet: EFundraisingCRMDataObject {

		private int templateSetID;
		private int qSPProgramID;
		private string supporterPath;
		private string genericPath;
		private string editPath;


		public TemplateSet() : this(int.MinValue) { }
		public TemplateSet(int templateSetID) : this(templateSetID, int.MinValue) { }
		public TemplateSet(int templateSetID, int qSPProgramID) : this(templateSetID, qSPProgramID, null) { }
		public TemplateSet(int templateSetID, int qSPProgramID, string supporterPath) : this(templateSetID, qSPProgramID, supporterPath, null) { }
		public TemplateSet(int templateSetID, int qSPProgramID, string supporterPath, string genericPath) : this(templateSetID, qSPProgramID, supporterPath, genericPath, null) { }
		public TemplateSet(int templateSetID, int qSPProgramID, string supporterPath, string genericPath, string editPath) {
			this.templateSetID = templateSetID;
			this.qSPProgramID = qSPProgramID;
			this.supporterPath = supporterPath;
			this.genericPath = genericPath;
			this.editPath = editPath;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TemplateSet>\r\n" +
			"	<TemplateSetID>" + templateSetID + "</TemplateSetID>\r\n" +
			"	<QSPProgramID>" + qSPProgramID + "</QSPProgramID>\r\n" +
			"	<SupporterPath>" + System.Web.HttpUtility.HtmlEncode(supporterPath) + "</SupporterPath>\r\n" +
			"	<GenericPath>" + System.Web.HttpUtility.HtmlEncode(genericPath) + "</GenericPath>\r\n" +
			"	<EditPath>" + System.Web.HttpUtility.HtmlEncode(editPath) + "</EditPath>\r\n" +
			"</TemplateSet>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("templateSetId")) {
					SetXmlValue(ref templateSetID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("qspProgramId")) {
					SetXmlValue(ref qSPProgramID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("supporterPath")) {
					SetXmlValue(ref supporterPath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("genericPath")) {
					SetXmlValue(ref genericPath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("editPath")) {
					SetXmlValue(ref editPath, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TemplateSet[] GetTemplateSets() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTemplateSets();
		}

		public static TemplateSet GetTemplateSetByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTemplateSetByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTemplateSet(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTemplateSet(this);
		}
		#endregion

		#region Properties
		public int TemplateSetID {
			set { templateSetID = value; }
			get { return templateSetID; }
		}

		public int QSPProgramID {
			set { qSPProgramID = value; }
			get { return qSPProgramID; }
		}

		public string SupporterPath {
			set { supporterPath = value; }
			get { return supporterPath; }
		}

		public string GenericPath {
			set { genericPath = value; }
			get { return genericPath; }
		}

		public string EditPath {
			set { editPath = value; }
			get { return editPath; }
		}

		#endregion
	}
}
