using System;
using System.Xml;

namespace GA.BDC.Core.EFundraisingCRM {

	public class SalesItemToAdd: EFundraisingCRMDataObject {

		private int salesItemToAddNo;
		private int saleToAddId;
		private int scratchBookId;
		private short serviceTypeId;
		private string groupName;
		private int quantitySold;
		private float unitPriceSold;
		private int quantityFree;
		private string suggestedCoupons;
		private float salesAmount;
		private float paidAmount;
		private float adjustedAmount;
		private float discountAmount;
		private float salesCommissionAmount;
		private float sponsorCommissionAmount;
		private float nbUnitsSold;
		private string manualProductDescription;


		public SalesItemToAdd() : this(int.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo) : this(salesItemToAddNo, int.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId) : this(salesItemToAddNo, saleToAddId, int.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId) : this(salesItemToAddNo, saleToAddId, scratchBookId, short.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, null) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, int.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, int.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, null) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount, float discountAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount, float discountAmount, float salesCommissionAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount, float discountAmount, float salesCommissionAmount, float sponsorCommissionAmount) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, sponsorCommissionAmount, float.MinValue) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount, float discountAmount, float salesCommissionAmount, float sponsorCommissionAmount, float nbUnitsSold) : this(salesItemToAddNo, saleToAddId, scratchBookId, serviceTypeId, groupName, quantitySold, unitPriceSold, quantityFree, suggestedCoupons, salesAmount, paidAmount, adjustedAmount, discountAmount, salesCommissionAmount, sponsorCommissionAmount, nbUnitsSold, null) { }
		public SalesItemToAdd(int salesItemToAddNo, int saleToAddId, int scratchBookId, short serviceTypeId, string groupName, int quantitySold, float unitPriceSold, int quantityFree, string suggestedCoupons, float salesAmount, float paidAmount, float adjustedAmount, float discountAmount, float salesCommissionAmount, float sponsorCommissionAmount, float nbUnitsSold, string manualProductDescription) {
			this.salesItemToAddNo = salesItemToAddNo;
			this.saleToAddId = saleToAddId;
			this.scratchBookId = scratchBookId;
			this.serviceTypeId = serviceTypeId;
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


		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<SalesItemToAdd>\r\n" +
			"	<SalesItemToAddNo>" + salesItemToAddNo + "</SalesItemToAddNo>\r\n" +
			"	<SaleToAddId>" + saleToAddId + "</SaleToAddId>\r\n" +
			"	<ScratchBookId>" + scratchBookId + "</ScratchBookId>\r\n" +
			"	<ServiceTypeId>" + serviceTypeId + "</ServiceTypeId>\r\n" +
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
			"</SalesItemToAdd>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(ToLowerCase(node.Name) == ToLowerCase("salesItemToAddNo")) {
					SetXmlValue(ref salesItemToAddNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("saleToAddId")) {
					SetXmlValue(ref saleToAddId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scratchBookId")) {
					SetXmlValue(ref scratchBookId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("serviceTypeId")) {
					SetXmlValue(ref serviceTypeId, node.InnerText);
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
		public static SalesItemToAdd[] GetSalesItemToAdds() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemToAdds();
		}

		public static SalesItemToAdd GetSalesItemToAddByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesItemToAddByID(id);
		}

		public int Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSalesItemToAdd(this);
		}

		public int Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.UpdateSalesItemToAdd(this);
		}
		#endregion

		#region Properties
		public int SalesItemToAddNo {
			set { salesItemToAddNo = value; }
			get { return salesItemToAddNo; }
		}

		public int SaleToAddId {
			set { saleToAddId = value; }
			get { return saleToAddId; }
		}

		public int ScratchBookId {
			set { scratchBookId = value; }
			get { return scratchBookId; }
		}

		public short ServiceTypeId {
			set { serviceTypeId = value; }
			get { return serviceTypeId; }
		}

		public string GroupName {
			set { groupName = value; }
			get { return groupName; }
		}

		public int QuantitySold {
			set { quantitySold = value; }
			get { return quantitySold; }
		}

		public float UnitPriceSold {
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

		public float SalesAmount {
			set { salesAmount = value; }
			get { return salesAmount; }
		}

		public float PaidAmount {
			set { paidAmount = value; }
			get { return paidAmount; }
		}

		public float AdjustedAmount {
			set { adjustedAmount = value; }
			get { return adjustedAmount; }
		}

		public float DiscountAmount {
			set { discountAmount = value; }
			get { return discountAmount; }
		}

		public float SalesCommissionAmount {
			set { salesCommissionAmount = value; }
			get { return salesCommissionAmount; }
		}

		public float SponsorCommissionAmount {
			set { sponsorCommissionAmount = value; }
			get { return sponsorCommissionAmount; }
		}

		public float NbUnitsSold {
			set { nbUnitsSold = value; }
			get { return nbUnitsSold; }
		}

		public string ManualProductDescription {
			set { manualProductDescription = value; }
			get { return manualProductDescription; }
		}

		#endregion
	}
}
