using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ARActivityType: EFundraisingCRMDataObject {

		private int aRActivityTypeId;
		private string description;


		public ARActivityType() : this(int.MinValue) { }
		public ARActivityType(int aRActivityTypeId) : this(aRActivityTypeId, null) { }
		public ARActivityType(int aRActivityTypeId, string description) {
			this.aRActivityTypeId = aRActivityTypeId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ARActivityType>\r\n" +
			"	<ARActivityTypeId>" + aRActivityTypeId + "</ARActivityTypeId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</ARActivityType>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("arActivityTypeId")) {
					SetXmlValue(ref aRActivityTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ARActivityType[] GetARActivityTypes() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARActivityTypes();
		}

		public static ARActivityType GetARActivityTypeByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetARActivityTypeByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertARActivityType(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateARActivityType(this);
		}
		#endregion

		#region Properties
		public int ARActivityTypeId {
			set { aRActivityTypeId = value; }
			get { return aRActivityTypeId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
