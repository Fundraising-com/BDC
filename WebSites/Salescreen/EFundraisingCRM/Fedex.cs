using System;
using System.Xml;

namespace efundraising.EFundraisingCRM {

	public enum FedexStatus
	{
		Ok,
		Error
	}

	public class Fedex : EFundraisingCRMDataObject, ICarrier {

		private int fedexId;
		private string fedexUid;
		private string companyName;
		private string contactName;
		private string addressLine1;
		private string addressLine2;
		private string city;
		private string provinceState;
		private string country;
		private string zipPostalCode;
		private string telephone;
		private string extention;
		private string taxIdSsn;
		private int fedexAccount;
		private string shipalertEmailAddress;
		private string shipalertEmailMessage;
		private int shipalertEmailOption;
		private int totalPackageWeight;
		private int numberOfPackages;
		private int dimensionHeight;
		private int dimensionWidth;
		private int dimensionLength;
		private string seviceLevel;
		private int billFreightChargesTo;
		private string interPartDescription;
		private double interUnitValue;
		private string interCurrency;
		private string interUnitOfMeasure;
		private int interQuantity;
		private string interCountryOfManufacture;
		private long interHarmonizedCode;
		private string interPartNumber;
		private string interMarksNumber;
		private string interSkuUpcItem;
		private int interBillDutiesTaxesTo;
		private DateTime interCreateDate;
		private string interTrackingNumber;
		private DateTime interLabelDateShippedDate;
		private DateTime interUpdateSaleDate;
		private double interShippingQuote;
		private int cancelled = 0;
		private double codAmount;
		private int codPaymentMethod;


		public Fedex() : this(int.MinValue) { }
		public Fedex(int fedexId) : this(fedexId, null) { }
		public Fedex(int fedexId, string fedexUid) : this(fedexId, fedexUid, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName) : this(fedexId, fedexUid, companyName, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName) : this(fedexId, fedexUid, companyName, contactName, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1) : this(fedexId, fedexUid, companyName, contactName, addressLine1, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, DateTime.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, null) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, DateTime.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, interLabelDateShippedDate, DateTime.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate, DateTime interUpdateSaleDate) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, interLabelDateShippedDate, interUpdateSaleDate, double.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate, DateTime interUpdateSaleDate, double interShippingQuote) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, interLabelDateShippedDate, interUpdateSaleDate, interShippingQuote, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate, DateTime interUpdateSaleDate, double interShippingQuote, int cancelled) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, interLabelDateShippedDate, interUpdateSaleDate, interShippingQuote, cancelled, double.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate, DateTime interUpdateSaleDate, double interShippingQuote, int cancelled, double codAmount) : this(fedexId, fedexUid, companyName, contactName, addressLine1, addressLine2, city, provinceState, country, zipPostalCode, telephone, extention, taxIdSsn, fedexAccount, shipalertEmailAddress, shipalertEmailMessage, shipalertEmailOption, totalPackageWeight, numberOfPackages, dimensionHeight, dimensionWidth, dimensionLength, seviceLevel, billFreightChargesTo, interPartDescription, interUnitValue, interCurrency, interUnitOfMeasure, interQuantity, interCountryOfManufacture, interHarmonizedCode, interPartNumber, interMarksNumber, interSkuUpcItem, interBillDutiesTaxesTo, interCreateDate, interTrackingNumber, interLabelDateShippedDate, interUpdateSaleDate, interShippingQuote, cancelled, codAmount, int.MinValue) { }
		public Fedex(int fedexId, string fedexUid, string companyName, string contactName, string addressLine1, string addressLine2, string city, string provinceState, string country, string zipPostalCode, string telephone, string extention, string taxIdSsn, int fedexAccount, string shipalertEmailAddress, string shipalertEmailMessage, int shipalertEmailOption, int totalPackageWeight, int numberOfPackages, int dimensionHeight, int dimensionWidth, int dimensionLength, string seviceLevel, int billFreightChargesTo, string interPartDescription, double interUnitValue, string interCurrency, string interUnitOfMeasure, int interQuantity, string interCountryOfManufacture, long interHarmonizedCode, string interPartNumber, string interMarksNumber, string interSkuUpcItem, int interBillDutiesTaxesTo, DateTime interCreateDate, string interTrackingNumber, DateTime interLabelDateShippedDate, DateTime interUpdateSaleDate, double interShippingQuote, int cancelled, double codAmount, int codPaymentMethod) 
		{
			this.fedexId = fedexId;
			this.fedexUid = fedexId.ToString();	// fedex uid is the same as the id (used by fedex machine to read data)
			this.companyName = companyName;
			this.contactName = contactName;
			this.addressLine1 = addressLine1;
			this.addressLine2 = addressLine2;
			this.city = city;
			this.provinceState = provinceState;
			this.country = country;
			this.zipPostalCode = zipPostalCode;
			this.telephone = telephone;
			this.extention = extention;
			this.taxIdSsn = taxIdSsn;
			this.fedexAccount = fedexAccount;
			this.shipalertEmailAddress = shipalertEmailAddress;
			this.shipalertEmailMessage = shipalertEmailMessage;
			this.shipalertEmailOption = shipalertEmailOption;
			this.totalPackageWeight = totalPackageWeight;
			this.numberOfPackages = numberOfPackages;
			this.dimensionHeight = dimensionHeight;
			this.dimensionWidth = dimensionWidth;
			this.dimensionLength = dimensionLength;
			this.seviceLevel = seviceLevel;
			this.billFreightChargesTo = billFreightChargesTo;
			this.interPartDescription = interPartDescription;
			this.interUnitValue = interUnitValue;
			this.interCurrency = interCurrency;
			this.interUnitOfMeasure = interUnitOfMeasure;
			this.interQuantity = interQuantity;
			this.interCountryOfManufacture = interCountryOfManufacture;
			this.interHarmonizedCode = interHarmonizedCode;
			this.interPartNumber = interPartNumber;
			this.interMarksNumber = interMarksNumber;
			this.interSkuUpcItem = interSkuUpcItem;
			this.interBillDutiesTaxesTo = interBillDutiesTaxesTo;
			this.interCreateDate = interCreateDate;
			this.interTrackingNumber = interTrackingNumber;
			this.interLabelDateShippedDate = interLabelDateShippedDate;
			this.interUpdateSaleDate = interUpdateSaleDate;
			this.interShippingQuote = interShippingQuote;
			this.cancelled = cancelled;
			this.codAmount = codAmount;
			this.codPaymentMethod = codPaymentMethod;
		}

		#region XML Methods

		#region Save XML
		public override string GenerateXML() {
			return "<Fedex>\r\n" +
				"	<FedexId>" + fedexId + "</FedexId>\r\n" +
				"	<CompanyName>" + System.Web.HttpUtility.HtmlEncode(companyName) + "</CompanyName>\r\n" +
				"	<ContactName>" + System.Web.HttpUtility.HtmlEncode(contactName) + "</ContactName>\r\n" +
				"	<AddressLine1>" + System.Web.HttpUtility.HtmlEncode(addressLine1) + "</AddressLine1>\r\n" +
				"	<AddressLine2>" + System.Web.HttpUtility.HtmlEncode(addressLine2) + "</AddressLine2>\r\n" +
				"	<City>" + System.Web.HttpUtility.HtmlEncode(city) + "</City>\r\n" +
				"	<ProvinceState>" + System.Web.HttpUtility.HtmlEncode(provinceState) + "</ProvinceState>\r\n" +
				"	<Country>" + System.Web.HttpUtility.HtmlEncode(country) + "</Country>\r\n" +
				"	<ZipPostalCode>" + System.Web.HttpUtility.HtmlEncode(zipPostalCode) + "</ZipPostalCode>\r\n" +
				"	<Telephone>" + System.Web.HttpUtility.HtmlEncode(telephone) + "</Telephone>\r\n" +
				"	<Extention>" + System.Web.HttpUtility.HtmlEncode(extention) + "</Extention>\r\n" +
				"	<TaxIdSsn>" + System.Web.HttpUtility.HtmlEncode(taxIdSsn) + "</TaxIdSsn>\r\n" +
				"	<FedexAccount>" + fedexAccount + "</FedexAccount>\r\n" +
				"	<ShipalertEmailAddress>" + System.Web.HttpUtility.HtmlEncode(shipalertEmailAddress) + "</ShipalertEmailAddress>\r\n" +
				"	<ShipalertEmailMessage>" + System.Web.HttpUtility.HtmlEncode(shipalertEmailMessage) + "</ShipalertEmailMessage>\r\n" +
				"	<ShipalertEmailOption>" + shipalertEmailOption + "</ShipalertEmailOption>\r\n" +
				"	<TotalPackageWeight>" + totalPackageWeight + "</TotalPackageWeight>\r\n" +
				"	<NumberOfPackages>" + numberOfPackages + "</NumberOfPackages>\r\n" +
				"	<DimensionHeight>" + dimensionHeight + "</DimensionHeight>\r\n" +
				"	<DimensionWidth>" + dimensionWidth + "</DimensionWidth>\r\n" +
				"	<DimensionLength>" + dimensionLength + "</DimensionLength>\r\n" +
				"	<SeviceLevel>" + System.Web.HttpUtility.HtmlEncode(seviceLevel) + "</SeviceLevel>\r\n" +
				"	<BillFreightChargesTo>" + billFreightChargesTo + "</BillFreightChargesTo>\r\n" +
				"	<InterPartDescription>" + System.Web.HttpUtility.HtmlEncode(interPartDescription) + "</InterPartDescription>\r\n" +
				"	<InterUnitValue>" + interUnitValue + "</InterUnitValue>\r\n" +
				"	<InterCurrency>" + System.Web.HttpUtility.HtmlEncode(interCurrency) + "</InterCurrency>\r\n" +
				"	<InterUnitOfMeasure>" + System.Web.HttpUtility.HtmlEncode(interUnitOfMeasure) + "</InterUnitOfMeasure>\r\n" +
				"	<InterQuantity>" + interQuantity + "</InterQuantity>\r\n" +
				"	<InterCountryOfManufacture>" + System.Web.HttpUtility.HtmlEncode(interCountryOfManufacture) + "</InterCountryOfManufacture>\r\n" +
				"	<InterHarmonizedCode>" + interHarmonizedCode + "</InterHarmonizedCode>\r\n" +
				"	<InterPartNumber>" + System.Web.HttpUtility.HtmlEncode(interPartNumber) + "</InterPartNumber>\r\n" +
				"	<InterMarksNumber>" + System.Web.HttpUtility.HtmlEncode(interMarksNumber) + "</InterMarksNumber>\r\n" +
				"	<InterSkuUpcItem>" + System.Web.HttpUtility.HtmlEncode(interSkuUpcItem) + "</InterSkuUpcItem>\r\n" +
				"	<InterBillDutiesTaxesTo>" + interBillDutiesTaxesTo + "</InterBillDutiesTaxesTo>\r\n" +
				"	<InterCreateDate>" + interCreateDate + "</InterCreateDate>\r\n" +
				"	<InterTrackingNumber>" + System.Web.HttpUtility.HtmlEncode(interTrackingNumber) + "</InterTrackingNumber>\r\n" +
				"	<InterLabelDateShippedDate>" + interLabelDateShippedDate + "</InterLabelDateShippedDate>\r\n" +
				"	<InterUpdateSaleDate>" + interUpdateSaleDate + "</InterUpdateSaleDate>\r\n" +
				"	<InterShippingQuote>" + interShippingQuote + "</InterShippingQuote>\r\n" +
				"	<Cancelled>" + cancelled + "</Cancelled>\r\n" +
				"	<CodAmount>" + codAmount + "</CodAmount>\r\n" +
				"	<CodPaymentMethod>" + codPaymentMethod + "</CodPaymentMethod>\r\n" +
				"</Fedex>\r\n";
		}
		#endregion

		#region Load Methods
		// loop through the xml and set the properties and child classes
		public override void Load(XmlNode childNodes) {
			foreach(XmlNode node in childNodes) {
				if(node.Name.ToLower() == "fedexId") {
					SetXmlValue(ref fedexId, node.InnerText);
				}
				if(node.Name.ToLower() == "companyName") {
					SetXmlValue(ref companyName, node.InnerText);
				}
				if(node.Name.ToLower() == "contactName") {
					SetXmlValue(ref contactName, node.InnerText);
				}
				if(node.Name.ToLower() == "addressLine1") {
					SetXmlValue(ref addressLine1, node.InnerText);
				}
				if(node.Name.ToLower() == "addressLine2") {
					SetXmlValue(ref addressLine2, node.InnerText);
				}
				if(node.Name.ToLower() == "city") {
					SetXmlValue(ref city, node.InnerText);
				}
				if(node.Name.ToLower() == "provinceState") {
					SetXmlValue(ref provinceState, node.InnerText);
				}
				if(node.Name.ToLower() == "country") {
					SetXmlValue(ref country, node.InnerText);
				}
				if(node.Name.ToLower() == "zipPostalCode") {
					SetXmlValue(ref zipPostalCode, node.InnerText);
				}
				if(node.Name.ToLower() == "telephone") {
					SetXmlValue(ref telephone, node.InnerText);
				}
				if(node.Name.ToLower() == "extention") {
					SetXmlValue(ref extention, node.InnerText);
				}
				if(node.Name.ToLower() == "taxIdSsn") {
					SetXmlValue(ref taxIdSsn, node.InnerText);
				}
				if(node.Name.ToLower() == "fedexAccount") {
					SetXmlValue(ref fedexAccount, node.InnerText);
				}
				if(node.Name.ToLower() == "shipalertEmailAddress") {
					SetXmlValue(ref shipalertEmailAddress, node.InnerText);
				}
				if(node.Name.ToLower() == "shipalertEmailMessage") {
					SetXmlValue(ref shipalertEmailMessage, node.InnerText);
				}
				if(node.Name.ToLower() == "shipalertEmailOption") {
					SetXmlValue(ref shipalertEmailOption, node.InnerText);
				}
				if(node.Name.ToLower() == "totalPackageWeight") {
					SetXmlValue(ref totalPackageWeight, node.InnerText);
				}
				if(node.Name.ToLower() == "numberOfPackages") {
					SetXmlValue(ref numberOfPackages, node.InnerText);
				}
				if(node.Name.ToLower() == "dimensionHeight") {
					SetXmlValue(ref dimensionHeight, node.InnerText);
				}
				if(node.Name.ToLower() == "dimensionWidth") {
					SetXmlValue(ref dimensionWidth, node.InnerText);
				}
				if(node.Name.ToLower() == "dimensionLength") {
					SetXmlValue(ref dimensionLength, node.InnerText);
				}
				if(node.Name.ToLower() == "seviceLevel") {
					SetXmlValue(ref seviceLevel, node.InnerText);
				}
				if(node.Name.ToLower() == "billFreightChargesTo") {
					SetXmlValue(ref billFreightChargesTo, node.InnerText);
				}
				if(node.Name.ToLower() == "interPartDescription") {
					SetXmlValue(ref interPartDescription, node.InnerText);
				}
				if(node.Name.ToLower() == "interUnitValue") {
					SetXmlValue(ref interUnitValue, node.InnerText);
				}
				if(node.Name.ToLower() == "interCurrency") {
					SetXmlValue(ref interCurrency, node.InnerText);
				}
				if(node.Name.ToLower() == "interUnitOfMeasure") {
					SetXmlValue(ref interUnitOfMeasure, node.InnerText);
				}
				if(node.Name.ToLower() == "interQuantity") {
					SetXmlValue(ref interQuantity, node.InnerText);
				}
				if(node.Name.ToLower() == "interCountryOfManufacture") {
					SetXmlValue(ref interCountryOfManufacture, node.InnerText);
				}
				if(node.Name.ToLower() == "interHarmonizedCode") {
					SetXmlValue(ref interHarmonizedCode, node.InnerText);
				}
				if(node.Name.ToLower() == "interPartNumber") {
					SetXmlValue(ref interPartNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "interMarksNumber") {
					SetXmlValue(ref interMarksNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "interSkuUpcItem") {
					SetXmlValue(ref interSkuUpcItem, node.InnerText);
				}
				if(node.Name.ToLower() == "interBillDutiesTaxesTo") {
					SetXmlValue(ref interBillDutiesTaxesTo, node.InnerText);
				}
				if(node.Name.ToLower() == "interCreateDate") {
					SetXmlValue(ref interCreateDate, node.InnerText);
				}
				if(node.Name.ToLower() == "interTrackingNumber") {
					SetXmlValue(ref interTrackingNumber, node.InnerText);
				}
				if(node.Name.ToLower() == "interLabelDateShippedDate") {
					SetXmlValue(ref interLabelDateShippedDate, node.InnerText);
				}
				if(node.Name.ToLower() == "interUpdateSaleDate") {
					SetXmlValue(ref interUpdateSaleDate, node.InnerText);
				}
				if(node.Name.ToLower() == "interShippingQuote") 
				{
					SetXmlValue(ref interShippingQuote, node.InnerText);
				}
				if(node.Name.ToLower() == "cancelled") 
				{
					SetXmlValue(ref cancelled, node.InnerText);
				}
				if(node.Name.ToLower() == "codAmount") 
				{
					SetXmlValue(ref codAmount, node.InnerText);
				}
				if(node.Name.ToLower() == "codPaymentMethod") 
				{
					SetXmlValue(ref codPaymentMethod, node.InnerText);
				}
			}
		}
		#endregion

		#endregion

		#region Data Source Methods
		public static Fedex[] GetFedexs() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFedexs();
		}

		public static Fedex[] GetFedexReadyForPromotionalKits() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFedexReadyForPromotionalKits();
		}

		public static Fedex[] GetFedexReadyForSales() 
		{
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFedexReadyForSales();
		}

		public static Fedex GetFedexByID(int id) {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			return dbo.GetFedexByID(id);
		}

		public FedexStatus Insert() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.InsertFedex(this);
			switch(returnValue)
			{
				case 1:
				case 2:
					return FedexStatus.Ok;
				default:
					return FedexStatus.Error;
			}
		}

		public FedexStatus Update() {
			DataAccess.EFundraisingCRMDatabase dbo = new DataAccess.EFundraisingCRMDatabase();
			int returnValue = dbo.UpdateFedex(this);
			switch(returnValue)
			{
				case 1:
					return FedexStatus.Ok;
			}
			return FedexStatus.Error;
		}
		#endregion

		#region Properties
		public int FedexId {
			set { fedexId = value; }
			get { return fedexId; }
		}

		public string FedexUid {
			get { return fedexUid; }
			set { fedexUid = value; }
		}

		public string CompanyName {
			set { companyName = value; }
			get { return companyName; }
		}

		public string ContactName {
			set { contactName = value; }
			get { return contactName; }
		}

		public string AddressLine1 {
			set { addressLine1 = value; }
			get { return addressLine1; }
		}

		public string AddressLine2 {
			set { addressLine2 = value; }
			get { return addressLine2; }
		}

		public string City {
			set { city = value; }
			get { return city; }
		}

		public string ProvinceState {
			set { provinceState = value; }
			get { return provinceState; }
		}

		public string Country {
			set { country = value; }
			get { return country; }
		}

		public string ZipPostalCode {
			set { zipPostalCode = value; }
			get { return zipPostalCode; }
		}

		public string Telephone {
			set { telephone = value; }
			get { return telephone; }
		}

		public string Extention {
			set { extention = value; }
			get { return extention; }
		}

		public string TaxIdSsn {
			set { taxIdSsn = value; }
			get { return taxIdSsn; }
		}

		public int FedexAccount {
			set { fedexAccount = value; }
			get { return fedexAccount; }
		}

		public string ShipalertEmailAddress {
			set { shipalertEmailAddress = value; }
			get { return shipalertEmailAddress; }
		}

		public string ShipalertEmailMessage {
			set { shipalertEmailMessage = value; }
			get { return shipalertEmailMessage; }
		}

		public int ShipalertEmailOption {
			set { shipalertEmailOption = value; }
			get { return shipalertEmailOption; }
		}

		public int TotalPackageWeight {
			set { totalPackageWeight = value; }
			get { return totalPackageWeight; }
		}

		public int NumberOfPackages {
			set { numberOfPackages = value; }
			get { return numberOfPackages; }
		}

		public int DimensionHeight {
			set { dimensionHeight = value; }
			get { return dimensionHeight; }
		}

		public int DimensionWidth {
			set { dimensionWidth = value; }
			get { return dimensionWidth; }
		}

		public int DimensionLength {
			set { dimensionLength = value; }
			get { return dimensionLength; }
		}

		public string SeviceLevel {
			set { seviceLevel = value; }
			get { return seviceLevel; }
		}

		public int BillFreightChargesTo {
			set { billFreightChargesTo = value; }
			get { return billFreightChargesTo; }
		}

		public string InterPartDescription {
			set { interPartDescription = value; }
			get { return interPartDescription; }
		}

		public double InterUnitValue {
			set { interUnitValue = value; }
			get { return interUnitValue; }
		}

		public string InterCurrency {
			set { interCurrency = value; }
			get { return interCurrency; }
		}

		public string InterUnitOfMeasure {
			set { interUnitOfMeasure = value; }
			get { return interUnitOfMeasure; }
		}

		public int InterQuantity {
			set { interQuantity = value; }
			get { return interQuantity; }
		}

		public string InterCountryOfManufacture {
			set { interCountryOfManufacture = value; }
			get { return interCountryOfManufacture; }
		}

		public long InterHarmonizedCode {
			set { interHarmonizedCode = value; }
			get { return interHarmonizedCode; }
		}

		public string InterPartNumber {
			set { interPartNumber = value; }
			get { return interPartNumber; }
		}

		public string InterMarksNumber {
			set { interMarksNumber = value; }
			get { return interMarksNumber; }
		}

		public string InterSkuUpcItem {
			set { interSkuUpcItem = value; }
			get { return interSkuUpcItem; }
		}

		public int InterBillDutiesTaxesTo {
			set { interBillDutiesTaxesTo = value; }
			get { return interBillDutiesTaxesTo; }
		}

		public DateTime InterCreateDate {
			set { interCreateDate = value; }
			get { return interCreateDate; }
		}

		public string InterTrackingNumber {
			set { interTrackingNumber = value; }
			get { return interTrackingNumber; }
		}

		public DateTime InterLabelDateShippedDate {
			set { interLabelDateShippedDate = value; }
			get { return interLabelDateShippedDate; }
		}

		public DateTime InterUpdateSaleDate {
			set { interUpdateSaleDate = value; }
			get { return interUpdateSaleDate; }
		}

		public double InterShippingQuote 
		{
			set { interShippingQuote = value; }
			get { return interShippingQuote; }
		}

		public int Cancelled 
		{
			set { cancelled = value; }
			get { return cancelled; }
		}

		public double CodAmount 
		{
			set { codAmount = value; }
			get { return codAmount; }
		}

		public int CodPaymentMethod 
		{
			set { codPaymentMethod = value; }
			get { return codPaymentMethod; }
		}

		#endregion

		#region ICarrier Members

		public void Send()
		{
			this.Insert();
		}

		public string TrackingNumber
		{
			get
			{
				return this.InterTrackingNumber;
			}
		}

		public int CarrierId
		{
			get
			{
				return Carrier.FEDEX.CarrierId;
			}
		}

		public string Description
		{
			get
			{
				return "FEDEX Shipping";
			}
		}

		#endregion
	}
}
