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
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.OrderMgt
{
	///<summary>OrderReports</summary>
	public class OrderReports : QSPFulfillment.CommonWeb.QSPPage
	{
		#region Item Declarations
		protected System.Web.UI.WebControls.DataGrid DataGrid1;
		protected System.Web.UI.WebControls.TextBox tbCriteria;
		protected System.Web.UI.WebControls.DropDownList ddlbSearchBy;
		protected System.Web.UI.WebControls.Button Button1;
        protected System.Web.UI.WebControls.DataGrid dgQSPCAList;
        
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
            this.DataGrid1.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.DataGrid1_ItemDataBound);
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
            Business.Report oReport = new Business.Report();
			
			if (ddlbSearchBy.SelectedValue == "1") 
			{
				//Order ID
				if (tbCriteria.Text + "" != "") 
				{
					DataGrid1.DataSource = oBatch.GetBatchByOrderId(Convert.ToInt32(tbCriteria.Text), this.GetFMID());
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

					// TEMPORARILY REMOVED UNTIL NEW REPORTS ARE UP
                    //dgQSPCAList.DataSource = oReport.GetQSPCAReportByCampaignID(Convert.ToInt32(tbCriteria.Text), this.GetFMID());
                    //dgQSPCAList.DataBind();
				}
			}
            else if (ddlbSearchBy.SelectedValue == "3") 
            {
                //Campaign ID
                if (tbCriteria.Text + "" != "") 
                {

                    DataGrid1.DataSource = oBatch.GetBatchesByAccountID(Convert.ToInt32(tbCriteria.Text), this.GetFMID());
                    DataGrid1.DataBind();
                    SetVisibleColumns();

					// TEMPORARILY REMOVED UNTIL NEW REPORTS ARE UP
                    //dgQSPCAList.DataSource = oReport.GetQSPCAReportByAccountID(Convert.ToInt32(tbCriteria.Text), this.GetFMID());
                    //dgQSPCAList.DataBind();
                }
            }
            else if (ddlbSearchBy.SelectedValue == "4") 
            {
                //Campaign ID
                if (tbCriteria.Text + "" != "") 
                {

                    DataGrid1.DataSource = oBatch.GetBatchesByAccountName(tbCriteria.Text, this.GetFMID());
                    DataGrid1.DataBind();
                    SetVisibleColumns();

                    // TEMPORARILY REMOVED UNTIL NEW REPORTS ARE UP
                    //dgQSPCAList.DataSource = oReport.GetQSPCAReportByAccountName(tbCriteria.Text, this.GetFMID());
                    //dgQSPCAList.DataBind();
                }
            }
		}

		private void DataGrid1_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			ParameterValueCollection parameterValues;
			ParameterValue parameterValue;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				Label     lbLanguage   = (Label)     e.Item.FindControl("lbLanguage");
				Literal   litOrderID   = (Literal)   e.Item.FindControl("ltOrderID");
				Literal   litBatchID   = (Literal)   e.Item.FindControl("ltBatchID");
				Literal   litBatchDate = (Literal)   e.Item.FindControl("ltBatchDate");
				
				RSGenerationLinkButton rsGenerationGroupSummaryReport = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationGroupSummaryReport");

				if(rsGenerationGroupSummaryReport != null)
				{
					rsGenerationGroupSummaryReport.Mode = FilePageMode.Internal;
					rsGenerationGroupSummaryReport.ReportName = "GroupSummaryReport";

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "OrderId";
					parameterValue.Value = litOrderID.Text;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "BatchId";
					parameterValue.Value = litBatchID.Text;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "BatchDate";
					parameterValue.Value = litBatchDate.Text;
					parameterValues.Add(parameterValue);

					rsGenerationGroupSummaryReport.ParameterValues = parameterValues;
				}

				RSGenerationLinkButton rsGenerationHomeroomSummaryReport = (RSGenerationLinkButton) e.Item.FindControl("rsGenerationHomeroomSummaryReport");

				if(rsGenerationHomeroomSummaryReport != null)
				{
					rsGenerationHomeroomSummaryReport.Mode = FilePageMode.Internal;
					rsGenerationHomeroomSummaryReport.ReportName = "HomeroomSummaryReport";

					parameterValues = new ParameterValueCollection();

					parameterValue = new ParameterValue();
					parameterValue.Name = "OrderId";
					parameterValue.Value = litOrderID.Text;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "BatchId";
					parameterValue.Value = litBatchID.Text;
					parameterValues.Add(parameterValue);

					parameterValue = new ParameterValue();
					parameterValue.Name = "BatchDate";
					parameterValue.Value = litBatchDate.Text;
					parameterValues.Add(parameterValue);

					rsGenerationHomeroomSummaryReport.ParameterValues = parameterValues;
				}
			}
		}

		private void SetVisibleColumns()
		{
//for when page serves users with and without the OrderMgt role
//not yet implemented
//			if(aUserProfile.HasRole("HomeOffice"))
//			{
//				this.DataGrid1.Columns[5].Visible = true;
//				this.DataGrid1.Columns[6].Visible = true;
//				this.DataGrid1.Columns[7].Visible = true;//false;
//				this.DataGrid1.Columns[8].Visible = true;//false;
//			}
//			else
//			{
//				this.DataGrid1.Columns[5].Visible = false;
//				this.DataGrid1.Columns[6].Visible = false;
//				this.DataGrid1.Columns[7].Visible = true;
//				this.DataGrid1.Columns[8].Visible = true;
//			}
		}

		///<summary>determines what FMID to qualify the search with</summary>
		///<returns>4 character FMID string</returns>
		private string GetFMID()
		{
			if(aUserProfile.HasRole("HomeOffice"))
			{
				return "9999";
			}
			else
			{
				return aUserProfile.FMID ;
			}
		}

		private void Button1_Click(object sender, System.EventArgs e)
		{
			PopulateDataGrid();
		}
		#endregion Data Grid

        private void DataGrid2_SelectedIndexChanged(object sender, System.EventArgs e)
        {
        
        }
	}
}
