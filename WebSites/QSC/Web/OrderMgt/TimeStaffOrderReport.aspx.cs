using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Business;
using QSPFulfillment.CommonWeb;

namespace QSPFulfillment.Reports
{
	/// <summary>
	/// Summary description for TimeStaffOrderReport.
	/// </summary>
	public class TimeStaffOrderReport :  QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.Label lblDateFrom;
		protected System.Web.UI.WebControls.Label lblDateTo;
		protected System.Web.UI.WebControls.Label lblAccount;
		protected System.Web.UI.WebControls.Label lblCamapaign;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Label lblReportName;
		protected System.Web.UI.WebControls.Label lblReportDetail;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucDateFrom,ucDateTo;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationTimeStaffOrderReport;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.TextBox tbCampaignId;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				lblErrorMessage.Text="";
				
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PrintButton.Click += new System.EventHandler(this.PrintButton_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void PrintButton_Click(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text=" ";
			
			DateTime OrderDateFrom;
			DateTime OrderDateTo;
			int AccountId;
			int CAId;
			string  sortby;

			if (this.tbAccountId.Text == "")
			{
				AccountId = 0;
			}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
			}

			if (this.tbCampaignId.Text == "")
			{
				CAId = 0;
			}
			else
			{
				CAId = Convert.ToInt32(this.tbCampaignId.Text);
			}


			if (this.ucDateFrom.Date  == System.DateTime.MinValue) 
			{
				lblErrorMessage.Text = "Order From date is required, please correct";
				return;
			}
			else
			{
				OrderDateFrom = this.ucDateFrom.Date;
			}

			if (this.ucDateTo.Date == System.DateTime.MinValue)
			{
				//if order Received date is null set it to current date
				this.ucDateTo.Date = System.DateTime.Now;
				OrderDateTo = System.DateTime.Now;
				
			}
			else
			{
				OrderDateTo   = this.ucDateTo.Date;
			}
			

			if (this.ucDateFrom.Date > this.ucDateTo.Date)
			{
				lblErrorMessage.Text = "Invalid Order From/To Date, please correct ";
				return;
			}

			sortby		   =  this.ddlSortBy.SelectedValue; 
   

			CallReport(OrderDateFrom, OrderDateTo, AccountId,CAId,  sortby);

		}
		private void CallReport(DateTime OrderDateFrom,
			DateTime OrderDateTo,
			int AccountId,
			int CAId, 
			string sortby)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue("Fromdate", OrderDateFrom.ToShortDateString());
			parameterValues.Add(parameterValue);
			
			parameterValue = new ParameterValue("Todate", OrderDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);

			
			{
				parameterValue = new ParameterValue("AccountId", AccountId.ToString());
				parameterValues.Add(parameterValue);
			}

			
			{
				parameterValue = new ParameterValue("CampaignId", CAId.ToString());
				parameterValues.Add(parameterValue);
			}

			
			parameterValue = new ParameterValue("SortBy", sortby);
			parameterValues.Add(parameterValue);

			rsGenerationTimeStaffOrderReport.Generate(parameterValues);
		
		}

		private void ddlSortBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			lblErrorMessage.Text = "";
			
		}
	}
}
