using System;
using System.Data;

namespace QSP.WebControl.DataAccess.Data
{
	/// <summary>
	/// Summary description for TransactionItem.
	/// </summary>
	internal class TransactionItems:System.Collections.CollectionBase,System.Collections.IEnumerable
	{
		public TransactionItems()
		{
	
		}
		public int Add(TransactionItem Value)
		{
			return(List.Add(Value));
		}
		public bool Contains(TransactionItem value )  
		{
			return(List.Contains( value ) );
		}
		public int IndexOf(TransactionItem Value )
		{
			return( List.IndexOf( Value ) );
		}
		public void Insert(int index,TransactionItem value )  
		{
			List.Insert( index, value );
		}
		public void Remove(TransactionItem value ) 
		{
			List.Remove(value);
		}
		public TransactionItem this[int Index]
		{
			get{return( (TransactionItem) List[Index] );}
			set{List[Index] = value;}
		}
	}
}