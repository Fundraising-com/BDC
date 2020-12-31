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

namespace QSPFulfillment.Finance
{
	/// <summary>
	/// Summary description for GroupRefundReport.
	/// </summary>
	public class GroupRefundReport : QSPFulfillment.CommonWeb.QSPPage
	{
		protected System.Web.UI.WebControls.Label lblAmountFrom;
		protected System.Web.UI.WebControls.Label lblAccountId;
		protected System.Web.UI.WebControls.Label lblAmountTo;
		protected System.Web.UI.WebControls.Label lblCampaignId;
		protected System.Web.UI.WebControls.Label lblSortBy;
		protected System.Web.UI.WebControls.Button PrintButton;
		protected System.Web.UI.WebControls.Label lblErrorMessage;
		protected System.Web.UI.WebControls.Label lblDeadOrderReport;
		protected QSPFulfillment.CommonWeb.UC.DateEntry ucCreateDateFrom, ucCreateDateTo;
		protected System.Web.UI.WebControls.TextBox tbAmountFrom;
		protected System.Web.UI.WebControls.TextBox tbAmountTo;
		protected System.Web.UI.WebControls.TextBox tbCampaignId;
		protected System.Web.UI.WebControls.TextBox tbAccountId;
		protected System.Web.UI.WebControls.Label Label1;
		protected System.Web.UI.WebControls.Label Label2;
		protected System.Web.UI.WebControls.Label Label3;
		protected QSPFulfillment.CommonWeb.RSGeneration rsGenerationGroupRefundReport;
		protected System.Web.UI.WebControls.DropDownList ddlSortBy;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				if(aUserProfile.HasRole("Finance"))
				{
					lblErrorMessage.Visible =false;
				}
				else
				{
					//user is not allowed to see this page
					Response.Redirect("../Common/AccessDenied.aspx?p=Finance_Reports_GroupRefundReport");
			
				}
			}
			lblErrorMessage.Visible =false;
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
			lblErrorMessage.Text =" ";

			decimal  RefundAmountFrom;
			decimal  RefundAmountTo;
			DateTime CreateDateFrom;
			DateTime CreateDateTo;
			int CampaignId;
			int	AccountId;
			string SortBy;
			
			// Refund Amount from is optional parameter
			if (this.tbAmountFrom.Text == "")
			{
				RefundAmountFrom = 0;
			}
			else
			{
				RefundAmountFrom = Convert.ToDecimal(this.tbAmountFrom.Text);
				
			}
			// Refund Amount to is optional parameter
			if (this.tbAmountTo.Text == "")
			{
				RefundAmountTo = 0.00M;
			}
			else
			{
				RefundAmountTo = Convert.ToDecimal(this.tbAmountTo.Text);
				
			}

			if (this.tbCampaignId.Text == "")
			{
				CampaignId = 0;
			}
			else
			{
				CampaignId = Convert.ToInt32(this.tbCampaignId.Text);
			}

			// Account Id is optional
			if (this.tbAccountId.Text == "")
			{
				AccountId = 0;
			}
			else
			{
				AccountId = Convert.ToInt32(this.tbAccountId.Text);
			}
			
			SortBy = this.ddlSortBy.SelectedValue; 

			//If catalystDateFrom is Null And dateto is not null
			if (this.ucCreateDateFrom.Date  == System.DateTime.MinValue) 
			{
				lblErrorMessage.Visible =true;
				lblErrorMessage.Text = "Invalid Catalyst From date, please correct";
				return;
			}
			else
			{
				CreateDateFrom = this.ucCreateDateFrom.Date;
			}

			// CatalystDate to	is null											
			if (this.ucCreateDateTo.Date  == System.DateTime.MinValue) 
				
			{
				this.ucCreateDateTo.Date = System.DateTime.Now;
				CreateDateTo = System.DateTime.Now;
				
			}
			else
			{
				CreateDateTo = this.ucCreateDateTo.Date;
			}

			if  (this.ucCreateDateFrom.Date > this.ucCreateDateTo.Date)
				
			{
				lblErrorMessage.Visible =true;
				lblErrorMessage.Text = "Invalid Catalyst From date, please correct";
				return;
			}
   
			CallReport(RefundAmountFrom, RefundAmountTo, CreateDateFrom, CreateDateTo, CampaignId, AccountId, SortBy);
		}

		private void CallReport(decimal  RefundAmountFrom, decimal  RefundAmountTo, DateTime CreateDateFrom, DateTime CreateDateTo,	int CampaignId,	int	AccountId, string SortBy)
		{
			ParameterValueCollection parameterValues = new ParameterValueCollection();
			ParameterValue parameterValue;

			parameterValue = new ParameterValue("DateCreatedFrom", CreateDateFrom.ToShortDateString());
			parameterValues.Add(parameterValue);

			parameterValue = new ParameterValue("DateCreatedTo", CreateDateTo.ToShortDateString());
			parameterValues.Add(parameterValue);
		
			if(RefundAmountFrom.ToString() != "") 
			{
				parameterValue = new ParameterValue("RefundAmountFrom", RefundAmountFrom.ToString());
				parameterValues.Add(parameterValue);
			}

			if(RefundAmountTo.ToString() != "") 
			{
				parameterValue = new ParameterValue("RefundAmountTo", RefundAmountTo.ToString());
				parameterValues.Add(parameterValue);
			}
		
			if(CampaignId != 0) 
			{
				parameterValue = new ParameterValue("CampaignID", CampaignId.ToString());
				parameterValues.Add(parameterValue);
			}

			if(AccountId != 0) 
			{
				parameterValue = new ParameterValue("AccountID", AccountId.ToString());
				parameterValues.Add(parameterValue);
			}

			parameterValue = new ParameterValue("SortBy", SortBy);
			parameterValues.Add(parameterValue);

			rsGenerationGroupRefundReport.Generate(parameterValues);
		}
	}
}
