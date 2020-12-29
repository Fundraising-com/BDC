using System;
using System.Xml;

namespace GA.BDC.Core.eFundraisingStore {

	public class ProgramPartner: eFundraisingStoreDataObject {

		private int programId;
		private int partnerId;
		private string programUrl;
		private DateTime createDate;


		public ProgramPartner() : this(int.MinValue) { }
		public ProgramPartner(int programId) : this(programId, int.MinValue) { }
		public ProgramPartner(int programId, int partnerId) : this(programId, partnerId, null) { }
		public ProgramPartner(int programId, int partnerId, string programUrl) : this(programId, partnerId, programUrl, DateTime.MinValue) { }
		public ProgramPartner(int programId, int partnerId, string programUrl, DateTime createDate) {
			this.programId = programId;
			this.partnerId = partnerId;
			this.programUrl = programUrl;
			this.createDate = createDate;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProgramPartner>\r\n" +
			"	<ProgramId>" + programId + "</ProgramId>\r\n" +
			"	<PartnerId>" + partnerId + "</PartnerId>\r\n" +
			"	<ProgramUrl>" + System.Web.HttpUtility.HtmlEncode(programUrl) + "</ProgramUrl>\r\n" +
			"	<CreateDate>" + createDate + "</CreateDate>\r\n" +
			"</ProgramPartner>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "programId") {
					SetXmlValue(ref programId, node.InnerText);
				}
				if(node.Name.ToLower() == "partnerId") {
					SetXmlValue(ref partnerId, node.InnerText);
				}
				if(node.Name.ToLower() == "programUrl") {
					SetXmlValue(ref programUrl, node.InnerText);
				}
				if(node.Name.ToLower() == "createDate") {
					SetXmlValue(ref createDate, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProgramPartner[] GetProgramPartners() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProgramPartners();
		}

		public static ProgramPartner GetProgramPartnerByID(int id) {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.GetProgramPartnerByID(id);
		}

		public int Insert() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.InsertProgramPartner(this);
		}

		public int Update() {
			DataAccess.eFundraisingStoreDatabase dbo = new DataAccess.eFundraisingStoreDatabase();
			return dbo.UpdateProgramPartner(this);
		}
		#endregion

		#region Properties
		public int ProgramId {
			set { programId = value; }
			get { return programId; }
		}

		public int PartnerId {
			set { partnerId = value; }
			get { return partnerId; }
		}

		public string ProgramUrl {
			set { programUrl = value; }
			get { return programUrl; }
		}

		public DateTime CreateDate {
			set { createDate = value; }
			get { return createDate; }
		}

		#endregion
	}
}
