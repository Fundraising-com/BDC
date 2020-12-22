using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class CommissionPaid: EFundraisingCRMDataObject {

		private int commissionYear;
		private int commissionMonth;
		private int consultantID;
		private int salesID;
		private int aRStatusID;
		private int totalCardSold;
		private float salesAmount;
		private float consultantCommissionAmount;


		public CommissionPaid() : this(int.MinValue) { }
		public CommissionPaid(int commissionYear) : this(commissionYear, int.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth) : this(commissionYear, commissionMonth, int.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID) : this(commissionYear, commissionMonth, consultantID, int.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID, int salesID) : this(commissionYear, commissionMonth, consultantID, salesID, int.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID, int salesID, int aRStatusID) : this(commissionYear, commissionMonth, consultantID, salesID, aRStatusID, int.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID, int salesID, int aRStatusID, int totalCardSold) : this(commissionYear, commissionMonth, consultantID, salesID, aRStatusID, totalCardSold, float.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID, int salesID, int aRStatusID, int totalCardSold, float salesAmount) : this(commissionYear, commissionMonth, consultantID, salesID, aRStatusID, totalCardSold, salesAmount, float.MinValue) { }
		public CommissionPaid(int commissionYear, int commissionMonth, int consultantID, int salesID, int aRStatusID, int totalCardSold, float salesAmount, float consultantCommissionAmount) {
			this.commissionYear = commissionYear;
			this.commissionMonth = commissionMonth;
			this.consultantID = consultantID;
			this.salesID = salesID;
			this.aRStatusID = aRStatusID;
			this.totalCardSold = totalCardSold;
			this.salesAmount = salesAmount;
			this.consultantCommissionAmount = consultantCommissionAmount;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<CommissionPaid>\r\n" +
			"	<CommissionYear>" + commissionYear + "</CommissionYear>\r\n" +
			"	<CommissionMonth>" + commissionMonth + "</CommissionMonth>\r\n" +
			"	<ConsultantID>" + consultantID + "</ConsultantID>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<ARStatusID>" + aRStatusID + "</ARStatusID>\r\n" +
			"	<TotalCardSold>" + totalCardSold + "</TotalCardSold>\r\n" +
			"	<SalesAmount>" + salesAmount + "</SalesAmount>\r\n" +
			"	<ConsultantCommissionAmount>" + consultantCommissionAmount + "</ConsultantCommissionAmount>\r\n" +
			"</CommissionPaid>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("commissionYear")) {
					SetXmlValue(ref commissionYear, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("commissionMonth")) {
					SetXmlValue(ref commissionMonth, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) {
					SetXmlValue(ref consultantID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arStatusId")) {
					SetXmlValue(ref aRStatusID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("totalCardSold")) {
					SetXmlValue(ref totalCardSold, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesAmount")) {
					SetXmlValue(ref salesAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantCommissionAmount")) {
					SetXmlValue(ref consultantCommissionAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static CommissionPaid[] GetCommissionPaids() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionPaids();
		}

		public static CommissionPaid GetCommissionPaidByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCommissionPaidByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertCommissionPaid(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateCommissionPaid(this);
		}
		#endregion

		#region Properties
		public int CommissionYear {
			set { commissionYear = value; }
			get { return commissionYear; }
		}

		public int CommissionMonth {
			set { commissionMonth = value; }
			get { return commissionMonth; }
		}

		public int ConsultantID {
			set { consultantID = value; }
			get { return consultantID; }
		}

		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int ARStatusID {
			set { aRStatusID = value; }
			get { return aRStatusID; }
		}

		public int TotalCardSold {
			set { totalCardSold = value; }
			get { return totalCardSold; }
		}

		public float SalesAmount {
			set { salesAmount = value; }
			get { return salesAmount; }
		}

		public float ConsultantCommissionAmount {
			set { consultantCommissionAmount = value; }
			get { return consultantCommissionAmount; }
		}

		#endregion
	}
}
