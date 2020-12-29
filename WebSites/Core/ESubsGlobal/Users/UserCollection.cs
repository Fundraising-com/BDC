using System;
using GA.BDC.Core.Collections;

namespace GA.BDC.Core.ESubsGlobal.Users {
	/// <summary>
	/// Summary description for UserCollection.
	/// </summary>
    [Serializable]
	public class UserCollection : BusinessCollectionBase {
		public UserCollection()	{

		}

		public static UserCollection operator +(UserCollection a, UserCollection b) {
			UserCollection newUserCollection = new UserCollection();
			
			foreach(eSubsGlobalUser user in a) {
				newUserCollection.Add(user);
			}

			foreach(eSubsGlobalUser user in b) {
				newUserCollection.Add(user);
			}

			return newUserCollection;
		}


		public void Add(eSubsGlobalUser user) {
			List.Add(user);
		}

		public void Sort(SortUserOrder order) {
			foreach(eSubsGlobalUser user in List) {
				user.SortOrder = order;
			}
			Sort();
		}

		public void Sort(SortUserBy sort) {
			foreach(eSubsGlobalUser user in List) {
				user.SortBy = sort;
			}

			Sort();
		}
	}
}
