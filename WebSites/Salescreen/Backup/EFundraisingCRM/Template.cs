using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Template: EFundraisingCRMDataObject {

		private int partnerID;
		private string templatePath;
		private string reportCenterPasswd;


		public Template() : this(int.MinValue) { }
		public Template(int partnerID) : this(partnerID, null) { }
		public Template(int partnerID, string templatePath) : this(partnerID, templatePath, null) { }
		public Template(int partnerID, string templatePath, string reportCenterPasswd) {
			this.partnerID = partnerID;
			this.templatePath = templatePath;
			this.reportCenterPasswd = reportCenterPasswd;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Template>\r\n" +
			"	<PartnerID>" + partnerID + "</PartnerID>\r\n" +
			"	<TemplatePath>" + System.Web.HttpUtility.HtmlEncode(templatePath) + "</TemplatePath>\r\n" +
			"	<ReportCenterPasswd>" + System.Web.HttpUtility.HtmlEncode(reportCenterPasswd) + "</ReportCenterPasswd>\r\n" +
			"</Template>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("templatePath")) {
					SetXmlValue(ref templatePath, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("reportcenterpasswd")) {
					SetXmlValue(ref reportCenterPasswd, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Template[] GetTemplates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTemplates();
		}

		public static Template GetTemplateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTemplateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTemplate(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTemplate(this);
		}
		#endregion

		#region Properties
		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public string TemplatePath {
			set { templatePath = value; }
			get { return templatePath; }
		}

		public string ReportCenterPasswd {
			set { reportCenterPasswd = value; }
			get { return reportCenterPasswd; }
		}

		#endregion
	}
}
