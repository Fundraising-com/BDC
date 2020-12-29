using System;

namespace GA.BDC.Core.ESubsGlobal.Payment
{
	/// <summary>
	/// Summary description for PaymentExceptionTypeCollection.
	/// </summary>
	public class PaymentExceptionTypeCollection : GA.BDC.Core.Collections.BusinessCollectionBase
	{
		public PaymentExceptionTypeCollection()
		{
			
		}

		#region Basic Methods For Collection
		/// <summary>Get or set the object at specified index.</summary>
		/// 
		public PaymentExceptionType this[int index]
		{
			get { return (PaymentExceptionType) List[index]; }
			set { List[index] = value; }
		}

		/// <summary>Add object to collection.</summary>
		/// <param name="obj">Lead object.</param>
		/// <returns>Index of the newly added object.</returns>
		/// 
		public int Add(PaymentExceptionType obj)
		{
			return List.Add(obj);
		}

		/// <summary>Add collection to collection of objects.</summary>
		/// <param name="obj">PaymentExceptionTypeCollection object.</param>
		/// 
		public void Add(PaymentExceptionTypeCollection obj)
		{
			if (obj != null)
			{
				for (int i = 0; i < obj.Count; i++)
				{
					List.Add(obj[i]);
				}
			}
		}

		/// <summary>Remove object from collection.</summary>
		/// <param name="obj">PaymentExceptionType object.</param>
		/// 
		public void Remove(PaymentExceptionType obj)
		{
			List.Remove(obj);
		}

		/// <summary>Check if object is in collection.</summary>
		/// <param name="obj">PaymentExceptionType object</param>
		/// <returns>True if object is in collection, else false.</returns>
		/// 
		public bool Contains(PaymentExceptionType obj)
		{
			return List.Contains(obj);
		}

		/// <summary>Get the index associated with the object in collection.</summary>
		/// <param name="obj">PaymentExceptionType object.</param>
		/// <returns>The index of the object.</returns>
		/// 
		public int IndexOf(PaymentExceptionType obj)
		{
			return List.IndexOf(obj);
		}

		/// <summary>Insert object into collection at the specified index.</summary>
		/// <param name="index">The location to insert object.</param>
		/// <param name="obj">PaymentExceptionType object.</param>
		/// 
		public void Insert(int index, PaymentExceptionType obj) 
		{
			List.Insert(index, obj);
		}

		#endregion

		public void SortByPropertyName(string PropertyName)
		{
			System.Collections.IEnumerator theIEnumerator = null;
			theIEnumerator = GetEnumerator();
			while (theIEnumerator.MoveNext())
			{
            GA.BDC.Core.BusinessBase.BusinessBase obj = theIEnumerator.Current as GA.BDC.Core.BusinessBase.BusinessBase;
				if (obj != null)
					obj.sortByPropertyName = PropertyName;
				theIEnumerator.MoveNext();
			}
			Sort();			
		}
	}
}
