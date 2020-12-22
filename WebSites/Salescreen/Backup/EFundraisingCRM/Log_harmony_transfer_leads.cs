using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class LogHarmonyTransferLeads: EFundraisingCRMDataObject {

		private int iD;
		private string listName;
		private string listDesc;
		private int oldConsultantId;
		private int newConsultantId;
		private int transfererId;
		private DateTime transferDate;
		private int leadId;


		public LogHarmonyTransferLeads() : this(int.MinValue) { }
		public LogHarmonyTransferLeads(int iD) : this(iD, null) { }
		public LogHarmonyTransferLeads(int iD, string listName) : this(iD, listName, null) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc) : this(iD, listName, listDesc, int.MinValue) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc, int oldConsultantId) : this(iD, listName, listDesc, oldConsultantId, int.MinValue) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc, int oldConsultantId, int newConsultantId) : this(iD, listName, listDesc, oldConsultantId, newConsultantId, int.MinValue) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc, int oldConsultantId, int newConsultantId, int transfererId) : this(iD, listName, listDesc, oldConsultantId, newConsultantId, transfererId, DateTime.MinValue) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc, int oldConsultantId, int newConsultantId, int transfererId, DateTime transferDate) : this(iD, listName, listDesc, oldConsultantId, newConsultantId, transfererId, transferDate, int.MinValue) { }
		public LogHarmonyTransferLeads(int iD, string listName, string listDesc, int oldConsultantId, int newConsultantId, int transfererId, DateTime transferDate, int leadId) {
			this.iD = iD;
			this.listName = listName;
			this.listDesc = listDesc;
			this.oldConsultantId = oldConsultantId;
			this.newConsultantId = newConsultantId;
			this.transfererId = transfererId;
			this.transferDate = transferDate;
			this.leadId = leadId;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<LogHarmonyTransferLeads>\r\n" +
			"	<ID>" + iD + "</ID>\r\n" +
			"	<ListName>" + System.Web.HttpUtility.HtmlEncode(listName) + "</ListName>\r\n" +
			"	<ListDesc>" + System.Web.HttpUtility.HtmlEncode(listDesc) + "</ListDesc>\r\n" +
			"	<OldConsultantId>" + oldConsultantId + "</OldConsultantId>\r\n" +
			"	<NewConsultantId>" + newConsultantId + "</NewConsultantId>\r\n" +
			"	<TransfererId>" + transfererId + "</TransfererId>\r\n" +
			"	<TransferDate>" + transferDate + "</TransferDate>\r\n" +
			"	<LeadId>" + leadId + "</LeadId>\r\n" +
			"</LogHarmonyTransferLeads>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("id")) {
					SetXmlValue(ref iD, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listName")) {
					SetXmlValue(ref listName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("listDesc")) {
					SetXmlValue(ref listDesc, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("oldConsultantId")) {
					SetXmlValue(ref oldConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("newConsultantId")) {
					SetXmlValue(ref newConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transfererId")) {
					SetXmlValue(ref transfererId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("transferDate")) {
					SetXmlValue(ref transferDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) {
					SetXmlValue(ref leadId, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static LogHarmonyTransferLeads[] GetLogHarmonyTransferLeadss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLogHarmonyTransferLeadss();
		}

		public static LogHarmonyTransferLeads GetLogHarmonyTransferLeadsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLogHarmonyTransferLeadsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertLogHarmonyTransferLeads(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateLogHarmonyTransferLeads(this);
		}
		#endregion

		#region Properties
		public int ID {
			set { iD = value; }
			get { return iD; }
		}

		public string ListName {
			set { listName = value; }
			get { return listName; }
		}

		public string ListDesc {
			set { listDesc = value; }
			get { return listDesc; }
		}

		public int OldConsultantId {
			set { oldConsultantId = value; }
			get { return oldConsultantId; }
		}

		public int NewConsultantId {
			set { newConsultantId = value; }
			get { return newConsultantId; }
		}

		public int TransfererId {
			set { transfererId = value; }
			get { return transfererId; }
		}

		public DateTime TransferDate {
			set { transferDate = value; }
			get { return transferDate; }
		}

		public int LeadId {
			set { leadId = value; }
			get { return leadId; }
		}

		#endregion
	}
}
