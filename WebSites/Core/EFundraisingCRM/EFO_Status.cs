using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class EFOStatus: EFundraisingCRMDataObject {

		private int statusID;
		private string status;


		public EFOStatus() : this(int.MinValue) { }
		public EFOStatus(int statusID) : this(statusID, null) { }
		public EFOStatus(int statusID, string status) {
			this.statusID = statusID;
			this.status = status;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EFOStatus>\r\n" +
			"	<StatusID>" + statusID + "</StatusID>\r\n" +
			"	<Status>" + System.Web.HttpUtility.HtmlEncode(status) + "</Status>\r\n" +
			"</EFOStatus>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("statusId")) {
					SetXmlValue(ref statusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("status")) {
					SetXmlValue(ref status, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EFOStatus[] GetEFOStatuss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOStatuss();
		}

		public static EFOStatus GetEFOStatusByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEFOStatusByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEFOStatus(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEFOStatus(this);
		}
		#endregion

		#region Properties
		public int StatusID {
			set { statusID = value; }
			get { return statusID; }
		}

		public string Status {
			set { status = value; }
			get { return status; }
		}

		#endregion
	}
}
