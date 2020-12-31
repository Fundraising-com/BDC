using System;
using System.Runtime.Serialization;

namespace QSPFulfillment.DataAccess
{
	/// <summary>
	/// Summary description for ParameterValueList.
	/// </summary>
	/// 
	[Serializable()] 
	public class ParameterValueList:System.Collections.CollectionBase
	{

		public ParameterValueList()
		{
		
		}
		public ParameterValue this[int Index]
		{
			get{return( (ParameterValue) List[Index] );}
			set{List[Index] = value;}
		}

		public int Add(ParameterValue Value)
		{
			return(List.Add(Value));
		}
		public int IndexOf(ParameterValue Value )  
		{
			return( List.IndexOf( Value ) );
		}

		public void Insert(int index,ParameterValue value )  
		{
			List.Insert( index, value );
		}

		public void Remove(ParameterValue value )  
		{
			List.Remove(value);
		}

		public bool Contains(ParameterValue value )  
		{
			return(List.Contains( value ) );
		}
		public void Merge(ParameterValueList List)
		{
			foreach(ParameterValue pv in List)
			{
				this.Add(pv);
			}
		}
		


	}
}

