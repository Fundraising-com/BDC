using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Proposal: EFundraisingCRMDataObject {

		private int proposalID;
		private string faxName;
		private string emailName;
		private string description;


		public Proposal() : this(int.MinValue) { }
		public Proposal(int proposalID) : this(proposalID, null) { }
		public Proposal(int proposalID, string faxName) : this(proposalID, faxName, null) { }
		public Proposal(int proposalID, string faxName, string emailName) : this(proposalID, faxName, emailName, null) { }
		public Proposal(int proposalID, string faxName, string emailName, string description) {
			this.proposalID = proposalID;
			this.faxName = faxName;
			this.emailName = emailName;
			this.description = description;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Proposal>\r\n" +
			"	<ProposalID>" + proposalID + "</ProposalID>\r\n" +
			"	<FaxName>" + System.Web.HttpUtility.HtmlEncode(faxName) + "</FaxName>\r\n" +
			"	<EmailName>" + System.Web.HttpUtility.HtmlEncode(emailName) + "</EmailName>\r\n" +
			"	<Description>" + System.Web.HttpUtility.HtmlEncode(description) + "</Description>\r\n" +
			"</Proposal>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("proposalId")) {
					SetXmlValue(ref proposalID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("faxName")) {
					SetXmlValue(ref faxName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("emailName")) {
					SetXmlValue(ref emailName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("description")) {
					SetXmlValue(ref description, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Proposal[] GetProposals() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProposals();
		}

		public static Proposal GetProposalByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProposalByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProposal(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProposal(this);
		}
		#endregion

		#region Properties
		public int ProposalID {
			set { proposalID = value; }
			get { return proposalID; }
		}

		public string FaxName {
			set { faxName = value; }
			get { return faxName; }
		}

		public string EmailName {
			set { emailName = value; }
			get { return emailName; }
		}

		public string Description {
			set { description = value; }
			get { return description; }
		}

		#endregion
	}
}
