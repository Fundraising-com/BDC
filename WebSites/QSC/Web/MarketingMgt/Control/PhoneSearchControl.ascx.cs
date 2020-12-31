namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.DataAccess.Business;
	using QSPFulfillment.DataAccess.Common;
	using QSPFulfillment.CustomerService;
	
	public delegate void SelectPhoneEventHandler(object sender, SelectPhoneClickedArgs e);

	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class PhoneSearchControl : MarketingMgtControlDataGrid
	{
		protected System.Web.UI.WebControls.Label lblMessage;
		protected DataGridObject dtgMain;

		public event SelectPhoneEventHandler SelectPhoneClick;
		
		private void Page_Load(object sender, System.EventArgs e)
		{
			
		}
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dtgMain.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_ItemCommand);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			try 
			{
				if(e.CommandName == "Select")
				{
					SelectPhoneClickedArgs args;
				
					args = new SelectPhoneClickedArgs(new QSPFulfillment.DataAccess.Common.ActionObject.Phone(GetID(e.Item), GetType(e.Item), GetPhoneListID(e.Item), GetPhoneNumber(e.Item), GetBestTimeToCall(e.Item)));
				
					if(SelectPhoneClick != null)
						SelectPhoneClick(source,args);
				} 
				else if(e.CommandName == "Delete") 
				{
					this.Page.BusCatalog.DeletePhone(GetID(e.Item));
				}
			} 
			catch(Exception ex) 
			{
				this.Page.ManageError(ex);
			}
		}

		public int PhoneListID 
		{
			get 
			{
				if(this.ViewState["PhoneListID"] == null)
					this.ViewState["PhoneListID"] = 0;

				return Convert.ToInt32(this.ViewState["PhoneListID"]);
			}
			set 
			{
				this.ViewState["PhoneListID"] = value;
			}
		}

		protected override void LoadData()
		{
			DataSource = new DataTable("Phone");

			this.Page.BusCatalog.SelectAllPhones(DataSource, PhoneListID);
		}
		private int GetID(DataGridItem e) 
		{
			return Convert.ToInt32(((Label)e.FindControl("lblID")).Text);
		}
		private int GetPhoneListID(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblPhoneListID")).Text);
		}
		private string GetPhoneNumber(DataGridItem e)
		{
			return ((Label)e.FindControl("lblPhoneNumber")).Text;
		}
		private int GetType(DataGridItem e)
		{
			return Convert.ToInt32(((Label)e.FindControl("lblType")).Text);
		}
		private string GetBestTimeToCall(DataGridItem e)
		{
			return ((Label)e.FindControl("lblBestTimeToCall")).Text;
		}
	}
}