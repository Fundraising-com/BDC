using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class GeneralComment: EFundraisingCRMDataObject {

		private int generalCommentId;
		private int leadId;
		private int salesId;
		private DateTime entryDate;
		private string generalComment;
		private string userName;
		private int departmentID;


		public GeneralComment() : this(int.MinValue) { }
		public GeneralComment(int generalCommentId) : this(generalCommentId, int.MinValue) { }
		public GeneralComment(int generalCommentId, int leadId) : this(generalCommentId, leadId, int.MinValue) { }
		public GeneralComment(int generalCommentId, int leadId, int salesId) : this(generalCommentId, leadId, salesId, DateTime.MinValue) { }
		public GeneralComment(int generalCommentId, int leadId, int salesId, DateTime entryDate) : this(generalCommentId, leadId, salesId, entryDate, null) { }
		public GeneralComment(int generalCommentId, int leadId, int salesId, DateTime entryDate, string generalComment) : this(generalCommentId, leadId, salesId, entryDate, generalComment, null) { }
		public GeneralComment(int generalCommentId, int leadId, int salesId, DateTime entryDate, string generalComment, string userName) : this(generalCommentId, leadId, salesId, entryDate, generalComment, userName, int.MinValue) { }
		public GeneralComment(int generalCommentId, int leadId, int salesId, DateTime entryDate, string generalComment, string userName, int departmentID) {
			this.generalCommentId = generalCommentId;
			this.leadId = leadId;
			this.salesId = salesId;
			this.entryDate = entryDate;
			this.generalComment = generalComment;
			this.userName = userName;
			this.departmentID = departmentID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<GeneralComment>\r\n" +
			"	<GeneralCommentId>" + generalCommentId + "</GeneralCommentId>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<EntryDate>" + entryDate + "</EntryDate>\r\n" +
			"	<GeneralComment>" + System.Web.HttpUtility.HtmlEncode(generalComment) + "</GeneralComment>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<DepartmentID>" + departmentID + "</DepartmentID>\r\n" +
			"</GeneralComment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("generalCommentId")) {
					SetXmlValue(ref generalCommentId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryDate")) {
					SetXmlValue(ref entryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("generalComment")) {
					SetXmlValue(ref generalComment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("departmentId")) {
					SetXmlValue(ref departmentID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static GeneralComment[] GetGeneralComments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGeneralComments();
		}

		public static GeneralComment GetGeneralCommentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetGeneralCommentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertGeneralComment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateGeneralComment(this);
		}
		#endregion

		#region Properties
		public int GeneralCommentId {
			set { generalCommentId = value; }
			get { return generalCommentId; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		public int SalesId {
			set { salesId = value; }
			get { return salesId; }
		}

		public DateTime EntryDate {
			set { entryDate = value; }
			get { return entryDate; }
		}

		public string GeneralCommentValue {
			set { generalComment = value; }
			get { return generalComment; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public int DepartmentID {
			set { departmentID = value; }
			get { return departmentID; }
		}

		#endregion
	}
}
