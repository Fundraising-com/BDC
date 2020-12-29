using System;
using GA.BDC.Core.Diagnostics;

namespace GA.BDC.Core.Utilities.MatchingCodes
{
	/// <summary>
	/// Summary description for CompareAddresses.
	/// This class let the user compare 2 addresses to see if they match.
	/// </summary>
	public class CompareAddresses
	{
	
		public CompareAddresses()
		{
		
		}

		//This method recieve 2 addresses and return True if they are the same or false if they are not
		public static bool Match(string streetAddress1, string zip1, string streetAddress2, string zip2)
		{
			bool match = false;
			try
			{
				//no field can be empty
    			if (streetAddress1 == "" || zip1 == "" || streetAddress2 == "" || zip2 == "")
				{
					match = false;
				}
				else
				{
					string code1  = MatchingCodes.GetMatchingCode(streetAddress1, zip1); 
					string code2  = MatchingCodes.GetMatchingCode(streetAddress2, zip2); 
					if (code1 == code2 && code1 != "invalid")
					{
						match = true;
					}
					else
					{
						match = false;
					}
				}
			}
			catch(Exception ex)
			{
				Logger.LogError("Error in CompareAddresses.Match", ex);
			}
					
			return match;
		}

	}
}
