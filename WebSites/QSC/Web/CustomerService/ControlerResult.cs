using System;
using QSPFulfillment.DataAccess;
using System.Data;
using QSPFulfillment.DataAccess.Business;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace QSPFulfillment.CustomerService
{

	
	/// <summary>
	/// Summary description for ControlResult.
	/// </summary>
	public class ControlerResult : CustomerServiceControl
	{
		//protected static ParameterValueList pvlList;
		protected DataTable Table = new DataTable("ResultSearch");
		
		[Bindable(true),Category("Property"),DefaultValue("")]
		public ParameterValueList List
		{
			get
			{
				if(ViewState["ControlerResultPVL"] == null)
					return new ParameterValueList();
			
				return (ParameterValueList)this.ViewState["ControlerResultPVL"];
			}
			set
			{
				this.ViewState["ControlerResultPVL"] = value;
			}
		}
		[Bindable(true),Category("Property"),DefaultValue("")]
		public ParameterValueList Filter
		{
			get
			{
				if(ViewState["ControlerResultFilter"] == null)
					return new ParameterValueList();
			
				return (ParameterValueList)this.ViewState["ControlerResultFilter"];
			}
			set
			{
				this.ViewState["ControlerResultFilter"] = value;
			}
		}
		public int ItemType
		{
			get
			{
				if(ViewState["mOrderItemType"] == null)
					return (int)ProductType.Magazine;
			
				return (int)this.ViewState["mOrderItemType"];
			}
			set
			{
				this.ViewState["mOrderItemType"] = value;
			}
			
		}
		public DataTable ListToSearch
		{
			get
			{
				if(ViewState["ListToSearch"] == null)
					ViewState["ListToSearch"] =	new DataTable();
			
				return (DataTable)this.ViewState["ListToSearch"];
			}
			set
			{
				this.ViewState["ListToSearch"] = value;
			}
		}
	}
}
