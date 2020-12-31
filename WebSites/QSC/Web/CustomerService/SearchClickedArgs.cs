using System;
using QSPFulfillment.DataAccess;
using QSPFulfillment.DataAccess.Business;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for SearchClickedEventHandler.
	/// </summary>
	/// 
	public delegate void SearchEventHandler(object sender, SearchClickedArgs e);
	
	public class SearchClickedArgs:EventArgs
	{
		private SearchMultiPage enumResult;
		private ParameterValueList pvlList;
		private ParameterValueList pvlFilter;
		private int oitType;
		
		public SearchClickedArgs(SearchMultiPage Value)
		{
			this.enumResult = Value;
		
		}
		public SearchClickedArgs(SearchMultiPage Value,ParameterValueList List)
		{
				this.enumResult = Value;
				this.pvlList = List;
		}
		public SearchClickedArgs(SearchMultiPage Value,ParameterValueList List,ParameterValueList Filter,int ItemType)
		{
			this.enumResult = Value;
			this.pvlList = List;
			this.pvlFilter =Filter;
			this.oitType = ItemType;
		}
		
		public SearchClickedArgs(ParameterValueList List)
		{
		
			this.pvlList = List;
		}
		public SearchMultiPage ResultType
		{
			get{return enumResult;}
		}
		public ParameterValueList List
		{
			get {return pvlList;}
		}
		public ParameterValueList Filter
		{
			get {return pvlFilter;}
		}
		public int ItemType
		{
			get{ return  oitType;}
		}
	}
}
