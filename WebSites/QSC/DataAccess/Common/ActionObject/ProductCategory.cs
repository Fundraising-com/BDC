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
	public class ProductCategory
	{
		private int iInstance = 0;
		private string sDescription = "";

		public ProductCategory()
		{
		}
		public ProductCategory(int Instance, string Description)
		{
			iInstance = Instance;
			sDescription = Description;
		}

		public int Instance
		{
			get
			{
				return iInstance;
			}
			set 
			{
				iInstance = value;
			}
		}
		public string Description
		{
			get
			{
				return sDescription;
			}
			set 
			{
				sDescription = value;
			}
		}								  
	}
}
