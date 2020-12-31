using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Wraps a DataGridItem to allow it to represent a magazine item.
	/// </summary>
	[Serializable]
	public class ProductContract
	{
		private ProductContractID productContractID = null;
		private string productCode = String.Empty;
		private int year = 0;
		private string season = String.Empty;

		public ProductContract() { }

		public ProductContract(ProductContractID productContractID, string productCode, int year, string season) 
		{
			this.productContractID = productContractID;
			this.productCode = productCode;
			this.year = year;
			this.season = season;
		}

		public ProductContractID ProductContractID
		{
			get 
			{
				return productContractID;
			}
			set 
			{
				productContractID = value;
			}
		}

		public string Product_code 
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
	}
}
