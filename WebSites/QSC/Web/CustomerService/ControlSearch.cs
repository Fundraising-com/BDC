using System;
using QSPFulfillment.DataAccess;
using System.ComponentModel;
using QSPFulfillment.DataAccess.Business;



namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for CustomerServiceControl.
	/// </summary>
	/// 


	public class ControlSearch: CustomerServiceControl
	{		
		
		protected ParameterValueList pvl;
		protected ParameterValueList pvlFilter;
		public event SearchEventHandler SearchClicked;
		protected SearchMultiPage enumResult;
		private int mOrderItemType;
		


		public void btnSearch_Click(object sender,System.EventArgs e)
		{
			try
			{
				pvl = GetValueToSearch();
				pvlFilter = GetValueToSearchFilter();
			
				mOrderItemType = ItemType;
				FireEvent();
			}
			catch(QSPFulfillment.DataAccess.Common.ExceptionFulf)
			{
				this.Page.SetPageError();
			}

		}

		protected virtual ParameterValueList GetValueToSearch()
		{
			throw new Exception("Implemente GetValueToSearch()");
		}
		protected virtual ParameterValueList GetValueToSearchFilter()
		{
			return new ParameterValueList();
		}
				
		protected void FireEvent()
		{
			if(SearchClicked != null)
			{
				SearchClicked(this,new SearchClickedArgs(ResultType,pvl,pvlFilter,ItemType));
			}
		}
		public virtual SearchMultiPage ResultType
		{
			get
			{
				throw new Exception("Implemente ResultType");
				
			}
		}
		[Bindable(true),Category("Property"),DefaultValue("")]
		public ParameterValueList List
		{
			get
			{
				return pvl;
			}
		}
		[Bindable(true),Category("Property"),DefaultValue("")]
		public ParameterValueList Filter
		{
			get
			{
				return pvl;
			}
		}
		[Bindable(true),Category("Property"),DefaultValue("")]
		public virtual int ItemType
		{
			get{return mOrderItemType;}
			set{mOrderItemType = value;}
		}
	}

	


}
