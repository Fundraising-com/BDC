namespace QSPFulfillment.MarketingMgt.Control
{
	using System;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using QSPFulfillment.CustomerService;

	public delegate void SelectContactProductEventHandler(object sender, SelectContactProductClickedArgs e);
	
	/// <summary>
	///		Summary description for ControlMagazineTerm.
	/// </summary>
	public class ContactProductListControl : MarketingMgtControlDataGrid
	{
		private System.Web.UI.WebControls.Label lblMessage;
		private DataGridObject dtgMain;

		public event SelectContactProductEventHandler DeleteContactProductClicked;
		
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e, DataGridObject dataGridObject, Label message)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			dtgMain = dataGridObject;
			lblMessage = message;
			InitializeComponent();
			this.dtgMain.ItemCommand += new DataGridCommandEventHandler(dtgMain_ItemCommand);
			base.OnInit(e,dtgMain,lblMessage);

		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void dtgMain_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Delete")
			{
				try 
				{
					SelectContactProductClickedArgs args;
				
					args = new SelectContactProductClickedArgs(GetContactProductID(e.Item));
				
					OnDeleteContactProductClicked(source, args);
				} 
				catch(Exception ex) 
				{
					this.Page.ManageError(ex);
				}
			}
		}

		public int ContactID 
		{
			get 
			{
				int iContactID = 0;

				if(this.ViewState["ContactID"] != null) 
				{
					iContactID = Convert.ToInt32(this.ViewState["ContactID"]);
				}

				return iContactID;
			}
			set 
			{
				this.ViewState["ContactID"] = value;
			}
		}

		public int Count 
		{
			get 
			{
				return this.dtgMain.Items.Count;
			}
		}

		protected virtual void OnDeleteContactProductClicked(object sender, SelectContactProductClickedArgs e) 
		{
			if(DeleteContactProductClicked != null) 
			{
				DeleteContactProductClicked(sender, e);
			}
		}

		protected virtual int GetContactProductID(DataGridItem e) 
		{
			throw new NotImplementedException("GetContactProductID");
		}
	}
}