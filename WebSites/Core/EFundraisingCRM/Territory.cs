using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Territory: EFundraisingCRMDataObject {

		private int territoryId;
		private string territoryName;


		public Territory() : this(int.MinValue) { }
		public Territory(int territoryId) : this(territoryId, null) { }
		public Territory(int territoryId, string territoryName) {
			this.territoryId = territoryId;
			this.territoryName = territoryName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Territory>\r\n" +
			"	<TerritoryId>" + territoryId + "</TerritoryId>\r\n" +
			"	<TerritoryName>" + System.Web.HttpUtility.HtmlEncode(territoryName) + "</TerritoryName>\r\n" +
			"</Territory>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("territoryId")) {
					SetXmlValue(ref territoryId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("territoryName")) {
					SetXmlValue(ref territoryName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Territory[] GetTerritorys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTerritorys();
		}

		public static Territory GetTerritoryByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTerritoryByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTerritory(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTerritory(this);
		}
		#endregion

		#region Properties
		public int TerritoryId {
			set { territoryId = value; }
			get { return territoryId; }
		}

		public string TerritoryName {
			set { territoryName = value; }
			get { return territoryName; }
		}

		#endregion
	}
}
