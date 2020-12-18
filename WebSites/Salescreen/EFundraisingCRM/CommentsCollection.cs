using System;

namespace efundraising.EFundraisingCRM
{
	/// <summary>
	/// Summary description for CommentsCollection.
	/// </summary>
	public class CommentsCollection : EFundraisingCRMCollectionBase 
	{
		public CommentsCollection()
		{
			
		}
       
		#region Operators
		public static CommentsCollection operator +(CommentsCollection collection1, CommentsCollection collection2) 
		{
			return (CommentsCollection)EFundraisingCRMCollectionBase.AddCollection(collection1, collection2);
		}

		public static CommentsCollection operator +(CommentsCollection collection, Comments item) 
		{
			return (CommentsCollection)EFundraisingCRMCollectionBase.AddItem(collection, item);
		}

		public static CommentsCollection operator -(CommentsCollection collection1, CommentsCollection collection2) 
		{
			return (CommentsCollection)EFundraisingCRMCollectionBase.RemoveCollection(collection1, collection2);
		}

		public static CommentsCollection operator -(CommentsCollection collection, Comments item) 
		{
			return (CommentsCollection)EFundraisingCRMCollectionBase.RemoveItem(collection, item);
		}
		#endregion

	}
}


