using System;
using System.Collections;

namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Represent collection of rules
	/// that appled on collection of object
	/// to get the set of objects matching
	/// the set of rules
	/// </summary>
	public class FilterRulesCollection:CollectionBase
	{
		public int Add(FilterRule rule)
		{
			return List.Add(rule);
		}
		
	}
}
