using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for CatalystDataProduct.
	/// </summary>
	public class CatalystDataProduct
	{
		private string productCode = String.Empty;
		private int year = 0;
		private string season = String.Empty;
		private string initialRemitCode = String.Empty;
		private string enteredRemitCode = String.Empty;
		private string initialProductSortName = String.Empty;
		private string enteredProductSortName = String.Empty;
		private string initialVendorNumber = String.Empty;
		private string enteredVendorNumber = String.Empty;
		private string initialVendorSiteName = String.Empty;
		private string enteredVendorSiteName = String.Empty;
		private string initialPayGroupLookUpCode = String.Empty;
		private string enteredPayGroupLookUpCode = String.Empty;

		public CatalystDataProduct() { }

		public CatalystDataProduct(string productCode, int year, string season, string initialRemitCode, string enteredRemitCode, string initialProductSortName, string enteredProductSortName, string initialVendorNumber, string enteredVendorNumber, string initialVendorSiteName, string enteredVendorSiteName, string initialPayGroupLookUpCode, string enteredPayGroupLookUpCode) 
		{
			this.productCode = productCode;
			this.year = year;
			this.season = season;
			this.initialRemitCode = initialRemitCode;
			this.enteredRemitCode = enteredRemitCode;
			this.initialProductSortName = initialProductSortName;
			this.enteredProductSortName = enteredProductSortName;
			this.initialVendorNumber = initialVendorNumber;
			this.enteredVendorNumber = enteredVendorNumber;
			this.initialVendorSiteName = initialVendorSiteName;
			this.enteredVendorSiteName = enteredVendorSiteName;
			this.initialPayGroupLookUpCode = initialPayGroupLookUpCode;
			this.enteredPayGroupLookUpCode = enteredPayGroupLookUpCode;
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

		public string InitialRemitCode 
		{
			get 
			{
				return initialRemitCode;
			}
			set 
			{
				initialRemitCode = value;
			}
		}

		public string EnteredRemitCode 
		{
			get 
			{
				return enteredRemitCode;
			}
			set 
			{
				enteredRemitCode = value;
			}
		}

		public string InitialProductSortName 
		{
			get 
			{
				return initialProductSortName;
			}
			set 
			{
				initialProductSortName = value;
			}
		}

		public string EnteredProductSortName 
		{
			get 
			{
				return enteredProductSortName;
			}
			set 
			{
				enteredProductSortName = value;
			}
		}

		public string InitialVendorNumber 
		{
			get 
			{
				return initialVendorNumber;
			}
			set 
			{
				initialVendorNumber = value;
			}
		}

		public string EnteredVendorNumber 
		{
			get 
			{
				return enteredVendorNumber;
			}
			set 
			{
				enteredVendorNumber = value;
			}
		}

		public string InitialVendorSiteName 
		{
			get 
			{
				return initialVendorSiteName;
			}
			set 
			{
				initialVendorSiteName = value;
			}
		}

		public string EnteredVendorSiteName 
		{
			get 
			{
				return enteredVendorSiteName;
			}
			set 
			{
				enteredVendorSiteName = value;
			}
		}

		public string InitialPayGroupLookUpCode 
		{
			get 
			{
				return initialPayGroupLookUpCode;
			}
			set 
			{
				initialPayGroupLookUpCode = value;
			}
		}

		public string EnteredPayGroupLookUpCode 
		{
			get 
			{
				return enteredPayGroupLookUpCode;
			}
			set 
			{
				enteredPayGroupLookUpCode = value;
			}
		}
	}
}
