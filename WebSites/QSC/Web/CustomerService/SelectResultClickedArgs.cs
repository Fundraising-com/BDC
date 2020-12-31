using System;
using QSPFulfillment.DataAccess;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for SearchClickedEventHandler.
	/// </summary>
	/// 
	public delegate void SelectResultEventHandler(object sender, SelectResultClickedArgs e);
	
	public class SelectResultClickedArgs:EventArgs
	{
		private CurrentOrderInfo CurrentOrderInfoM;
		private bool bIsMultipleSelection;	
	

		public SelectResultClickedArgs(CurrentOrderInfo Value,bool IsMutilpleSelection)
		{
				 CurrentOrderInfoM = Value;
				 bIsMultipleSelection = IsMutilpleSelection;
		}
		public SelectResultClickedArgs(bool IsMutilpleSelection)
		{
			
			bIsMultipleSelection = IsMutilpleSelection;
		}
		
		public CurrentOrderInfo OrderInfo
		{
			get
			{
				return CurrentOrderInfoM;
			}
		}
		public bool IsMultipleSelection
		{
			get{return bIsMultipleSelection;}
			
		}
		
			
		
	}
	
}
