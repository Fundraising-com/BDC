using System;
using QSPFulfillment.DataAccess.Common.ActionObject;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.MarketingMgt
{
	/// <summary>
	/// Summary description for SelectMagazineClickedArgs.
	/// </summary>
	public class SelectProductContractClickedArgs : System.EventArgs
	{
		private ProductContractID productContractID;

		public SelectProductContractClickedArgs(ProductContractID productContractID)
		{
			this.productContractID = productContractID;
		}
		
		public ProductContractID ProductContractID
		{
			get
			{
				return productContractID;
			}
		}
	}
}
