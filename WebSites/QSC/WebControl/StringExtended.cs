using System;

namespace QSP.WebControl
{
	/// <summary>
	/// Custom string functions.
	/// </summary>
	public class StringExtended
	{
		private const string ACCENTS = "áàâäéèêëíìîïóòôöúùûüñÁÀÂÄÉÈÊËÍÌÎÏÓÒÔÖÚÙÛÜÑ";
		private const string NO_ACCENTS = "aaaaeeeeiiiioooouuuunAAAAEEEEIIIIOOOOUUUUN";

		public static string ReplaceAccents(string inputString) 
		{
			string outputString = inputString;

			for(int i = 0; i < ACCENTS.Length; i++) 
			{
				outputString = outputString.Replace(ACCENTS[i], NO_ACCENTS[i]);
			}

			return outputString;
		}
	}
}
