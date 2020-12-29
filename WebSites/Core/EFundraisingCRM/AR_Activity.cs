using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ARActivity: EFundraisingCRMDataObject {

		private int aRActivityID;
		private int aRActivityTypeID;
		private int salesID;
		private int aRConsultantID;
		private DateTime aRActivityDate;
		private DateTime completedDate;
		private string comments;


		public ARActivity() : this(int.MinValue) { }
		public ARActivity(int aRActivityID) : this(aRActivityID, int.MinValue) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID) : this(aRActivityID, aRActivityTypeID, int.MinValue) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID, int salesID) : this(aRActivityID, aRActivityTypeID, salesID, int.MinValue) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID, int salesID, int aRConsultantID) : this(aRActivityID, aRActivityTypeID, salesID, aRConsultantID, DateTime.MinValue) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID, int salesID, int aRConsultantID, DateTime aRActivityDate) : this(aRActivityID, aRActivityTypeID, salesID, aRConsultantID, aRActivityDate, DateTime.MinValue) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID, int salesID, int aRConsultantID, DateTime aRActivityDate, DateTime completedDate) : this(aRActivityID, aRActivityTypeID, salesID, aRConsultantID, aRActivityDate, completedDate, null) { }
		public ARActivity(int aRActivityID, int aRActivityTypeID, int salesID, int aRConsultantID, DateTime aRActivityDate, DateTime completedDate, string comments) {
			this.aRActivityID = aRActivityID;
			this.aRActivityTypeID = aRActivityTypeID;
			this.salesID = salesID;
			this.aRConsultantID = aRConsultantID;
			this.aRActivityDate = aRActivityDate;
			this.completedDate = completedDate;
			this.comments = comments;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ARActivity>\r\n" +
			"	<ARActivityID>" + aRActivityID + "</ARActivityID>\r\n" +
			"	<ARActivityTypeID>" + aRActivityTypeID + "</ARActivityTypeID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<ARConsultantID>" + aRConsultantID + "</ARConsultantID>\r\n" +
			"	<ARActivityDate>" + aRActivityDate + "</ARActivityDate>\r\n" +
			"	<CompletedDate>" + completedDate + "</CompletedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"</ARActivity>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("arActivityId")) {
					SetXmlValue(ref aRActivityID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arActivityTypeId")) {
					SetXmlValue(ref aRActivityTypeID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arConsultantId")) {
					SetXmlValue(ref aRConsultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arActivityDate")) {
					SetXmlValue(ref aRActivityDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("completedDate")) {
					SetXmlValue(ref completedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ARActivity[] GetARActivitys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARActivitys();
		}

		public static ARActivity GetARActivityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARActivityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertARActivity(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateARActivity(this);
		}
		#endregion

		#region Properties
		public int ARActivityID {
			set { aRActivityID = value; }
			get { return aRActivityID; }
		}

		public int ARActivityTypeID {
			set { aRActivityTypeID = value; }
			get { return aRActivityTypeID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int ARConsultantID {
			set { aRConsultantID = value; }
			get { return aRConsultantID; }
		}

		public DateTime ARActivityDate {
			set { aRActivityDate = value; }
			get { return aRActivityDate; }
		}

		public DateTime CompletedDate {
			set { completedDate = value; }
			get { return completedDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		#endregion
	}
}
