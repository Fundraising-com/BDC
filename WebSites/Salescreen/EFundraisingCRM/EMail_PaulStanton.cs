using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EMailPaulStanton: EFundraisingCRMDataObject {

		private int iD;
		private string goodEmail;


		public EMailPaulStanton() : this(int.MinValue) { }
		public EMailPaulStanton(int iD) : this(iD, null) { }
		public EMailPaulStanton(int iD, string goodEmail) {
			this.iD = iD;
			this.goodEmail = goodEmail;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EMailPaulStanton>\r\n" +
			"	<ID>" + iD + "</ID>\r\n" +
			"	<GoodEmail>" + System.Web.HttpUtility.HtmlEncode(goodEmail) + "</GoodEmail>\r\n" +
			"</EMailPaulStanton>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("id")) {
					SetXmlValue(ref iD, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("goodemail")) {
					SetXmlValue(ref goodEmail, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static EMailPaulStanton[] GetEMailPaulStantons() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEMailPaulStantons();
		}

		public static EMailPaulStanton GetEMailPaulStantonByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEMailPaulStantonByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEMailPaulStanton(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEMailPaulStanton(this);
		}
		#endregion

		#region Properties
		public int ID {
			set { iD = value; }
			get { return iD; }
		}

		public string GoodEmail {
			set { goodEmail = value; }
			get { return goodEmail; }
		}

		#endregion
	}
}
