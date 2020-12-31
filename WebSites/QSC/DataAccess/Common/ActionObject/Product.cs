using System;
using System.Runtime.Serialization;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for Magazine.
	/// </summary>
	/// 
	[Serializable]
	public class Product
	{
		private int productInstance = 0;
		private string productCode = "";
		private int year = 0;
		private string season = "";
		private string productName = "";
		private string language = "";
		private int category = 0;
		private int status = 0;
		private ProductType productType = ProductType.Magazine;
		private int daysLeadTime = 0;
		private int term = 0;
		private int publisherNumber = 0;
		private string note = "";
		private int vendorNumber = 0;
		private string vendorSiteName = "";
		private string payGroupLookUpCode = "";
		private string currency = "";
	    private string vendorProductCode = "";


		public Product() { }

        public Product(int productInstance, string productCode, int year, string season, ProductType productType) : this(productInstance, productCode, year, season, String.Empty, String.Empty, 0, 0, productType, 0, 0, 0, String.Empty, 0, String.Empty, String.Empty, String.Empty, String.Empty) { }

        public Product(int productInstance, string productCode, int year, string season, string productName, string language, int category, int status, ProductType productType, int daysLeadTime, int term, int publisherNumber, string note, int vendorNumber, string vendorSiteName, string payGroupLookUpCode, string currency, string vendorProductCode)
		{
			this.productInstance = productInstance;
			this.productCode = productCode;
			this.year = year;
			this.season = season;
			this.productName = productName;
			this.language = language;
			this.category = category;
			this.status = status;
			this.productType = productType;
			this.daysLeadTime = daysLeadTime;
			this.term = term;
			this.publisherNumber = publisherNumber;
			this.note = note;
			this.vendorNumber = vendorNumber;
			this.vendorSiteName = vendorSiteName;
			this.payGroupLookUpCode = payGroupLookUpCode;
			this.currency = currency;
            this.vendorProductCode = vendorProductCode;
		}

		public int ProductInstance 
		{
			get 
			{
				return productInstance;
			}
			set 
			{
				productInstance = value;
			}
		}

		public string ProductCode
		{
			get
			{
				return productCode;
			}
			set 
			{
				productCode = value;
			}
		}

		public int Year 
		{
			get 
			{
				return year;
			}
			set 
			{
				year = value;
			}
		}

		public string Season 
		{
			get 
			{
				return season;
			}
			set 
			{
				season = value;
			}
		}

		public string ProductName
		{
			get
			{
				return productName;
			}
			set 
			{
				productName = value;
			}
		}

		public string Language
		{
			get
			{
				return language;
			}
			set 
			{
				language = value;
			}
		}

		public int Category
		{
			get
			{
				return category;
			}
			set 
			{
				category = value;
			}
		}

		public int Status
		{
			get
			{
				return status;
			}
			set 
			{
				status = value;
			}
		}

		public ProductType ProductType
		{
			get
			{
				return productType;
			}
			set 
			{
				productType = value;
			}
		}

		public int DaysLeadTime
		{
			get
			{
				return daysLeadTime;
			}
			set 
			{
				daysLeadTime = value;
			}
		}

		public int Term
		{
			get
			{
				return term;
			}
			set 
			{
				term = value;
			}
		}

		public int PublisherNumber
		{
			get
			{
				return publisherNumber;
			}
			set 
			{
				publisherNumber = value;
			}
		}

		public string Note
		{
			get
			{
				return note;
			}
			set 
			{
				note = value;
			}
		}

		public int VendorNumber
		{
			get
			{
				return vendorNumber;
			}
			set 
			{
				vendorNumber = value;
			}
		}

		public string VendorSiteName
		{
			get
			{
				return vendorSiteName;
			}
			set 
			{
				vendorSiteName = value;
			}
		}

		public string PayGroupLookUpCode
		{
			get
			{
				return payGroupLookUpCode;
			}
			set 
			{
				payGroupLookUpCode = value;
			}
		}

		public string Currency
		{
			get
			{
				return currency;
			}
			set 
			{
				currency = value;
			}
        }  

        public string VendorProductCode
        {
            get
            {
                return vendorProductCode;
            }
            set
            {
                vendorProductCode = value;
            }
        } 
	}
}
