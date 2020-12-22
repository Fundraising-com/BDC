using System;

namespace QSP.WebControl
{
	/// <summary>
	/// Summary description for AlphaSearchClicked.
	/// </summary>
	public class AlphaSearchClickedArgs:EventArgs
	{

		private char myCharSelected;
		private AlphaSearchClickedArgs()
		{
		}
		internal AlphaSearchClickedArgs(char CharSelected)
		{
			myCharSelected = CharSelected;
		}
		/// <summary>
		/// Character selected in the AlphaSearch
		/// </summary>
		public char CharSelected
		{
			get
			{
				return myCharSelected;
			}
		}
	}
}
