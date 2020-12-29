using System;

namespace GA.BDC.Core.BarCode
{
	/// <summary>
	/// Summary description for Code39Table.
	/// </summary>
	public class Code39Table
	{
		private static Code39Table singletonInstance = null;
		// Hardcoded for now
		private char[] charTable;

		private Code39Table()
		{
			charTable = new char[43]
			{	'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
				'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
				'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T',
				'U', 'V', 'W', 'X', 'Y', 'Z', '-', '.', ' ', '$',
				'/', '+', '%'};
		}

		public static Code39Table Instance 
		{
			get 
			{
				if(singletonInstance == null) 
				{
					singletonInstance = new Code39Table();
				}

				return singletonInstance;
			}
		}

		public char GetCharacter(int value) 
		{
			return charTable[value];
		}

		public int GetCharacterValue(char code39Character) 
		{
			return Array.IndexOf(charTable, code39Character);
		}
	}
}
