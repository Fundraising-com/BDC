using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class Division: EFundraisingCRMDataObject {

		private short divisionId;
		private string divisionName;
		private string logo;
		private string shortName;


		public Division() : this(short.MinValue) { }
		public Division(short divisionId) : this(divisionId, null) { }
		public Division(short divisionId, string divisionName) : this(divisionId, divisionName, null) { }
		public Division(short divisionId, string divisionName, string logo) : this(divisionId, divisionName, logo, null) { }
		public Division(short divisionId, string divisionName, string logo, string shortName) {
			this.divisionId = divisionId;
			this.divisionName = divisionName;
			this.logo = logo;
			this.shortName = shortName;
		}

		#region Static Data

		public static Division ScratchCard {
			get { return new Division(1, "ScratchCard", null, "SC"); }
		}

		public static Division eFundRaising {
			get { return new Division(2, "eFundRaising", null, "SC"); }
		}

		public static Division eFundraisingSuperStore {
			get { return new Division(3, "ScratchCard", null, "SC"); }
		}

		public static Division eFundraisingOnline {
			get { return new Division(4, "ScratchCard", null, "SC"); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Division>\r\n" +
			"	<DivisionId>" + divisionId + "</DivisionId>\r\n" +
			"	<DivisionName>" + System.Web.HttpUtility.HtmlEncode(divisionName) + "</DivisionName>\r\n" +
			"	<Logo>" + System.Web.HttpUtility.HtmlEncode(logo) + "</Logo>\r\n" +
			"	<ShortName>" + System.Web.HttpUtility.HtmlEncode(shortName) + "</ShortName>\r\n" +
			"</Division>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("divisionId")) {
					SetXmlValue(ref divisionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("divisionName")) {
					SetXmlValue(ref divisionName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("logo")) {
					SetXmlValue(ref logo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shortName")) {
					SetXmlValue(ref shortName, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Division[] GetDivisions() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDivisions();
		}

		/*
		public static Division GetDivisionByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetDivisionByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertDivision(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateDivision(this);
		}*/
		#endregion

		#region Properties
		public short DivisionId {
			set { divisionId = value; }
			get { return divisionId; }
		}

		public string DivisionName {
			set { divisionName = value; }
			get { return divisionName; }
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
