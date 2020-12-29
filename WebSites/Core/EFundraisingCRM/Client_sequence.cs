using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class ClientSequence: EFundraisingCRMDataObject {

		private string clientSequenceCode;
		private string description;
		private int isActive;


		public ClientSequence() : this(null) { }
		public ClientSequence(string clientSequenceCode) : this(clientSequenceCode, null) { }
		public ClientSequence(string clientSequenceCode, string description) : this(clientSequenceCode, description, int.MinValue) { }
		public ClientSequence(string clientSequenceCode, string description, int isActive) {
			this.clientSequenceCode = clientSequenceCode;
			this.description = description;
			this.isActive = isActive;
		}

		#region Static Data
		public static ClientSequence AgentCanada {
			get { return new ClientSequence("CA", "Agent Canada", 1); }
		}

		public static ClientSequence DistributorCanada {
			get { return new ClientSequence("CD", "Distributor Canada", 1); }
		}

		public static ClientSequence ClientofAgentCanada {
			get { return new ClientSequence("CG", "Client of Agent Canada", 1); }
		}

		public static ClientSequence CanadaDirectClient {
			get { return new ClientSequence("CI", "Canada Direct Client", 1); }
		}

		public static ClientSequence CanadaSamples {
			get { return new ClientSequence("CS", "Canada Samples", 0); }
		}

		public static ClientSequence CanadianUnofficialAgent {
			get { return new ClientSequence("CU", "Canadian Unofficial Agent", 0); }
		}

		public static ClientSequence AgentUSA {
			get { return new ClientSequence("UA", "Agent USA", 1); }
		}

		public static ClientSequence DistributorUSA {
			get { return new ClientSequence("UD", "Distributor USA", 1); }
		}

		public static ClientSequence ClientofAgentUSA {
			get { return new ClientSequence("UG", "Client of Agent USA", 1); }
		}

		public static ClientSequence USADirectClient {
			get { return new ClientSequence("UI", "USA Direct Client", 1); }
		}

		public static ClientSequence UnofficalAgentsUSA {
			get { return new ClientSequence("UN", "Unoffical Agents USA", 0); }
		}

		public static ClientSequence USASamples {
			get { return new ClientSequence("US", "USA Samples", 0); }
		}
		
		public static ClientSequence OnlineFundraising 
		{
			get { return new ClientSequence("OF", "Online Fundraising", 1); }
		}

		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ClientSequence>\r\n" +
			"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"	<IsActive>" + isActive + "</IsActive>\r\n" +
			"</ClientSequence>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("clientSequenceCode")) {
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isActive")) {
					SetXmlValue(ref isActive, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ClientSequence[] GetClientSequences() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientSequences();
		}

		
		/*
		public static ClientSequence GetClientSequenceByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetClientSequenceByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertClientSequence(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateClientSequence(this);
		}*/
		#endregion

		#region Properties
		public string ClientSequenceCode {
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		public int IsActive {
			set { isActive = value; }
			get { return isActive; }
		}

		#endregion
	}
}
