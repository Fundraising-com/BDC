using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class TempUnsub: EFundraisingCRMDataObject {

		private string eMAIL;


		public TempUnsub() : this(null) { }
		public TempUnsub(string eMAIL) {
			this.eMAIL = eMAIL;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<TempUnsub>\r\n" +
			"	<EMAIL>" + System.Web.HttpUtility.HtmlEncode(eMAIL) + "</EMAIL>\r\n" +
			"</TempUnsub>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("email")) {
					SetXmlValue(ref eMAIL, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		/*
		public static TempUnsub[] GetTempUnsubs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempUnsubs();
		}

		public static TempUnsub GetTempUnsubByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetTempUnsubByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertTempUnsub(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateTempUnsub(this);
		}*/
		#endregion

		#region Properties
		public string EMAIL {
			set { eMAIL = value; }
			get { return eMAIL; }
		}

		#endregion
	}
}
