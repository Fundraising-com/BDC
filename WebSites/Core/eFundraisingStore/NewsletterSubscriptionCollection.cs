using System;

namespace GA.BDC.Core.eFundraisingStore 
{
	
	/*
	 * Object represents a collection of NewsletterSubcriptions.  
	 * One client can have multiple NewsletterSubcriptions, for each partners.
	 * 
	 */
	public class NewsletterSubscriptionCollection : eFundraisingStoreCollectionBase
	{
		public NewsletterSubscriptionCollection() 
		{
			
		}

		

		#region Comparable Methods
		// sort the collection list using the default sort argument of
		// the default one.
		public void Sort() 
		{
			// sort the collection
			SortProcess();
		}

		// sort the collection list using a custom comparer
		public void Sort(System.Collections.IComparer comparer) 
		{
			NewsletterSubscriptionCollection copy =
				(NewsletterSubscriptionCollection)eFundraisingStoreCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static NewsletterSubscriptionCollection operator +(NewsletterSubscriptionCollection collection1, NewsletterSubscriptionCollection collection2) 
		{
			return (NewsletterSubscriptionCollection)eFundraisingStoreCollectionBase.AddCollection(collection1, collection2);
		}

		public static NewsletterSubscriptionCollection operator +(NewsletterSubscriptionCollection collection, NewsletterSubscription item) 
		{
			return (NewsletterSubscriptionCollection)eFundraisingStoreCollectionBase.AddItem(collection, item);
		}

		public static NewsletterSubscriptionCollection operator -(NewsletterSubscriptionCollection collection1, NewsletterSubscriptionCollection collection2) 
		{
			return (NewsletterSubscriptionCollection)eFundraisingStoreCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static NewsletterSubscriptionCollection operator -(NewsletterSubscriptionCollection collection, NewsletterSubscription item) 
		{
			return (NewsletterSubscriptionCollection)eFundraisingStoreCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
