using System;
using System.Reflection;
using efundraising.Core;
using efundraising.EFundraisingCRM;

namespace efundraising.EFundraisingCRM
{
	#region Enumerators
		public enum Operator{AND, OR}
	#endregion
	/// <summary>
	/// <list type="number">
	///<listheader><description>Main Logic class for CRM View System
	/// Class takes collection of Rules
	/// and applies 'em on collection
	/// of CRM object in two ways:</description></listheader>
	/// <item><description>By reference - initilial collection
	/// get affected (collection gets overwritten 
	/// by the result set).</description></item>
	/// <item><description>By Value - initial collection stays
	/// untouched by collection rules, the result
	/// set is wriiten into new output collection.</description></item>
	/// </list>
	/// </summary>
	/// <example>
	/// <code>
	/// // initialize input collection, fill collection with elements:
	/// CRMCollectionBase yourCollection = new CRMCollection().GetAllElements;
	/// // create a new instance of ViewFilter class for collection of yourCollectionType
	/// // that consists of objects of yourObjectType type: 
	/// ViewsFilter vf = new ViewsFilter(typeof (yourObjectType),typeof(yourCollectionType));
	/// // set and/or operator for collection of rules:
	/// f.AndOr = Operator.OR;
	/// // add a filter to a collection of rules:
	/// // on-the-fly we initialize a filter with name of the property of the object
	/// search string, true for case sensetive and rule operand as "not":
	/// f.AddFilter(new FilterRule(youtClassPropertyName,search string,true,Negator.NOT));
	/// // applying rule on collection of crmObjects:
	/// vf.ExecuteFilterByRef(yourCollection);
	/// </code>
	///</example>
	///<exception cref="System.MemberAccessException">
	/// Arise when your tries to initilaze collection from an abstruct class
	///</exception>
	///<exception cref="System.InvalidCastException">
	/// Watch that the type your provide for object matches with type of objects in the CRM Collection
	///</exception>
	///<exception cref="System.NullReferenceException">
	/// Watch that the type your provide for object matches with type of objects in the CRM Collection
	///</exception>
	sealed public class ViewsFilter:BusinessBase
	{
		
		#region Private Variables
		/// <summary>
		/// Specifies the object type in input collection
		/// </summary>
		private Type objectType;
		/// <summary>
		/// Specifies input collection type
		/// </summary>
		private Type collectionType;
		/// <summary>
		/// <list type="number">
		/// <listheader><description>Specifies the logical operation between rules:</description></listheader>
		/// <item><description>AND(default) - to be in result set object must
		/// match all rules in FilterRulesCollection</description></item>
		/// <item><description>OR - to be in result set object must
		/// match at least rules in FilterRulesCollection</description></item>
		/// </list>
		/// </summary>
		private Operator andOr = Operator.AND;
		/// <summary>
		/// Collection of rules that applies on CrmCollection
		/// </summary>
		private FilterRulesCollection rules;
		/// <summary>
		/// Collection of CRMObjects needed for internal manipulation
		/// </summary>
		private EFundraisingCRMCollectionBase outputCollection;
		#endregion

		#region Constructors
		public ViewsFilter( Type _objectType, Type _collectionType)
		{

			rules = new FilterRulesCollection();
			objectType = _objectType;
			collectionType = _collectionType;
			outputCollection = new ViewCollection();
		
			try
			{
				outputCollection = (EFundraisingCRMCollectionBase) Activator.CreateInstance(collectionType);
			}
			catch(System.MemberAccessException)
			{
				throw new EFundraisingCRMException("You have to specify CRM collection which is not abstruct.");
			}
			catch(System.InvalidCastException)
			{
				throw new EFundraisingCRMException("The type of object specified for collection does not match the object type in collection provided.");
			}
		
		}
		#endregion
		#region Private Functions 
		#region Helper Private Functions

		#endregion
		/// <summary>
		/// Function does actual matching of object against the single rule based on options specified
		/// in Wildcard object
		/// </summary>
		/// <param name="o"> object on which actual matching operation performed <seealso cref="EFundraisingCRMObject"/></param>
		/// <param name="w">class that contains parameters defining how matching is performed <seealso cref="Wildcards"/></param>
		/// <param name="f">structural representaion of rule against which the actual matching is peformed<seealso cref="Wildcards"/></param>
		/// <returns></returns>
		private bool Match(EFundraisingCRMObject o,Wildcards w,FilterRule f)
		{
			if(f.LikeNot == Negator.LIKE)
			{
				try
				{
					return w.IsMatch(objectType.GetProperty(f.RuleName).GetValue(o,null).ToString());
				}
				catch(System.NullReferenceException)
				{
					throw new EFundraisingCRMException("Property provided for search does not exist in the given object type (CASE SENSETIVE?!) or the object type provided does not exist in given collection.");
				}
			}
			/***compliment of the rule (case "not")****/
			else 
			{
				try
				{
					return !w.IsMatch(objectType.GetProperty(f.RuleName).GetValue(o,null).ToString());
				}
				catch(System.NullReferenceException)
				{
					throw new EFundraisingCRMException("Property provided for search does not exist in the given object type (CASE SENSETIVE?!) or the object type provided does not exist in given collection.");
				}
			}
		}
		#endregion
		#region Public Functions
		/// <summary>
		/// Adds a single rule to collection of rules
		/// </summary>
		/// <param name="input">Object representaion of rule that applies on CRMObjectCollection<seealso cref="FilterRule"/></param>
		public void AddFilter(FilterRule input)
		{
			rules.Add(input);
		}
		/// <summary>
		/// Applies collection of rules on collection of CRMObjects (main logic of the class is implemented here)
		/// input collection stays untouched --> logic of function similiar to access of struct by value
		/// </summary>
		/// <param name="inputCollection">collection of object on which the collection of rules will be applied<seealso cref="EFundraisingCRMCollectionBase"/></param>
		/// <returns>return result set - collection of CRM objects that had satisfied the rules<seealso cref="EFundraisingCRMCollectionBase"/></returns>
		public EFundraisingCRMCollectionBase ExecuteFilterByVal(EFundraisingCRMCollectionBase inputCollection)
		{
			/*** if input collection of rules contains no rules we do depth copy of input collection and return new collection***/
			if(rules.Count == 0)
			{
				return (EFundraisingCRMCollectionBase)inputCollection.Clone();
			}
			else
			{
				/**** if logical relation between rules is "or" (object in collection must satisfy at least one rule in order to be in output collection) ***/
				if(andOr== Operator.OR)
				{
					foreach( EFundraisingCRMDataObject o in inputCollection)
					{
						
						foreach(FilterRule f in rules)
						{
							Wildcards searchString = null;
							/*** if capital letters are an issue we call regular constructor ***/
							if(f.CaseSensetive)
							{
								searchString = new Wildcards(f.RuleValue);
							}
						
							/*** if capital letters are not important for search we call rconstructor for WildCard object with .IgnoreCase option***/
							else
							{
								searchString = new Wildcards(f.RuleValue,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							}
							if(this.Match(o,searchString,f))
							{
								/***if object passed the comparison against the single rule we check if object is already in output collection**/
								/*** if not, we add object to output collection  **********************************************************************/                 
								if(!outputCollection.Contains(o))
								{
									outputCollection.Add(o);
								}
							}
						}
					}
				}
				else
				{
					/*************** If logical operand for collection is "and" (in order to be in output collection object must satisfy all rule in rules collection ***/
					foreach( EFundraisingCRMDataObject o in inputCollection)
					{
						/*** count how many rules the current object satisfied***/
						int ruleMatchCounter = 0;
						foreach(FilterRule f in rules)
						{
							Wildcards searchString = null;
							/*** if capital letters are an issue we call regular constructor ***/
							if(f.CaseSensetive)
							{
								searchString = new Wildcards(f.RuleValue);
							}
							/*** if capital letters are not important for search we call rconstructor for WildCard object with .IgnoreCase option***/
							else
							{
								searchString = new Wildcards(f.RuleValue,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							}
							if(this.Match(o,searchString,f))
							{
								/*** if object pass the rule, we increase counter ***/
								ruleMatchCounter++;
							}
						}
						/*** if object passed all rules in collection of rules ***/
						if(ruleMatchCounter == rules.Count)
						{
							/***if object passed the comparison against the collection rule we check if object is already in output collection**/
							/*** if not, we add object to output collection  **********************************************************************/   
							if(!outputCollection.Contains(o))
							{
								outputCollection.Add(o);
							}
						}
					}
				}
				return (EFundraisingCRMCollectionBase)outputCollection;
			}
			
		}
		/// <summary>
		/// Applies collection of rules on collection of CRMObjects (main logic of the class is implemented here)
		/// input collection gets overwritten by the result set --> logic of function similiar to access of struct by reference
		/// </summary>
		/// <param name="inputCollection">collection of object on which the collection of rules will be applied(gets overwiiten by output collection<seealso cref="EFundraisingCRMCollectionBase"/></param>
		public void ExecuteFilterByRef(EFundraisingCRMCollectionBase inputCollection)
		{
			/*** if set of rules is empty we do nothing ***/
			if(rules.Count == 0)
			{
			}
			else
			{
				/**** if logical relation between rules is "or" (object in collection must satisfy at least one rule in order to be in output collection) ***/
				if(andOr== Operator.OR)
				{
					foreach( EFundraisingCRMDataObject o in inputCollection)
					{
						
						foreach(FilterRule f in rules)
						{
							Wildcards searchString = null;
							/*** if capital letters are an issue we call regular constructor ***/
							if(f.CaseSensetive)
							{
								searchString = new Wildcards(f.RuleValue);
							}
							/*** if capital letters are not important for search we call rconstructor for WildCard object with .IgnoreCase option***/
							else
							{
								searchString = new Wildcards(f.RuleValue,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							}
							if(this.Match(o,searchString,f))
							{
								/***if object passed the comparison against the single rule we check if object is already in output collection**/
								/*** if not, we add object to output collection  **********************************************************************/ 
								if(!outputCollection.Contains(o))
								{
									outputCollection.Add(o);
								}
							}
						}
					}
				}
				else
				{
					/*************** If logical operand for collection is "and" (in order to be in output collection object must satisfy all rule in rules collection ***/
					foreach( EFundraisingCRMDataObject o in inputCollection)
					{
						int ruleMatchCounter = 0;
						foreach(FilterRule f in rules)
						{
							Wildcards searchString = null;
							/*** if capital letters are an issue we call regular constructor ***/
							if(f.CaseSensetive)
							{
								searchString = new Wildcards(f.RuleValue);
							}
							/*** if capital letters are not important for search we call rconstructor for WildCard object with .IgnoreCase option***/
							else
							{
								searchString = new Wildcards(f.RuleValue,System.Text.RegularExpressions.RegexOptions.IgnoreCase);
							}
							if(this.Match(o,searchString,f))
							{
								ruleMatchCounter++;
							}
						}
						if(ruleMatchCounter == rules.Count)
						{
							/***if object passed the comparison against the single rule we check if object is already in output collection**/
							/*** if not, we add object to output collection  **********************************************************************/ 
							if(!outputCollection.Contains(o))
							{
								outputCollection.Add(o);
							}
						}
					}
				
				}
				/*** We are done with matching by now, so we are erasing input collection ***/
				inputCollection.Clear();
				/*** We overwrite input collection with output collection thru shallow copy ***/
				foreach(EFundraisingCRMDataObject o in outputCollection)
				{
					inputCollection.Add(o.ShallowCopy());
				}
				
			}
			
		}
		#endregion
		#region Public Properties
		public Operator  AndOr
		{
			get{ return andOr;}
			set{ andOr = value;}
		}
		#endregion

	}
}
