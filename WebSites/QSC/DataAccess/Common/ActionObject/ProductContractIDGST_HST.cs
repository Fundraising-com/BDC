using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductContractGST_HST.
	/// </summary>
	[Serializable]
	public class ProductContractIDGST_HST : ProductContractID
	{
		int magPriceInstanceGST = 0;
		int magPriceInstanceHST = 0;

		public ProductContractIDGST_HST() { }

		public ProductContractIDGST_HST(int magPriceInstanceGST, int magPriceInstanceHST) 
		{
			this.magPriceInstanceGST = magPriceInstanceGST;
			this.magPriceInstanceHST = magPriceInstanceHST;
		}

		public override int MagPriceInstanceGST
		{
			get
			{
				return magPriceInstanceGST;
			}
			set
			{
				magPriceInstanceGST = value;
			}
		}

		public override int MagPriceInstanceHST 
		{
			get 
			{
				return magPriceInstanceHST;
			}
			set 
			{
				magPriceInstanceHST = value;
			}
		}
	}
}
