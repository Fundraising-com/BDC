using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TerritoryDef: EFundraisingCRMDataObject {

		private string zip;
		private int territoryID;


		public TerritoryDef() : this(null) { }
		public TerritoryDef(string zip) : this(zip, int.MinValue) { }
		public TerritoryDef(string zip, int territoryID) {
			this.zip = zip;
			this.territoryID = territoryID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TerritoryDef>\r\n" +
			"	<Zip>" + System.Web.HttpUtility.HtmlEncode(zip) + "</Zip>\r\n" +
			"	<TerritoryID>" + territoryID + "</TerritoryID>\r\n" +
			"</TerritoryDef>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("zip")) {
					SetXmlValue(ref zip, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("territoryId")) {
					SetXmlValue(ref territoryID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static TerritoryDef[] GetTerritoryDefs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTerritoryDefs();
		}

		/*
		public static TerritoryDef GetTerritoryDefByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTerritoryDefByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTerritoryDef(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTerritoryDef(this);
		}*/
		#endregion

		#region Properties
		public string Zip {
			set { zip = value; }
			get { return zip; }
		}

		public int TerritoryID {
			set { territoryID = value; }
			get { return territoryID; }
		}

		#endregion
	}
}
