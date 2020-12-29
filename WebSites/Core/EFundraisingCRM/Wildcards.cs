using System;


namespace GA.BDC.Core.EFundraisingCRM
{
	/// <summary>
	/// Static class, inherits from Regex class, 
	/// helps to recognize wildcard characters 
	/// in the search string and apply 
	/// corresponding logic of wildcard characters
	/// and have an abilty(es)(thru overloading
	/// constructor) to add Regex' options.
	/// </summary>
	public class Wildcards:System.Text.RegularExpressions.Regex
	{
		
		public Wildcards(string searchPattern):base(ToRegex(searchPattern),System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace){}
		/*****Initialize the class with Regex options, can be several, divided by "|" character******/
		public Wildcards(string searchPattern, System.Text.RegularExpressions.RegexOptions op):base(ToRegex(searchPattern),op){}
		public static string ToRegex(string searchPattern)
		{
                      /***convert search string to regex-standarts string with respect of "*" and "?" charactes***/
                return "^" + System.Text.RegularExpressions.Regex.Escape(searchPattern).Replace("\\*", ".*").Replace("\\?", ".") + "$";
           
		}
	}

}
