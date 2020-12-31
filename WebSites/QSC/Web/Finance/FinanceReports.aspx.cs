using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace QSPFulfillment.Finance
{
	///<summary>FinanceReports</summary>
	public class FinanceReports : QSPFulfillment.CommonWeb.QSPPage
	{
		#region Item Declarations
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox tbCriteria;
		protected System.Web.UI.WebControls.DropDownList ddlbSearchBy;
		protected System.Web.UI.WebControls.Button Button1;
		protected System.Web.UI.HtmlControls.HtmlForm Form1;
		#endregion Item Declarations

		#region Page Initialization
		///<summary>Required method for Designer support</summary>
		override protected void OnInit(EventArgs e)
		{
			InitializeComponent();
			base.OnInit(e);
		}

		///<summary>Required method for Designer support</summary>
		private void InitializeComponent()
		{
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);
		}

		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!IsPostBack) 
			{
				PopulateDataGrid();
			}
		}
		#endregion Page Initialization

		#region Data Grid
		private void PopulateDataGrid()
		{
			Business.Batch oBatch = new Business.Batch();
			
			if (ddlbSearchBy.SelectedValue == "1") 
			{
				//Order ID
				if (tbCriteria.Text + "" != "") 
				{
					DataGrid1.DataSource = oBatch.GetBatchByOrderId(Convert.ToInt32(tbCriteria.Text));
					DataGrid1.DataBind();
					SetVisibleColumns();
				}
			}
			else if (ddlbSearchBy.SelectedValue == "2") 
			{
				//Campaign ID
				if (tbCriteria.Text + "" != "") 
				{
					DataGrid1.DataSource = oBatch.GetBatchesByCampaignID(Convert.ToInt32(tbCriteria.Text), this.GetFMID());
					DataGrid1.DataBind();
					SetVisibleColumns();
				}
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			PopulateDataGrid();
		}
		#endregion Data Grid
	}
}
