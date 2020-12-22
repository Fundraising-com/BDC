using System;
using System.Reflection;
using efundraising.Core;
using efundraising.efundraisingCore;
using efundraising.EFundraisingCRM;

namespace efundraising.EFundraisingCRM
{
	
	#region Enumerators
	/// <summary>
	/// Negator enumerator is used for Filter Rule
	/// to set whether the rule should act as "LIKE"
	/// or as "Not" clause.
	/// </summary>
	public enum Negator{LIKE, NOT}
	
	#endregion
	/// <summary>
	/// The Class represents a single rule that will
	/// be applied on CRM Collection
	/// </summary>
	public class FilterRule:BusinessBase
	{
		#region Private Variables
		/// <summary>
		/// Name of the rule should match
		/// the Property of the particular 
		/// object in the collection
		/// </summary>
		private string ruleName = null;
		/// <summary>
		/// Value represents the value(search string)
		/// for particular property in the collection 
		/// of objects
		/// </summary>
		private string ruleValue = null;
		/// <summary>
		/// caseSensetive defines how the rule will
		/// be apply regarding/disregarding capital letters
		/// in the search string (default disregard capital
		/// letters)
		/// </summary>
		private bool caseSensetive = false;
		/// <summary>
		/// Negator enumerator defines NOT or LIKE clause
		/// for each rule (default LIKE)
		/// </summary>
		private Negator likeNot = Negator.LIKE;
		#endregion

		#region Constructors
		public FilterRule(string name, string searchValue)
		{
			this.ruleName = name;
			this.ruleValue = searchValue;
		}
		public FilterRule(string name, string searchValue,bool caseSens):this(name,searchValue)
		{
			this.caseSensetive = caseSens;
		}
		public FilterRule(string name, string searchValue,Negator neg):this(name,searchValue)
		{
			this.likeNot = neg;
		}
		
		public FilterRule(string name, string searchValue,bool caseSens,Negator neg):this(name,searchValue,caseSens)
		{
			this.likeNot = neg;
		}
		public FilterRule(string name, string searchValue,Negator neg, bool caseSens):this (name,searchValue, neg)
		{
			this.caseSensetive = caseSens;
		}
		#endregion

		#region Public Properties
		public string RuleName
		{
			get{return ruleName;}
			set{ruleName = value;}
		}
		public string RuleValue
		{
			get{return ruleValue;}
			set{ruleValue = value;}
		}
		
		public bool CaseSensetive
		{
			get{ return caseSensetive;}
			set{caseSensetive = value;}
		}
		public Negator LikeNot
		{
			get{return likeNot;}
			set{likeNot = value;}
		}
		#endregion
        

	}
}
