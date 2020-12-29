using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AssociateMentorComment: EFundraisingCRMDataObject {

		private int assMentorCommentID;
		private int associateID;
		private int mentorID;
		private DateTime commentDate;
		private string comments;


		public AssociateMentorComment() : this(int.MinValue) { }
		public AssociateMentorComment(int assMentorCommentID) : this(assMentorCommentID, int.MinValue) { }
		public AssociateMentorComment(int assMentorCommentID, int associateID) : this(assMentorCommentID, associateID, int.MinValue) { }
		public AssociateMentorComment(int assMentorCommentID, int associateID, int mentorID) : this(assMentorCommentID, associateID, mentorID, DateTime.MinValue) { }
		public AssociateMentorComment(int assMentorCommentID, int associateID, int mentorID, DateTime commentDate) : this(assMentorCommentID, associateID, mentorID, commentDate, null) { }
		public AssociateMentorComment(int assMentorCommentID, int associateID, int mentorID, DateTime commentDate, string comments) {
			this.assMentorCommentID = assMentorCommentID;
			this.associateID = associateID;
			this.mentorID = mentorID;
			this.commentDate = commentDate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AssociateMentorComment>\r\n" +
			"	<AssMentorCommentID>" + assMentorCommentID + "</AssMentorCommentID>\r\n" +
			"	<AssociateID>" + associateID + "</AssociateID>\r\n" +
			"	<MentorID>" + mentorID + "</MentorID>\r\n" +
			"	<CommentDate>" + commentDate + "</CommentDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</AssociateMentorComment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("assMentorCommentId")) {
					SetXmlValue(ref assMentorCommentID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("associateId")) {
					SetXmlValue(ref associateID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mentorId")) {
					SetXmlValue(ref mentorID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commentDate")) {
					SetXmlValue(ref commentDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AssociateMentorComment[] GetAssociateMentorComments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentorComments();
		}

		public static AssociateMentorComment GetAssociateMentorCommentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentorCommentByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAssociateMentorComment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAssociateMentorComment(this);
		}
		#endregion

		#region Properties
		public int AssMentorCommentID {
			set { assMentorCommentID = value; }
			get { return assMentorCommentID; }
		}

		public int AssociateID {
			set { associateID = value; }
			get { return associateID; }
		}

		public int MentorID {
			set { mentorID = value; }
			get { return mentorID; }
		}

		public DateTime CommentDate {
			set { commentDate = value; }
			get { return commentDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
