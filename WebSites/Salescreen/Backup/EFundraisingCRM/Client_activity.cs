using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ClientActivity: EFundraisingCRMDataObject {

		private int clientActivityId;
		private int clientId;
		private string clientSequenceCode;
		private short clientActivityTypeId;
		private DateTime clientActivityDate;
		private DateTime completedDate;
		private string comments;
		private bool isContacted = false;


		public ClientActivity() : this(int.MinValue) { }
		public ClientActivity(int clientActivityId) : this(clientActivityId, int.MinValue) { }
		public ClientActivity(int clientActivityId, int clientId) : this(clientActivityId, clientId, null) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode) : this(clientActivityId, clientId, clientSequenceCode, short.MinValue) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode, short clientActivityTypeId) : this(clientActivityId, clientId, clientSequenceCode, clientActivityTypeId, DateTime.MinValue) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode, short clientActivityTypeId, DateTime clientActivityDate) : this(clientActivityId, clientId, clientSequenceCode, clientActivityTypeId, clientActivityDate, DateTime.MinValue) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode, short clientActivityTypeId, DateTime clientActivityDate, DateTime completedDate) : this(clientActivityId, clientId, clientSequenceCode, clientActivityTypeId, clientActivityDate, completedDate, null) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode, short clientActivityTypeId, DateTime clientActivityDate, DateTime completedDate, string comments) : this(clientActivityId, clientId, clientSequenceCode, clientActivityTypeId, clientActivityDate, completedDate, comments, false) { }
		public ClientActivity(int clientActivityId, int clientId, string clientSequenceCode, short clientActivityTypeId, DateTime clientActivityDate, DateTime completedDate, string comments, bool isContacted) {
			this.clientActivityId = clientActivityId;
			this.clientId = clientId;
			this.clientSequenceCode = clientSequenceCode;
			this.clientActivityTypeId = clientActivityTypeId;
			this.clientActivityDate = clientActivityDate;
			this.completedDate = completedDate;
			this.comments = comments;
			this.isContacted = isContacted;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientActivity>\r\n" +
			"	<ClientActivityId>" + clientActivityId + "</ClientActivityId>\r\n" +
			"	<ClientId>" + clientId + "</ClientId>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<ClientActivityTypeId>" + clientActivityTypeId + "</ClientActivityTypeId>\r\n" +
			"	<ClientActivityDate>" + clientActivityDate + "</ClientActivityDate>\r\n" +
			"	<CompletedDate>" + completedDate + "</CompletedDate>\r\n" +
			"	<Comments>" + System.Web.HttpUtility.HtmlEncode(comments) + "</Comments>\r\n" +
			"	<IsContacted>" + isContacted + "</IsContacted>\r\n" +
			"</ClientActivity>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("clientActivityId")) {
					SetXmlValue(ref clientActivityId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientId")) {
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientSequenceCode")) {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientActivityTypeId")) {
					SetXmlValue(ref clientActivityTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientActivityDate")) {
					SetXmlValue(ref clientActivityDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("completedDate")) {
					SetXmlValue(ref completedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comments")) {
					SetXmlValue(ref comments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isContacted")) {
					SetXmlValue(ref isContacted, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientActivity[] GetClientActivitys() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientActivitys();
		}

		public static ClientActivity GetClientActivityByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientActivityByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClientActivity(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateClientActivity(this);
		}
		#endregion

		#region Properties
		public int ClientActivityId {
			set { clientActivityId = value; }
			get { return clientActivityId; }
		}

		public int ClientId {
			set { clientId = value; }
			get { return clientId; }
		}

		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public short ClientActivityTypeId {
			set { clientActivityTypeId = value; }
			get { return clientActivityTypeId; }
		}

		public DateTime ClientActivityDate {
			set { clientActivityDate = value; }
			get { return clientActivityDate; }
		}

		public DateTime CompletedDate {
			set { completedDate = value; }
			get { return completedDate; }
		}

		public string Comments {
			set { comments = value; }
			get { return comments; }
		}

		public bool IsContacted {
			set { isContacted = value; }
			get { return isContacted; }
		}

		#endregion
	}
}
