using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class ProposalTags: EFundraisingCRMDataObject {

		private int proposalID;
		private int tagsID;


		public ProposalTags() : this(int.MinValue) { }
		public ProposalTags(int proposalID) : this(proposalID, int.MinValue) { }
		public ProposalTags(int proposalID, int tagsID) {
			this.proposalID = proposalID;
			this.tagsID = tagsID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<ProposalTags>\r\n" +
			"	<ProposalID>" + proposalID + "</ProposalID>\r\n" +
			"	<TagsID>" + tagsID + "</TagsID>\r\n" +
			"</ProposalTags>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("proposalId")) {
					SetXmlValue(ref proposalID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("tagsId")) {
					SetXmlValue(ref tagsID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static ProposalTags[] GetProposalTagss() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProposalTagss();
		}

		public static ProposalTags GetProposalTagsByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetProposalTagsByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertProposalTags(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateProposalTags(this);
		}
		#endregion

		#region Properties
		public int ProposalID {
			set { proposalID = value; }
			get { return proposalID; }
		}

		public int TagsID {
			set { tagsID = value; }
			get { return tagsID; }
		}

		#endregion
	}
}
