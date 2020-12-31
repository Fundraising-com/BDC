using System;
using System.Web.UI.WebControls;

namespace QSPFulfillment.CustomerService
{
	/// <summary>
	/// Summary description for DataGridObjectSelect.
	/// </summary>
	public class CustomerServiceDataGridObjectSelect : DataGridObject
	{
		bool bSelectedIndexFound = false;

		public CustomerServiceDataGridObjectSelect() : base()
		{
			this.PageIndexChanged +=new DataGridPageChangedEventHandler(CustomerServiceDataGridObjectSelect_PageIndexChanged);
			this.ItemCommand += new DataGridCommandEventHandler(CustomerServiceDataGridObjectSelect_ItemCommand);
			this.ItemDataBound += new DataGridItemEventHandler(CustomerServiceDataGridObjectSelect_ItemDataBound);
			this.PreRender += new EventHandler(CustomerServiceDataGridObjectSelect_PreRender);
		}

		private void CustomerServiceDataGridObjectSelect_PreRender(object sender, EventArgs e)
		{
			this.PageChanged = false;
		}

		private void CustomerServiceDataGridObjectSelect_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			this.CurrentPageIndex = e.NewPageIndex;
			this.SelectedIndex = -1;
			((CustomerServicePage) this.Page).NewSearch = false;
			this.PageChanged = true;
		}

		private void CustomerServiceDataGridObjectSelect_ItemCommand(object source, DataGridCommandEventArgs e)
		{
			if(e.CommandName == DataGrid.SelectCommandName)
			{
				this.SelectedKeyIndex = ((Label) e.Item.FindControl(IndexColumnName)).Text;
				((CustomerServicePage) this.Page).ResultSelected = false;
			}
		}

		public string IndexColumnName 
		{
			get 
			{
				if(this.ViewState["IndexColumnName"] == null)
					return "";

				return this.ViewState["IndexColumnName"].ToString();
			}
			set 
			{
				this.ViewState["IndexColumnName"] = value;
			}
		}

		public string SelectedKeyIndex 
		{
			get 
			{
				if(this.ViewState["SelectedKeyIndex"] == null)
					return "";

				return this.ViewState["SelectedKeyIndex"].ToString();
			}
			set 
			{
				this.ViewState["SelectedKeyIndex"] = value;
			}
		}

		public bool PageChanged 
		{
			get 
			{
				if(ViewState["PageChanged"] == null) 
				{
					return false;
				} 
				else 
				{
					return Convert.ToBoolean(ViewState["PageChanged"]);
				}
			}
			set 
			{
				ViewState["PageChanged"] = value;
			}
		}

		private void CustomerServiceDataGridObjectSelect_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if(((CustomerServicePage) this.Page).ResultSelected)
			{
				if(e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.SelectedItem)
				{
				
					if(this.SelectedKeyIndex == ((Label) e.Item.FindControl(IndexColumnName)).Text) 
					{
						if(!bSelectedIndexFound) 
						{
							bSelectedIndexFound = true;
							this.SelectedIndex = e.Item.ItemIndex;
							
							e.Item.BackColor = this.SelectedItemStyle.BackColor;
							e.Item.ForeColor = this.SelectedItemStyle.ForeColor;
							e.Item.Font.CopyFrom(this.SelectedItemStyle.Font);
						}
					}
				}
			}
		}

		public override void DataBind()
		{
			if(!((CustomerServicePage) this.Page).ResultSelected)
			{
				this.SelectedIndex	=-1;
			}

			if(((CustomerServicePage) this.Page).NewSearch || ((CustomerServicePage) this.Page).PageChanged) 
			{
				this.CurrentPageIndex = 0;
			}

			base.DataBind ();
		}
	}
}
