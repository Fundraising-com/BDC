using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SCSECTION: EFundraisingCRMDataObject {

		private int sectionId;
		private string sectionTitle;
		private string sectionImage;
		private string sectionText;
		private string sectionTemplate;
		private string sectionSubTitle;


		public SCSECTION() : this(int.MinValue) { }
		public SCSECTION(int sectionId) : this(sectionId, null) { }
		public SCSECTION(int sectionId, string sectionTitle) : this(sectionId, sectionTitle, null) { }
		public SCSECTION(int sectionId, string sectionTitle, string sectionImage) : this(sectionId, sectionTitle, sectionImage, null) { }
		public SCSECTION(int sectionId, string sectionTitle, string sectionImage, string sectionText) : this(sectionId, sectionTitle, sectionImage, sectionText, null) { }
		public SCSECTION(int sectionId, string sectionTitle, string sectionImage, string sectionText, string sectionTemplate) : this(sectionId, sectionTitle, sectionImage, sectionText, sectionTemplate, null) { }
		public SCSECTION(int sectionId, string sectionTitle, string sectionImage, string sectionText, string sectionTemplate, string sectionSubTitle) {
			this.sectionId = sectionId;
			this.sectionTitle = sectionTitle;
			this.sectionImage = sectionImage;
			this.sectionText = sectionText;
			this.sectionTemplate = sectionTemplate;
			this.sectionSubTitle = sectionSubTitle;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SCSECTION>\r\n" +
			"	<SectionId>" + sectionId + "</SectionId>\r\n" +
			"	<SectionTitle>" + System.Web.HttpUtility.HtmlEncode(sectionTitle) + "</SectionTitle>\r\n" +
			"	<SectionImage>" + System.Web.HttpUtility.HtmlEncode(sectionImage) + "</SectionImage>\r\n" +
			"	<SectionText>" + System.Web.HttpUtility.HtmlEncode(sectionText) + "</SectionText>\r\n" +
			"	<SectionTemplate>" + System.Web.HttpUtility.HtmlEncode(sectionTemplate) + "</SectionTemplate>\r\n" +
			"	<SectionSubTitle>" + System.Web.HttpUtility.HtmlEncode(sectionSubTitle) + "</SectionSubTitle>\r\n" +
			"</SCSECTION>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("sectionId")) {
					SetXmlValue(ref sectionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionTitle")) {
					SetXmlValue(ref sectionTitle, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionImage")) {
					SetXmlValue(ref sectionImage, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionText")) {
					SetXmlValue(ref sectionText, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionTemplate")) {
					SetXmlValue(ref sectionTemplate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sectionSubTitle")) {
					SetXmlValue(ref sectionSubTitle, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SCSECTION[] GetSCSECTIONs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSCSECTIONs();
		}

		public static SCSECTION GetSCSECTIONByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSCSECTIONByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSCSECTION(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSCSECTION(this);
		}
		#endregion

		#region Properties
		public int SectionId {
			set { sectionId = value; }
			get { return sectionId; }
		}

		public string SectionTitle {
			set { sectionTitle = value; }
			get { return sectionTitle; }
		}

		public string SectionImage {
			set { sectionImage = value; }
			get { return sectionImage; }
		}

		public string SectionText {
			set { sectionText = value; }
			get { return sectionText; }
		}

		public string SectionTemplate {
			set { sectionTemplate = value; }
			get { return sectionTemplate; }
		}

		public string SectionSubTitle {
			set { sectionSubTitle = value; }
			get { return sectionSubTitle; }
		}

		#endregion
	}
}
