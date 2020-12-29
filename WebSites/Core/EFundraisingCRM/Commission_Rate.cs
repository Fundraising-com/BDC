using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class CommissionRate: EFundraisingCRMDataObject {

		private int consultantID;
		private float commissionRateFree;
		private float commissionRateNoFree;
		private int scratchBookID;


		public CommissionRate() : this(int.MinValue) { }
		public CommissionRate(int consultantID) : this(consultantID, float.MinValue) { }
		public CommissionRate(int consultantID, float commissionRateFree) : this(consultantID, commissionRateFree, float.MinValue) { }
		public CommissionRate(int consultantID, float commissionRateFree, float commissionRateNoFree) : this(consultantID, commissionRateFree, commissionRateNoFree, int.MinValue) { }
		public CommissionRate(int consultantID, float commissionRateFree, float commissionRateNoFree, int scratchBookID) {
			this.consultantID = consultantID;
			this.commissionRateFree = commissionRateFree;
			this.commissionRateNoFree = commissionRateNoFree;
			this.scratchBookID = scratchBookID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CommissionRate>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<CommissionRateFree>" + commissionRateFree + "</CommissionRateFree>\r\n" +
			"	<CommissionRateNoFree>" + commissionRateNoFree + "</CommissionRateNoFree>\r\n" +
			"	<ScratchBookID>" + scratchBookID + "</ScratchBookID>\r\n" +
			"</CommissionRate>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRateFree")) {
					SetXmlValue(ref commissionRateFree, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionRateNoFree")) {
					SetXmlValue(ref commissionRateNoFree, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookID, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CommissionRate[] GetCommissionRates() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionRates();
		}

		public static CommissionRate GetCommissionRateByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionRateByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionRate(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionRate(this);
		}
		#endregion

		#region Properties
		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public float CommissionRateFree {
			set { commissionRateFree = value; }
			get { return commissionRateFree; }
		}

		public float CommissionRateNoFree {
			set { commissionRateNoFree = value; }
			get { return commissionRateNoFree; }
		}

		public int ScratchBookID {
			set { scratchBookID = value; }
			get { return scratchBookID; }
		}

		#endregion
	}
}
