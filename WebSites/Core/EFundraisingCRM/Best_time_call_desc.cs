using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class BestTimeCallDesc: EFundraisingCRMDataObject {

		private short bestTimeCallId;
		private short languageId;
		private string description;


		public BestTimeCallDesc() : this(short.MinValue) { }
		public BestTimeCallDesc(short bestTimeCallId) : this(bestTimeCallId, short.MinValue) { }
		public BestTimeCallDesc(short bestTimeCallId, short languageId) : this(bestTimeCallId, languageId, null) { }
		public BestTimeCallDesc(short bestTimeCallId, short languageId, string description) {
			this.bestTimeCallId = bestTimeCallId;
			this.languageId = languageId;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<BestTimeCallDesc>\r\n" +
			"	<BestTimeCallId>" + bestTimeCallId + "</BestTimeCallId>\r\n" +
			"	<LanguageId>" + languageId + "</LanguageId>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</BestTimeCallDesc>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("bestTimeCallId")) {
					SetXmlValue(ref bestTimeCallId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("languageId")) {
					SetXmlValue(ref languageId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static BestTimeCallDesc[] GetBestTimeCallDescs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBestTimeCallDescs();
		}

		/*
		public static BestTimeCallDesc GetBestTimeCallDescByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetBestTimeCallDescByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertBestTimeCallDesc(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateBestTimeCallDesc(this);
		}*/
		#endregion

		#region Properties
		public short BestTimeCallId {
			set { bestTimeCallId = value; }
			get { return bestTimeCallId; }
		}

		public short LanguageId {
			set { languageId = value; }
			get { return languageId; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
