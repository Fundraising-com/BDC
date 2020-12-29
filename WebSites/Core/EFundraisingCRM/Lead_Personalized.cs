using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class LeadPersonalized: EFundraisingCRMDataObject {

		private string goodEmail;


		public LeadPersonalized() : this(null) { }
		public LeadPersonalized(string goodEmail) {
			this.goodEmail = goodEmail;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LeadPersonalized>\r\n" +
			"	<GoodEmail>" + System.Web.HttpUtility.HtmlEncode(goodEmail) + "</GoodEmail>\r\n" +
			"</LeadPersonalized>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("goodemail")) {
					SetXmlValue(ref goodEmail, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static LeadPersonalized[] GetLeadPersonalizeds() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPersonalizeds();
		}

		public static LeadPersonalized GetLeadPersonalizedByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLeadPersonalizedByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLeadPersonalized(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLeadPersonalized(this);
		}*/
		#endregion

		#region Properties
		public string GoodEmail {
			set { goodEmail = value; }
			get { return goodEmail; }
		}

		#endregion
	}
}
