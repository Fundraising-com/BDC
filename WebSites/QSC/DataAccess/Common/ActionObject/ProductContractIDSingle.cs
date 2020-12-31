using System;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	/// <summary>
	/// Summary description for ProductContractIDSingle.
	/// </summary>
	[Serializable]
	public class ProductContractIDSingle : ProductContractID
	{
		private int magPriceInstance = 0;

		public ProductContractIDSingle() { }

		public ProductContractIDSingle(int magPriceInstance) : this()
		{
			this.magPriceInstance = magPriceInstance;
		}

		public override int MagPriceInstanceGST
		{
			get
			{
				return magPriceInstance;
			}
			set
			{
				magPriceInstance = value;
			}
		}

		public override int MagPriceInstanceHST
		{
			get
			{
				return ProductContractID.EmptyValue;
			}
			set { }
		}
	}
}
