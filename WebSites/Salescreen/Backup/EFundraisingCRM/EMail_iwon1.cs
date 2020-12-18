using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class EMailIwon1: EFundraisingCRMDataObject {

		private int iD;
		private string goodEmail;


		public EMailIwon1() : this(int.MinValue) { }
		public EMailIwon1(int iD) : this(iD, null) { }
		public EMailIwon1(int iD, string goodEmail) {
			this.iD = iD;
			this.goodEmail = goodEmail;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<EMailIwon1>\r\n" +
			"	<ID>" + iD + "</ID>\r\n" +
			"	<GoodEmail>" + System.Web.HttpUtility.HtmlEncode(goodEmail) + "</GoodEmail>\r\n" +
			"</EMailIwon1>\r\n";
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
		public static EMailIwon1[] GetEMailIwon1s() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEMailIwon1s();
		}

		public static EMailIwon1 GetEMailIwon1ByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetEMailIwon1ByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertEMailIwon1(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateEMailIwon1(this);
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
