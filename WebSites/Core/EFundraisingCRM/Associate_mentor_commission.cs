using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class AssociateMentorCommission: EFundraisingCRMDataObject {

		private int associateId;
		private int mentorId;
		private short productClassId;
		private float commissionRate;
		private string comments;


		public AssociateMentorCommission() : this(int.MinValue) { }
		public AssociateMentorCommission(int associateId) : this(associateId, int.MinValue) { }
		public AssociateMentorCommission(int associateId, int mentorId) : this(associateId, mentorId, short.MinValue) { }
		public AssociateMentorCommission(int associateId, int mentorId, short productClassId) : this(associateId, mentorId, productClassId, float.MinValue) { }
		public AssociateMentorCommission(int associateId, int mentorId, short productClassId, float commissionRate) : this(associateId, mentorId, productClassId, commissionRate, null) { }
		public AssociateMentorCommission(int associateId, int mentorId, short productClassId, float commissionRate, string comments) {
			this.associateId = associateId;
			this.mentorId = mentorId;
			this.productClassId = productClassId;
			this.commissionRate = commissionRate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<AssociateMentorCommission>\r\n" +
			"	<AssociateId>" + associateId + "</AssociateId>\r\n" +
			"	<MentorId>" + mentorId + "</MentorId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<CommissionRate>" + commissionRate + "</CommissionRate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</AssociateMentorCommission>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("associateId")) {
					SetXmlValue(ref associateId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mentorId")) {
					SetXmlValue(ref mentorId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRate")) {
					SetXmlValue(ref commissionRate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static AssociateMentorCommission[] GetAssociateMentorCommissions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentorCommissions();
		}

		public static AssociateMentorCommission GetAssociateMentorCommissionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAssociateMentorCommissionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAssociateMentorCommission(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAssociateMentorCommission(this);
		}
		#endregion

		#region Properties
		public int AssociateId {
			set { associateId = value; }
			get { return associateId; }
		}

		public int MentorId {
			set { mentorId = value; }
			get { return mentorId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public float CommissionRate {
			set { commissionRate = value; }
			get { return commissionRate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
