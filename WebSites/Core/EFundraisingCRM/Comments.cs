using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Comments: EFundraisingCRMDataObject {

		private int commentsID;
		private int priorityID = 2;  // Medium
		private int salesID;
		private int consultantID;
		private int leadID;
		private int departmentID;
		private DateTime entryDate;
		private string comments;


		public Comments() : this(int.MinValue) { }
		public Comments(int commentsID) : this(commentsID, int.MinValue) { }
		public Comments(int commentsID, int priorityID) : this(commentsID, priorityID, int.MinValue) { }
		public Comments(int commentsID, int priorityID, int salesID) : this(commentsID, priorityID, salesID, int.MinValue) { }
		public Comments(int commentsID, int priorityID, int salesID, int consultantID) : this(commentsID, priorityID, salesID, consultantID, int.MinValue) { }
		public Comments(int commentsID, int priorityID, int salesID, int consultantID, int leadID) : this(commentsID, priorityID, salesID, consultantID, leadID, int.MinValue) { }
		public Comments(int commentsID, int priorityID, int salesID, int consultantID, int leadID, int departmentID) : this(commentsID, priorityID, salesID, consultantID, leadID, departmentID, DateTime.MinValue) { }
		public Comments(int commentsID, int priorityID, int salesID, int consultantID, int leadID, int departmentID, DateTime entryDate) : this(commentsID, priorityID, salesID, consultantID, leadID, departmentID, entryDate, null) { }
		public Comments(int commentsID, int priorityID, int salesID, int consultantID, int leadID, int departmentID, DateTime entryDate, string comments) {
			this.commentsID = commentsID;
			this.priorityID = priorityID;
			this.salesID = salesID;
			this.consultantID = consultantID;
			this.leadID = leadID;
			this.departmentID = departmentID;
			this.entryDate = entryDate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Comments>\r\n" +
			"	<CommentsID>" + commentsID + "</CommentsID>\r\n" +
			"	<PriorityID>" + priorityID + "</PriorityID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<LeadID>" + leadID + "</LeadID>\r\n" +
			"	<DepartmentID>" + departmentID + "</DepartmentID>\r\n" +
			"	<EntryDate>" + entryDate + "</EntryDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</Comments>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("commentsId")) {
					SetXmlValue(ref commentsID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("priorityId")) {
					SetXmlValue(ref priorityID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("departmentId")) {
					SetXmlValue(ref departmentID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("entryDate")) {
					SetXmlValue(ref entryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Comments[] GetCommentss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommentss();
		}

		public static Comments GetCommentsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommentsByID(id);
		}

		
		public static Comments[] GetCommentsBySaleID(int saleId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommentsBySaleID(saleId);
		}

		//used for single sales screen comment
		public static Comments GetCommentBySaleIDAndLeadID(int saleId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommentBySaleIDAndLeadID(saleId,0);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertComments(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateComments(this);
		}
		#endregion

		#region Properties
		public int CommentsID {
			set { commentsID = value; }
			get { return commentsID; }
		}

		public int PriorityID {
			set { priorityID = value; }
			get { return priorityID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public int LeadID {
			set { leadID = value; }
			get { return leadID; }
		}

		public int DepartmentID {
			set { departmentID = value; }
			get { return departmentID; }
		}

		public DateTime EntryDate {
			set { entryDate = value; }
			get { return entryDate; }
		}

		public string Comment {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
