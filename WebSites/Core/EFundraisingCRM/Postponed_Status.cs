using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PostponedStatus: EFundraisingCRMDataObject {

		private int postponedStatusID;
		private string description;


		public PostponedStatus() : this(int.MinValue) { }
		public PostponedStatus(int postponedStatusID) : this(postponedStatusID, null) { }
		public PostponedStatus(int postponedStatusID, string description) {
			this.postponedStatusID = postponedStatusID;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PostponedStatus>\r\n" +
			"	<PostponedStatusID>" + postponedStatusID + "</PostponedStatusID>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</PostponedStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("postponedStatusId")) {
					SetXmlValue(ref postponedStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PostponedStatus[] GetPostponedStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostponedStatuss();
		}

		public static PostponedStatus GetPostponedStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPostponedStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPostponedStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePostponedStatus(this);
		}
		#endregion

		#region Properties
		public int PostponedStatusID {
			set { postponedStatusID = value; }
			get { return postponedStatusID; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
