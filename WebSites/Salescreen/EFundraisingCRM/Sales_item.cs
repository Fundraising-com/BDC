using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public class SalesItem: EFundraisingCRMDataObject
	{

		private int salesId;
		private int salesItemNo;
		private int scratchBookId;
		private short serviceTypeId;
		private short productClassId;
		private string groupName;
		private int quantitySold;
		private decimal unitPriceSold;
		private int quantityFree;
		private string suggestedCoupons;
		private decimal salesAmount;
		private decimal paidAmount;
		private decimal adjustedAmount;
		private decimal discountAmount;
		private decimal salesCommissionAmount;
		private decimal sponsorCommissionAmount;
		private decimal nbUnitsSold;
		private string manualProductDescription;
		private int participantId = int.MinValue;
        private decimal commissionRate = 0;


		public SalesItem() : this(int.MinValue) { }
		public SalesItem(int salesId) : this(salesId, int.MinValue) { }
		public SalesItem(int salesId, int salesItemNo) : this(salesId, salesItemNo, int.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId) : this(salesId, salesItemNo, scratchBookId, short.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, short.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, null) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, int.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, int.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, null) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount, decimal discountAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount, decimal discountAmount, decimal salesCommissionAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount, decimal discountAmount, decimal salesCommissionAmount, decimal sponsorCommissionAmount) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, sponsorCommissionAmount, decimal.MinValue) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount, decimal discountAmount, decimal salesCommissionAmount, decimal sponsorCommissionAmount, decimal nbUnitsSold) : this(salesId, salesItemNo, scratchBookId, serviceTypeId, productClassId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, sponsorCommissionAmount, nbUnitsSold, null) { }
		public SalesItem(int salesId, int salesItemNo, int scratchBookId, short serviceTypeId, short productClassId, string groupName, int quantitySold, decimal unitPriceSold, int quantityFree, string suggestedCoupons, decimal salesAmount, decimal paidAmount, decimal adjustedAmount, decimal discountAmount, decimal salesCommissionAmount, decimal sponsorCommissionAmount, decimal nbUnitsSold, string manualProductDescription) {
			this.salesId = salesId;
			this.salesItemNo = salesItemNo;
			this.scratchBookId = scratchBookId;
			this.serviceTypeId = serviceTypeId;
			this.productClassId = productClassId;
			this.groupName = groupName;
			this.quantitySold = quantitySold;
			this.unitPriceSold = unitPriceSold;
			this.quantityFree = quantityFree;
			this.suggestedCoupons = suggestedCoupons;
			this.salesAmount = salesAmount;
			this.paidAmount = paidAmount;
			this.adjustedAmount = adjustedAmount;
			this.discountAmount = discountAmount;
			this.salesCommissionAmount = salesCommissionAmount;
			this.sponsorCommissionAmount = sponsorCommissionAmount;
			this.nbUnitsSold = nbUnitsSold;
			this.manualProductDescription = manualProductDescription;
		}

		#region Methods
		public string ToHumanReadableString() {
			ScratchBook scratchBook = ScratchBook.GetScratchBookByID(scratchBookId);
			string text = "Product: {0,-40} P.Code: {1, 20} Price: {2:#,###,###.##}$ Quantity: {3}";
			return string.Format(text, scratchBook.Description, scratchBook.ProductCode, unitPriceSold, quantitySold);
		}
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesItem>\r\n" +
			"	<SalesId>" + salesId + "</SalesId>\r\n" +
			"	<SalesItemNo>" + salesItemNo + "</SalesItemNo>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<ServiceTypeId>" + serviceTypeId + "</ServiceTypeId>\r\n" +
			"	<ProductClassId>" + productClassId + "</ProductClassId>\r\n" +
			"	<GroupName>" + System.Web.HttpUtility.HtmlEncode(groupName) + "</GroupName>\r\n" +
			"	<QuantitySold>" + quantitySold + "</QuantitySold>\r\n" +
			"	<UnitPriceSold>" + unitPriceSold + "</UnitPriceSold>\r\n" +
			"	<QuantityFree>" + quantityFree + "</QuantityFree>\r\n" +
			"	<SuggestedCoupons>" + System.Web.HttpUtility.HtmlEncode(suggestedCoupons) + "</SuggestedCoupons>\r\n" +
			"	<SalesAmount>" + salesAmount + "</SalesAmount>\r\n" +
			"	<PaidAmount>" + paidAmount + "</PaidAmount>\r\n" +
			"	<AdjustedAmount>" + adjustedAmount + "</AdjustedAmount>\r\n" +
			"	<DiscountAmount>" + discountAmount + "</DiscountAmount>\r\n" +
			"	<SalesCommissionAmount>" + salesCommissionAmount + "</SalesCommissionAmount>\r\n" +
			"	<SponsorCommissionAmount>" + sponsorCommissionAmount + "</SponsorCommissionAmount>\r\n" +
			"	<NbUnitsSold>" + nbUnitsSold + "</NbUnitsSold>\r\n" +
			"	<ManualProductDescription>" + System.Web.HttpUtility.HtmlEncode(manualProductDescription) + "</ManualProductDescription>\r\n" +
			"</SalesItem>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) {
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemNo")) {
					SetXmlValue(ref salesItemNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("serviceTypeId")) {
					SetXmlValue(ref serviceTypeId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productClassId")) {
					SetXmlValue(ref productClassId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("groupName")) {
					SetXmlValue(ref groupName, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantitySold")) {
					SetXmlValue(ref quantitySold, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("unitPriceSold")) {
					SetXmlValue(ref unitPriceSold, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("quantityFree")) {
					SetXmlValue(ref quantityFree, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("suggestedCoupons")) {
					SetXmlValue(ref suggestedCoupons, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesAmount")) {
					SetXmlValue(ref salesAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paidAmount")) {
					SetXmlValue(ref paidAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("adjustedAmount")) {
					SetXmlValue(ref adjustedAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("discountAmount")) {
					SetXmlValue(ref discountAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesCommissionAmount")) {
					SetXmlValue(ref salesCommissionAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorCommissionAmount")) {
					SetXmlValue(ref sponsorCommissionAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("nbUnitsSold")) {
					SetXmlValue(ref nbUnitsSold, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("manualProductDescription")) {
					SetXmlValue(ref manualProductDescription, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static SalesItem[] GetSalesItems() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItems();
		}

		public static EFundraisingCRMCollectionBase GetCollectionSalesItems() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetCollectionSalesItems();
		}

		public static SalesItem[] GetSalesItemsBySaleID(int salesID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesBySaleId(salesID);
		}

		public static SalesItemCollection GetSalesItemsBySaleID_Collection(int salesID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleItemsBySaleID(salesID);
		}

		public static int GetMaxSalesItemNo(int salesId)
		{
			return 1;
//			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
//			return dbo.GetMaxSalesItemNo(salesId);
		}

		public static SalesItem GetSalesItemByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemByID(id);
		}

		
		public static SalesItem GetSalesItemBySaleIdAndSaleItemNo(int salesId, int SalesItemNo) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemBySaleIdAndSaleItemNo (salesId, SalesItemNo);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesItem(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesItem(this);
		}

		public static int Update2(SalesItem si) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesItem(si);
		}


		/*public int Delete(int scratchBookID, int itemNo) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.DeleteSalesItem(scratchBookID, itemNo);
		}*/


		#endregion

		#region Properties
		public int SalesId 
		{
			set { salesId = value; }
			get { return salesId; }
		}

		public int SalesItemNo {
			set { salesItemNo = value; }
			get { return salesItemNo; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short ServiceTypeId {
			set { serviceTypeId = value; }
			get { return serviceTypeId; }
		}

		public short ProductClassId {
			set { productClassId = value; }
			get { return productClassId; }
		}

		public string GroupName {
			set { groupName = value; }
			get { return groupName; }
		}

		public int QuantitySold {
			set { quantitySold = value; }
			get { return quantitySold; }
		}

		public decimal UnitPriceSold {
			set { unitPriceSold = value; }
			get { return unitPriceSold; }
		}

		public int QuantityFree {
			set { quantityFree = value; }
			get { return quantityFree; }
		}

		public string SuggestedCoupons {
			set { suggestedCoupons = value; }
			get { return suggestedCoupons; }
		}

		public decimal SalesAmount {
			set { salesAmount = value; }
			get { return salesAmount; }
		}

		public decimal PaidAmount {
			set { paidAmount = value; }
			get { return paidAmount; }
		}

		public decimal AdjustedAmount {
			set { adjustedAmount = value; }
			get { return adjustedAmount; }
		}

		public decimal DiscountAmount {
			set { discountAmount = value; }
			get { return discountAmount; }
		}

		public decimal SalesCommissionAmount {
			set { salesCommissionAmount = value; }
			get { return salesCommissionAmount; }
		}

		public decimal SponsorCommissionAmount {
			set { sponsorCommissionAmount = value; }
			get { return sponsorCommissionAmount; }
		}

		public decimal NbUnitsSold {
			set { nbUnitsSold = value; }
			get { return nbUnitsSold; }
		}

        public decimal CommissionRate
        {
            set { commissionRate = value; }
            get { return commissionRate; }
        }

		public string ManualProductDescription {
			set { manualProductDescription = value; }
			get { return manualProductDescription; }
		}

		public int ParticipantId
		{
			get {return participantId;}
			set {participantId = value;}
		}

		#endregion

		#region IComparable Members

		public override int CompareTo(object obj)
		{
			// TODO:  Add SalesItem.CompareTo implementation
			SalesItem theItem = obj as SalesItem;
			if (theItem != null)
			{
				return string.Compare(manualProductDescription, theItem.ManualProductDescription);
//				if (manualProductDescription == null)
//					return manualProductDescription.CompareTo(theItem.ManualProductDescription);
//				else
//					return string.Empty.CompareTo(theItem.ManualProductDescription);
			}
			return 0;
		}

		#endregion
	}
}
