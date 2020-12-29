using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Template: eFundraisingStoreDataObject {

		private int templateId;
		private string name;
		private string path;
		private DateTime createDate;


		public Template() : this(int.MinValue) { }
		public Template(int templateId) : this(templateId, null) { }
		public Template(int templateId, string name) : this(templateId, name, null) { }
		public Template(int templateId, string name, string path) : this(templateId, name, path, DateTime.MinValue) { }
		public Template(int templateId, string name, string path, DateTime createDate) {
			this.templateId = templateId;
			this.name = name;
			this.path = path;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Template>\r\n" +
			"	<TemplateId>" + templateId + "</TemplateId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Path>" + System.Web.HttpUtility.HtmlEncode(path) + "</Path>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</Template>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "templateId") {
					SetXmlValue(ref templateId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "path") {
					SetXmlValue(ref path, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Template[] GetTemplates() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTemplates();
		}

		public static Template GetTemplateByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTemplateByID(id);
		}
		public static Template GetTemplateByPackagePageNameAndRootPackageID( string pageName, int rootID)
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTemplateByPackagePageNameAndRootPackageID(pageName, rootID);
		}
		public static Template GetTemplateByProductPageName( string pageName)
		{
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetTemplateByProductPageName(pageName);
		}
		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertTemplate(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateTemplate(this);
		}
		#endregion

		#region Properties
		public int TemplateId {
			set { templateId = value; }
			get { return templateId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Path {
			set { path = value; }
			get { return path; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
