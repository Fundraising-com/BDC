using System;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for ProductTypeChangedArgs.
	/// </summary>
	public class ProductTypeChangedArgs : System.EventArgs
	{
		private ProductType productType;

		public ProductTypeChangedArgs(ProductType productType)
		{
			this.productType = productType;
		}
		
		public ProductType ProductType
		{
			get{return productType;}
		}
	}
}
