using System;
using Business.Objects;
using QSPFulfillment.Fulfillment.Control;

namespace QSPFulfillment.Fulfillment
{
	/// <summary>
	/// Argument for Select Season event handler.
	/// </summary>
	public class SelectSeasonClickedArgs:System.EventArgs
	{
		private Season season;

		public SelectSeasonClickedArgs(Season oSeason)
		{
			this.season = oSeason;
		}
		
		public Season Season
		{
			get{return this.season;}
		}
	}
}
