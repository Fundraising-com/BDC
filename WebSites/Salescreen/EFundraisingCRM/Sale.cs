using System;
using System.Xml;
using System.Data;
using System.Data.Sql;
using System.Collections;


namespace efundraising.EFundraisingCRM 
{

	// list of comparable options
	public enum SaleComparable 
	{
		SalesId,
		ClientId,
		ConfirmedDate,
		SalesDate
	}

	public class Sale: EFundraisingCRMDataObject
	{
		// declare our sort option and set client id as default value
		private SaleComparable sortBy = SaleComparable.SalesId;

		
        
        // list of data mapped variables
		private int salesId;
		private int consultantId;
		private short carrierId;
		private short shippingOptionId;
		private short paymentTermId;
		private string clientSequenceCode;
		private int clientId;
		private int salesStatusId;
		private short paymentMethodId;
		private short poStatusId;
		private int productionStatusId;
		private int sponsorConsultantId;
		private int arConsultantId;
		private int arStatusId;
		private int leadId;
		private int billingCompanyId;
		private short upfrontPaymentMethodId;
		private int confirmerId;
		private int collectionStatusId;
		private int confirmationMethodId;
		private int creditApprovalMethodId;
		private string poNumber;
		private string creditCardNo;
		private string expiryDate;
		private DateTime salesDate;
		private decimal shippingFees;
		private decimal shippingFeesDiscount;
		private DateTime paymentDueDate;
		private DateTime confirmedDate;
		private DateTime scheduledDeliveryDate;
		private DateTime scheduledShipDate;
		private DateTime actualShipDate;
		private string waybillNo;
		private string comment;
		private int couponSheetAssigned;
		private double totalAmount;
		private DateTime invoiceDate;
		private DateTime cancellationDate;
		private int isOrdered;
		private DateTime poReceivedOn;
		private int isDelivered;
		private int localSponsorFound;
		private DateTime boxReturnDate;
		private DateTime reshipDate;
		private decimal upfrontPaymentRequired;
		private DateTime upfrontPaymentDueDate;
		private int sponsorRequired;
		private DateTime actualDeliveryDate;
		private string accountingComments;
		private string ssnNumber;
		private string ssnAddress;
		private string ssnCity;
		private string ssnStateCode;
		private string ssnCountryCode;
		private string ssnZipCode;
		private int isValidated;
		private DateTime promisedDueDate;
		private int generalFlag;
		private short fuelsurcharge;
        private int poconcomm;
		private string packageDescription;
		private bool convert2TallySalePackByParticipants = false;
		private bool isEnterByStudent = false;
		private bool isPackedByStudent = false;
		private int carrierTrackingId;
		private PostalAddress postalAddress = null;
		private int extOrderID;
		private int qspOrderID;
        private int sapOrderStatusID;
        private int promotionCodeID;
        


		//public static readonly int TallySaleProductClassId = 23;
		//private static readonly int[] TallySaleProductClassIds = {21, 23, 24};

		private System.Collections.ArrayList applicableTaxes = new System.Collections.ArrayList();
		private System.Collections.ArrayList salesItems = new System.Collections.ArrayList();
		private System.Collections.ArrayList participants = new System.Collections.ArrayList();

		//		public static bool IsTallySaleProductClass(int classId) 
		//		{
		//			for (int i= 0; i< TallySaleClassIds.Length; i++) {
		//				if (TallySaleClassIds[i] == classId)
		//					return true;
		//			}
		//			return false;
		//		}

		public Sale() : this(int.MinValue) { }
		public Sale(int salesId) : this(salesId, int.MinValue) { }
		public Sale(int salesId, int consultantId) : this(salesId, consultantId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId) : this(salesId, consultantId, carrierId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId) : this(salesId, consultantId, carrierId, shippingOptionId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, decimal.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, short.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, ssnStateCode, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, 
			ssnStateCode, ssnCountryCode, null) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, 
			ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, ssnZipCode, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode, int isValidated) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, actualDeliveryDate, 
			accountingComments, ssnNumber, ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, ssnZipCode, isValidated, DateTime.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode, int isValidated, DateTime promisedDueDate) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, sponsorRequired, 
			actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, ssnZipCode, isValidated, promisedDueDate, int.MinValue) { }
		public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode, int isValidated, DateTime promisedDueDate, int generalFlag) : this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate, 

		            sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, ssnZipCode, isValidated, promisedDueDate, generalFlag, short.MinValue) { }

        public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode, int isValidated, DateTime promisedDueDate, int generalFlag, short fuelsurcharge): this(salesId, consultantId, carrierId, shippingOptionId, paymentTermId, clientSequenceCode, clientId, salesStatusId, paymentMethodId, poStatusId, productionStatusId, sponsorConsultantId, arConsultantId, arStatusId, leadId, billingCompanyId, upfrontPaymentMethodId, confirmerId, collectionStatusId, confirmationMethodId, creditApprovalMethodId, poNumber, creditCardNo, expiryDate, salesDate, shippingFees, shippingFeesDiscount, paymentDueDate, confirmedDate, scheduledDeliveryDate, scheduledShipDate, actualShipDate, waybillNo, comment, couponSheetAssigned, totalAmount, invoiceDate, cancellationDate, isOrdered, poReceivedOn, isDelivered, localSponsorFound, boxReturnDate, reshipDate, upfrontPaymentRequired, upfrontPaymentDueDate,

                sponsorRequired, actualDeliveryDate, accountingComments, ssnNumber, ssnAddress, ssnCity, ssnStateCode, ssnCountryCode, ssnZipCode, isValidated, promisedDueDate, generalFlag, fuelsurcharge, int.MinValue) { }



        public Sale(int salesId, int consultantId, short carrierId, short shippingOptionId, short paymentTermId, string clientSequenceCode, int clientId, int salesStatusId, short paymentMethodId, short poStatusId, int productionStatusId, int sponsorConsultantId, int arConsultantId, int arStatusId, int leadId, int billingCompanyId, short upfrontPaymentMethodId, int confirmerId, int collectionStatusId, int confirmationMethodId, int creditApprovalMethodId, string poNumber, string creditCardNo, string expiryDate, DateTime salesDate, decimal shippingFees, decimal shippingFeesDiscount, DateTime paymentDueDate, DateTime confirmedDate, DateTime scheduledDeliveryDate, DateTime scheduledShipDate, DateTime actualShipDate, string waybillNo, string comment, int couponSheetAssigned, double totalAmount, DateTime invoiceDate, DateTime cancellationDate, int isOrdered, DateTime poReceivedOn, int isDelivered, int localSponsorFound, DateTime boxReturnDate, DateTime reshipDate, decimal upfrontPaymentRequired, DateTime upfrontPaymentDueDate, int sponsorRequired, DateTime actualDeliveryDate, string accountingComments, string ssnNumber, string ssnAddress, string ssnCity, string ssnStateCode, string ssnCountryCode, string ssnZipCode, int isValidated, DateTime promisedDueDate, int generalFlag, short fuelsurcharge, int poconcomm) 
		{

			this.salesId = salesId;
			this.consultantId = consultantId;
			this.carrierId = carrierId;
			this.shippingOptionId = shippingOptionId;
			this.paymentTermId = paymentTermId;
			this.clientSequenceCode = clientSequenceCode;
			this.clientId = clientId;
			this.salesStatusId = salesStatusId;
			this.paymentMethodId = paymentMethodId;
			this.poStatusId = poStatusId;
			this.productionStatusId = productionStatusId;
			this.sponsorConsultantId = sponsorConsultantId;
			this.arConsultantId = arConsultantId;
			this.arStatusId = arStatusId;
			this.leadId = leadId;
			this.billingCompanyId = billingCompanyId;
			this.upfrontPaymentMethodId = upfrontPaymentMethodId;
			this.confirmerId = confirmerId;
			this.collectionStatusId = collectionStatusId;
			this.confirmationMethodId = confirmationMethodId;
			this.creditApprovalMethodId = creditApprovalMethodId;
			this.poNumber = poNumber;
			this.creditCardNo = creditCardNo;
			this.expiryDate = expiryDate;
			this.salesDate = salesDate;
			if (shippingFees == decimal.MinValue)
				this.shippingFees = 0;
			else
				this.shippingFees = shippingFees;
			if (shippingFeesDiscount == decimal.MinValue)
				this.shippingFeesDiscount = 0;
			else
				this.shippingFeesDiscount = shippingFeesDiscount;
			this.paymentDueDate = paymentDueDate;
			this.confirmedDate = confirmedDate;
			this.scheduledDeliveryDate = scheduledDeliveryDate;
			this.scheduledShipDate = scheduledShipDate;
			this.actualShipDate = actualShipDate;
			this.waybillNo = waybillNo;
			this.comment = comment;
			if (couponSheetAssigned == int.MinValue)
				this.couponSheetAssigned = 0;
			else
				this.couponSheetAssigned = couponSheetAssigned;
			if (totalAmount == float.MinValue)
				this.totalAmount = 0;
			else
				this.totalAmount = totalAmount;
			this.invoiceDate = invoiceDate;
			this.cancellationDate = cancellationDate;
			if (isOrdered == int.MinValue)
				this.isOrdered = 0;
			else
				this.isOrdered = isOrdered;
			this.poReceivedOn = poReceivedOn;
			if (isDelivered == int.MinValue)
				this.isDelivered = 0;
			else
				this.isDelivered = isDelivered;
			if (localSponsorFound == int.MinValue)
				this.localSponsorFound = 0;
			else
				this.localSponsorFound = localSponsorFound;
			this.boxReturnDate = boxReturnDate;
			this.reshipDate = reshipDate;
			this.upfrontPaymentRequired = upfrontPaymentRequired;
			this.upfrontPaymentDueDate = upfrontPaymentDueDate;
			if (sponsorRequired == int.MinValue)
				this.sponsorRequired = 1;
			else
				this.sponsorRequired = sponsorRequired;
			this.actualDeliveryDate = actualDeliveryDate;
			this.accountingComments = accountingComments;
			this.ssnNumber = ssnNumber;
			this.ssnAddress = ssnAddress;
			this.ssnCity = ssnCity;
			this.ssnStateCode = ssnStateCode;
			this.ssnCountryCode = ssnCountryCode;
			this.ssnZipCode = ssnZipCode;
			if (isValidated == int.MinValue)
				this.isValidated = 0;
			else
				this.isValidated = isValidated;
			this.promisedDueDate = promisedDueDate;
			if (generalFlag == int.MinValue)
				this.generalFlag = 0;
			else
				this.generalFlag = generalFlag;
			this.fuelsurcharge = fuelsurcharge;
            this.poconcomm = poconcomm;
		}

		#region Methods
		public string ToHumanReadableString() 
		{
			string paymentMethod = PaymentMethod.GetPaymentMethodByID(paymentMethodId).Description;
			string billingCompanyName = BillingCompany.GetBillingCompanyByID(billingCompanyId).BillingCompanyName;
			
			//			creditCardNo;
			//			expiryDate;
			//			salesDate;
			//			comment;
			//			totalAmount;
			
			//string text = "{0,-40} {1:#,###,###.##}$ {2, -20} CC#: {3, 15} Expires: {4} Date: {5)";
			//return string.Format(text, billingCompanyName, ((decimal)totalAmount), paymentMethod, creditCardNo, expiryDate, salesDate.ToShortDateString() + " " + salesDate.ToShortTimeString());
			string maskedCCNumber = "";
			if (creditCardNo.Length == 16)
				maskedCCNumber = "************" + creditCardNo.Substring(12, 4);
			return billingCompanyName + " Total: " + ((decimal)totalAmount).ToString() + " " + paymentMethod + ": " + maskedCCNumber + " Expire: " + expiryDate + " Sale Processed on: " + salesDate.ToShortDateString() + " " + salesDate.ToShortTimeString();
		}

		public void CalculateTotalArAmount() 
		{
			totalAmount = 0;
			foreach(SalesItem si in SalesItems) 
			{
				totalAmount += (float)si.SalesAmount;
			}
		}
		
		public double CalculateTotalPaymentsAndAdjustmentsAmount()
		{
			double totalAmount = 0;
			PaymentCollection payments = Payment.GetPaymentsBySaleId(this.salesId);
			if (payments.Count > 0)
				foreach(Payment p in payments)
					totalAmount += p.PaymentAmount;
			Adjustment[] adjustments = Adjustment.GetAdjustmentsBySaleID(this.salesId);
			if (adjustments.Length > 0)
				for(int i = 0; i < adjustments.Length; i++)
					totalAmount += adjustments[i].AdjustmentAmount;
			return totalAmount;		
			
		}
		
		public ProductClass GetProductClass()
		{
			ProductClass pc = null;
			ScratchBook sc = null;
			SalesItem[] saleItems = SalesItem.GetSalesItemsBySaleID(this.salesId);
			if (saleItems.Length > 0)
				sc = ScratchBook.GetScratchBookByID(saleItems[0].ScratchBookId);
			if (sc != null)
				pc = ProductClass.GetProductClassById(sc.ProductClassId);
			return pc;		
		}
		
		public int Update() 
		 {
			 DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			 return dbo.UpdateSale(this);
		 }

		public ProductClass GetPO()
		{
			ProductClass pc = null;
			ScratchBook sc = null;
			SalesItem[] saleItems = SalesItem.GetSalesItemsBySaleID(this.salesId);
			if (saleItems.Length > 0)
				sc = ScratchBook.GetScratchBookByID(saleItems[0].ScratchBookId);
			if (sc != null)
				pc = ProductClass.GetProductClassById(sc.ProductClassId);
			return pc;		
		}
		
		#endregion

		#region XML Methods

		#region Save XML
		public override string GenerateXML() 
		{
			return "<Sale>\r\n" +
				"	<SalesId>" + salesId + "</SalesId>\r\n" +
				"	<ConsultantId>" + consultantId + "</ConsultantId>\r\n" +
				"	<CarrierId>" + carrierId + "</CarrierId>\r\n" +
				"	<ShippingOptionId>" + shippingOptionId + "</ShippingOptionId>\r\n" +
				"	<PaymentTermId>" + paymentTermId + "</PaymentTermId>\r\n" +
				"	<ClientSequenceCode>" + System.Web.HttpUtility.HtmlEncode(clientSequenceCode) + "</ClientSequenceCode>\r\n" +
				"	<ClientId>" + clientId + "</ClientId>\r\n" +
				"	<SalesStatusId>" + salesStatusId + "</SalesStatusId>\r\n" +
				"	<PaymentMethodId>" + paymentMethodId + "</PaymentMethodId>\r\n" +
				"	<PoStatusId>" + poStatusId + "</PoStatusId>\r\n" +
				"	<ProductionStatusId>" + productionStatusId + "</ProductionStatusId>\r\n" +
				"	<SponsorConsultantId>" + sponsorConsultantId + "</SponsorConsultantId>\r\n" +
				"	<ArConsultantId>" + arConsultantId + "</ArConsultantId>\r\n" +
				"	<ArStatusId>" + arStatusId + "</ArStatusId>\r\n" +
				"	<LeadId>" + leadId + "</LeadId>\r\n" +
				"	<BillingCompanyId>" + billingCompanyId + "</BillingCompanyId>\r\n" +
				"	<UpfrontPaymentMethodId>" + upfrontPaymentMethodId + "</UpfrontPaymentMethodId>\r\n" +
				"	<ConfirmerId>" + confirmerId + "</ConfirmerId>\r\n" +
				"	<CollectionStatusId>" + collectionStatusId + "</CollectionStatusId>\r\n" +
				"	<ConfirmationMethodId>" + confirmationMethodId + "</ConfirmationMethodId>\r\n" +
				"	<CreditApprovalMethodId>" + creditApprovalMethodId + "</CreditApprovalMethodId>\r\n" +
				"	<PoNumber>" + System.Web.HttpUtility.HtmlEncode(poNumber) + "</PoNumber>\r\n" +
				"	<CreditCardNo>" + System.Web.HttpUtility.HtmlEncode(creditCardNo) + "</CreditCardNo>\r\n" +
				"	<ExpiryDate>" + System.Web.HttpUtility.HtmlEncode(expiryDate) + "</ExpiryDate>\r\n" +
				"	<SalesDate>" + salesDate + "</SalesDate>\r\n" +
				"	<ShippingFees>" + shippingFees + "</ShippingFees>\r\n" +
				"	<ShippingFeesDiscount>" + shippingFeesDiscount + "</ShippingFeesDiscount>\r\n" +
				"	<PaymentDueDate>" + paymentDueDate + "</PaymentDueDate>\r\n" +
				"	<ConfirmedDate>" + confirmedDate + "</ConfirmedDate>\r\n" +
				"	<ScheduledDeliveryDate>" + scheduledDeliveryDate + "</ScheduledDeliveryDate>\r\n" +
				"	<ScheduledShipDate>" + scheduledShipDate + "</ScheduledShipDate>\r\n" +
				"	<ActualShipDate>" + actualShipDate + "</ActualShipDate>\r\n" +
				"	<WaybillNo>" + System.Web.HttpUtility.HtmlEncode(waybillNo) + "</WaybillNo>\r\n" +
				"	<Comment>" + System.Web.HttpUtility.HtmlEncode(comment) + "</Comment>\r\n" +
				"	<CouponSheetAssigned>" + couponSheetAssigned + "</CouponSheetAssigned>\r\n" +
				"	<TotalAmount>" + totalAmount + "</TotalAmount>\r\n" +
				"	<InvoiceDate>" + invoiceDate + "</InvoiceDate>\r\n" +
				"	<CancellationDate>" + cancellationDate + "</CancellationDate>\r\n" +
				"	<IsOrdered>" + isOrdered + "</IsOrdered>\r\n" +
				"	<PoReceivedOn>" + poReceivedOn + "</PoReceivedOn>\r\n" +
				"	<IsDelivered>" + isDelivered + "</IsDelivered>\r\n" +
				"	<LocalSponsorFound>" + localSponsorFound + "</LocalSponsorFound>\r\n" +
				"	<BoxReturnDate>" + boxReturnDate + "</BoxReturnDate>\r\n" +
				"	<ReshipDate>" + reshipDate + "</ReshipDate>\r\n" +
				"	<UpfrontPaymentRequired>" + upfrontPaymentRequired + "</UpfrontPaymentRequired>\r\n" +
				"	<UpfrontPaymentDueDate>" + upfrontPaymentDueDate + "</UpfrontPaymentDueDate>\r\n" +
				"	<SponsorRequired>" + sponsorRequired + "</SponsorRequired>\r\n" +
				"	<ActualDeliveryDate>" + actualDeliveryDate + "</ActualDeliveryDate>\r\n" +
				"	<AccountingComments>" + System.Web.HttpUtility.HtmlEncode(accountingComments) + "</AccountingComments>\r\n" +
				"	<SsnNumber>" + System.Web.HttpUtility.HtmlEncode(ssnNumber) + "</SsnNumber>\r\n" +
				"	<SsnAddress>" + System.Web.HttpUtility.HtmlEncode(ssnAddress) + "</SsnAddress>\r\n" +
				"	<SsnCity>" + System.Web.HttpUtility.HtmlEncode(ssnCity) + "</SsnCity>\r\n" +
				"	<SsnStateCode>" + System.Web.HttpUtility.HtmlEncode(ssnStateCode) + "</SsnStateCode>\r\n" +
				"	<SsnCountryCode>" + System.Web.HttpUtility.HtmlEncode(ssnCountryCode) + "</SsnCountryCode>\r\n" +
				"	<SsnZipCode>" + System.Web.HttpUtility.HtmlEncode(ssnZipCode) + "</SsnZipCode>\r\n" +
				"	<IsValidated>" + isValidated + "</IsValidated>\r\n" +
				"	<PromisedDueDate>" + promisedDueDate + "</PromisedDueDate>\r\n" +
				"	<GeneralFlag>" + generalFlag + "</GeneralFlag>\r\n" +
				"	<Fuelsurcharge>" + fuelsurcharge + "</Fuelsurcharge>\r\n" +
				"</Sale>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) 
		{
			foreach(XmlNode node in childNodes) 
			{
				if(ToLowerCase(node.Name) == ToLowerCase("salesId")) 
				{
					SetXmlValue(ref salesId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("consultantId")) 
				{
					SetXmlValue(ref consultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("carrierId")) 
				{
					SetXmlValue(ref carrierId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingOptionId")) 
				{
					SetXmlValue(ref shippingOptionId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentTermId")) 
				{
					SetXmlValue(ref paymentTermId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientSequenceCode")) 
				{
					SetXmlValue(ref clientSequenceCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("clientId")) 
				{
					SetXmlValue(ref clientId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesStatusId")) 
				{
					SetXmlValue(ref salesStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentMethodId")) 
				{
					SetXmlValue(ref paymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("poStatusId")) 
				{
					SetXmlValue(ref poStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("productionStatusId")) 
				{
					SetXmlValue(ref productionStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorConsultantId")) 
				{
					SetXmlValue(ref sponsorConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arConsultantId")) 
				{
					SetXmlValue(ref arConsultantId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("arStatusId")) 
				{
					SetXmlValue(ref arStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("leadId")) 
				{
					SetXmlValue(ref leadId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("billingCompanyId")) 
				{
					SetXmlValue(ref billingCompanyId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentMethodId")) 
				{
					SetXmlValue(ref upfrontPaymentMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("confirmerId")) 
				{
					SetXmlValue(ref confirmerId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("collectionStatusId")) 
				{
					SetXmlValue(ref collectionStatusId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("confirmationMethodId")) 
				{
					SetXmlValue(ref confirmationMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditApprovalMethodId")) 
				{
					SetXmlValue(ref creditApprovalMethodId, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("poNumber")) 
				{
					SetXmlValue(ref poNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("creditCardNo")) 
				{
					SetXmlValue(ref creditCardNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("expiryDate")) 
				{
					SetXmlValue(ref expiryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("salesDate")) 
				{
					SetXmlValue(ref salesDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingFees")) 
				{
					SetXmlValue(ref shippingFees, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("shippingFeesDiscount")) 
				{
					SetXmlValue(ref shippingFeesDiscount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("paymentDueDate")) 
				{
					SetXmlValue(ref paymentDueDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("confirmedDate")) 
				{
					SetXmlValue(ref confirmedDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scheduledDeliveryDate")) 
				{
					SetXmlValue(ref scheduledDeliveryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("scheduledShipDate")) 
				{
					SetXmlValue(ref scheduledShipDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("actualShipDate")) 
				{
					SetXmlValue(ref actualShipDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("waybillNo")) 
				{
					SetXmlValue(ref waybillNo, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("comment")) 
				{
					SetXmlValue(ref comment, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("couponSheetAssigned")) 
				{
					SetXmlValue(ref couponSheetAssigned, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("totalAmount")) 
				{
					SetXmlValue(ref totalAmount, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("invoiceDate")) 
				{
					SetXmlValue(ref invoiceDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("cancellationDate")) 
				{
					SetXmlValue(ref cancellationDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isOrdered")) 
				{
					SetXmlValue(ref isOrdered, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("poReceivedOn")) 
				{
					SetXmlValue(ref poReceivedOn, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isDelivered")) 
				{
					SetXmlValue(ref isDelivered, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("localSponsorFound")) 
				{
					SetXmlValue(ref localSponsorFound, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("boxReturnDate")) 
				{
					SetXmlValue(ref boxReturnDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("reshipDate")) 
				{
					SetXmlValue(ref reshipDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentRequired")) 
				{
					SetXmlValue(ref upfrontPaymentRequired, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("upfrontPaymentDueDate")) 
				{
					SetXmlValue(ref upfrontPaymentDueDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("sponsorRequired")) 
				{
					SetXmlValue(ref sponsorRequired, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("actualDeliveryDate")) 
				{
					SetXmlValue(ref actualDeliveryDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("accountingComments")) 
				{
					SetXmlValue(ref accountingComments, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnNumber")) 
				{
					SetXmlValue(ref ssnNumber, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnAddress")) 
				{
					SetXmlValue(ref ssnAddress, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnCity")) 
				{
					SetXmlValue(ref ssnCity, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnStateCode")) 
				{
					SetXmlValue(ref ssnStateCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnCountryCode")) 
				{
					SetXmlValue(ref ssnCountryCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("ssnZipCode")) 
				{
					SetXmlValue(ref ssnZipCode, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("isValidated")) 
				{
					SetXmlValue(ref isValidated, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("promisedDueDate")) 
				{
					SetXmlValue(ref promisedDueDate, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("generalFlag")) 
				{
					SetXmlValue(ref generalFlag, node.InnerText);
				}
				if(ToLowerCase(node.Name) == ToLowerCase("fuelsurcharge")) 
				{
					SetXmlValue(ref fuelsurcharge, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Sale[] GetSales() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSales();
		}

		public static Sale GetSaleByID(int id) 
		{
            return GetSaleByID(id, "");
		}

        public static Sale GetSaleByID(int id, string passphrase)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetSaleByID(id, passphrase);
        }


		public static Sale GetLatestSaleByLeadID(int leadID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetLatestSaleByLeadID(leadID);
		}

		public static SaleCollection GetSamplesByCarrierId(int carrierID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSamplesByCarrierId(carrierID);
		}

		public static SaleCollection GetSalesByProductClassAndStatusID(int productClassID, int statusID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesByProductClassAndStatusID(productClassID, statusID);
			           
		}

		public static SaleCollection GetSalesNewWFC() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesNewWFC();
			           
		}

		public static SaleCollection GetSalesByCarrierIdAndProductClassId(int carrierID, int productClassId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesByCarrierIdAndProductClassId(carrierID, productClassId);
		}

		public static Sale[] GetUnpaidSaleByLeadID(int leadID) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetUnpaidSaleByLeadID(leadID);
		}

		public static Sale[] GetSalesByClient(Client cl)  
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesByClientId(cl.ClientId, cl.ClientSequenceCode);
		}

		
		public static SaleCollection GetSaleCollectionsByClient(Client cl)  
		{
			Sale[] sales = GetSalesByClient(cl);
			SaleCollection saleCol = new SaleCollection();
				
			if (sales != null)
			{
				for (int i=0; i < sales.Length; i++)
   	          		saleCol.Add(sales[i]);
			}
			return saleCol;
		}

		public static Sale[] GetSalesReadyForFedex() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSalesReadyForFedex();
		}

		//		public static Sale[] GetTallySales(int client_id, string clientSequenceCode)
		//		{
		//			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
		//			return dbo.GetTallySalesByClientIdAndSequenceCode(client_id, clientSequenceCode);
		//		}
		
		public static int RecalculeTotalSaleAmount(int salesId)
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.RecalculeTotalSaleAmount(salesId);
		}

		//		public static Sale[] GetTallySalesPackByParticipant(int client_id, string clientSequenceCode)
		//		{
		//			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
		//			return dbo.GetTallySalesByClientIdAndSequenceCodePack(client_id, clientSequenceCode);
		//		}


		public static int GetPackageId(int saleID)
		{
			int packageId = int.MinValue;

			SalesItem[] salesItems = SalesItem.GetSalesItemsBySaleID(saleID);
			if (salesItems != null && salesItems.Length >0)
			{
				for (int i=0; i< salesItems.Length; i++)
				{
					int scratchBookId = salesItems[i].ScratchBookId;
					ScratchBook scrBook = ScratchBook.GetScratchBookByID(scratchBookId);
					if (scrBook != null && Sale.IsTallySaleProductClass(scrBook.ProductClassId))
					{
						packageId = scrBook.PackageId;
						break;
					}
				}
			}
			return packageId;
		}

		private static string[] GetTallySaleProductClassIds()
		{
			string classIds = (efundraising.Configuration.ApplicationSettings.GetConfig()["EFundraisingProd.TallySale.ProductClassIds", "ProductClassIds"]); 
			return classIds.Split(',');
		}

		public static string GetTallySaleProductClassIdsInString()
		{
			string classIds = (efundraising.Configuration.ApplicationSettings.GetConfig()["EFundraisingProd.TallySale.ProductClassIds", "ProductClassIds"]); 
			return classIds;
		}


		public static bool IsTallySaleProductClass(int classId) 
		{
			string[] TallySaleProductClassIds = GetTallySaleProductClassIds();
			for (int i= 0; i< TallySaleProductClassIds.Length; i++) 
			{
				if ( Convert.ToInt32(TallySaleProductClassIds[i]) == classId)
					return true;
			}
			return false;
		}



        public static DataTable GetSaleCommssions(int sId)
        {
            DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
            return dbo.GetSaleCommissions(sId);

      
        }



		public static PostalAddress GetSaleAddressBySaleId(int sId) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetSaleAddressBySaleId(sId);
		}

		
		public static int UpdateSaleAddress(Sale s) 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			if (s.postalAddress == null)
				return -1;

			// Get current address to compare
			PostalAddress currentAddress = GetSaleAddressBySaleId(s.SalesId);
			// If the passing address is differrent to the current address
			if (s.postalAddress.IsDifferent(currentAddress))
				return dbo.UpdateSaleAddress(s, s.postalAddress);
			
			return -1;
		}

		public int Insert() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.InsertSale(this);
		}

		

		#endregion

        #region LINQ
  
         
        public static Linq.sale GetSaleByOEID(int extOrderId, DataAccess.EFundraisingCRMDatabase dbo)
        {            
            return dbo.GetSaleByExtID(extOrderId);
        }



        #endregion 

        #region Properties

        public PostalAddress SaleAddress
		{
			get
			{
				return postalAddress;
			}
			set
			{
				postalAddress = value;
			}
		}
		public int SalesId 
		{
			set { salesId = value; }
			get { return salesId; }
		}

		public int ConsultantId 
		{
			set { consultantId = value; }
			get { return consultantId; }
		}

		public short CarrierId 
		{
			set { carrierId = value; }
			get { return carrierId; }
		}

		public short ShippingOptionId 
		{
			set { shippingOptionId = value; }
			get { return shippingOptionId; }
		}

		public short PaymentTermId 
		{
			set { paymentTermId = value; }
			get { return paymentTermId; }
		}

		public string ClientSequenceCode 
		{
			set { clientSequenceCode = value; }
			get { return clientSequenceCode; }
		}

		public int ClientId 
		{
			set { clientId = value; }
			get { return clientId; }
		}

		public int SalesStatusId 
		{
			set { salesStatusId = value; }
			get { return salesStatusId; }
		}

		public short PaymentMethodId 
		{
			set { paymentMethodId = value; }
			get { return paymentMethodId; }
		}

		public short PoStatusId 
		{
			set { poStatusId = value; }
			get { return poStatusId; }
		}

		public int ProductionStatusId 
		{
			set { productionStatusId = value; }
			get { return productionStatusId; }
		}

		public int SponsorConsultantId 
		{
			set { sponsorConsultantId = value; }
			get { return sponsorConsultantId; }
		}

		public int ArConsultantId 
		{
			set { arConsultantId = value; }
			get { return arConsultantId; }
		}

		public int ArStatusId 
		{
			set { arStatusId = value; }
			get { return arStatusId; }
		}

		public int LeadId 
		{
			set { leadId = value; }
			get { return leadId; }
		}

		public int BillingCompanyId 
		{
			set { billingCompanyId = value; }
			get { return billingCompanyId; }
		}

		public short UpfrontPaymentMethodId 
		{
			set { upfrontPaymentMethodId = value; }
			get { return upfrontPaymentMethodId; }
		}

		public int ConfirmerId 
		{
			set { confirmerId = value; }
			get { return confirmerId; }
		}

		public int CollectionStatusId 
		{
			set { collectionStatusId = value; }
			get { return collectionStatusId; }
		}

		public int ConfirmationMethodId 
		{
			set { confirmationMethodId = value; }
			get { return confirmationMethodId; }
		}

		public int CreditApprovalMethodId 
		{
			set { creditApprovalMethodId = value; }
			get { return creditApprovalMethodId; }
		}

		public string PoNumber 
		{
			set { poNumber = value; }
			get { return poNumber; }
		}

		public string CreditCardNo 
		{
			set { creditCardNo = value; }
			get { return creditCardNo; }
		}

		public string ExpiryDate 
		{
			set { expiryDate = value; }
			get { return expiryDate; }
		}

		public DateTime SalesDate 
		{
			set { salesDate = value; }
			get { return salesDate; }
		}

		public decimal ShippingFees 
		{
			set { shippingFees = value; }
			get { return shippingFees; }
		}

		public decimal ShippingFeesDiscount 
		{
			set { shippingFeesDiscount = value; }
			get { return shippingFeesDiscount; }
		}

		public DateTime PaymentDueDate 
		{
			set { paymentDueDate = value; }
			get { return paymentDueDate; }
		}

		public DateTime ConfirmedDate 
		{
			set { confirmedDate = value; }
			get { return confirmedDate; }
		}

		public DateTime ScheduledDeliveryDate 
		{
			set { scheduledDeliveryDate = value; }
			get { return scheduledDeliveryDate; }
		}

		public DateTime ScheduledShipDate 
		{
			set { scheduledShipDate = value; }
			get { return scheduledShipDate; }
		}

		public DateTime ActualShipDate 
		{
			set { actualShipDate = value; }
			get { return actualShipDate; }
		}

		public string WaybillNo 
		{
			set { waybillNo = value; }
			get { return waybillNo; }
		}

		public string Comment 
		{
			set { comment = value; }
			get { return comment; }
		}

		public int CouponSheetAssigned 
		{
			set { couponSheetAssigned = value; }
			get { return couponSheetAssigned; }
		}

		public double TotalAmount 
		{
			set { totalAmount = value; }
			get { return totalAmount; }
		}

		public DateTime InvoiceDate 
		{
			set { invoiceDate = value; }
			get { return invoiceDate; }
		}

		public DateTime CancellationDate 
		{
			set { cancellationDate = value; }
			get { return cancellationDate; }
		}

		public int IsOrdered 
		{
			set { isOrdered = value; }
			get { return isOrdered; }
		}

		public DateTime PoReceivedOn 
		{
			set { poReceivedOn = value; }
			get { return poReceivedOn; }
		}

		public int IsDelivered 
		{
			set { isDelivered = value; }
			get { return isDelivered; }
		}

		public int LocalSponsorFound 
		{
			set { localSponsorFound = value; }
			get { return localSponsorFound; }
		}

		public DateTime BoxReturnDate 
		{
			set { boxReturnDate = value; }
			get { return boxReturnDate; }
		}

		public DateTime ReshipDate 
		{
			set { reshipDate = value; }
			get { return reshipDate; }
		}

		public decimal UpfrontPaymentRequired 
		{
			set { upfrontPaymentRequired = value; }
			get { return upfrontPaymentRequired; }
		}

		public DateTime UpfrontPaymentDueDate 
		{
			set { upfrontPaymentDueDate = value; }
			get { return upfrontPaymentDueDate; }
		}

		public int SponsorRequired 
		{
			set { sponsorRequired = value; }
			get { return sponsorRequired; }
		}

		public DateTime ActualDeliveryDate 
		{
			set { actualDeliveryDate = value; }
			get { return actualDeliveryDate; }
		}

		public string AccountingComments 
		{
			set { accountingComments = value; }
			get { return accountingComments; }
		}

		public string SsnNumber 
		{
			set { ssnNumber = value; }
			get { return ssnNumber; }
		}

		public string SsnAddress 
		{
			set { ssnAddress = value; }
			get { return ssnAddress; }
		}

		public string SsnCity 
		{
			set { ssnCity = value; }
			get { return ssnCity; }
		}

		public string SsnStateCode 
		{
			set { ssnStateCode = value; }
			get { return ssnStateCode; }
		}

		public string SsnCountryCode 
		{
			set { ssnCountryCode = value; }
			get { return ssnCountryCode; }
		}

		public string SsnZipCode 
		{
			set { ssnZipCode = value; }
			get { return ssnZipCode; }
		}

		public int IsValidated 
		{
			set { isValidated = value; }
			get { return isValidated; }
		}

		public DateTime PromisedDueDate 
		{
			set { promisedDueDate = value; }
			get { return promisedDueDate; }
		}

		public int GeneralFlag 
		{
			set { generalFlag = value; }
			get { return generalFlag; }
		}

        public short Fuelsurcharge
        {
            set { fuelsurcharge = value; }
            get { return fuelsurcharge; }
        }

        public int POConComm
		{
            set { poconcomm = value; }
            get { return poconcomm; }
		}

		public SaleComparable SortBy 
		{
			set { sortBy = value; }
			get { return sortBy; }
		}

		public System.Collections.ArrayList ApplicableTaxes
		{
			set { applicableTaxes = value; }
			get { return applicableTaxes; }
		}

		public System.Collections.ArrayList SalesItems 
		{
			set { salesItems = value; }
			get { return salesItems; }
		}

		public System.Collections.ArrayList Participants
		{
			get
			{
				return participants;
			}
		}

		public string PackageDescription
		{
			get
			{
				return packageDescription;
			}

			set
			{
				packageDescription = value;
			}
		}

		public bool Convert2TallySalePackByParticipants
		{
			get
			{
				return convert2TallySalePackByParticipants;
			}

			set
			{
				convert2TallySalePackByParticipants = value;
			}
		}

		public bool IsPackedByStudent
		{
			get {return isPackedByStudent;}
			set {isPackedByStudent = value;}
		}

		public bool IsEnterByStudent
		{
			get {return isEnterByStudent;}
			set {isEnterByStudent = value;}
		}

		public int CarrierTrackingId
		{
			get { return carrierTrackingId; }
			set { carrierTrackingId = value; }
		}

		public int ExtOrderID
		{
			set { extOrderID = value; }
			get { return extOrderID; }
		}

		public int QspOrderID
		{
			set { qspOrderID = value; }
			get { return qspOrderID; }
		}

        public int SAPOrderStatusID
        {
            set { sapOrderStatusID = value; }
            get { return sapOrderStatusID; }
        }

        public int PromotionCodeID
        {
            set { promotionCodeID = value; }
            get { return promotionCodeID; }
        }


		#endregion

		#region IComparable Members

		// Method that will be used by ArrayList.Sort()
		public override int CompareTo(object obj) 
		{
			// check if the two objects are the same type (same as if(obj is Sale))
			if(!CheckObjectIntegrity(obj, typeof(Sale))) 
			{
				throw new EFundraisingCRMException("CompareTo(): Object is not Sale Object");
			}

			// get the object to compare with
			Sale o = (Sale)obj;
			
			// Compare the two object depending of their sort by argument
			switch(sortBy) 
			{
				case SaleComparable.ClientId:
					return clientId.CompareTo(o.ClientId);
				case SaleComparable.ConfirmedDate:
					return confirmedDate.CompareTo(o.ConfirmedDate);
				case SaleComparable.SalesDate:
					return salesDate.CompareTo(o.SalesDate);
				case SaleComparable.SalesId:
					return salesId.CompareTo(o.SalesId);
				default:
					// compare argument not found, throw exception
					throw new EFundraisingCRMException("Sale.CompareTo invalid comparer option");
			}
		}

		#endregion
	}
}
