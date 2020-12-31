using System.Collections;

namespace QSPFulfillment.DataAccess.Common.ActionObject
{
	public class CatalystDataProductCollection : CollectionBase 
	{
		public void Add(CatalystDataProduct catalystDataProduct) 
		{
			this.List.Add(catalystDataProduct);
		}

		public void Remove(CatalystDataProduct catalystDataProduct) 
		{
			this.List.Remove(catalystDataProduct);
		}

		public CatalystDataProduct this[int index] 
		{
			get 
			{
				return (CatalystDataProduct) this.List[index];
			}
		}
	}
}