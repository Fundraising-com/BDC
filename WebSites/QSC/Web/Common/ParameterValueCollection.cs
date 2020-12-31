// Created by:	Benoit Nadon
// Date:		2005-10-03

using System;
using System.Collections;

namespace QSPFulfillment.CommonWeb
{
	/// <summary>
	/// ParameterValueCollection is a typed collection that handles object of type
	/// ParameterValue.
	/// </summary>
	[Serializable]
	public class ParameterValueCollection : CollectionBase 
	{
		public void Add(ParameterValue parameterValue) 
		{
			this.List.Add(parameterValue);
		}

		public void Remove(ParameterValue parameterValue) 
		{
			this.List.Remove(parameterValue);
		}

		public ParameterValue this[int index] 
		{
			get 
			{
				return (ParameterValue) this.List[index];
			}
		}
	}
}