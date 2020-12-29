using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class Division: eFundraisingStoreDataObject {

		private int divisionId;
		private string name;
		private string logo;
		private string shortName;


		public Division() : this(int.MinValue) { }
		public Division(int divisionId) : this(divisionId, null) { }
		public Division(int divisionId, string name) : this(divisionId, name, null) { }
		public Division(int divisionId, string name, string logo) : this(divisionId, name, logo, null) { }
		public Division(int divisionId, string name, string logo, string shortName) {
			this.divisionId = divisionId;
			this.name = name;
			this.logo = logo;
			this.shortName = shortName;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Division>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<Name>" + System.Web.HttpUtility.HtmlEncode(name) + "</Name>\r\n" +
			"	<Logo>" + System.Web.HttpUtility.HtmlEncode(logo) + "</Logo>\r\n" +
			"	<ShortName>" + System.Web.HttpUtility.HtmlEncode(shortName) + "</ShortName>\r\n" +
			"</Division>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "divisionId") {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(node.Name.ToLower() == "name") {
					SetXmlValue(ref name, node.InnerText);
				}
				if(node.Name.ToLower() == "logo") {
					SetXmlValue(ref logo, node.InnerText);
				}
				if(node.Name.ToLower() == "shortName") {
					SetXmlValue(ref shortName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Division[] GetDivisions() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetDivisions();
		}

		public static Division GetDivisionByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetDivisionByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertDivision(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateDivision(this);
		}
		#endregion

		#region Properties
		public int DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public string Name {
			set { name = value; }
			get { return name; }
		}

		public string Logo {
			set { logo = value; }
			get { return logo; }
		}

		public string ShortName {
			set { shortName = value; }
			get { return shortName; }
		}

		#endregion
	}
}
