using System;

namespace QSPFulfillment.OrderMgt
{
	/// <summary>
	/// Summary description for SearchClickedEventHandler.
	/// </summary>
	/// 
	public delegate void SearchEventHandler(object sender, SearchClickedArgs e);
	
	public class SearchClickedArgs:EventArgs
	{
		DateTime dFrom;
		DateTime dTo;

		public SearchClickedArgs(DateTime From,DateTime To)
		{
			dFrom = From;
			dTo = To;
		}
		public DateTime From
		{
			get{return dFrom;}
		}
		public DateTime To
		{
			get{return dTo;}
		}
	}
}
