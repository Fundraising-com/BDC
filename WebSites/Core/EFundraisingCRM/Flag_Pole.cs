using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class FlagPole: EFundraisingCRMDataObject {

		private int flagPoleID;
		private string mDRID;


		public FlagPole() : this(int.MinValue) { }
		public FlagPole(int flagPoleID) : this(flagPoleID, null) { }
		public FlagPole(int flagPoleID, string mDRID) {
			this.flagPoleID = flagPoleID;
			this.mDRID = mDRID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<FlagPole>\r\n" +
			"	<FlagPoleID>" + flagPoleID + "</FlagPoleID>\r\n" +
			"	<MDRID>" + System.Web.HttpUtility.HtmlEncode(mDRID) + "</MDRID>\r\n" +
			"</FlagPole>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("flagPoleId")) {
					SetXmlValue(ref flagPoleID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("mdrId")) {
					SetXmlValue(ref mDRID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static FlagPole[] GetFlagPoles() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFlagPoles();
		}

		public static FlagPole GetFlagPoleByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFlagPoleByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertFlagPole(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateFlagPole(this);
		}
		#endregion

		#region Properties
		public int FlagPoleID {
			set { flagPoleID = value; }
			get { return flagPoleID; }
		}

		public string MDRID {
			set { mDRID = value; }
			get { return mDRID; }
		}

		#endregion
	}
}
