using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class Adjustment: EFundraisingCRMDataObject {

		private int salesID;
		private int adjustmentNo;
		private int reasonID;
		private DateTime adjustmentDate;
		private double adjustmentAmount;
		private string comment;
		private double adjustmentOnShipping;
		private double adjustmentOnTaxes;
		private double adjustmentOnSaleAmount;
        
        private int chargeID; /* added to adjustment table, removing adjustment dependancy on OE */


		public Adjustment() : this(int.MinValue) { }
		public Adjustment(int salesID) : this(salesID, int.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo) : this(salesID, adjustmentNo, int.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID) : this(salesID, adjustmentNo, reasonID, DateTime.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate) : this(salesID, adjustmentNo, reasonID, adjustmentDate, double.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount) : this(salesID, adjustmentNo, reasonID, adjustmentDate, adjustmentAmount, null) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount, string comment) : this(salesID, adjustmentNo, reasonID, adjustmentDate, adjustmentAmount, comment, double.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount, string comment, double adjustmentOnShipping) : this(salesID, adjustmentNo, reasonID, adjustmentDate, adjustmentAmount, comment, adjustmentOnShipping, double.MinValue) { }
		public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount, string comment, double adjustmentOnShipping, double adjustmentOnTaxes) : this(salesID, adjustmentNo, reasonID, adjustmentDate, adjustmentAmount, comment, adjustmentOnShipping, adjustmentOnTaxes, double.MinValue) { }
        public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount, string comment, double adjustmentOnShipping, double adjustmentOnTaxes, double adjustmentOnSaleAmount) : this(salesID, adjustmentNo, reasonID, adjustmentDate, adjustmentAmount, comment, adjustmentOnShipping, adjustmentOnTaxes, adjustmentOnSaleAmount, int.MinValue) { }
        
        public Adjustment(int salesID, int adjustmentNo, int reasonID, DateTime adjustmentDate, double adjustmentAmount, string comment, double adjustmentOnShipping, double adjustmentOnTaxes, double adjustmentOnSaleAmount, int chargeID ) {
			this.salesID = salesID;
			this.adjustmentNo = adjustmentNo;
			this.reasonID = reasonID;
			this.adjustmentDate = adjustmentDate;
			this.adjustmentAmount = adjustmentAmount;
			this.comment = comment;
			this.adjustmentOnShipping = adjustmentOnShipping;
			this.adjustmentOnTaxes = adjustmentOnTaxes;
			this.adjustmentOnSaleAmount = adjustmentOnSaleAmount;
            this.chargeID = chargeID;
		}


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Adjustment>\r\n" +
			"	<SalesID>" + salesID + "</SalesID>\r\n" +
			"	<AdjustmentNo>" + adjustmentNo + "</AdjustmentNo>\r\n" +
			"	<ReasonID>" + reasonID + "</ReasonID>\r\n" +
			"	<AdjustmentDate>" + adjustmentDate + "</AdjustmentDate>\r\n" +
			"	<AdjustmentAmount>" + adjustmentAmount + "</AdjustmentAmount>\r\n" +
			"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
			"	<AdjustmentOnShipping>" + adjustmentOnShipping + "</AdjustmentOnShipping>\r\n" +
			"	<AdjustmentOnTaxes>" + adjustmentOnTaxes + "</AdjustmentOnTaxes>\r\n" +
			"	<AdjustmentOnSaleAmount>" + adjustmentOnSaleAmount + "</AdjustmentOnSaleAmount>\r\n" +
			"</Adjustment>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentNo")) {
					SetXmlValue(ref adjustmentNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("reasonId")) {
					SetXmlValue(ref reasonID, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentDate")) {
					SetXmlValue(ref adjustmentDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentAmount")) {
					SetXmlValue(ref adjustmentAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) {
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentOnShipping")) {
					SetXmlValue(ref adjustmentOnShipping, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentOnTaxes")) {
					SetXmlValue(ref adjustmentOnTaxes, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustmentOnSaleAmount")) {
					SetXmlValue(ref adjustmentOnSaleAmount, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
             
        public static Adjustment[] GetAdjustments() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdjustments();
		}

        
        public static int GetAdjustmentLatestNo(int saleId, DataAccess.EFundraisingCRMDatabase dbo)
        {
            return dbo.GetAdjustmentLatestNo(saleId);
        }

        public static efundraising.EFundraisingCRM.Linq.Adjustment GetAdjustmentDoubleEntry(int saleID, decimal adjAmount, DataAccess.EFundraisingCRMDatabase dbo)
        {
            return dbo.GetAdjustmentDoubleInsert(saleID, adjAmount);
        }

        public static bool GetAdjustmentDuplicate(int adjustmentId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetAdjustmentDuplicate(adjustmentId);
        }

        public static efundraising.EFundraisingCRM.Linq.Adjustment GetAdjustmentDoubleInsert(int saleID, decimal adjustmentAmount)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetAdjustmentDoubleInsert(saleID,adjustmentAmount);
        }

		public static Adjustment GetAdjustmentByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdjustmentByID(id);
		}

        public static decimal GetTotalAdjustmentBySaleID(int id)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase(true);
            return dbo.GetTotalAdjustmentBySaleId(id);
        }


        public static Adjustment GetAdjustmentBySaleIDForSaleScreen(int saleId, bool isDiscount)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetAdjustmentBySaleIDForSaleScreen(saleId, isDiscount);
        }

		public static Adjustment[] GetAdjustmentsBySaleID(int id) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetAdjustmentsBySaleID(id);
		}

        public static Adjustment[] GetLatestAdjustmentsBySaleID(int id)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetLatestAdjustmentsBySaleID(id);
        }

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertAdjustment(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateAdjustment(this);
		}


        public void UpdateSaleAdjustment(Adjustment adj)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            dbo.UpdateSaleAdjustmentDisSur(this);
        }

        public void  SubmitAdjustmentToDb(Adjustment adj)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            dbo.SubmitAdjustmentToDb(this);
        }


		#endregion

		#region Properties
		public int SalesID {
			set { salesID = value; }
			get { return salesID; }
		}

		public int AdjustmentNo {
			set { adjustmentNo = value; }
			get { return adjustmentNo; }
		}

		public int ReasonID {
			set { reasonID = value; }
			get { return reasonID; }
		}

		public DateTime AdjustmentDate {
			set { adjustmentDate = value; }
			get { return adjustmentDate; }
		}

		public double AdjustmentAmount {
			set { adjustmentAmount = value; }
			get { return adjustmentAmount; }
		}

		public string Comment {
			set { comment = value; }
			get { return comment; }
		}

		public double AdjustmentOnShipping {
			set { adjustmentOnShipping = value; }
			get { return adjustmentOnShipping; }
		}

		public double AdjustmentOnTaxes {
			set { adjustmentOnTaxes = value; }
			get { return adjustmentOnTaxes; }
		}

		public double AdjustmentOnSaleAmount {
			set { adjustmentOnSaleAmount = value; }
			get { return adjustmentOnSaleAmount; }
		}

        public int ChargeID
        {
            set { chargeID = value; }
            get { return chargeID; }
        }

		#endregion
	}
}
