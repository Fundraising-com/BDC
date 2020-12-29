using System;

namespace GA.BDC.Core.EFundraisingCRM 
{
	/*
	 * Object represents a collection of sales.  
	 * One client can have multiple sales.
	 * 
	 */
	public class PaymentCollection : EFundraisingCRMCollectionBase 
	{

		public PaymentCollection() 
		{
	
		}
		
		public void GetAllPayments() 
		{
			foreach(Payment p in Payment.GetPayments()) 
			{
				List.Add(p);
			}

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
			PaymentCollection copy =
				(PaymentCollection)EFundraisingCRMCollectionBase.SortWithComparable(this, comparer);
			ReplaceByCollection(copy);
		}
		
		#endregion
       
		#region Operators
		public static PaymentCollection operator +(PaymentCollection collection1, PaymentCollection collection2) 
		{
			return (PaymentCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static PaymentCollection operator +(PaymentCollection collection, Sale item) 
		{
			return (PaymentCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static PaymentCollection operator -(PaymentCollection collection1, PaymentCollection collection2) 
		{
			return (PaymentCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static PaymentCollection operator -(PaymentCollection collection, Sale item) 
		{
			return (PaymentCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion
	}
}
