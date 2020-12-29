using System;

namespace GA.BDC.Core.StoreJunction
{
	/// <summary>
	/// Summary description for GoToStore.
	/// </summary>
	public abstract class GoToStore {

		public GoToStore() {

		}

		public abstract string BuildStoreUrl();
	}
}
