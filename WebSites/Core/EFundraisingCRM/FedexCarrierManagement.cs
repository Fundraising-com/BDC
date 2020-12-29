using System;
using System.Text.RegularExpressions;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Summary description for FedexCarrierManagement.
	/// </summary>
	public class FedexCarrierManagement : CarrierManagement
	{
		private const int OUR_FEDEX_ACCOUNT = 161245119;
		private const int MAX_SCRATCHARDS = 60; // the maximum number of scratchcards that fit in on box (package)
		
		public FedexCarrierManagement()
		{
			
		}

		#region Public Methods

		public Fedex SendSale(int productClassId, int saleId, string companyName, string contactName, string addressLine1, string addressLine2,
			string city, string state, string country, string zip, string telephone, string extension, string shipalertEmailAddress,
			string serviceLevel, string packageType, int weight)
		{
			Fedex fedex = new Fedex();
			 
            fedex.CompanyName = companyName;
			fedex.ContactName = contactName;
			fedex.AddressLine1 = addressLine1;
			fedex.AddressLine2 = addressLine2;
			fedex.City = city;
			fedex.ProvinceState = state;
			fedex.Country = country;
			fedex.ZipPostalCode = zip;
			fedex.Telephone = Regex.Replace(telephone, @"\D", "");
			fedex.Extention = extension;
		
			fedex.FedexAccount = OUR_FEDEX_ACCOUNT;
			fedex.ShipalertEmailAddress = shipalertEmailAddress;
			fedex.ShipalertEmailOption = 1;
			fedex.SeviceLevel = serviceLevel;
			fedex.BillFreightChargesTo = 1;  // always bill to us by default
			fedex.TotalPackageWeight = weight;
			fedex.InterQuantity = GetNumberOfItemsForSale(saleId);
			fedex.NumberOfPackages = CalculateNumberOfPackages(productClassId, fedex.InterQuantity);

			// all infos related to harmizedcode come from harmonizedinfo class
			HarmonizedInfo hi = HarmonizedInfo.CreateHarmonizedInfoByProductClassId(productClassId, saleId);
			fedex.InterUnitValue = hi.UnitValue;
			fedex.InterUnitOfMeasure = hi.UnitOfMeasure;
			fedex.InterCountryOfManufacture = hi.CountryOfManufacture;
			fedex.InterCurrency = hi.Currency;
			fedex.InterHarmonizedCode = hi.HarmonizedCode;
			fedex.InterPartDescription = hi.PartDescription;
			fedex.FedexUid = hi.FedexUid; // our internal ref number (ex: for samples and orders: SALEID +(B for brochures or SC for scratchcards) or promo kits : LEADID +(K for kit)

			fedex.InterBillDutiesTaxesTo = 1; // always bill to us by default
			fedex.InterCreateDate = DateTime.Now;

			fedex.Cancelled = 0;


			FedexStatus status = fedex.Insert();
			if (status == FedexStatus.Ok)
				return fedex;
			else
				return null;
		}

		public Fedex SendPromoKit(int leadId, string companyName, string contactName, string addressLine1, string addressLine2,
			string city, string state, string country, string zip, string telephone, string extension, string shipalertEmailAddress,
			int numberOfItems, string serviceLevel, string packageType, int weight)
		{
			Fedex fedex = new Fedex();
			 
			fedex.CompanyName = companyName;
			fedex.ContactName = contactName;
			fedex.AddressLine1 = addressLine1;
			fedex.AddressLine2 = addressLine2;
			fedex.City = city;
			fedex.ProvinceState = state;
			fedex.Country = country;
			fedex.ZipPostalCode = zip;
			fedex.Telephone = Regex.Replace(telephone, @"\D", "");
			fedex.Extention = extension;
		
			fedex.FedexAccount = OUR_FEDEX_ACCOUNT;
			fedex.ShipalertEmailAddress = shipalertEmailAddress;
			fedex.ShipalertEmailOption = 1;
			fedex.NumberOfPackages = 1;
			fedex.SeviceLevel = serviceLevel;
			fedex.BillFreightChargesTo = 1;  // always bill to us by default
			fedex.TotalPackageWeight = weight;
			fedex.InterQuantity = numberOfItems;

			// all infos related to harmizedcode come from harmonizedinfo class
			HarmonizedInfo hi = HarmonizedInfo.CreateHarmonizedInfoForPromoKit(leadId);
			fedex.InterUnitValue = hi.UnitValue;
			fedex.InterUnitOfMeasure = hi.UnitOfMeasure;
			fedex.InterCountryOfManufacture = hi.CountryOfManufacture;
			fedex.InterCurrency = hi.Currency;
			fedex.InterHarmonizedCode = hi.HarmonizedCode;
			fedex.InterPartDescription = hi.PartDescription;
			fedex.FedexUid = hi.FedexUid; // our internal ref number (ex: for samples and orders: SALEID +(B for brochures or SC for scratchcards) or promo kits : LEADID +(K for kit)

			fedex.InterBillDutiesTaxesTo = 1; // always bill to us by default
			fedex.InterCreateDate = DateTime.Now;

			fedex.Cancelled = 0;

			FedexStatus status = fedex.Insert();
			if (status == FedexStatus.Ok)
				return fedex;
			else
				return null;
		}


		public void ProcessPromoKitsReady()
		{

			PromotionalKit[] promoKits = PromotionalKit.GetPromotionalKitsReadyForFedex();
			Fedex[] fedexs = Fedex.GetFedexReadyForPromotionalKits();

			if (promoKits != null && fedexs != null)
			{
				if (promoKits.Length > 0)
					if (promoKits.Length == fedexs.Length)
					{
						for (int i = 0; i < promoKits.Length; i++)
						{
							promoKits[i].SentDate = DateTime.Now;
							fedexs[i].InterUpdateSaleDate = DateTime.Now;
						}

						TransactionController trans = new TransactionController();
						trans.UpdateFedexObjects(promoKits, null, fedexs);
					}
					else
					{
						throw new ArgumentException("The number of PromoKits does not match the number of Fedex entries.");
					}

			}
			
			
		}

		public void ProcessSalesReady()
		{

			Sale[] sales = Sale.GetSalesReadyForFedex();
			Fedex[] fedexs = Fedex.GetFedexReadyForSales();
			
			if (sales != null && fedexs != null)
			{
				if (sales.Length > 0)	
					if (sales.Length == fedexs.Length)
					{
						for (int i = 0; i < sales.Length; i++)
						{
							sales[i].ActualShipDate = DateTime.Now;
							sales[i].WaybillNo = FindTrackingNumberForSale(sales[i], fedexs);
							sales[i].ProductionStatusId = ProductionStatus.StockShipped.ProductionStatusID;
							fedexs[i].InterUpdateSaleDate = DateTime.Now;
						}

						TransactionController trans = new TransactionController();
						trans.UpdateFedexObjects(null, sales, fedexs);
					}
					else
					{
						throw new ArgumentException("The number of Sales does not match the number of Fedex entries.");
					}

			}
			
			
		}

		#endregion
		
		#region Private Methods

		private int CalculateNumberOfPackages(int productClassId, int numberOfItems)
		{
			int numberOfPackages;

			// for scratchcards
			if (productClassId == 1)
			{
				numberOfPackages = (int)System.Math.Ceiling((double)numberOfItems / MAX_SCRATCHARDS);
				return numberOfPackages;
			}

			return 1;

		}

		private string FindTrackingNumberForSale(Sale sale, Fedex[] fedexs)
		{
			string trackingNumber = null;

			for (int i=0; i<fedexs.Length; i++)
			{
				if (fedexs[i].FedexId == sale.CarrierTrackingId)
				{
					trackingNumber = fedexs[i].TrackingNumber;
					break;
				}
			}

			return trackingNumber;
		}

		private int GetNumberOfItemsForSale(int saleId)
		{
			int total = 0;
			SalesItem[] items = SalesItem.GetSalesItemsBySaleID(saleId);

			foreach(SalesItem si in items)
			{
				total += si.QuantitySold;
				total += si.QuantityFree;
			}

			return total;
		}


		#endregion

		#region Static Methods

		public static ListItem[] GetServiceLevels()
		{
			ListItem[] items = new ListItem[10];
			items[0] = new ListItem("1", "Priority Overnight");
			items[1] = new ListItem("2", "Standard Overnight");
			items[2] = new ListItem("3", "2Day");
			items[3] = new ListItem("4", "1Day Freight");
			items[4] = new ListItem("5", "2Day Freight");
			items[5] = new ListItem("6", "Fist Overnight");
			items[6] = new ListItem("7", "Express Saver");
			items[7] = new ListItem("8", "3Day Freight");
			items[8] = new ListItem("R", "Ground Service");
			items[9] = new ListItem("10", "International Economy");
			return items;
		}

		public static ListItem[] GetPackageTypes()
		{
			ListItem[] items = new ListItem[3];
			items[0] = new ListItem("1", "Your Packaging");
			items[1] = new ListItem("2", "FedEx Env.");
			items[2] = new ListItem("3", "FedEx Pack");
			return items;
		}

		#endregion

		#region Structures and Sub-Classes

		public struct ListItem
		{
			string id;
			string desc;
	
			public ListItem(string id, string desc)
			{
				this.id = id;
				this.desc = desc;
			}

			public string Id
			{
				get { return id; }
			}

			public string Desc
			{
				get { return desc; }
			}
		}
		
		// used to regroup some related and dynamically generated infos
		public class HarmonizedInfo
		{
			double unitValue = double.MinValue;
			string currency = "";
			string unitOfMeasure = "";
			string countryOfManufacture = "";
			long harmonizedCode = long.MinValue;
			string partDescription = "";
			string fedexUid = "";
			
			public HarmonizedInfo()
			{}

			public static HarmonizedInfo CreateHarmonizedInfoByProductClassId(int productClassId, int saleId)
			{
				HarmonizedInfo hi = new HarmonizedInfo();
				
				switch(productClassId)
				{
					// Scratchcards
					case 1:
						hi.unitValue = 2;
						hi.currency = "USD";
						hi.unitOfMeasure = "LB";
						hi.countryOfManufacture = "CA";
						hi.harmonizedCode = 4817200000;
						hi.fedexUid = saleId + " SC";
						hi.partDescription = "SCRATCH CARDS";
						break;
					// Brochures
					case 8:
					case 24:
						hi.unitValue = 0.10;
						hi.currency = "USD";
						hi.unitOfMeasure = "LB";
						hi.countryOfManufacture = "CA";
						hi.harmonizedCode = 4901100000;
						hi.fedexUid = saleId + " B";
						hi.partDescription = "BROCHURES";
						break;
					// Samples
					case 2:
						hi.unitValue = 10;
						hi.currency = "USD";
						hi.unitOfMeasure = "LB";
						hi.countryOfManufacture = "CA";
						hi.harmonizedCode = 0;
						hi.fedexUid = saleId + " S";
						hi.partDescription = "GIFT SAMPLES";
						break;
				}

				return hi;
			}

			public static HarmonizedInfo CreateHarmonizedInfoForPromoKit(int leadId)
			{
				HarmonizedInfo hi = new HarmonizedInfo();
				
				hi.unitValue = 1;
				hi.currency = "USD";
				hi.unitOfMeasure = "EA";
				hi.countryOfManufacture = "CA";
				hi.harmonizedCode = 4817200000;
				hi.fedexUid = leadId + " K";
				hi.partDescription = "SCRATCH CARD KITS";

				return hi;
			}
			

			public double UnitValue
			{
				get {return unitValue; }
			}
			public string Currency
			{
				get {return currency; }
			}
			public string UnitOfMeasure
			{
				get {return unitOfMeasure; }
			}
			public string CountryOfManufacture
			{
				get {return countryOfManufacture; }
			}
			public long HarmonizedCode
			{
				get {return harmonizedCode; }
			}
			public string PartDescription
			{
				get {return partDescription; }
			}
			public string FedexUid
			{
				get {return fedexUid; }
			}

		}
		
		#endregion

	}

	
}
