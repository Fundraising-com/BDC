using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EFOTag: EFundraisingCRMDataObject {

		private int emailTypeID;
		private string tagName;
		private int tagID;


		public EFOTag() : this(int.MinValue) { }
		public EFOTag(int emailTypeID) : this(emailTypeID, null) { }
		public EFOTag(int emailTypeID, string tagName) : this(emailTypeID, tagName, int.MinValue) { }
		public EFOTag(int emailTypeID, string tagName, int tagID) {
			this.emailTypeID = emailTypeID;
			this.tagName = tagName;
			this.tagID = tagID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOTag>\r\n" +
			"	<EmailTypeID>" + emailTypeID + "</EmailTypeID>\r\n" +
			"	<TagName>" + System.Web.HttpUtility.HtmlEncode(tagName) + "</TagName>\r\n" +
			"	<TagID>" + tagID + "</TagID>\r\n" +
			"</EFOTag>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("emailTypeId")) {
					SetXmlValue(ref emailTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tagName")) {
					SetXmlValue(ref tagName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tagId")) {
					SetXmlValue(ref tagID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOTag[] GetEFOTags() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOTags();
		}

		public static EFOTag GetEFOTagByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOTagByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOTag(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOTag(this);
		}
		#endregion

		#region Properties
		public int EmailTypeID {
			set { emailTypeID = value; }
			get { return emailTypeID; }
		}

		public string TagName {
			set { tagName = value; }
			get { return tagName; }
		}

		public int TagID {
			set { tagID = value; }
			get { return tagID; }
		}

		#endregion
	}
}
