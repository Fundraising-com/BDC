using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SponsorFoundStool: EFundraisingCRMDataObject {

		private int stoolID;
		private int salesID;
		private string userName;
		private int valeur;
		private string modifDate;


		public SponsorFoundStool() : this(int.MinValue) { }
		public SponsorFoundStool(int stoolID) : this(stoolID, int.MinValue) { }
		public SponsorFoundStool(int stoolID, int salesID) : this(stoolID, salesID, null) { }
		public SponsorFoundStool(int stoolID, int salesID, string userName) : this(stoolID, salesID, userName, int.MinValue) { }
		public SponsorFoundStool(int stoolID, int salesID, string userName, int valeur) : this(stoolID, salesID, userName, valeur, null) { }
		public SponsorFoundStool(int stoolID, int salesID, string userName, int valeur, string modifDate) {
			this.stoolID = stoolID;
			this.salesID = salesID;
			this.userName = userName;
			this.valeur = valeur;
			this.modifDate = modifDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SponsorFoundStool>\r\n" +
			"	<StoolID>" + stoolID + "</StoolID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<UserName>" + System.Web.HttpUtility.HtmlEncode(userName) + "</UserName>\r\n" +
			"	<Valeur>" + valeur + "</Valeur>\r\n" +
			"	<ModifDate>" + System.Web.HttpUtility.HtmlEncode(modifDate) + "</ModifDate>\r\n" +
			"</SponsorFoundStool>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("stoolId")) {
					SetXmlValue(ref stoolID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("userName")) {
					SetXmlValue(ref userName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("valeur")) {
					SetXmlValue(ref valeur, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("modifDate")) {
					SetXmlValue(ref modifDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SponsorFoundStool[] GetSponsorFoundStools() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSponsorFoundStools();
		}

		public static SponsorFoundStool GetSponsorFoundStoolByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSponsorFoundStoolByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSponsorFoundStool(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSponsorFoundStool(this);
		}
		#endregion

		#region Properties
		public int StoolID {
			set { stoolID = value; }
			get { return stoolID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public string UserName {
			set { userName = value; }
			get { return userName; }
		}

		public int Valeur {
			set { valeur = value; }
			get { return valeur; }
		}

		public string ModifDate {
			set { modifDate = value; }
			get { return modifDate; }
		}

		#endregion
	}
}
