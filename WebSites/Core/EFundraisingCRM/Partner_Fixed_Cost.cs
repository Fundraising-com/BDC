using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class PartnerFixedCost: EFundraisingCRMDataObject {

		private int partnerID;
		private float costByLead;


		public PartnerFixedCost() : this(int.MinValue) { }
		public PartnerFixedCost(int partnerID) : this(partnerID, float.MinValue) { }
		public PartnerFixedCost(int partnerID, float costByLead) {
			this.partnerID = partnerID;
			this.costByLead = costByLead;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<PartnerFixedCost>\r\n" +
			"	<PartnerID>" + partnerID + "</PartnerID>\r\n" +
			"	<CostByLead>" + costByLead + "</CostByLead>\r\n" +
			"</PartnerFixedCost>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("partnerId")) {
					SetXmlValue(ref partnerID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("costByLead")) {
					SetXmlValue(ref costByLead, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static PartnerFixedCost[] GetPartnerFixedCosts() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerFixedCosts();
		}

		public static PartnerFixedCost GetPartnerFixedCostByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetPartnerFixedCostByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertPartnerFixedCost(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdatePartnerFixedCost(this);
		}
		#endregion

		#region Properties
		public int PartnerID {
			set { partnerID = value; }
			get { return partnerID; }
		}

		public float CostByLead {
			set { costByLead = value; }
			get { return costByLead; }
		}

		#endregion
	}
}
