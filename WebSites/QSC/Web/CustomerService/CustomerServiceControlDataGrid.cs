using System;
using System.Data;
using System.Web.UI.WebControls;
using QSPFulfillment.DataAccess.Common;
using QSPFulfillment.DataAccess.Business;
using QSPFulfillment.DataAccess;
using System.ComponentModel;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for CustomerServicePageDataGrid.
	/// </summary>
	public class CustomerServiceControlDataGrid:CustomerServiceControl
	{
		
		private DataGridObject dtgMain;
		private bool mIsSelectOnly = true;
		private DataViewObject dtvMain;
		private int iNewIDInserted = 0;
		private Label lblMessage;
		
		public event System.EventHandler DataBound;
		

		public CustomerServiceControlDataGrid()
		{
			
		}
		#region Web Form Designer generated code
		protected void OnInit(EventArgs e,DataGridObject Grid,Label LabelMessage)
		{
			this.dtgMain = Grid;
			this.lblMessage = LabelMessage;
			//this.busSystem = Business;
			InitializeComponent();
			base.OnInit(e);							
		}
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.dtgMain.ItemCommand +=new DataGridCommandEventHandler(dtgMain_ItemCommand);
			this.dtgMain.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dtgMain_PageIndexChanged);
			this.dtgMain.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_CancelCommand);
			this.dtgMain.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgMain_EditCommand);			
			this.dtgMain.UpdateCommand += new DataGridCommandEventHandler(dtgMain_UpdateCommand);
		}
		#endregion
		private void dtgMain_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dtgMain.CurrentPageIndex = e.NewPageIndex;
			this.Page.NewSearch = false;
			this.dtgMain.SelectedIndex =-1;
            DataBind();
		}
		private void dtgMain_EditCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.dtgMain.EditItemIndex = e.Item.ItemIndex;
			this.dtgMain.ShowFooter = false;
			this.Page.NewSearch = false;
			DataBind();
		}

		private void dtgMain_CancelCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			this.dtgMain.EditItemIndex = -1;
			this.dtgMain.ShowFooter = true;
			DataBind();
		}
		private void dtgMain_UpdateCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				Update(e);
				this.dtgMain.EditItemIndex = -1;
				this.dtgMain.ShowFooter = true;
				DataBind();
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private void dtgMain_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if(e.CommandName == "Insert")
			{
				try
				{
					Insert(e);
					
					DataBind();
				}
				catch(ExceptionFulf ex)
				{
					this.Page.SetPageError(ex);
				}
			}
		}
		
		public bool IsSelectOnly
		{
			get
			{
				try
				{
					if(Request.QueryString["ID"] != null)
						return Convert.ToBoolean(Request.QueryString["ID"]);
					else
						return true;
				}
				catch
				{
					return true;
				}
			}
			set
			{
				mIsSelectOnly = value;
			}
		}

		protected virtual void OnDataBound(object sender, System.EventArgs args) 
		{
			if(DataBound != null) 
			{
				DataBound(sender, args);
			}
		}
		
		protected void Bind()
		{
			this.dtgMain.DataSource = GetView();
			if(NewIDInserted !=0)
				this.dtgMain.EnsureVisibility(dtvMain.GetPosition(this.dtgMain.DataKeyField,NewIDInserted));
			if(this.Page.NewSearch)
				this.dtgMain.CurrentPageIndex = 0;

			if(dtvMain.Table.Rows.Count !=0)
			{
				this.dtgMain.DataBind();
				this.lblMessage.Visible= false;
				this.dtgMain.Visible=true;
			}
			else
			{
				//Todo: Validate that the message work
				this.lblMessage.Text = Message;
				this.lblMessage.Visible= true;
				this.dtgMain.Visible= false;
			}
		}
		protected virtual void LoadData()
		{
		
		}
		public override void DataBind()
		{
			try
			{
				LoadData();
				Bind();
				
				OnDataBound(this, EventArgs.Empty);
			}
			catch(ExceptionFulf ex)
			{
				this.Page.SetPageError(ex);
			}
		}
		private DataViewObject GetView()
		{
			if(dtvMain == null)
				dtvMain = new DataViewObject();

			dtvMain.Table = DataSource;
			dtvMain.RowFilter = this.dtgMain.FilterExpression;
			dtvMain.Sort = this.dtgMain.SortExpression;
			
			return dtvMain;
		}
		protected virtual void Update(DataGridCommandEventArgs e)
		{
			
		}
		protected virtual void Insert(DataGridCommandEventArgs e)
		{
		
		}

		public ParameterValueList List
		{
			get{return (ParameterValueList)this.ViewState["ParameterValueList"];}
			set{this.ViewState["ParameterValueList"] = value;}
		}
		
		
		public int NewIDInserted
		{
			get{return iNewIDInserted;}
			set{iNewIDInserted = value;}
		}
		
		public string Message
		{
			get
			{
				return this.lblMessage.Text;
			}
			set
			{
				this.lblMessage.Text = value;
			}
		}
		
		
	}
}
	

