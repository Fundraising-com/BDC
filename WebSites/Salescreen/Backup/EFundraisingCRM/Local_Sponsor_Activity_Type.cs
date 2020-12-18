using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LocalSponsorActivityType: EFundraisingCRMDataObject {

		private int localSponsorActivityTypeId;
		private string description;


		public LocalSponsorActivityType() : this(int.MinValue) { }
		public LocalSponsorActivityType(int localSponsorActivityTypeId) : this(localSponsorActivityTypeId, null) { }
		public LocalSponsorActivityType(int localSponsorActivityTypeId, string description) {
			this.localSponsorActivityTypeId = localSponsorActivityTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LocalSponsorActivityType>\r\n" +
			"	<LocalSponsorActivityTypeId>" + localSponsorActivityTypeId + "</LocalSponsorActivityTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</LocalSponsorActivityType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorActivityTypeId")) {
					SetXmlValue(ref localSponsorActivityTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LocalSponsorActivityType[] GetLocalSponsorActivityTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorActivityTypes();
		}

		public static LocalSponsorActivityType GetLocalSponsorActivityTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLocalSponsorActivityTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLocalSponsorActivityType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLocalSponsorActivityType(this);
		}
		#endregion

		#region Properties
		public int LocalSponsorActivityTypeId {
			set { localSponsorActivityTypeId = value; }
			get { return localSponsorActivityTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
